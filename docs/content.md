# Content files

[All docs](index.md)

## A mod's folder

| Path | What it is |
| --- | --- |
| `mod.json` | Required. The mod's id and name: see [mod.json](#modjson). |
| `items/*.json` | Item kinds, one per file: see [Items](#items). |
| `vehicles/*.json` | Vehicle kinds, one per file: see [Vehicles](#vehicles). |
| `trees/*.json` | Tree species, one per file: see [Trees](#trees). |
| `materials/*.json` | Materials for items and vehicles, one per file: see [Materials](#materials). |
| `sounds/` | `.ogg` and `.wav` files, loaded with the mod for scripts to play. |
| `icon.png` | Optional. The mod's picture in the mod list; otherwise it shows its initials. |
| `*.cs` | Scripts, anywhere in the folder except hidden folders, `bin` and `obj`: see [Scripts and the sandbox](sandbox.md). |

Models are `.glb` or `.gltf` files, and other files are found by paths relative to the mod's folder, e.g. `models/cone.glb`. Scripts can't read files outside it.

A mod can also be one `.zip` of that folder, for sharing: its files at the top of the zip, or in one folder at the top (what zipping the mod's folder makes). The game unpacks it when it starts and again whenever the zip changes, up to 20000 files and 1024 MB unpacked.

A model's own materials are shown with the game's shader: base colour and texture, normal and emission maps, metallic and roughness amounts (not their texture), unlit, alpha (opaque, mask or blend), double-sided and vertex colours, on the model's first UVs. Older specular-glossiness materials work too. For glow, scrolling, projected textures and the rest, give parts a [material](#materials) with the `materials` field.

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
| `materials` | object: each model material's name, or `"*"` for the rest, to a [material](#materials) id | `{}` | Materials for the model's parts, by the names its materials have in the model file, e.g. `{ "Body": "paint", "Glass": "game:glass" }`; "\*" covers every part not named. A plain id is this mod's own material. Parts not listed keep the model's own materials. |

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
| `materials` | object: each model material's name, or `"*"` for the rest, to a [material](#materials) id | `{}` | Materials for the model's parts, by the names its materials have in the model file, e.g. `{ "Body": "paint", "Glass": "game:glass" }`; "\*" covers every part not named. A plain id is this mod's own material. Parts not listed keep the model's own materials. |
| `nitrous` | [Nitrous](#nitrous-objects) object | none | A nitrous tank: the driver holds Sprint (Shift) to boost, with flames out of the exhausts and a tank gauge on the HUD. Leave it out for no nitrous. |

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

<a name="nitrous-objects"></a>

### Nitrous objects

A vehicle's nitrous tank.

In scripts, this is [`NitrousSpec`](api/ModVehicles.md#nitrousspec).

| Key | Type | Default | Description |
| --- | --- | --- | --- |
| `capacity` | number | `4` | Seconds of boost in a full tank. |
| `acceleration` | number | `8` | Extra forward push while boosting, in metres per second per second. |
| `maxSpeed` | number | `30` | The boost stops pushing above this speed, in metres per second. |
| `refillTime` | number | `15` | Seconds an empty tank takes to refill. It starts a couple of seconds after the boost ends. |
| `exhausts` | list of `[x, y, z]` | `[]` | Where the flames come out, at the end of each exhaust pipe; they point straight back. Empty: two at the back of the model. |

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

<a name="materials"></a>

## Materials

Describes a material. Materials in a mod's `materials` folder are JSON files with these fields, e.g.

```json
{
  "id": "neon", "shader": "unlit", "color": "#2a1a40",
  "emission": "#ff40c0", "emissionStrength": 4, "pulse": 0.5
}
```

Use them on a model's parts with the `materials` field of an item or vehicle, which maps the model's material names to material ids.

In scripts, this is [`MaterialSpec`](api/ModMaterials.md#materialspec).

| Key | Type | Default | Description |
| --- | --- | --- | --- |
| `id` | text | the file's name | The id within the mod: lowercase letters, digits and '\_', e.g. "neon". |
| `shader` | text | `"lit"` | "lit" (the game's look, lit by the sun and lamps), "unlit" (its own colour, for screens and signs) or "glass" (the game's see-through glass: uses the colour, opacity and smoothness). |
| `base` | text | none | Optional: a material to start from, e.g. "game:cobble". Fields left at their defaults keep that material's values, so `{ "base": "game:cobble", "color": "#ffcc88" }` is warmer cobble. |
| `color` | colour: `[r, g, b]` (0 to 1) or `"#rrggbb"` | `"#FFFFFF"` | Multiplies the texture. |
| `opacity` | number | `1` | 1 is solid, 0 invisible. Below 1 the surface is see-through and casts no shadow. |
| `texture` | text | none | A .png in the mod's folder, e.g. "textures/paint.png". None gives a plain colour. |
| `textureStrength` | number | `1` | 0 shows only the texture's average colour, 1 the whole texture. |
| `normalMap` | text | none | Optional normal map, a .png in the mod's folder. |
| `normalStrength` | number | `1` | How strongly the normal map bends the light. |
| `mapping` | text | `"projected"` | "projected": textures are laid over the model from its sides, in metres, so the model needs no UVs and every texel is the same size (the game's look). "uv": the model's own UVs, for textures painted to fit it. |
| `textureSize` | number | `2` | Metres one copy of the texture covers, for "projected" mapping. |
| `tiling` | number | `1` | Copies of the texture across the UVs, for "uv" mapping. |
| `scrollX` | number | `0` | Moves the texture sideways, in copies per second; 0 for still. |
| `scrollY` | number | `0` | Moves the texture up, in copies per second; 0 for still. |
| `metallic` | number | `0` | 0 is plastic or wood, 1 bare metal. |
| `smoothness` | number | `0.3` | 0 is rough and matte, 1 polished. |
| `emission` | colour: `[r, g, b]` (0 to 1) or `"#rrggbb"` | `"#000000"` | Light the surface gives off itself, e.g. "#ff4000". Black for none. |
| `emissionStrength` | number | `1` | Multiplies the emission; above 1 makes it glow. |
| `emissionMap` | text | none | Optional .png that masks the emission, e.g. lit windows on a wall. |
| `pulse` | number | `0` | Fades the emission in and out this many times a second; 0 for steady. |
| `wave` | number | `0` | Moves the surface in and out by up to this many metres, like a flag; 0 for still. |
| `waveSpeed` | number | `1` | Waves per second. |
| `alphaCutoff` | number | `0` | Above 0, parts of the texture with less opacity than this are cut away completely, e.g. 0.5 for leaves or a fence. Unlike [`MaterialSpec.Opacity`](api/ModMaterials.md#materialspec-opacity), what's left stays solid and casts shadows. |
| `doubleSided` | `true` or `false` | `false` | Draws both sides of each face, for thin things like leaves and flags. |
| `flatShading` | `true` or `false` | `false` | Lights each face flat, for a faceted low-poly look. |

<a name="game-materials"></a>

## Game materials

The game's own materials, for a part's material or a material's `base`. Use them as `game:` and the name, e.g. `"game:cobble"`. There are 300:

`armor_plating_1`, `baobabbark`, `bark`, `bark2`, `bark_shurikeno`, `beat_up_metal`, `birchbark`, `bombastic`, `boomite`, `burlwood1`, `burlwood2`, `burlwood3`, `burlwoodplank1`, `burlwoodplank2`, `burlwoodplank3`, `cactus`, `cactusnocolor`, `candycane`, `carbonfiber`, `cardboardlight`, `cautiontape`, `ceramictilesbig`, `ceramictilesmedium`, `checkered_1`, `checkered_2`, `chestnutplanks`, `chestnutwood`, `chickenmushroom`, `chipboard`, `christmaswallpaper`, `christmaswallpapercheckered`, `christmaswallpapersnowflake`, `christmaswallpapertree`, `circle_textured_metal`, `cobaltgranite`, `cobble`, `construction_particle_board`, `construction_structural_plywood`, `coral1`, `coral2`, `coral3`, `coral4`, `coral4b`, `coral5`, `coral6`, `coralwood1`, `coralwood2`, `cottonwoodbark`, `cypressbark`, `cypressplanks`, `cypresssanded`, `defaultbark`, `developmentgrid`, `diamandcactus`, `diamandcactus2`, `diamond_plate`, `drycoral1`, `errortexture`, `evilmush`, `fabric2`, `foil2`, `foilgradient`, `foiluranium`, `forestfloor`, `forestsand`, `future_metal`, `future_panels_1`, `ghostmushroom`, `granitegreeb`, `granitemetal`, `grasssnow`, `grassv2`, `greenacid_1`, `grid`, `honeycomb`, `industrial_walls`, `lavaglowy`, `leaf1`, `leaf3`, `leafvar2`, `lightfoil`, `lightgrass`, `lightgrass_2`, `lightmetal`, `materialvariant_2`, `materialvariant_3`, `matte_wood`, `matte_wood_brick`, `matte_wood_brick_double`, `matte_wood_checkerboard`, `matte_wood_checkerboard_double`, `matte_wood_checkerboard_triple`, `matte_wood_chevron`, `matte_wood_endless`, `matte_wood_herringbone`, `matte_wood_herringbone_double`, `matte_wood_herringbone_half`, `matte_woodglow`, `messysand`, `metal`, `metal_feathers`, `metal_floor_1`, `metal_grate`, `metal_grate_1`, `metal_grid`, `metal_grid_1`, `metal_grid_3`, `metal_grid_v2`, `metal_grill_13`, `metal_grill_16`, `metal_grill_4`, `metal_grill_5`, `metal_grill_5a`, `metal_ornament`, `metal_panel_8`, `metal_panels_1`, `metal_plate_17`, `metal_plate_18`, `metal_plate_22`, `metal_plate_31`, `metal_plate_46`, `metal_sheet_01`, `metal_sheet_02`, `metal_sheet_03`, `metal_sheet_04`, `metal_sheet_05`, `metal_sheet_06`, `metal_surface_01`, `metal_surface_02`, `metal_tiles_1`, `metal_tiles_2`, `metal_ventilation_1`, `metalchrome`, `metalchromeshiny`, `mossycobblestone`, `mossyslate`, `mossyslate2`, `new_graph`, `normalstud`, `noshading`, `oceansand`, `oil`, `old_brick`, `old_cobblestone`, `old_concrete`, `old_concrete_2`, `old_foil`, `old_grass`, `old_ice`, `old_planks`, `old_sand`, `old_wood`, `old_woodplanks`, `omega`, `opalblue`, `opalbrown`, `opalred`, `palmbark`, `peaceandhearts_dark`, `peaceandhearts_light`, `permaforst`, `petalsanddaisies_dark`, `petalsanddaisies_light`, `pinebark`, `plank`, `plank1`, `plastic_rubber_grid`, `podzol`, `pricklycactus`, `prototype`, `radioactivestone`, `raindbowbutterfry`, `rawbluesteel`, `rawlithium`, `rawopal`, `rawopalblue`, `rawopalred`, `rawore`, `red_sci_fi_metal`, `reinforced_metal`, `ribbed_chipped_metal`, `ribbed_chipped_metal_2`, `rockytrail`, `rough_metal`, `rusted_ribbed_metal`, `sandybeach`, `sci_fi_fabric`, `sci_fi_floor_2`, `sci_fi_grid`, `sci_fi_metal_panel_4`, `sci_fi_metal_panel_5`, `sci_fi_padded_fabric`, `sci_fi_painted_walls`, `sci_fi_panel_1`, `sci_fi_panels_1`, `sci_fi_panels_10`, `sci_fi_panels_11`, `sci_fi_panels_12`, `sci_fi_panels_13`, `sci_fi_panels_14`, `sci_fi_panels_2`, `sci_fi_panels_3`, `sci_fi_panels_4`, `sci_fi_panels_5`, `sci_fi_panels_6`, `sci_fi_panels_7`, `sci_fi_panels_8`, `sci_fi_panels_9`, `sci_fi_screens_1`, `scuffed_iron`, `scuffed_iron_2`, `semirough_metal`, `ship_corridor`, `smooth_fabric`, `space_crate`, `space_crate_1`, `space_cruiser_panels_2`, `spaceship_panels`, `spearmintcandycane`, `steel_plate`, `steel_plate_1`, `storage_container`, `storage_container_2`, `studs`, `studssmall`, `sunandsakura_dark`, `sunandsakura_light`, `tech_braided_cable_1`, `tech_braided_cable_2`, `tech_braided_cable_3`, `tech_braided_cable_4`, `tech_grid`, `tech_plastic_base_1`, `tech_plastic_base_2`, `tech_plastic_grid_1`, `tech_plastic_grid_2`, `tech_plastic_grid_3`, `tech_plastic_grid_4`, `tech_plastic_grid_5`, `tech_polystyrene_1`, `truffle`, `turf`, `uniqueplank1`, `used_stainless_steel`, `used_stainless_steel_2`, `varnished_wood`, `varnished_wood_brick`, `varnished_wood_brick_double`, `varnished_wood_checkerboard`, `varnished_wood_checkerboard_double`, `varnished_wood_checkerboard_triple`, `varnished_wood_chevron`, `varnished_wood_endless`, `varnished_wood_herringbone`, `varnished_wood_herringbone_double`, `varnished_wood_herringbone_half`, `vented_metal_panel`, `vented_metal_panels`, `vertically_lined_metal`, `vintagewallpaper01`, `vintagewallpaper02`, `vintagewallpaper03`, `vintagewallpaper04`, `vintagewallpaper05`, `vintagewallpaper06`, `vintagewallpaper07`, `vintagewallpaper08`, `vintagewallpaper09`, `vintagewallpaper10`, `vintagewallpaper11`, `vintagewallpaper12`, `vintagewallpaper13`, `vintagewallpaper14`, `vintagewallpaper15`, `vintagewallpaper16`, `voidcystal`, `water`, `wavyplank`, `wavyplankglowy`, `weld`, `wetrock`, `wood6`, `wooddark`, `woodlight`, `woodold`, `woodplankshiny`, `worn_military_siding_1`, `worn_modern_panels_1`, `worn_wood`, `worn_wood_brick`, `worn_wood_brick_double`, `worn_wood_checkerboard`, `worn_wood_checkerboard_double`, `worn_wood_checkerboard_triple`, `worn_wood_chevron`, `worn_wood_endless`, `worn_wood_herringbone`, `worn_wood_herringbone_double`, `worn_wood_herringbone_half`, `zigzag_fabric`
