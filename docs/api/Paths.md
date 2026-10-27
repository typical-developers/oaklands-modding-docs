# Paths

[All docs](../index.md) · In this page: [`WalkerSpec`](#walkerspec) · [`MoveState`](#movestate) · [`PathRequest`](#pathrequest)

<a name="walkerspec"></a>

## WalkerSpec

*class*

The size and pace of something that walks: for [`WorldItem.SetWalker()`](ModWorld.md#worlditem-setwalker) and [`ModWorld.FindPath()`](ModWorld.md#modworld-findpath). The defaults are about a person's.

### Fields

<a name="walkerspec-radius"></a>

#### Radius

```csharp
float Radius = 0.35f
```

Half its width, in metres: it keeps this far from walls.

<a name="walkerspec-height"></a>

#### Height

```csharp
float Height = 1.8f
```

Feet to head, in metres.

<a name="walkerspec-stepheight"></a>

#### StepHeight

```csharp
float StepHeight = 0.4f
```

Tallest step it walks straight up, in metres.

<a name="walkerspec-maxslope"></a>

#### MaxSlope

```csharp
float MaxSlope = 45f
```

Steepest ground it walks up, in degrees.

<a name="walkerspec-maxdrop"></a>

#### MaxDrop

```csharp
float MaxDrop = 1.5f
```

Furthest a path drops in one go, in metres. It can still fall further.

<a name="walkerspec-speed"></a>

#### Speed

```csharp
float Speed = 2f
```

Metres per second, for moves that don't give one.

<a name="walkerspec-turnspeed"></a>

#### TurnSpeed

```csharp
float TurnSpeed = 360f
```

Degrees per second it turns to face where it's going.

<a name="walkerspec-facemovement"></a>

#### FaceMovement

```csharp
bool FaceMovement = true
```

Turns to face the way it walks. Off to turn it yourself.

<a name="movestate"></a>

## MoveState

*enum*

What a walking item is doing (see [`WorldItem.MoveState`](ModWorld.md#worlditem-movestate)).

| Value | Description |
| --- | --- |
| <a name="movestate-idle"></a>`Idle` | Standing still, or not a walker. |
| <a name="movestate-walking"></a>`Walking` | Walking a path or a direction. |
| <a name="movestate-planning"></a>`Planning` | Working out a path, which takes a few frames. |
| <a name="movestate-arrived"></a>`Arrived` | Got where [`WorldItem.MoveTo()`](ModWorld.md#worlditem-moveto) sent it. |
| <a name="movestate-nopath"></a>`NoPath` | There's no way there it could find. |
| <a name="movestate-stuck"></a>`Stuck` | Something kept getting in its way. |

<a name="pathrequest"></a>

## PathRequest

*class*

A path being worked out by [`ModWorld.FindPath()`](ModWorld.md#modworld-findpath). Finding one takes a few frames, so check [`PathRequest.IsDone`](Paths.md#pathrequest-isdone) or pass a callback.

### Properties

<a name="pathrequest-from"></a>

#### From

```csharp
Vector3 From { get; }
```

<a name="pathrequest-to"></a>

#### To

```csharp
Vector3 To { get; }
```

<a name="pathrequest-isdone"></a>

#### IsDone

```csharp
bool IsDone { get; }
```

False while the path is still being worked out.

<a name="pathrequest-reached"></a>

#### Reached

```csharp
bool Reached { get; }
```

Whether the path gets to [`PathRequest.To`](Paths.md#pathrequest-to). If not, the [`PathRequest.Points`](Paths.md#pathrequest-points) end as close as it could get (a goal inside a wall, or further than the search reaches, about 120 m): walk there and look again.

<a name="pathrequest-points"></a>

#### Points

```csharp
List<Vector3> Points { get; }
```

Points to walk through, starting at [`PathRequest.From`](Paths.md#pathrequest-from), on the ground. Empty if there's no way at all. A copy each time.

<a name="pathrequest-length"></a>

#### Length

```csharp
float Length { get; }
```

The path's length in metres.

### Methods

<a name="pathrequest-cancel"></a>

#### Cancel

```csharp
void Cancel()
```

Stops looking. The callback isn't run.
