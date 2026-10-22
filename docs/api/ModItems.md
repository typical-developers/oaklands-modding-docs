# ModItems

[All docs](../index.md) · In this page: [`ModItems`](#moditems) · [`ItemColliderShape`](#itemcollidershape) · [`ItemSpec`](#itemspec) · [`InteractionSpec`](#interactionspec)

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
| `Box` | One box around the whole model. |
| `Convex` | A convex hull per mesh. Closer fit, a little slower. |

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

A .glb or .gltf file, relative to the mod's folder, e.g. "models/cone.glb". One metre in the model is one metre in the game.

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
