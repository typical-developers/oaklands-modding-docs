# ModWorldData

[All docs](../index.md)

<a name="modworlddata"></a>

## ModWorldData

*class*

Named values this mod keeps for the whole world, saved with it, e.g. "starter kit given" or which droid belongs to whom. Each mod has its own; another mod's can't be read. They're kept even while the mod is turned off for the world. Read them once [`ModEvents.WorldLoaded`](ModEvents.md#modevents-worldloaded) has been raised (at [`ModEvents.ServerStarted`](ModEvents.md#modevents-serverstarted) the save hasn't been read yet). Server only: clients don't have them; send what they need with [`ModNetwork`](ModNetwork.md#modnetwork). At most 1024 keys of up to 128 characters, values up to 16384 characters.

### Methods

<a name="modworlddata-forplayer"></a>

#### ForPlayer

```csharp
ModWorldData ForPlayer(Player player)
```

This mod's values for one player in this world, e.g. which tool they've unlocked or what they carry. Kept by the player's lasting [`Player.Id`](ModWorld.md#player-id), so they're there again when the player comes back, saved with the world, with the same limits per player as the world's. Server only.

#### ForPlayer

```csharp
ModWorldData ForPlayer(string playerId)
```

Like [`ModWorldData.ForPlayer()`](ModWorldData.md#modworlddata-forplayer), by [`Player.Id`](ModWorld.md#player-id), e.g. for someone who isn't online.

<a name="modworlddata-getplayerids"></a>

#### GetPlayerIds

```csharp
List<string> GetPlayerIds()
```

The players' ids this mod has kept values for with [`ModWorldData.ForPlayer()`](ModWorldData.md#modworlddata-forplayer).

<a name="modworlddata-has"></a>

#### Has

```csharp
bool Has(string key)
```

<a name="modworlddata-setint"></a>

#### SetInt

```csharp
void SetInt(string key, int value)
```

<a name="modworlddata-setfloat"></a>

#### SetFloat

```csharp
void SetFloat(string key, float value)
```

<a name="modworlddata-setbool"></a>

#### SetBool

```csharp
void SetBool(string key, bool value)
```

<a name="modworlddata-setstring"></a>

#### SetString

```csharp
void SetString(string key, string value)
```

<a name="modworlddata-setvector3"></a>

#### SetVector3

```csharp
void SetVector3(string key, Vector3 value)
```

<a name="modworlddata-setquaternion"></a>

#### SetQuaternion

```csharp
void SetQuaternion(string key, Quaternion value)
```

<a name="modworlddata-setcolor"></a>

#### SetColor

```csharp
void SetColor(string key, Color value)
```

<a name="modworlddata-getint"></a>

#### GetInt

```csharp
int GetInt(string key, int fallback = 0)
```

<a name="modworlddata-getfloat"></a>

#### GetFloat

```csharp
float GetFloat(string key, float fallback = 0)
```

<a name="modworlddata-getbool"></a>

#### GetBool

```csharp
bool GetBool(string key, bool fallback = false)
```

<a name="modworlddata-getstring"></a>

#### GetString

```csharp
string GetString(string key, string fallback = null)
```

<a name="modworlddata-getvector3"></a>

#### GetVector3

```csharp
Vector3 GetVector3(string key, Vector3 fallback = default(Vector3))
```

<a name="modworlddata-getquaternion"></a>

#### GetQuaternion

```csharp
Quaternion GetQuaternion(string key, Quaternion fallback = default(Quaternion))
```

<a name="modworlddata-getcolor"></a>

#### GetColor

```csharp
Color GetColor(string key, Color fallback = default(Color))
```

<a name="modworlddata-remove"></a>

#### Remove

```csharp
void Remove(string key)
```

Removes a value.

<a name="modworlddata-getkeys"></a>

#### GetKeys

```csharp
List<string> GetKeys()
```

Every key this mod has set in the world.
