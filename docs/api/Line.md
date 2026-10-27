# Line

[All docs](../index.md)

<a name="line"></a>

## Line

*class*

A line drawn in the world as a round tube through points: a cable, a rope, a laser. Made with [`ModEffects.CreateLine()`](ModEffects.md#modeffects-createline); only this player sees it, so draw it on every client from what they all know (e.g. the positions of the two items a cable joins, each [`ModEvents.Tick`](ModEvents.md#modevents-tick)). It has no collision. A mod can have at most 256, of up to 2048 points each.

### Properties

<a name="line-isvalid"></a>

#### IsValid

```csharp
bool IsValid { get; }
```

False once destroyed, or if it couldn't be made (too many lines).

<a name="line-width"></a>

#### Width

```csharp
float Width { get; set; }
```

How thick it is, in metres.

<a name="line-color"></a>

#### Color

```csharp
Color Color { get; set; }
```

Tints the material (or colors it, with none).

<a name="line-material"></a>

#### Material

```csharp
string Material { get; set; }
```

A material's full id, e.g. "game:metal" or "my\_mod:cable", or null for plain color. Its texture runs along the line.

<a name="line-visible"></a>

#### Visible

```csharp
bool Visible { get; set; }
```

### Methods

<a name="line-setpoints"></a>

#### SetPoints

```csharp
void SetPoints(IList<Vector3> points)
```

Draws the line through these points (in the world, at least two).

<a name="line-setrope"></a>

#### SetRope

```csharp
void SetRope(Vector3 from, Vector3 to, float sag = 0.3, int segments = 16)
```

Draws a rope or cable hanging between two points, sagging `sag` metres in the middle, with `segments` straight bits (4 to 128).

<a name="line-destroy"></a>

#### Destroy

```csharp
void Destroy()
```

Removes the line.
