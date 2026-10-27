# ModItems

[All docs](../index.md) · In this page: [`ModItems`](#moditems) · [`ItemColliderShape`](#itemcollidershape) · [`CarryClicks`](#carryclicks) · [`CarrySpec`](#carryspec) · [`LightKind`](#lightkind) · [`LightSpec`](#lightspec) · [`ItemSpec`](#itemspec) · [`InteractionSpec`](#interactionspec)

<a name="moditems"></a>

## ModItems

*class*

Item kinds: the game's own and those added by mods. Items in a mod's `items` folder are registered before [`Mod.OnLoad()`](Mod.md#mod-onload) runs; scripts can add more with [`ModItems.Register()`](ModItems.md#moditems-register).

### Methods

<a name="moditems-register"></a>

#### Register

```csharp
string Register(ItemSpec spec)
```

Adds an item kind, or updates it if this mod already added one with the same id. Returns its full id, "modid:itemid", which is what [`ModWorld.SpawnItem()`](ModWorld.md#modworld-spawnitem) and save files use. Register items on every peer (i.e. not only when [`ModContext.IsServer`](ModContext.md#modcontext-isserver)), or clients can't show them.

<a name="moditems-oninteract"></a>

#### OnInteract

```csharp
void OnInteract(string itemId, string interactionId, Action<InteractEvent> handler)
```

Runs `handler` on the server when a player uses one of an item's interactions. `itemId` is the full id, e.g. "lamp:lamp"; null matches any item.

<a name="moditems-exists"></a>

#### Exists

```csharp
bool Exists(string id)
```

Whether an item kind exists, e.g. "axe" or "traffic\_cone:cone".

<a name="moditems-getids"></a>

#### GetIds

```csharp
List<string> GetIds()
```

Every item kind's id.

<a name="itemcollidershape"></a>

## ItemColliderShape

*enum*

How an item's or vehicle's shape is made solid.

| Value | Description |
| --- | --- |
| <a name="itemcollidershape-box"></a>`Box` | One box around the whole model. A model with no meshes (or one still loading) gets a 0.4 m box; skinned meshes count, in their bind pose. |
| <a name="itemcollidershape-convex"></a>`Convex` | A convex hull per mesh. Closer fit, a little slower. |
| <a name="itemcollidershape-none"></a>`None` | Not solid at all: nothing bumps into it, the crosshair and rays go through it, and it never falls or gets pushed (it stays where it's put, as if [`WorldItem.IsKinematic`](ModWorld.md#worlditem-iskinematic)), e.g. cable laid along the ground or decorations attached to something. Players can't pick it up or interact with it. Items only. |

<a name="carryclicks"></a>

## CarryClicks

*enum*

What the mouse buttons do while a player carries an item.

| Value | Description |
| --- | --- |
| <a name="carryclicks-place"></a>`Place` | Left click puts it down where the outline shows (the game's usual). |
| <a name="carryclicks-mod"></a>`Mod` | The clicks go to the mod instead, as [`ModEvents.HeldItemUsed`](ModEvents.md#modevents-helditemused) (left, or right as [`HeldItemUseEvent.Alt`](ModEvents.md#helditemuseevent-alt)). The drop key still lets go of it. |

<a name="carryspec"></a>

## CarrySpec

*class*

How players carry an item in both hands, e.g. `"carry": { "clicks": "Mod", "prompts": true }`.

### Fields

<a name="carryspec-allowed"></a>

#### Allowed

```csharp
bool Allowed = true
```

Whether players can pick it up to carry at all (its "Hold" option).

<a name="carryspec-clicks"></a>

#### Clicks

```csharp
CarryClicks Clicks = CarryClicks.Place
```

What left and right click do while it's carried.

<a name="carryspec-prompts"></a>

#### Prompts

```csharp
bool Prompts
```

Keep showing other things' prompts (and their interactions) while it's carried, e.g. for a tool that's used on machines. The game's own prompts that need empty hands still don't show.

<a name="carryspec-upright"></a>

#### Upright

```csharp
bool Upright
```

Put it down standing the way its model stands (+Y up), turned to face away from the player, instead of lying on its longest side.

<a name="lightkind"></a>

## LightKind

*enum*

What a [`LightSpec`](ModItems.md#lightspec) gives off.

| Value | Description |
| --- | --- |
| <a name="lightkind-point"></a>`Point` | Every way, like a bulb. |
| <a name="lightkind-spot"></a>`Spot` | A cone along [`LightSpec.Direction`](ModItems.md#lightspec-direction), like a headlight or torch. |

<a name="lightspec"></a>

## LightSpec

*class*

A light on an item or vehicle that lights up what's around it, e.g. `{ "name": "head", "kind": "Spot", "position": [0, 0.6, 1.2], "color": [1, 0.95, 0.8], "range": 25, "angle": 50 }`. List them in the spec ([`ItemSpec.Lights`](ModItems.md#itemspec-lights), [`VehicleSpec.Lights`](ModVehicles.md#vehiclespec-lights)) or add them to one item with [`WorldItem.AddLight()`](ModWorld.md#worlditem-addlight); switch them with [`WorldItem.SetLightOn()`](ModWorld.md#worlditem-setlighton). At most 8 per item. They don't cast shadows unless [`LightSpec.Shadows`](ModItems.md#lightspec-shadows) says so (shadows cost a lot; use them sparingly).

### Fields

<a name="lightspec-name"></a>

#### Name

```csharp
string Name
```

What scripts call it, e.g. "head". Unique per item.

<a name="lightspec-kind"></a>

#### Kind

```csharp
LightKind Kind = LightKind.Point
```

<a name="lightspec-position"></a>

#### Position

```csharp
Vector3 Position
```

Where it is, in the item's own space (or the node's, with [`LightSpec.Node`](ModItems.md#lightspec-node)).

<a name="lightspec-direction"></a>

#### Direction

```csharp
Vector3 Direction = Vector3.forward
```

Which way a spot points, in the same space. Forward (+Z) by default.

<a name="lightspec-node"></a>

#### Node

```csharp
string Node
```

Optional: a model node it moves with, e.g. a droid's "Head" bone, so it follows animations and [`WorldItem.SetNodeRotation()`](ModWorld.md#worlditem-setnoderotation).

<a name="lightspec-color"></a>

#### Color

```csharp
Color Color = Color.white
```

<a name="lightspec-range"></a>

#### Range

```csharp
float Range = 5f
```

How far it reaches, in metres (0.1 to 100).

<a name="lightspec-angle"></a>

#### Angle

```csharp
float Angle = 60f
```

A spot's cone, in degrees across (1 to 170).

<a name="lightspec-intensity"></a>

#### Intensity

```csharp
float Intensity = 1f
```

Brightness: 1 is a bright bulb; headlights 2 to 5 (0 to 50).

<a name="lightspec-on"></a>

#### On

```csharp
bool On = true
```

Whether it starts switched on.

<a name="lightspec-shadows"></a>

#### Shadows

```csharp
bool Shadows
```

Whether it casts shadows.

<a name="itemspec"></a>

## ItemSpec

*class*

Describes an item kind. Items in a mod's `items` folder are JSON files with these fields, e.g. `{ "id": "cone", "name": "Traffic Cone", "model": "models/cone.glb", "mass": 2 }`.

### Fields

<a name="itemspec-id"></a>

#### Id

```csharp
string Id
```

The id within the mod: lowercase letters, digits and '\_', e.g. "cone". Never change it once players have saves that contain it.

<a name="itemspec-name"></a>

#### Name

```csharp
string Name
```

What players see, e.g. "Traffic Cone".

<a name="itemspec-model"></a>

#### Model

```csharp
string Model
```

A .glb or .gltf file, relative to the mod's folder, e.g. "models/cone.glb", or one of the mod's Prefab Editor prefabs, e.g. "prefabs/crate.json". One metre in the model is one metre in the game. (A prefab made for an item becomes an item of its own id without an items/ file.)

<a name="itemspec-icon"></a>

#### Icon

```csharp
string Icon
```

A square .png icon for the hotbar and inventory, relative to the mod's folder. Optional.

<a name="itemspec-mass"></a>

#### Mass

```csharp
float Mass = 1f
```

Kilograms.

<a name="itemspec-scale"></a>

#### Scale

```csharp
float Scale = 1f
```

Multiplies the model's size.

<a name="itemspec-collider"></a>

#### Collider

```csharp
ItemColliderShape Collider = ItemColliderShape.Box
```

How the model is made solid.

<a name="itemspec-visualonly"></a>

#### VisualOnly

```csharp
List<string> VisualOnly = new()
```

Model nodes that are only for show: they (and everything under them) get no collider and don't count towards the item's box, e.g. `["Ring", "Cable"]` for thin parts that would otherwise shove players. If every mesh is listed, the item isn't solid at all, like [`ItemColliderShape.None`](ModItems.md#itemcollidershape-none).

<a name="itemspec-carry"></a>

#### Carry

```csharp
CarrySpec Carry = new()
```

How players carry it in both hands; see [`CarrySpec`](ModItems.md#carryspec).

<a name="itemspec-station"></a>

#### Station

```csharp
string Station
```

Optional: makes the item a crafting station of this kind, e.g. "workbench" or one of the mod's own from [`ModCrafting.AddStation()`](ModCrafting.md#modcrafting-addstation) (a plain id is this mod's). Players standing near it can make that kind's recipes, and it gets a "Use" option that opens the Crafting tab.

<a name="itemspec-lights"></a>

#### Lights

```csharp
List<LightSpec> Lights = new()
```

Lights on the item; see [`LightSpec`](ModItems.md#lightspec). At most 8.

<a name="itemspec-interactions"></a>

#### Interactions

```csharp
List<InteractionSpec> Interactions = new()
```

Options shown when a player looks at the item, besides holding it. At most 4. Handle them with [`ModItems.OnInteract()`](ModItems.md#moditems-oninteract).

<a name="itemspec-materials"></a>

#### Materials

```csharp
Dictionary<string, string> Materials = new()
```

Materials for the model's parts, by the names its materials have in the model file, e.g. `{ "Body": "paint", "Glass": "game:glass" }`; "\*" covers every part not named. A plain id is this mod's own material. Parts not listed keep the model's own materials.

<a name="itemspec-inventory"></a>

#### Inventory

```csharp
InventorySpec Inventory = new()
```

How it sits in players' inventories, e.g. `"inventory": { "size": [2, 2], "weight": 3 }`. Items are storable (one per stack, keeping their data) unless this says `"storable": false`.

<a name="interactionspec"></a>

## InteractionSpec

*class*

One option in an item's prompt, e.g. `{ "id": "light", "label": "Turn On", "labelKey": "lightLabel" }`.

### Fields

<a name="interactionspec-id"></a>

#### Id

```csharp
string Id
```

What handlers see, e.g. "light".

<a name="interactionspec-label"></a>

#### Label

```csharp
string Label
```

What players see, e.g. "Turn On".

<a name="interactionspec-holdseconds"></a>

#### HoldSeconds

```csharp
float HoldSeconds
```

Seconds the key must be held; 0 for a press.

<a name="interactionspec-labelkey"></a>

#### LabelKey

```csharp
string LabelKey
```

Optional: an item data key holding text to show instead of [`InteractionSpec.Label`](ModItems.md#interactionspec-label), so it can change per item (set it with [`WorldItem.SetData()`](ModWorld.md#worlditem-setdata)).

<a name="interactionspec-enabledkey"></a>

#### EnabledKey

```csharp
string EnabledKey
```

Optional: an item data key that must be true for the option to show.

<a name="interactionspec-nodes"></a>

#### Nodes

```csharp
List<string> Nodes = new()
```

Optional: names of model nodes this option belongs to, e.g. `["PortA", "PortB"]`, so one item can have many click targets (connectors, buttons, switches). The option then only shows while the crosshair is on one of them (or a part under one), [`InteractEvent.Node`](ModEvents.md#interactevent-node) says which, and [`InteractionSpec.LabelKey`](ModItems.md#interactionspec-labelkey) and [`InteractionSpec.EnabledKey`](ModItems.md#interactionspec-enabledkey) are looked up as "key:node" first (e.g. "portLabel:PortA"), falling back to the plain key. Parts are told apart by their meshes' boxes, so any collider shape works.
