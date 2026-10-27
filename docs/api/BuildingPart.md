# BuildingPart

[All docs](../index.md)

<a name="buildingpart"></a>

## BuildingPart

*struct* · implements `IEquatable<BuildingPart>`

One part built with the hammer: a blueprint until it's filled with material (logs dragged into it, say), then solid. From [`ModWorld.FindBuildingParts()`](ModWorld.md#modworld-findbuildingparts); the values are as they were when it was found, so find it again (or [`ModWorld.GetBuildingPart()`](ModWorld.md#modworld-getbuildingpart)) for fresh ones. Server only.

Building is limited by claimed zones, so filling and placing take the id of the player the work is done for ([`Player.Id`](ModWorld.md#player-id), e.g. whoever owns a worker droid), and fail where they couldn't build themselves.

### Properties

<a name="buildingpart-id"></a>

#### Id

```csharp
uint Id { get; }
```

The part's id, which stays the same while the world is open (not across reloads).

<a name="buildingpart-isvalid"></a>

#### IsValid

```csharp
bool IsValid { get; }
```

Whether the part still exists.

<a name="buildingpart-schematicid"></a>

#### SchematicId

```csharp
string SchematicId { get; }
```

Its kind, e.g. "wall" (see [`ModWorld.GetSchematicIds()`](ModWorld.md#modworld-getschematicids)).

<a name="buildingpart-name"></a>

#### Name

```csharp
string Name { get; }
```

What players see, e.g. "Wall".

<a name="buildingpart-position"></a>

#### Position

```csharp
Vector3 Position { get; }
```

Its middle.

<a name="buildingpart-rotation"></a>

#### Rotation

```csharp
Quaternion Rotation { get; }
```

<a name="buildingpart-size"></a>

#### Size

```csharp
Vector3 Size { get; }
```

Metres, along its own axes.

<a name="buildingpart-fillmaterial"></a>

#### FillMaterial

```csharp
string FillMaterial { get; }
```

The material going in, e.g. "oak\_wood" (see [`ModWorld.GetFillMaterialIds()`](ModWorld.md#modworld-getfillmaterialids)), or null while it's empty and takes any.

<a name="buildingpart-filled"></a>

#### Filled

```csharp
float Filled { get; }
```

How full it is, 0 to 1. 1 is finished: solid.

<a name="buildingpart-cubicmetres"></a>

#### CubicMetres

```csharp
float CubicMetres { get; }
```

How much material fills it, in cubic metres.

<a name="buildingpart-isfinished"></a>

#### IsFinished

```csharp
bool IsFinished { get; }
```

Whether it's full and solid.

<a name="buildingpart-cubicmetresneeded"></a>

#### CubicMetresNeeded

```csharp
float CubicMetresNeeded { get; }
```

Material still needed, in cubic metres.

### Methods

<a name="buildingpart-fill"></a>

#### Fill

```csharp
bool Fill(WorldItem resource, string builderId)
```

Puts a resource item into the part, as if a player dragged it in: a log (it fills with its tree's wood) or another item that fills. The whole item goes in and is removed. False if it doesn't fill anything, the part's finished or takes another material, or `builderId` may not build here.

<a name="buildingpart-addmaterial"></a>

#### AddMaterial

```csharp
bool AddMaterial(string fillMaterialId, float cubicMetres, string builderId)
```

Puts material straight in, without an item: e.g. wood a droid has carried in its own store. `cubicMetres` is after any yield. False as for [`BuildingPart.Fill()`](BuildingPart.md#buildingpart-fill), or for no such material.

### Operators

<a name="buildingpart-op_equality"></a>

#### operator ==

```csharp
static bool operator ==(BuildingPart a, BuildingPart b)
```

<a name="buildingpart-op_inequality"></a>

#### operator !=

```csharp
static bool operator !=(BuildingPart a, BuildingPart b)
```
