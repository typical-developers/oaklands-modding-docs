# ModMeshes

[All docs](../index.md)

<a name="modmeshes"></a>

## ModMeshes

*class*

Models made in code with [`MeshBuilder`](MeshBuilder.md#meshbuilder): for an item, tool or vehicle kind's `Model`, or for one item with [`WorldItem.SetModel()`](ModWorld.md#worlditem-setmodel). Make them on every peer (like items: each player's copy of the mod builds the same mesh); none of it is saved or written to a file, so build them again from what the mod keeps (item data, [`ModContext.WorldData`](ModContext.md#modcontext-worlddata)) when the world loads. JSON files load before scripts, so a model made here is for specs registered from scripts. At most 1024 per mod.

Models can change as often as needed, e.g. a statue carved a chip at a time: register it again under the same name. With the same material slots as before it's refilled in place, so everything showing it changes at once and only its colliders are rebuilt; that's quick enough to do every time something changes, not every frame.

### Methods

<a name="modmeshes-register"></a>

#### Register

```csharp
string Register(string name, MeshBuilder mesh)
```

Adds a model, or replaces this mod's one of the same name, and returns its id, "mesh:modid/name", for a spec's `Model` or [`WorldItem.SetModel()`](ModWorld.md#worlditem-setmodel) (a spec of this mod's can also say just "mesh:name"). Replacing it changes everything that shows it. The builder is copied, so it can be changed or reused afterwards.

<a name="modmeshes-exists"></a>

#### Exists

```csharp
bool Exists(string name)
```

Whether this mod has a model of that name (or id).

<a name="modmeshes-remove"></a>

#### Remove

```csharp
bool Remove(string name)
```

Removes a model the mod is done with, e.g. a carved statue that was destroyed. Anything still showing it goes back to its kind's model. False if there was none.
