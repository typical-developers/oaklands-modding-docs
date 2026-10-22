"""Writes models/pickaxe.glb: a low-poly pickaxe, flat shaded, with two
plain-colour materials, and icons/pickaxe.png and icons/stone.png.
Run from the mod folder: python source/make_pickaxe.py

Tools are modelled held upright: the grip at the origin, the head up (+Y)
and the pick pointing forward (+Z). Units are metres (glTF's Y up)."""

import json
import math
import struct
from pathlib import Path

MATERIALS = [
    ("Wood", (0.45, 0.28, 0.14, 1.0)),
    ("Stone", (0.52, 0.52, 0.55, 1.0)),
]

parts = {name: ([], [], []) for name, _ in MATERIALS}


def face(material, corners):
    """A flat polygon, corners counter-clockwise seen from outside."""
    positions, normals, indices = parts[material]
    a, b, c = corners[0], corners[1], corners[2]
    u = [b[i] - a[i] for i in range(3)]
    v = [c[i] - a[i] for i in range(3)]
    n = [u[1] * v[2] - u[2] * v[1], u[2] * v[0] - u[0] * v[2], u[0] * v[1] - u[1] * v[0]]
    length = math.sqrt(sum(x * x for x in n)) or 1.0
    n = [x / length for x in n]
    start = len(positions)
    for corner in corners:
        positions.append(corner)
        normals.append(n)
    for i in range(1, len(corners) - 1):
        indices += [start, start + i, start + i + 1]


def box(material, lo, hi):
    """An axis-aligned box from corner lo to corner hi."""
    x0, y0, z0 = lo
    x1, y1, z1 = hi
    face(material, [(x0, y1, z1), (x1, y1, z1), (x1, y1, z0), (x0, y1, z0)])  # top
    face(material, [(x0, y0, z0), (x1, y0, z0), (x1, y0, z1), (x0, y0, z1)])  # bottom
    face(material, [(x0, y0, z1), (x1, y0, z1), (x1, y1, z1), (x0, y1, z1)])  # front (+z)
    face(material, [(x1, y0, z0), (x0, y0, z0), (x0, y1, z0), (x1, y1, z0)])  # back
    face(material, [(x1, y0, z1), (x1, y0, z0), (x1, y1, z0), (x1, y1, z1)])  # right
    face(material, [(x0, y0, z0), (x0, y0, z1), (x0, y1, z1), (x0, y1, z0)])  # left


def wedge(material, base_lo, base_hi, tip, axis_sign):
    """A four-sided point from a square end (at z = base_lo[2] or base_hi[2]) to a tip."""
    x0, y0, z0 = base_lo
    x1, y1, z1 = base_hi
    z = z1 if axis_sign > 0 else z0
    square = [(x0, y0, z), (x1, y0, z), (x1, y1, z), (x0, y1, z)]
    if axis_sign < 0:
        square.reverse()
    for i in range(4):
        a, b = square[i], square[(i + 1) % 4]
        face(material, [a, b, tip])


# Handle: the grip is at the origin, a little above its bottom end.
box("Wood", (-0.018, -0.12, -0.018), (0.018, 0.56, 0.018))

# Head: a block round the top of the handle, a long pick forward and a short one back.
box("Stone", (-0.03, 0.5, -0.06), (0.03, 0.58, 0.06))
wedge("Stone", (-0.03, 0.5, -0.06), (0.03, 0.58, 0.06), (0.0, 0.47, 0.3), +1)
wedge("Stone", (-0.03, 0.5, -0.06), (0.03, 0.58, 0.06), (0.0, 0.5, -0.18), -1)

buffer = bytearray()
views, accessors, primitives = [], [], []


def add_view(data, target):
    while len(buffer) % 4:
        buffer.append(0)
    views.append({"buffer": 0, "byteOffset": len(buffer), "byteLength": len(data), "target": target})
    buffer.extend(data)
    return len(views) - 1


for index, (name, _) in enumerate(MATERIALS):
    positions, normals, indices = parts[name]
    pos_data = b"".join(struct.pack("<3f", *p) for p in positions)
    nrm_data = b"".join(struct.pack("<3f", *n) for n in normals)
    idx_data = b"".join(struct.pack("<H", i) for i in indices)
    lo = [min(p[k] for p in positions) for k in range(3)]
    hi = [max(p[k] for p in positions) for k in range(3)]
    accessors.append({"bufferView": add_view(pos_data, 34962), "componentType": 5126, "count": len(positions), "type": "VEC3", "min": lo, "max": hi})
    accessors.append({"bufferView": add_view(nrm_data, 34962), "componentType": 5126, "count": len(normals), "type": "VEC3"})
    accessors.append({"bufferView": add_view(idx_data, 34963), "componentType": 5123, "count": len(indices), "type": "SCALAR"})
    primitives.append({"attributes": {"POSITION": len(accessors) - 3, "NORMAL": len(accessors) - 2}, "indices": len(accessors) - 1, "material": index})

while len(buffer) % 4:
    buffer.append(0)

gltf = {
    "asset": {"version": "2.0", "generator": "Oaklands make_pickaxe.py"},
    "scene": 0,
    "scenes": [{"nodes": [0]}],
    "nodes": [{"name": "Pickaxe", "mesh": 0}],
    "meshes": [{"name": "Pickaxe", "primitives": primitives}],
    "materials": [
        {"name": name, "pbrMetallicRoughness": {"baseColorFactor": list(color), "metallicFactor": 0.0, "roughnessFactor": 0.85}}
        for name, color in MATERIALS
    ],
    "buffers": [{"byteLength": len(buffer)}],
    "bufferViews": views,
    "accessors": accessors,
}

json_chunk = json.dumps(gltf, separators=(",", ":")).encode()
json_chunk += b" " * (-len(json_chunk) % 4)
total = 12 + 8 + len(json_chunk) + 8 + len(buffer)

root = Path(__file__).resolve().parent.parent
out = root / "models" / "pickaxe.glb"
with open(out, "wb") as f:
    f.write(struct.pack("<4sII", b"glTF", 2, total))
    f.write(struct.pack("<I4s", len(json_chunk), b"JSON"))
    f.write(json_chunk)
    f.write(struct.pack("<I4s", len(buffer), b"BIN\0"))
    f.write(buffer)

print(f"Wrote {out} ({total} bytes)")

# Icons: 32 px pixel art, drawn here so the mod has no binary sources.
try:
    from PIL import Image, ImageDraw
except ImportError:
    print("Pillow isn't installed; skipping the icons.")
else:
    wood, stone, dark = (115, 72, 36, 255), (133, 133, 140, 255), (70, 70, 76, 255)

    icon = Image.new("RGBA", (32, 32), (0, 0, 0, 0))
    draw = ImageDraw.Draw(icon)
    draw.line([(7, 27), (21, 9)], fill=wood, width=3)
    draw.polygon([(9, 5), (20, 6), (28, 13), (26, 15), (19, 10), (12, 9)], fill=stone, outline=dark)
    icon.save(root / "icons" / "pickaxe.png")

    icon = Image.new("RGBA", (32, 32), (0, 0, 0, 0))
    draw = ImageDraw.Draw(icon)
    draw.polygon([(8, 22), (11, 12), (19, 9), (25, 14), (24, 23), (15, 26)], fill=stone, outline=dark)
    draw.line([(13, 14), (19, 12)], fill=(170, 170, 176, 255), width=2)
    icon.save(root / "icons" / "stone.png")
    print("Wrote icons/pickaxe.png and icons/stone.png")
