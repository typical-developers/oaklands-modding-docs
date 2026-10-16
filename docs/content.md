# Content files

[All docs](index.md)

## A mod's folder

| Path | What it is |
| --- | --- |
| `mod.json` | Required. The mod's id and name: see [mod.json](#modjson). |
| `items/*.json` | Item kinds, one per file: see [Items](#items). |
| `vehicles/*.json` | Vehicle kinds, one per file: see [Vehicles](#vehicles). |
| `trees/*.json` | Tree species, one per file: see [Trees](#trees). |
| `sounds/` | `.ogg` and `.wav` files, loaded with the mod for scripts to play. |
| `icon.png` | Optional. The mod's picture in the mod list; otherwise it shows its initials. |
| `*.cs` | Scripts, anywhere in the folder except hidden folders, `bin` and `obj`: see [Scripts and the sandbox](sandbox.md). |

Models are `.glb` or `.gltf` files, and other files are found by paths relative to the mod's folder, e.g. `models/cone.glb`. Scripts can't read files outside it.

Keys in the JSON files aren't case sensitive, but a key the game doesn't know is an error, so typos don't go unnoticed. A kind's `id` is its file name when left out, so `items/cone.json` is `cone`; in the game, and in saves, it becomes `modid:cone`. Changing an id after players have used it orphans what they've made with it.

<a name="modjson"></a>

## mod.json

```json
{ "id": "my_mod", "name": "My Mod", "version": "1.0.0", "author": "You", "api": 1 }
```

| Key | Type | Default | Description |
| --- | --- | --- | --- |
| `id` | text | none | Required. 1 to 32 lowercase letters, digits or '\_', e.g. "my\_mod". Content ids become "my\_mod:thing", and worlds remember mods by it, so never change it once players use the mod. |
| `name` | text | none | What players see in the mod list. Defaults to the id. |
| `version` | text | `"1.0.0"` | Shown in the mod list, e.g. "1.2.0". |
| `author` | text | none | Who made it, shown in the mod list. |
| `description` | text | none | A sentence about what it adds, shown in the mod list. |
| `api` | whole number | `1` | The API version the mod was made for. |

<a name="items"></a>

## Items

Describes an item kind. Items in a mod's `items` folder are JSON files with these fields, e.g. `{ "id": "cone", "name": "Traffic Cone", "model": "models/cone.glb", "mass": 2 }`.

In scripts, this is [`ItemSpec`](api/ModItems.md#itemspec).

| Key | Type | Default | Description |
| --- | --- | --- | --- |
| `id` | text | the file's name | The id within the mod: lowercase letters, digits and '\_', e.g. "cone". Never change it once players have saves that contain it. |
| `name` | text | none | What players see, e.g. "Traffic Cone". |
| `model` | text | none | A .glb or .gltf file, relative to the mod's folder, e.g. "models/cone.glb". One metre in the model is one metre in the game. |
| `icon` | text | none | A square .png icon for the hotbar and inventory, relative to the mod's folder. Optional. |
| `mass` | number | `1` | Kilograms. |
| `scale` | number | `1` | Multiplies the model's size. |
| `collider` | one of `"box"`, `"convex"` | `"box"` | How the model is made solid. |
| `interactions` | list of [Interaction](#interaction-objects) objects | `[]` | Options shown when a player looks at the item, besides holding it. At most 4. Handle them with [`ModItems.OnInteract()`](api/ModItems.md#moditems-oninteract). |

<a name="interaction-objects"></a>

### Interaction objects

One option in an item's prompt, e.g. `{ "id": "light", "label": "Turn On", "labelKey": "lightLabel" }`.

In scripts, this is [`InteractionSpec`](api/ModItems.md#interactionspec).

| Key | Type | Default | Description |
| --- | --- | --- | --- |
| `id` | text | the file's name | What handlers see, e.g. "light". |
| `label` | text | none | What players see, e.g. "Turn On". |
| `holdSeconds` | number | `0` | Seconds the key must be held; 0 for a press. |
| `labelKey` | text | none | Optional: an item data key holding text to show instead of [`InteractionSpec.Label`](api/ModItems.md#interactionspec-label), so it can change per item (set it with [`WorldItem.SetData()`](api/ModWorld.md#worlditem-setdata)). |
| `enabledKey` | text | none | Optional: an item data key that must be true for the option to show. |

<a name="vehicles"></a>

## Vehicles

Describes a vehicle, in its own space: metres, +Y up, +Z forward, with the origin on the ground under its middle. As JSON (vectors as [x, y, z]):

```json
{
  "id": "kart", "name": "Go-Kart", "model": "models/kart.glb", "mass": 250,
  "seats": [ { "name": "Driver", "position": [0, 0.45, -0.2] } ],
  "wheels": [
    { "position": [-0.55, 0.2, 0.7], "radius": 0.2, "steer": true, "drive": false, "visual": "WheelFL" },
    ...
  ],
  "engine": { "torque": 600, "maxSpeed": 18 }
}
```

In scripts, this is [`VehicleSpec`](api/ModVehicles.md#vehiclespec).

| Key | Type | Default | Description |
| --- | --- | --- | --- |
| `id` | text | the file's name | The id within the mod: lowercase letters, digits and '\_', e.g. "kart". |
| `name` | text | none | What players see, e.g. "Go-Kart". |
| `model` | text | none | A .glb or .gltf file, relative to the mod's folder. |
| `mass` | number | `1000` | Kilograms. |
| `collider` | one of `"box"`, `"convex"` | `"box"` | Box: one box round the body. Convex: a hull per mesh. |
| `centerOfMass` | `[x, y, z]` | `[0, 0.3, 0]` | Lower rolls over less. |
| `seats` | list of [Seat](#seat-objects) objects | `[]` | The first is the driver's. At most 6. |
| `wheels` | list of [Wheel](#wheel-objects) objects | `[]` | Every wheel, driven or not. |
| `engine` | [Engine](#engine-objects) object | see below | How it drives. |
| `suspension` | [Suspension](#suspension-objects) object | see below | How the wheels spring. |
| `cameraPivot` | `[x, y, z]` | `[0, 1.5, -1]` | Where the third-person camera orbits. |
| `cameraDistance` | number | `7` | How far the third-person camera sits from the pivot, in metres. |
| `interactions` | list of [Interaction](#interaction-objects) objects | `[]` | Extra prompt options, like items'. At most 4. |

<a name="seat-objects"></a>

### Seat objects

A seat in a vehicle.

In scripts, this is [`SeatSpec`](api/ModVehicles.md#seatspec).

| Key | Type | Default | Description |
| --- | --- | --- | --- |
| `name` | text | `"Seat"` | What the prompt to sit in it says, e.g. "Driver". |
| `position` | `[x, y, z]` | `[0, 0, 0]` | Where the sitting player's middle goes. |

<a name="wheel-objects"></a>

### Wheel objects

A vehicle's wheel.

In scripts, this is [`WheelSpec`](api/ModVehicles.md#wheelspec).

| Key | Type | Default | Description |
| --- | --- | --- | --- |
| `position` | `[x, y, z]` | `[0, 0, 0]` | The wheel's middle, resting on its suspension. |
| `radius` | number | `0.4` | In metres. |
| `steer` | `true` or `false` | `false` | Whether it turns with the steering. |
| `drive` | `true` or `false` | `true` | Whether the engine turns it. |
| `visual` | text | none | Name of the model's node to spin and steer with this wheel, e.g. "WheelFL". Everything under it moves with the wheel. |

<a name="engine-objects"></a>

### Engine objects

How a vehicle drives.

In scripts, this is [`EngineSpec`](api/ModVehicles.md#enginespec).

| Key | Type | Default | Description |
| --- | --- | --- | --- |
| `torque` | number | `3000` | Newton-metres per driven wheel. |
| `reverseTorque` | number | `2000` | Newton-metres per driven wheel, backwards. |
| `maxSpeed` | number | `25` | Metres per second. |
| `maxReverseSpeed` | number | `8` | Metres per second, backwards. |
| `brake` | number | `4000` | Braking torque per wheel, in newton-metres. |
| `steerAngle` | number | `35` | Degrees, at low speed. |

<a name="suspension-objects"></a>

### Suspension objects

How a vehicle's wheels spring.

In scripts, this is [`SuspensionSpec`](api/ModVehicles.md#suspensionspec).

| Key | Type | Default | Description |
| --- | --- | --- | --- |
| `distance` | number | `0.3` | How far the wheels travel, in metres. |
| `spring` | number | `35000` | Stiffness: higher bounces less but feels harder. |
| `damper` | number | `4500` | How quickly bouncing dies down. |

<a name="trees"></a>

## Trees

Describes a tree species. As JSON:

```json
{
  "id": "birch", "name": "Birch", "shape": "slender", "seed": 7,
  "materials": "aspen", "barkColor": [1, 0.97, 0.9], "leafColor": "#9fd36b",
  "settings": { "Trunk": { "Height": 16 } }
}
```

In scripts, this is [`TreeSpeciesSpec`](api/ModTrees.md#treespeciesspec).

| Key | Type | Default | Description |
| --- | --- | --- | --- |
| `id` | text | the file's name | The id within the mod: lowercase letters, digits and '\_', e.g. "birch". |
| `name` | text | none | What players see, e.g. "Birch". |
| `shape` | text | `"broadleaf"` | "broadleaf" (like ash), "conifer" (like pine) or "slender" (like aspen). |
| `seed` | whole number | `1` | Different seeds give differently shaped trees. |
| `variants` | whole number | `2` | Different trees per growth stage, 1 to 6. More look less alike but take longer to grow when the mod loads. |
| `materials` | text | `"ash"` | The species whose bark, leaf and cut-wood materials to start from, e.g. "ash". |
| `barkTexture` | text | none | Optional PNG in the mod's folder for the bark. |
| `leafTexture` | text | none | Optional PNG in the mod's folder for the leaf cards (with transparency). |
| `barkColor` | colour: `[r, g, b]` (0 to 1) or `"#rrggbb"` | `"#FFFFFF"` | Tints the bark; white leaves it as the materials have it. |
| `leafColor` | colour: `[r, g, b]` (0 to 1) or `"#rrggbb"` | `"#FFFFFF"` | Tints the leaves; white leaves them as the materials have them. |
| `settings` | object | none | Advanced: any of the Tree Generator's settings, as JSON, applied on top of [`TreeSpeciesSpec.Shape`](api/ModTrees.md#treespeciesspec-shape), e.g. `{ "Trunk": { "Height": 16 } }`. |
