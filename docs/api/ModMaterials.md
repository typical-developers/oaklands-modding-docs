# ModMaterials

[All docs](../index.md) · In this page: [`ModMaterials`](#modmaterials) · [`MaterialSpec`](#materialspec)

<a name="modmaterials"></a>

## ModMaterials

*class*

Materials: how the surfaces of items and vehicles look. A mod's `materials` folder is loaded before [`Mod.OnLoad()`](Mod.md#mod-onload) runs; scripts can add more with [`ModMaterials.Register()`](ModMaterials.md#modmaterials-register). The game's own materials can be used too, as "game:" ids, e.g. "game:cobble".

### Methods

<a name="modmaterials-register"></a>

#### Register

```csharp
string Register(MaterialSpec spec)
```

Adds a material, or updates it if this mod already added one with the same id; things already showing it change straight away. Returns its full id, "modid:materialid". Register materials on every peer, as each draws its own world.

<a name="modmaterials-exists"></a>

#### Exists

```csharp
bool Exists(string id)
```

Whether a material exists, e.g. "game:cobble" or "my\_mod:paint".

<a name="modmaterials-getgamematerials"></a>

#### GetGameMaterials

```csharp
List<string> GetGameMaterials()
```

The game's own materials' ids, e.g. "game:cobble".

<a name="materialspec"></a>

## MaterialSpec

*class*

Describes a material. Materials in a mod's `materials` folder are JSON files with these fields, e.g.

```json
{
  "id": "neon", "shader": "unlit", "color": "#2a1a40",
  "emission": "#ff40c0", "emissionStrength": 4, "pulse": 0.5
}
```

Use them on a model's parts with the `materials` field of an item or vehicle, which maps the model's material names to material ids.

### Fields

<a name="materialspec-id"></a>

#### Id

```csharp
string Id
```

The id within the mod: lowercase letters, digits and '\_', e.g. "neon".

<a name="materialspec-shader"></a>

#### Shader

```csharp
string Shader = "lit"
```

"lit" (the game's look, lit by the sun and lamps), "unlit" (its own colour, for screens and signs) or "glass" (the game's see-through glass: uses the colour, opacity and smoothness).

<a name="materialspec-base"></a>

#### Base

```csharp
string Base
```

Optional: a material to start from, e.g. "game:cobble". Fields left at their defaults keep that material's values, so `{ "base": "game:cobble", "color": "#ffcc88" }` is warmer cobble.

<a name="materialspec-color"></a>

#### Color

```csharp
Color Color = Color.white
```

Multiplies the texture.

<a name="materialspec-opacity"></a>

#### Opacity

```csharp
float Opacity = 1f
```

1 is solid, 0 invisible. Below 1 the surface is see-through and casts no shadow.

<a name="materialspec-texture"></a>

#### Texture

```csharp
string Texture
```

A .png in the mod's folder, e.g. "textures/paint.png". None gives a plain colour.

<a name="materialspec-texturestrength"></a>

#### TextureStrength

```csharp
float TextureStrength = 1f
```

0 shows only the texture's average colour, 1 the whole texture.

<a name="materialspec-normalmap"></a>

#### NormalMap

```csharp
string NormalMap
```

Optional normal map, a .png in the mod's folder.

<a name="materialspec-normalstrength"></a>

#### NormalStrength

```csharp
float NormalStrength = 1f
```

How strongly the normal map bends the light.

<a name="materialspec-mapping"></a>

#### Mapping

```csharp
string Mapping = "projected"
```

"projected": textures are laid over the model from its sides, in metres, so the model needs no UVs and every texel is the same size (the game's look). "uv": the model's own UVs, for textures painted to fit it.

<a name="materialspec-texturesize"></a>

#### TextureSize

```csharp
float TextureSize = 2f
```

Metres one copy of the texture covers, for "projected" mapping.

<a name="materialspec-tiling"></a>

#### Tiling

```csharp
float Tiling = 1f
```

Copies of the texture across the UVs, for "uv" mapping.

<a name="materialspec-scrollx"></a>

#### ScrollX

```csharp
float ScrollX
```

Moves the texture sideways, in copies per second; 0 for still.

<a name="materialspec-scrolly"></a>

#### ScrollY

```csharp
float ScrollY
```

Moves the texture up, in copies per second; 0 for still.

<a name="materialspec-metallic"></a>

#### Metallic

```csharp
float Metallic
```

0 is plastic or wood, 1 bare metal.

<a name="materialspec-smoothness"></a>

#### Smoothness

```csharp
float Smoothness = 0.3f
```

0 is rough and matte, 1 polished.

<a name="materialspec-emission"></a>

#### Emission

```csharp
Color Emission = Color.black
```

Light the surface gives off itself, e.g. "#ff4000". Black for none.

<a name="materialspec-emissionstrength"></a>

#### EmissionStrength

```csharp
float EmissionStrength = 1f
```

Multiplies the emission; above 1 makes it glow.

<a name="materialspec-emissionmap"></a>

#### EmissionMap

```csharp
string EmissionMap
```

Optional .png that masks the emission, e.g. lit windows on a wall.

<a name="materialspec-pulse"></a>

#### Pulse

```csharp
float Pulse
```

Fades the emission in and out this many times a second; 0 for steady.

<a name="materialspec-wave"></a>

#### Wave

```csharp
float Wave
```

Moves the surface in and out by up to this many metres, like a flag; 0 for still.

<a name="materialspec-wavespeed"></a>

#### WaveSpeed

```csharp
float WaveSpeed = 1f
```

Waves per second.

<a name="materialspec-alphacutoff"></a>

#### AlphaCutoff

```csharp
float AlphaCutoff
```

Above 0, parts of the texture with less opacity than this are cut away completely, e.g. 0.5 for leaves or a fence. Unlike [`MaterialSpec.Opacity`](ModMaterials.md#materialspec-opacity), what's left stays solid and casts shadows.

<a name="materialspec-doublesided"></a>

#### DoubleSided

```csharp
bool DoubleSided
```

Draws both sides of each face, for thin things like leaves and flags.

<a name="materialspec-flatshading"></a>

#### FlatShading

```csharp
bool FlatShading
```

Lights each face flat, for a faceted low-poly look.
