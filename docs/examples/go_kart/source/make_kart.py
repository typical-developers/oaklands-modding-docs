"""Writes models/kart.glb (a low-poly go-kart, flat shaded, wheels as their
own nodes named WheelFL/WheelFR/WheelRL/WheelRR with their origin at the
wheel's middle) and sounds/horn.wav. Run from the mod folder:
python source/make_kart.py

glTF is +Y up, +Z forward and +X to the vehicle's LEFT; the game turns that
into Unity's +X right on import, so the left wheels sit at +X here."""

import json
import math
import struct
import wave
from pathlib import Path

HERE = Path(__file__).resolve().parent.parent

MATERIALS = [
    ("Body", (0.85, 0.12, 0.1, 1.0)),
    ("Frame", (0.15, 0.15, 0.17, 1.0)),
    ("Seat", (0.1, 0.1, 0.12, 1.0)),
    ("Tyre", (0.07, 0.07, 0.07, 1.0)),
    ("Hub", (0.75, 0.75, 0.78, 1.0)),
]

WHEEL_RADIUS = 0.22
WHEEL_WIDTH = 0.16
WHEELS = {
    "WheelFL": (0.62, WHEEL_RADIUS, 0.72),
    "WheelFR": (-0.62, WHEEL_RADIUS, 0.72),
    "WheelRL": (0.66, WHEEL_RADIUS, -0.72),
    "WheelRR": (-0.66, WHEEL_RADIUS, -0.72),
}


class Part:
    def __init__(self):
        self.prims = {}

    def face(self, material, corners):
        positions, normals, indices = self.prims.setdefault(material, ([], [], []))
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

    def box(self, material, center, size):
        cx, cy, cz = center
        hx, hy, hz = size[0] / 2, size[1] / 2, size[2] / 2
        x0, x1, y0, y1, z0, z1 = cx - hx, cx + hx, cy - hy, cy + hy, cz - hz, cz + hz
        self.face(material, [(x0, y1, z1), (x1, y1, z1), (x1, y1, z0), (x0, y1, z0)])  # top
        self.face(material, [(x0, y0, z0), (x1, y0, z0), (x1, y0, z1), (x0, y0, z1)])  # bottom
        self.face(material, [(x0, y0, z1), (x1, y0, z1), (x1, y1, z1), (x0, y1, z1)])  # +z
        self.face(material, [(x1, y0, z0), (x0, y0, z0), (x0, y1, z0), (x1, y1, z0)])  # -z
        self.face(material, [(x1, y0, z1), (x1, y0, z0), (x1, y1, z0), (x1, y1, z1)])  # +x
        self.face(material, [(x0, y0, z0), (x0, y0, z1), (x0, y1, z1), (x0, y1, z0)])  # -x

    def wheel(self, sides=10):
        # Around the X axis, centred on the origin.
        hw = WHEEL_WIDTH / 2
        ring = [(math.cos(2 * math.pi * i / sides) * WHEEL_RADIUS, math.sin(2 * math.pi * i / sides) * WHEEL_RADIUS) for i in range(sides)]
        for i in range(sides):
            j = (i + 1) % sides
            (y0, z0), (y1, z1) = ring[i], ring[j]
            self.face("Tyre", [(-hw, y0, z0), (-hw, y1, z1), (hw, y1, z1), (hw, y0, z0)])
        self.face("Hub", [(hw, y, z) for (y, z) in ring])
        self.face("Hub", [(-hw, y, z) for (y, z) in reversed(ring)])


body = Part()
body.box("Frame", (0, 0.16, 0), (1.05, 0.06, 1.75))           # chassis plate
body.box("Body", (0, 0.27, 0.78), (0.9, 0.18, 0.32))           # front nose
body.box("Body", (0.48, 0.26, 0), (0.12, 0.16, 1.1))           # left pod
body.box("Body", (-0.48, 0.26, 0), (0.12, 0.16, 1.1))          # right pod
body.box("Seat", (0, 0.3, -0.25), (0.46, 0.22, 0.46))          # seat base
body.box("Seat", (0, 0.56, -0.47), (0.46, 0.5, 0.1))           # seat back
body.box("Frame", (0, 0.42, 0.42), (0.05, 0.05, 0.4))          # steering column
body.box("Frame", (0, 0.55, 0.3), (0.34, 0.05, 0.05))          # steering wheel
body.box("Body", (0, 0.32, -0.82), (0.95, 0.12, 0.14))         # rear bumper

wheel = Part()
wheel.wheel()

buffer = bytearray()
views, accessors, meshes = [], [], []
material_index = {name: i for i, (name, _) in enumerate(MATERIALS)}


def add_view(data, target):
    while len(buffer) % 4:
        buffer.append(0)
    views.append({"buffer": 0, "byteOffset": len(buffer), "byteLength": len(data), "target": target})
    buffer.extend(data)
    return len(views) - 1


def add_mesh(name, part):
    primitives = []
    for material, (positions, normals, indices) in part.prims.items():
        lo = [min(p[k] for p in positions) for k in range(3)]
        hi = [max(p[k] for p in positions) for k in range(3)]
        accessors.append({"bufferView": add_view(b"".join(struct.pack("<3f", *p) for p in positions), 34962), "componentType": 5126, "count": len(positions), "type": "VEC3", "min": lo, "max": hi})
        accessors.append({"bufferView": add_view(b"".join(struct.pack("<3f", *n) for n in normals), 34962), "componentType": 5126, "count": len(normals), "type": "VEC3"})
        accessors.append({"bufferView": add_view(b"".join(struct.pack("<H", i) for i in indices), 34963), "componentType": 5123, "count": len(indices), "type": "SCALAR"})
        primitives.append({"attributes": {"POSITION": len(accessors) - 3, "NORMAL": len(accessors) - 2}, "indices": len(accessors) - 1, "material": material_index[material]})
    meshes.append({"name": name, "primitives": primitives})
    return len(meshes) - 1


body_mesh = add_mesh("Body", body)
wheel_mesh = add_mesh("Wheel", wheel)

nodes = [{"name": "Kart", "children": []}, {"name": "Body", "mesh": body_mesh}]
nodes[0]["children"].append(1)
for name, position in WHEELS.items():
    nodes.append({"name": name, "mesh": wheel_mesh, "translation": list(position)})
    nodes[0]["children"].append(len(nodes) - 1)

while len(buffer) % 4:
    buffer.append(0)

gltf = {
    "asset": {"version": "2.0", "generator": "Oaklands make_kart.py"},
    "scene": 0,
    "scenes": [{"nodes": [0]}],
    "nodes": nodes,
    "meshes": meshes,
    "materials": [
        {"name": name, "pbrMetallicRoughness": {"baseColorFactor": list(color), "metallicFactor": 0.0, "roughnessFactor": 0.8}}
        for name, color in MATERIALS
    ],
    "buffers": [{"byteLength": len(buffer)}],
    "bufferViews": views,
    "accessors": accessors,
}

json_chunk = json.dumps(gltf, separators=(",", ":")).encode()
json_chunk += b" " * (-len(json_chunk) % 4)
total = 12 + 8 + len(json_chunk) + 8 + len(buffer)

(HERE / "models").mkdir(exist_ok=True)
with open(HERE / "models" / "kart.glb", "wb") as f:
    f.write(struct.pack("<4sII", b"glTF", 2, total))
    f.write(struct.pack("<I4s", len(json_chunk), b"JSON"))
    f.write(json_chunk)
    f.write(struct.pack("<I4s", len(buffer), b"BIN\0"))
    f.write(buffer)

# Horn: two short square-wave beeps a third apart.
RATE = 22050
samples = []
for frequency, length in ((523.0, 0.16), (0.0, 0.05), (659.0, 0.22)):
    for i in range(int(RATE * length)):
        t = i / RATE
        fade = min(1.0, (length - t) * 40, t * 200)
        value = (1 if math.sin(2 * math.pi * frequency * t) >= 0 else -1) if frequency else 0
        samples.append(int(value * fade * 0.25 * 32767))

(HERE / "sounds").mkdir(exist_ok=True)
with wave.open(str(HERE / "sounds" / "horn.wav"), "wb") as w:
    w.setnchannels(1)
    w.setsampwidth(2)
    w.setframerate(RATE)
    w.writeframes(b"".join(struct.pack("<h", s) for s in samples))

print("Wrote models/kart.glb and sounds/horn.wav")
