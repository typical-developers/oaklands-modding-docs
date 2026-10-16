"""Writes models/cone.glb: a low-poly traffic cone, flat shaded, with three
plain-colour materials. Run from the mod folder: python source/make_cone.py
Units are metres, Y up (glTF's convention)."""

import json
import math
import struct
from pathlib import Path

SIDES = 10
BASE_HALF = 0.19
BASE_HEIGHT = 0.035
BOTTOM_RADIUS = 0.15
TOP_RADIUS = 0.028
HEIGHT = 0.7
BAND = (0.36, 0.48)  # white band, as heights

MATERIALS = [
    ("Orange", (0.95, 0.33, 0.05, 1.0)),
    ("White", (0.92, 0.92, 0.9, 1.0)),
    ("Base", (0.12, 0.11, 0.1, 1.0)),
]

# Per material: positions, normals, indices (flat shaded, so every face has
# its own vertices).
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


def radius_at(y):
    t = (y - BASE_HEIGHT) / (HEIGHT - BASE_HEIGHT)
    return BOTTOM_RADIUS + (TOP_RADIUS - BOTTOM_RADIUS) * t


def ring(y):
    r = radius_at(y)
    return [(r * math.cos(2 * math.pi * i / SIDES), y, -r * math.sin(2 * math.pi * i / SIDES)) for i in range(SIDES)]


# Base slab.
h, y0, y1 = BASE_HALF, 0.0, BASE_HEIGHT
top = [(-h, y1, h), (h, y1, h), (h, y1, -h), (-h, y1, -h)]
bottom = [(-h, y0, -h), (h, y0, -h), (h, y0, h), (-h, y0, h)]
face("Base", top)
face("Base", bottom)
for i in range(4):
    a, b = top[i], top[(i + 1) % 4]
    face("Base", [(a[0], y0, a[2]), (b[0], y0, b[2]), b, a])

# Body, in three bands.
for (low, high, material) in [(BASE_HEIGHT, BAND[0], "Orange"), (BAND[0], BAND[1], "White"), (BAND[1], HEIGHT, "Orange")]:
    lower, upper = ring(low), ring(high)
    for i in range(SIDES):
        j = (i + 1) % SIDES
        face(material, [lower[i], lower[j], upper[j], upper[i]])

# Cap.
face("Orange", ring(HEIGHT))

# Pack into one buffer.
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
    "asset": {"version": "2.0", "generator": "Oaklands make_cone.py"},
    "scene": 0,
    "scenes": [{"nodes": [0]}],
    "nodes": [{"name": "Cone", "mesh": 0}],
    "meshes": [{"name": "Cone", "primitives": primitives}],
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

out = Path(__file__).resolve().parent.parent / "models" / "cone.glb"
with open(out, "wb") as f:
    f.write(struct.pack("<4sII", b"glTF", 2, total))
    f.write(struct.pack("<I4s", len(json_chunk), b"JSON"))
    f.write(json_chunk)
    f.write(struct.pack("<I4s", len(buffer), b"BIN\0"))
    f.write(buffer)

print(f"Wrote {out} ({total} bytes)")
