# ModWorld

[All docs](../index.md) · In this page: [`ModWorld`](#modworld) · [`Player`](#player) · [`WorldItem`](#worlditem)

<a name="modworld"></a>

## ModWorld

*class*

Players and items in the world. Server only unless noted.

### Methods

<a name="modworld-getplayers"></a>

#### GetPlayers

```csharp
List<Player> GetPlayers()
```

Everyone connected.

<a name="modworld-spawnitem"></a>

#### SpawnItem

```csharp
WorldItem SpawnItem(string itemId, Vector3 position, Quaternion rotation)
```

Spawns an item, e.g. "axe" or "traffic\_cone:cone". Returns an invalid item if there's no such kind.

#### SpawnItem

```csharp
WorldItem SpawnItem(string itemId, Vector3 position)
```

<a name="modworld-planttree"></a>

#### PlantTree

```csharp
bool PlantTree(string species, Vector3 position, float yaw = 0, int stage = -1)
```

Plants a tree, e.g. "ash" or "mymod:birch", saved with the world. `stage` picks how grown it is, from 0 (a sprout) up; -1 is fully grown. False if there's no such species or already a tree on that spot. Server only.

<a name="modworld-groundat"></a>

#### GroundAt

```csharp
Vector3 GroundAt(Vector3 position)
```

The ground (or whatever's solid) under a point, or the point itself if there's nothing there.

<a name="modworld-finditems"></a>

#### FindItems

```csharp
List<WorldItem> FindItems(Vector3 center, float radius)
```

Items whose centre is within `radius` metres.

<a name="player"></a>

## Player

*struct* · implements `IEquatable<Player>`

A connected player. Becomes invalid when they leave; reading from an invalid player gives defaults rather than throwing.

### Properties

<a name="player-isvalid"></a>

#### IsValid

```csharp
bool IsValid { get; }
```

<a name="player-id"></a>

#### Id

```csharp
string Id { get; }
```

Their lasting id, e.g. a Steam id. Stays the same between sessions.

<a name="player-name"></a>

#### Name

```csharp
string Name { get; }
```

<a name="player-ishost"></a>

#### IsHost

```csharp
bool IsHost { get; }
```

Whether they're hosting the world, so own the server.

<a name="player-position"></a>

#### Position

```csharp
Vector3 Position { get; }
```

Their feet.

<a name="player-forward"></a>

#### Forward

```csharp
Vector3 Forward { get; }
```

The way their body faces, flat.

### Methods

<a name="player-sendmessage"></a>

#### SendMessage

```csharp
void SendMessage(string text)
```

Shows a line in chat to just this player.

### Operators

<a name="player-op_equality"></a>

#### operator ==

```csharp
static bool operator ==(Player a, Player b)
```

<a name="player-op_inequality"></a>

#### operator !=

```csharp
static bool operator !=(Player a, Player b)
```

<a name="worlditem"></a>

## WorldItem

*struct* · implements `IEquatable<WorldItem>`

An item in the world. Becomes invalid when it's destroyed, picked up into an inventory, or unloaded.

### Properties

<a name="worlditem-isvalid"></a>

#### IsValid

```csharp
bool IsValid { get; }
```

Whether the item exists here. Items can be read on clients too; changing them is server only.

<a name="worlditem-itemid"></a>

#### ItemId

```csharp
string ItemId { get; }
```

Its kind, e.g. "axe" or "traffic\_cone:cone".

<a name="worlditem-position"></a>

#### Position

```csharp
Vector3 Position { get; set; }
```

<a name="worlditem-rotation"></a>

#### Rotation

```csharp
Quaternion Rotation { get; set; }
```

<a name="worlditem-velocity"></a>

#### Velocity

```csharp
Vector3 Velocity { get; set; }
```

Metres per second. Setting it throws the item.

<a name="worlditem-hasdatastore"></a>

#### HasDataStore

```csharp
bool HasDataStore { get; }
```

Whether this item can hold data (only mods' own items and vehicles can).

### Methods

<a name="worlditem-destroy"></a>

#### Destroy

```csharp
void Destroy()
```

Removes the item from the world. Server only.

<a name="worlditem-hasdata"></a>

#### HasData

```csharp
bool HasData(string key)
```

<a name="worlditem-setdata"></a>

#### SetData

```csharp
void SetData(string key, int value)
```

#### SetData

```csharp
void SetData(string key, float value)
```

#### SetData

```csharp
void SetData(string key, bool value)
```

#### SetData

```csharp
void SetData(string key, string value)
```

#### SetData

```csharp
void SetData(string key, Vector3 value)
```

#### SetData

```csharp
void SetData(string key, Quaternion value)
```

#### SetData

```csharp
void SetData(string key, Color value)
```

<a name="worlditem-removedata"></a>

#### RemoveData

```csharp
void RemoveData(string key)
```

Removes a value. Server only.

<a name="worlditem-getint"></a>

#### GetInt

```csharp
int GetInt(string key, int fallback = 0)
```

<a name="worlditem-getfloat"></a>

#### GetFloat

```csharp
float GetFloat(string key, float fallback = 0)
```

<a name="worlditem-getbool"></a>

#### GetBool

```csharp
bool GetBool(string key, bool fallback = false)
```

<a name="worlditem-getstring"></a>

#### GetString

```csharp
string GetString(string key, string fallback = null)
```

<a name="worlditem-getvector3"></a>

#### GetVector3

```csharp
Vector3 GetVector3(string key, Vector3 fallback = default(Vector3))
```

<a name="worlditem-getquaternion"></a>

#### GetQuaternion

```csharp
Quaternion GetQuaternion(string key, Quaternion fallback = default(Quaternion))
```

<a name="worlditem-getcolor"></a>

#### GetColor

```csharp
Color GetColor(string key, Color fallback = default(Color))
```

### Operators

<a name="worlditem-op_equality"></a>

#### operator ==

```csharp
static bool operator ==(WorldItem a, WorldItem b)
```

<a name="worlditem-op_inequality"></a>

#### operator !=

```csharp
static bool operator !=(WorldItem a, WorldItem b)
```
