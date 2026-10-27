# WorldTree

[All docs](../index.md) · In this page: [`WorldTree`](#worldtree) · [`ChopResult`](#chopresult) · [`WorldHit`](#worldhit)

<a name="worldtree"></a>

## WorldTree

*struct* · implements `IEquatable<WorldTree>`

A tree in the world, standing or cut down to a stump, from [`ModWorld.FindTrees()`](ModWorld.md#modworld-findtrees) or [`WorldHit.Tree`](WorldTree.md#worldhit-tree). Trees aren't items: one stays the same tree (the same [`WorldTree.Id`](WorldTree.md#worldtree-id) on every peer and after a reload) while it's chopped, felled and grows back. What comes off it when it's felled are logs, items with the id "tree\_log", for [`ModWorld.FindItems()`](ModWorld.md#modworld-finditems).

### Properties

<a name="worldtree-id"></a>

#### Id

```csharp
ulong Id { get; }
```

Its lasting id: the same on every peer, made from where it stands.

<a name="worldtree-isvalid"></a>

#### IsValid

```csharp
bool IsValid { get; }
```

Whether the tree is in the world here (trees far from everyone may be unloaded on clients).

<a name="worldtree-species"></a>

#### Species

```csharp
string Species { get; }
```

Its species' id, e.g. "ash" or "my\_mod:birch".

<a name="worldtree-position"></a>

#### Position

```csharp
Vector3 Position { get; }
```

The base of its trunk.

<a name="worldtree-isstanding"></a>

#### IsStanding

```csharp
bool IsStanding { get; }
```

True while it has a trunk to chop above the ground; false once it's been felled to a stump (or is still growing back).

<a name="worldtree-height"></a>

#### Height

```csharp
float Height { get; }
```

How tall the part still standing is, in metres.

### Operators

<a name="worldtree-op_equality"></a>

#### operator ==

```csharp
static bool operator ==(WorldTree a, WorldTree b)
```

<a name="worldtree-op_inequality"></a>

#### operator !=

```csharp
static bool operator !=(WorldTree a, WorldTree b)
```

<a name="chopresult"></a>

## ChopResult

*enum*

What a chop into a tree did, from [`ModWorld.ChopTree()`](ModWorld.md#modworld-choptree).

| Value | Description |
| --- | --- |
| <a name="chopresult-missed"></a>`Missed` | Nothing: no such tree, no trunk at that height, or no damage. |
| <a name="chopresult-chopped"></a>`Chopped` | It cut in, but the tree's still standing. |
| <a name="chopresult-felled"></a>`Felled` | It cut through and the tree is coming down. |

<a name="worldhit"></a>

## WorldHit

*struct*

What a ray hit, from [`ModWorld.Raycast()`](ModWorld.md#modworld-raycast). When [`WorldHit.Hit`](WorldTree.md#worldhit-hit) is false the rest is defaults.

### Properties

<a name="worldhit-hit"></a>

#### Hit

```csharp
bool Hit { get; }
```

Whether it hit anything.

<a name="worldhit-kind"></a>

#### Kind

```csharp
ToolHitKind Kind { get; }
```

What sort of thing it hit.

<a name="worldhit-point"></a>

#### Point

```csharp
Vector3 Point { get; }
```

<a name="worldhit-normal"></a>

#### Normal

```csharp
Vector3 Normal { get; }
```

<a name="worldhit-distance"></a>

#### Distance

```csharp
float Distance { get; }
```

From the ray's origin, in metres.

<a name="worldhit-item"></a>

#### Item

```csharp
WorldItem Item { get; }
```

The item, log or vehicle, when [`WorldHit.Kind`](WorldTree.md#worldhit-kind) is Item.

<a name="worldhit-player"></a>

#### Player

```csharp
Player Player { get; }
```

The player, when [`WorldHit.Kind`](WorldTree.md#worldhit-kind) is Player.

<a name="worldhit-tree"></a>

#### Tree

```csharp
WorldTree Tree { get; }
```

The tree, when [`WorldHit.Kind`](WorldTree.md#worldhit-kind) is Tree.

<a name="worldhit-node"></a>

#### Node

```csharp
string Node { get; }
```

When [`WorldHit.Kind`](WorldTree.md#worldhit-kind) is Item: the name of the item's model node at [`WorldHit.Point`](WorldTree.md#worldhit-point) (the mesh part whose box it's in or nearest), e.g. "PortB"; null if none or not an item. See [`WorldItem.GetNodeAt()`](ModWorld.md#worlditem-getnodeat).
