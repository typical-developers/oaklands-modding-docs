# MeshBuilder

[All docs](../index.md)

<a name="meshbuilder"></a>

## MeshBuilder

*class*

A 3D model built in code: add shapes, or vertices and triangles yourself, then [`ModMeshes.Register()`](ModMeshes.md#modmeshes-register) it and use what that returns as an item's, tool's or vehicle's `Model`:

```csharp
var mesh = new MeshBuilder();
mesh.AddBox(new Vector3(0, 0.5f, 0), new Vector3(1, 1, 1), Color.white);
mesh.Material = "Lid";
mesh.AddCylinder(new Vector3(0, 1, 0), new Vector3(0, 1.1f, 0), 0.4f, Color.red);
string model = Meshes.Register("crate", mesh);   // "mesh:my_mod/crate"
Items.Register(new ItemSpec { Id = "crate", Name = "Crate", Model = model, Mass = 5 });
```

Metres, +Y up, +Z forward. Triangles face the side their corners go clockwise round. Vertex colors tint the material, and [`MeshBuilder.Material`](MeshBuilder.md#meshbuilder-material) names the slot later triangles go in, for the spec's `Materials` (e.g. `{ "Lid": "game:plank" }`). At most 262144 vertices.

### Fields

<a name="meshbuilder-maxvertices"></a>

#### MaxVertices

```csharp
const int MaxVertices = 262144
```

<a name="meshbuilder-defaultmaterial"></a>

#### DefaultMaterial

```csharp
const string DefaultMaterial = "Default"
```

### Properties

<a name="meshbuilder-vertexcount"></a>

#### VertexCount

```csharp
int VertexCount { get; }
```

<a name="meshbuilder-trianglecount"></a>

#### TriangleCount

```csharp
int TriangleCount { get; }
```

<a name="meshbuilder-material"></a>

#### Material

```csharp
string Material { get; set; }
```

The material slot triangles added from now on go in. Each slot is one material on the model, named for the spec's `Materials`; without one there, it's plain white tinted by the vertex colors.

<a name="meshbuilder-autonormals"></a>

#### AutoNormals

```csharp
bool AutoNormals { get; set; }
```

When true (the default), vertices given no normal get smooth ones worked out from the triangles round them. The shapes below give their own: flat sides on boxes, smooth round cylinders and spheres.

### Methods

<a name="meshbuilder-addvertex"></a>

#### AddVertex

```csharp
int AddVertex(Vector3 position, Vector3 normal, Vector2 uv, Color color)
```

Adds a vertex and returns its index, for [`MeshBuilder.AddTriangle()`](MeshBuilder.md#meshbuilder-addtriangle). A zero normal is worked out.

#### AddVertex

```csharp
int AddVertex(Vector3 position)
```

#### AddVertex

```csharp
int AddVertex(Vector3 position, Color color)
```

<a name="meshbuilder-addtriangle"></a>

#### AddTriangle

```csharp
void AddTriangle(int a, int b, int c)
```

A triangle between three vertices, facing the side they go clockwise round.

<a name="meshbuilder-addquad"></a>

#### AddQuad

```csharp
void AddQuad(Vector3 a, Vector3 b, Vector3 c, Vector3 d, Color color)
```

A flat four-sided face, its corners clockwise as seen from the front, with UVs from 0 to 1 across it.

<a name="meshbuilder-addbox"></a>

#### AddBox

```csharp
void AddBox(Vector3 center, Vector3 size, Color color, Quaternion rotation = default(Quaternion))
```

A box with flat sides, turned by `rotation` (none for default).

<a name="meshbuilder-addcylinder"></a>

#### AddCylinder

```csharp
void AddCylinder(Vector3 from, Vector3 to, float radius, Color color, int sides = 12, bool caps = true)
```

A round rod from one point to another, with flat ends unless `caps` is false.

<a name="meshbuilder-addsphere"></a>

#### AddSphere

```csharp
void AddSphere(Vector3 center, float radius, Color color, int segments = 16)
```

A smooth ball, `segments` round (4 to 64).

<a name="meshbuilder-addmesh"></a>

#### AddMesh

```csharp
void AddMesh(MeshBuilder other, Vector3 position, Quaternion rotation, Vector3 scale)
```

Copies another builder's vertices and triangles in, moved, turned and scaled, keeping their material slots.

<a name="meshbuilder-clear"></a>

#### Clear

```csharp
void Clear()
```

Empties the builder to start again.
