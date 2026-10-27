# ModImages

[All docs](../index.md)

<a name="modimages"></a>

## ModImages

*class*

Pictures made in code with [`ImageBuilder`](ImageBuilder.md#imagebuilder), used by the name [`ModImages.Register()`](ModImages.md#modimages-register) returns ("image:name") wherever a mod's image file goes: `Icon` in item, tool and inventory specs, `Texture`, `NormalMap` and `EmissionMap` in material specs, and [`ModUI.Image()`](ModUI.md#modui-image). Register them before what uses them, on every peer. At most 128 per mod.

### Methods

<a name="modimages-register"></a>

#### Register

```csharp
string Register(string name, ImageBuilder image, bool smooth = false)
```

Adds a picture, or replaces this mod's one of the same name, and returns "image:name". Replacing it at the same size updates whatever shows it straight away (materials, UI, icons): fine for a screen or gauge redrawn now and then. Pixels stay crisp, as the game's textures are, unless `smooth`.

<a name="modimages-exists"></a>

#### Exists

```csharp
bool Exists(string name)
```

Whether this mod has a picture of that name.

<a name="modimages-load"></a>

#### Load

```csharp
ImageBuilder Load(string path)
```

Reads a .png or .jpg from the mod's folder (e.g. "textures/base.png") to draw on, at most 1024 by 1024.
