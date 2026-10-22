# ModContext

[All docs](../index.md)

<a name="modcontext"></a>

## ModContext

*class*

One loaded mod's view of the game. Everything a mod registers goes through here, so it can all be removed when the mod unloads.

### Properties

<a name="modcontext-id"></a>

#### Id

```csharp
string Id { get; }
```

The mod's id from its mod.json, e.g. "traffic\_cone".

<a name="modcontext-name"></a>

#### Name

```csharp
string Name { get; }
```

The mod's name from its mod.json.

<a name="modcontext-version"></a>

#### Version

```csharp
string Version { get; }
```

The mod's version from its mod.json.

<a name="modcontext-isserver"></a>

#### IsServer

```csharp
bool IsServer { get; }
```

Whether this copy of the game is the server. Gameplay (events, commands, spawning) only happens on the server; clients only use a mod's content.

<a name="modcontext-isclient"></a>

#### IsClient

```csharp
bool IsClient { get; }
```

Whether this copy of the game is in a world as a player (also true on the host). UI, sounds and input belong here.

<a name="modcontext-localplayer"></a>

#### LocalPlayer

```csharp
Player LocalPlayer { get; }
```

The player on this machine, while [`ModContext.IsClient`](ModContext.md#modcontext-isclient).

<a name="modcontext-log"></a>

#### Log

```csharp
ModLog Log { get; }
```

Writing to the game's log.

<a name="modcontext-events"></a>

#### Events

```csharp
ModEvents Events { get; }
```

Things happening in the game: players, chat, items, trees, ticks.

<a name="modcontext-commands"></a>

#### Commands

```csharp
ModCommands Commands { get; }
```

Chat commands, with suggestions as players type.

<a name="modcontext-items"></a>

#### Items

```csharp
ModItems Items { get; }
```

Item kinds, and what happens when they're used.

<a name="modcontext-tools"></a>

#### Tools

```csharp
ModTools Tools { get; }
```

Tool kinds held from the hotbar, and what happens when they're used.

<a name="modcontext-inventory"></a>

#### Inventory

```csharp
ModInventory Inventory { get; }
```

Players' inventories, and items that only live in them.

<a name="modcontext-crafting"></a>

#### Crafting

```csharp
ModCrafting Crafting { get; }
```

Recipes in the Crafting tab.

<a name="modcontext-vehicles"></a>

#### Vehicles

```csharp
ModVehicles Vehicles { get; }
```

Vehicle kinds.

<a name="modcontext-trees"></a>

#### Trees

```csharp
ModTrees Trees { get; }
```

Tree species.

<a name="modcontext-materials"></a>

#### Materials

```csharp
ModMaterials Materials { get; }
```

Materials for items' and vehicles' surfaces.

<a name="modcontext-world"></a>

#### World

```csharp
ModWorld World { get; }
```

Players, items and the ground in the running world.

<a name="modcontext-chat"></a>

#### Chat

```csharp
ModChat Chat { get; }
```

Messages in everyone's chat.

<a name="modcontext-timers"></a>

#### Timers

```csharp
ModTimers Timers { get; }
```

Running code later, or again and again.

<a name="modcontext-network"></a>

#### Network

```csharp
ModNetwork Network { get; }
```

Messages between this mod on the server and on clients.

<a name="modcontext-effects"></a>

#### Effects

```csharp
ModEffects Effects { get; }
```

Sounds.

<a name="modcontext-ui"></a>

#### UI

```csharp
ModUI UI { get; }
```

The mod's own on-screen panels.

<a name="modcontext-input"></a>

#### Input

```csharp
ModInput Input { get; }
```

Keys and mouse buttons.

<a name="modcontext-settings"></a>

#### Settings

```csharp
ModSettings Settings { get; }
```

The mod's options in the game's Settings screen.

<a name="modcontext-isloaded"></a>

#### IsLoaded

```csharp
bool IsLoaded { get; }
```

False once the mod has been unloaded.
