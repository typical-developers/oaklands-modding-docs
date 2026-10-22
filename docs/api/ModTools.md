# ModTools

[All docs](../index.md) · In this page: [`ModTools`](#modtools) · [`ToolSwing`](#toolswing) · [`ToolSpec`](#toolspec) · [`ToolHold`](#toolhold) · [`ToolHitKind`](#toolhitkind) · [`ToolUseEvent`](#tooluseevent)

<a name="modtools"></a>

## ModTools

*class*

Tools added by mods: held in the hand from the hotbar like the axe, swung with the left mouse button (and, if they have one, a second use on the right). JSON files in a mod's `tools` folder are registered before [`Mod.OnLoad()`](Mod.md#mod-onload); scripts can add more with [`ModTools.Register()`](ModTools.md#modtools-register). What a use does is up to the mod: handle it with [`ModTools.OnUse()`](ModTools.md#modtools-onuse). On the ground a tool is an item like any other, so spawn one with [`ModWorld.SpawnItem()`](ModWorld.md#modworld-spawnitem) or give it with [`ModInventory.Give()`](ModInventory.md#modinventory-give).

### Methods

<a name="modtools-register"></a>

#### Register

```csharp
string Register(ToolSpec spec)
```

Adds a tool kind, or updates this mod's one with the same id. Returns its full id, "modid:toolid". Register on every peer.

<a name="modtools-onuse"></a>

#### OnUse

```csharp
void OnUse(string toolId, Action<ToolUseEvent> handler)
```

Runs `handler` on the server when a player uses a tool. `toolId` is the full id, e.g. "mining:pickaxe"; null matches any mod tool.

<a name="toolswing"></a>

## ToolSwing

*enum*

How a tool moves when it's used.

| Value | Description |
| --- | --- |
| `Swing` | Back, then down through the target, like an axe or hammer. |
| `Stab` | Straight forward and back, like a spear or screwdriver. |
| `Raise` | Lifted up and lowered, like a torch or a drink. |
| `None` | Doesn't move. |

<a name="toolspec"></a>

## ToolSpec

*class*

Describes a tool, e.g. a mod's `tools/pickaxe.json`:

```json
{
  "id": "pickaxe", "name": "Pickaxe", "model": "models/pickaxe.glb",
  "icon": "icons/pickaxe.png", "swing": "Swing", "useSeconds": 0.7, "range": 2.5,
  "hold": { "position": [0, 0, 0.1], "rotation": [0, 0, 0] },
  "inventory": { "size": [1, 3] }
}
```

### Fields

<a name="toolspec-id"></a>

#### Id

```csharp
string Id
```

The id within the mod: lowercase letters, digits and '\_', e.g. "pickaxe".

<a name="toolspec-name"></a>

#### Name

```csharp
string Name
```

What players see, e.g. "Pickaxe".

<a name="toolspec-model"></a>

#### Model

```csharp
string Model
```

A .glb or .gltf file, relative to the mod's folder. Model it held upright, the handle's grip at the origin and the head up (+Y), facing forward (+Z).

<a name="toolspec-icon"></a>

#### Icon

```csharp
string Icon
```

A square .png icon for the hotbar and inventory. Optional.

<a name="toolspec-mass"></a>

#### Mass

```csharp
float Mass = 1f
```

Kilograms, lying on the ground.

<a name="toolspec-scale"></a>

#### Scale

```csharp
float Scale = 1f
```

Multiplies the model's size.

<a name="toolspec-collider"></a>

#### Collider

```csharp
ItemColliderShape Collider = ItemColliderShape.Box
```

How the model is made solid on the ground.

<a name="toolspec-materials"></a>

#### Materials

```csharp
Dictionary<string, string> Materials = new()
```

Materials for the model's parts, as for [`ItemSpec.Materials`](ModItems.md#itemspec-materials).

<a name="toolspec-hold"></a>

#### Hold

```csharp
ToolHold Hold = new()
```

Where it sits in the hand others see. Usually fine as it is.

<a name="toolspec-viewhold"></a>

#### ViewHold

```csharp
ToolHold ViewHold = new() { Position = new Vector3(0.35f, -0.35f, 0.6f) }
```

Where it sits in first person, from the camera.

<a name="toolspec-swing"></a>

#### Swing

```csharp
ToolSwing Swing = ToolSwing.Swing
```

How it moves when used.

<a name="toolspec-useseconds"></a>

#### UseSeconds

```csharp
float UseSeconds = 0.5f
```

Seconds between uses (the swing fits in this). 0.1 to 10.

<a name="toolspec-range"></a>

#### Range

```csharp
float Range = 2.5f
```

How far a use reaches, in metres, for what it hits. 0.5 to 10.

<a name="toolspec-repeat"></a>

#### Repeat

```csharp
bool Repeat = true
```

Keeps being used while the button is held.

<a name="toolspec-altuse"></a>

#### AltUse

```csharp
bool AltUse
```

Has a second use, on the right mouse button (see [`ToolUseEvent.Alt`](ModTools.md#tooluseevent-alt)).

<a name="toolspec-inventory"></a>

#### Inventory

```csharp
InventorySpec Inventory = new() { Category = InventoryCategory.Tool, Size = new Vector2Int(1, 2) }
```

How it sits in the inventory. Tools are always storable.

<a name="toolhold"></a>

## ToolHold

*class*

A tool's place in the hand, in metres and degrees.

### Fields

<a name="toolhold-position"></a>

#### Position

```csharp
Vector3 Position
```

<a name="toolhold-rotation"></a>

#### Rotation

```csharp
Vector3 Rotation
```

Euler angles, degrees.

<a name="toolhitkind"></a>

## ToolHitKind

*enum*

What a tool's use struck, for [`ToolUseEvent.HitKind`](ModTools.md#tooluseevent-hitkind).

| Value | Description |
| --- | --- |
| `None` | Nothing within reach. |
| `Ground` | The ground. |
| `Item` | An item, log, vehicle or other world object: see [`ToolUseEvent.HitItem`](ModTools.md#tooluseevent-hititem). |
| `Player` | Another player: see [`ToolUseEvent.HitPlayer`](ModTools.md#tooluseevent-hitplayer). |
| `Tree` | A standing tree. |
| `Other` | Anything else, like a building. |

<a name="tooluseevent"></a>

## ToolUseEvent

*class*

A player using a tool, for [`ModTools.OnUse()`](ModTools.md#modtools-onuse).

### Properties

<a name="tooluseevent-toolid"></a>

#### ToolId

```csharp
string ToolId { get; }
```

The tool's full id, e.g. "mining:pickaxe".

<a name="tooluseevent-player"></a>

#### Player

```csharp
Player Player { get; }
```

Who used it. On a client, the local player.

<a name="tooluseevent-alt"></a>

#### Alt

```csharp
bool Alt { get; }
```

True for the second use (right mouse button).

<a name="tooluseevent-aimorigin"></a>

#### AimOrigin

```csharp
Vector3 AimOrigin { get; }
```

Where they looked from (their eyes).

<a name="tooluseevent-aimdirection"></a>

#### AimDirection

```csharp
Vector3 AimDirection { get; }
```

Which way they looked.

<a name="tooluseevent-hitkind"></a>

#### HitKind

```csharp
ToolHitKind HitKind { get; }
```

<a name="tooluseevent-hit"></a>

#### Hit

```csharp
bool Hit { get; }
```

Whether anything was in reach.

<a name="tooluseevent-hitpoint"></a>

#### HitPoint

```csharp
Vector3 HitPoint { get; }
```

<a name="tooluseevent-hitnormal"></a>

#### HitNormal

```csharp
Vector3 HitNormal { get; }
```

<a name="tooluseevent-hitdistance"></a>

#### HitDistance

```csharp
float HitDistance { get; }
```

<a name="tooluseevent-hititem"></a>

#### HitItem

```csharp
WorldItem HitItem { get; }
```

The world object struck, when [`ToolUseEvent.HitKind`](ModTools.md#tooluseevent-hitkind) is Item.

<a name="tooluseevent-hitplayer"></a>

#### HitPlayer

```csharp
Player HitPlayer { get; }
```

The player struck, when [`ToolUseEvent.HitKind`](ModTools.md#tooluseevent-hitkind) is Player.
