# ModCrafting

[All docs](../index.md) · In this page: [`ModCrafting`](#modcrafting) · [`RecipeCategory`](#recipecategory) · [`RecipeSpec`](#recipespec) · [`CraftedEvent`](#craftedevent)

<a name="modcrafting"></a>

## ModCrafting

*class*

Recipes in the inventory's Crafting tab. JSON files in a mod's `recipes` folder are added before [`Mod.OnLoad()`](Mod.md#mod-onload); scripts can add more with [`ModCrafting.AddRecipe()`](ModCrafting.md#modcrafting-addrecipe). Recipes can use and make the game's items (e.g. "log", "planks"), this mod's, and those of mods loaded before it.

### Methods

<a name="modcrafting-addrecipe"></a>

#### AddRecipe

```csharp
string AddRecipe(RecipeSpec spec)
```

Adds a recipe, or updates this mod's one with the same id. Returns its full id, "modid:recipeid". Add recipes on every peer, so the Crafting tab shows them.

<a name="modcrafting-getstationids"></a>

#### GetStationIds

```csharp
List<string> GetStationIds()
```

The station kinds recipes can name, e.g. "workbench".

<a name="recipecategory"></a>

## RecipeCategory

*enum*

The groups recipes are listed under.

| Value | Description |
| --- | --- |
| `Tools` |  |
| `Building` |  |
| `Materials` |  |
| `Survival` |  |

<a name="recipespec"></a>

## RecipeSpec

*class*

Describes a recipe, e.g. a mod's `recipes/pickaxe.json`:

```json
{
  "id": "pickaxe", "output": "pickaxe", "station": "workbench", "seconds": 4,
  "ingredients": { "planks": 2, "stone": 3 }
}
```

Plain ids are looked up as this mod's first, then the game's; "othermod:item" names another mod's.

### Fields

<a name="recipespec-id"></a>

#### Id

```csharp
string Id
```

The id within the mod: lowercase letters, digits and '\_'.

<a name="recipespec-name"></a>

#### Name

```csharp
string Name
```

What the recipe is called. Empty for the name of what it makes.

<a name="recipespec-description"></a>

#### Description

```csharp
string Description
```

Empty for the description of what it makes.

<a name="recipespec-category"></a>

#### Category

```csharp
RecipeCategory Category = RecipeCategory.Tools
```

<a name="recipespec-station"></a>

#### Station

```csharp
string Station
```

The station kind it's made at, e.g. "workbench" (see [`ModCrafting.GetStationIds()`](ModCrafting.md#modcrafting-getstationids)); empty for by hand.

<a name="recipespec-output"></a>

#### Output

```csharp
string Output
```

What it makes: an inventory item, a mod item or tool. An item that can't be stored (or a vehicle) is put down beside the station instead.

<a name="recipespec-count"></a>

#### Count

```csharp
int Count = 1
```

How many it makes, for stackable items.

<a name="recipespec-ingredients"></a>

#### Ingredients

```csharp
Dictionary<string, int> Ingredients = new()
```

What it uses up: item id to how many.

<a name="recipespec-seconds"></a>

#### Seconds

```csharp
float Seconds = 2f
```

Seconds to make one.

<a name="craftedevent"></a>

## CraftedEvent

*struct*

A player making a recipe once, for [`ModEvents.Crafted`](ModEvents.md#modevents-crafted).

### Properties

<a name="craftedevent-player"></a>

#### Player

```csharp
Player Player { get; }
```

<a name="craftedevent-recipeid"></a>

#### RecipeId

```csharp
string RecipeId { get; }
```

The recipe's id: "modid:recipeid" for mods' recipes, plain for the game's.

<a name="craftedevent-output"></a>

#### Output

```csharp
string Output { get; }
```

The id of what it made.

<a name="craftedevent-count"></a>

#### Count

```csharp
int Count { get; }
```

How many it made.
