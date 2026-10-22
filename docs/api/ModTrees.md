# ModTrees

[All docs](../index.md) · In this page: [`ModTrees`](#modtrees) · [`TreeSpeciesSpec`](#treespeciesspec)

<a name="modtrees"></a>

## ModTrees

*class*

Tree species added by mods. They're grown by the same generator as the game's own trees, so they chop, fall and split into logs the same way. JSON files in a mod's `trees` folder are registered before [`Mod.OnLoad()`](Mod.md#mod-onload); plant them with [`ModWorld.PlantTree()`](ModWorld.md#modworld-planttree).

### Methods

<a name="modtrees-register"></a>

#### Register

```csharp
string Register(TreeSpeciesSpec spec)
```

Adds a species, or updates it if this mod already added one with the same id. Returns its full id, "modid:speciesid". Growing the variants takes a moment, so register species while loading, on every peer.

<a name="modtrees-getspeciesids"></a>

#### GetSpeciesIds

```csharp
List<string> GetSpeciesIds()
```

Every species' id, the game's own (e.g. "ash") and mods'.

<a name="treespeciesspec"></a>

## TreeSpeciesSpec

*class*

Describes a tree species. As JSON:

```json
{
  "id": "birch", "name": "Birch", "shape": "slender", "seed": 7,
  "materials": "aspen", "barkColor": [1, 0.97, 0.9], "leafColor": "#9fd36b",
  "settings": { "Trunk": { "Height": 16 } }
}
```

### Fields

<a name="treespeciesspec-id"></a>

#### Id

```csharp
string Id
```

The id within the mod: lowercase letters, digits and '\_', e.g. "birch".

<a name="treespeciesspec-name"></a>

#### Name

```csharp
string Name
```

What players see, e.g. "Birch".

<a name="treespeciesspec-model"></a>

#### Model

```csharp
string Model
```

Optional: a tree built by hand in the game's Tree Editor (a file in the mod's folder, e.g. "tree\_models/cherry.json") instead of grown by the generator. Shape, seed, variants and settings are then unused; the materials and colours below still apply.

<a name="treespeciesspec-shape"></a>

#### Shape

```csharp
string Shape = "broadleaf"
```

"broadleaf" (like ash), "conifer" (like pine) or "slender" (like aspen).

<a name="treespeciesspec-seed"></a>

#### Seed

```csharp
int Seed = 1
```

Different seeds give differently shaped trees.

<a name="treespeciesspec-variants"></a>

#### Variants

```csharp
int Variants = 2
```

Different trees per growth stage, 1 to 6. More look less alike but take longer to grow when the mod loads.

<a name="treespeciesspec-materials"></a>

#### Materials

```csharp
string Materials = "ash"
```

The species whose bark, leaf and cut-wood materials to start from, e.g. "ash".

<a name="treespeciesspec-barktexture"></a>

#### BarkTexture

```csharp
string BarkTexture
```

Optional PNG in the mod's folder for the bark.

<a name="treespeciesspec-leaftexture"></a>

#### LeafTexture

```csharp
string LeafTexture
```

Optional PNG in the mod's folder for the leaf cards (with transparency).

<a name="treespeciesspec-barkcolor"></a>

#### BarkColor

```csharp
Color BarkColor = Color.white
```

Tints the bark; white leaves it as the materials have it.

<a name="treespeciesspec-leafcolor"></a>

#### LeafColor

```csharp
Color LeafColor = Color.white
```

Tints the leaves; white leaves them as the materials have them.

<a name="treespeciesspec-settings"></a>

#### Settings

```csharp
string Settings
```

Advanced: any of the Tree Generator's settings, as JSON, applied on top of [`TreeSpeciesSpec.Shape`](ModTrees.md#treespeciesspec-shape), e.g. `{ "Trunk": { "Height": 16 } }`.
