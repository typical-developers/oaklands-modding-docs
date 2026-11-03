# ModWorld

[All docs](../index.md) · In this page: [`ModWorld`](#modworld) · [`GroundFilter`](#groundfilter) · [`Player`](#player) · [`WorldItem`](#worlditem)

<a name="modworld"></a>

## ModWorld

*class*

Players and items in the world. Server only unless noted.

### Properties

<a name="modworld-timeofday"></a>

#### TimeOfDay

```csharp
float TimeOfDay { get; set; }
```

The hour of the day, 0 up to 24 (13.5 is half past one in the afternoon), the same for every player. Read it anywhere; setting it changes the sky for everyone and is saved with the world. Server only to set.

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

The ground (or whatever's solid and stays put, like buildings and rocks) under a point, or the point itself if there's nothing there. Items, vehicles, players and trees are left out; see [`ModWorld.GroundAt()`](ModWorld.md#modworld-groundat) to choose.

#### GroundAt

```csharp
Vector3 GroundAt(Vector3 position, GroundFilter filter)
```

The highest thing under a point that `filter` counts, or the point itself if there's nothing there. Looks from 200 m above to 200 m below.

<a name="modworld-finditems"></a>

#### FindItems

```csharp
List<WorldItem> FindItems(Vector3 center, float radius)
```

Items whose centre is within `radius` metres, including logs ("tree\_log") and vehicles (whose [`WorldItem.Velocity`](ModWorld.md#worlditem-velocity) reads right even while someone drives them). Works on clients too, for the items they have.

<a name="modworld-finditemsbyid"></a>

#### FindItemsById

```csharp
List<WorldItem> FindItemsById(string itemId)
```

Every item of one kind in the world, wherever it is, e.g. "my\_mod:droid". Works on clients too, for the items they have.

<a name="modworld-raycast"></a>

#### Raycast

```csharp
WorldHit Raycast(Vector3 origin, Vector3 direction, float maxDistance = 100, WorldItem ignore = default(WorldItem))
```

What a ray hits first: the ground, an item, a player, a tree or a building, up to `maxDistance` metres (at most 1000). `ignore` is an item to see through, such as the one casting it. Works on clients too, e.g. from [`Player.LookOrigin`](ModWorld.md#player-lookorigin) on the local player.

<a name="modworld-findtrees"></a>

#### FindTrees

```csharp
List<WorldTree> FindTrees(Vector3 center, float radius)
```

Trees whose base is within `radius` metres (at most 500), standing or stumps; check [`WorldTree.IsStanding`](WorldTree.md#worldtree-isstanding).

<a name="modworld-choptree"></a>

#### ChopTree

```csharp
ChopResult ChopTree(WorldTree tree, float damage, Vector3 from, float height = 0.8)
```

Chops into a standing tree's trunk `height` metres up, as an axe swung from `from` would: the notch opens towards it and the tree falls away from it. An axe does 12 a hit; a grown trunk takes a few hundred. Pieces that come off become logs ("tree\_log" items), and [`ModEvents.TreeFelled`](ModEvents.md#modevents-treefelled) is raised as usual. Server only.

<a name="modworld-felltree"></a>

#### FellTree

```csharp
bool FellTree(WorldTree tree, Vector3 from, float height = 0.8)
```

Cuts a standing tree straight through, falling away from `from`. Server only.

<a name="modworld-findpath"></a>

#### FindPath

```csharp
PathRequest FindPath(Vector3 from, Vector3 to, WalkerSpec walker = null, Action<PathRequest> done = null)
```

Starts working out a walkable path between two points for something of `walker`'s size (a person's if null): over the ground, buildings and rocks, up steps and slopes it can manage, round trees, items and walls. It takes a few frames; `done` runs when it's ready (or poll [`PathRequest.IsDone`](Paths.md#pathrequest-isdone)). One search covers about 120 m; for further, walk to the end and look again. Walking items do all this themselves with [`WorldItem.MoveTo()`](ModWorld.md#worlditem-moveto). Server only.

<a name="modworld-findbuildingparts"></a>

#### FindBuildingParts

```csharp
List<BuildingPart> FindBuildingParts(Vector3 center, float radius, bool unfinishedOnly = true)
```

Parts built with the hammer that overlap a sphere: only unfinished blueprints unless `unfinishedOnly` is false. Sorted lowest first, then nearest, which is a good order to fill them in. Server only.

<a name="modworld-getbuildingpart"></a>

#### GetBuildingPart

```csharp
BuildingPart GetBuildingPart(uint id)
```

A part as it is now, by id; invalid if it's gone. Server only.

<a name="modworld-placebuildingpart"></a>

#### PlaceBuildingPart

```csharp
BuildingPart PlaceBuildingPart(string schematicId, Vector3 position, Quaternion rotation, Vector3 size, string builderId)
```

Places a new blueprint part, e.g. "wall", as the hammer would for `builderId` (who must be allowed to build there). `size` is in metres, snapped to the building grid (5 cm); zero for the schematic's own size. Returns the part, or an invalid one with the reason in the log. Server only.

<a name="modworld-getschematicids"></a>

#### GetSchematicIds

```csharp
List<string> GetSchematicIds()
```

The kinds of part that can be placed, e.g. "wall".

<a name="modworld-getfillmaterialids"></a>

#### GetFillMaterialIds

```csharp
List<string> GetFillMaterialIds()
```

The materials parts can be filled with, one per kind of wood and so on.

<a name="groundfilter"></a>

## GroundFilter

*enum*

What [`ModWorld.GroundAt()`](ModWorld.md#modworld-groundat) stands things on.

| Value | Description |
| --- | --- |
| <a name="groundfilter-static"></a>`Static` | Terrain and anything that stays put: buildings, rocks, placed structures. |
| <a name="groundfilter-terrain"></a>`Terrain` | Only the terrain itself, ignoring everything built or placed on it. |
| <a name="groundfilter-anything"></a>`Anything` | Anything solid, items and vehicles included (still not players or trees). |

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

The middle of their body, about 1 m above their feet (see [`Player.FeetPosition`](ModWorld.md#player-feetposition); their eyes are [`Player.LookOrigin`](ModWorld.md#player-lookorigin)).

<a name="player-feetposition"></a>

#### FeetPosition

```csharp
Vector3 FeetPosition { get; }
```

Where they stand: the bottom of their body.

<a name="player-forward"></a>

#### Forward

```csharp
Vector3 Forward { get; }
```

The way their body faces, flattened (no up or down; see [`Player.LookDirection`](ModWorld.md#player-lookdirection) for that).

<a name="player-lookorigin"></a>

#### LookOrigin

```csharp
Vector3 LookOrigin { get; }
```

Their eyes: where the camera is, for [`ModWorld.Raycast()`](ModWorld.md#modworld-raycast).

<a name="player-lookdirection"></a>

#### LookDirection

```csharp
Vector3 LookDirection { get; }
```

Where they're looking, up and down included. Exact for [`ModContext.LocalPlayer`](ModContext.md#modcontext-localplayer) on its own client. On the server, clients report how far up or down they look about ten times a second, so it can be a degree or so behind; for exact clicks use [`InteractEvent.Point`](ModEvents.md#interactevent-point) or a tool's [`ToolUseEvent.HitPoint`](ModTools.md#tooluseevent-hitpoint). Other clients only know the way the body faces.

<a name="player-lookingat"></a>

#### LookingAt

```csharp
WorldHit LookingAt { get; }
```

What's under their crosshair within reach (the interaction prompt's reach, about 3 m), from the same ray the prompt uses: the item, the point on it and its [`WorldHit.Node`](WorldTree.md#worldhit-node). On [`ModContext.LocalPlayer`](ModContext.md#modcontext-localplayer)'s own client it's exactly what the prompt sees; on the server it's worked out from [`Player.LookDirection`](ModWorld.md#player-lookdirection). [`ModEvents.HoverChanged`](ModEvents.md#modevents-hoverchanged) says when the local player's changes.

<a name="player-helditem"></a>

#### HeldItem

```csharp
WorldItem HeldItem { get; }
```

The item they carry in both hands (picked up with "Hold"), or an invalid item. Readable on the server for anyone and on a client for the local player.

<a name="player-heldtool"></a>

#### HeldTool

```csharp
string HeldTool { get; }
```

The id of the tool in their hand from the hotbar, e.g. "axe" or "mining:pickaxe", or null for none. Readable on the server for anyone and on a client for the local player.

<a name="player-flying"></a>

#### Flying

```csharp
bool Flying { get; set; }
```

Whether they fly: no gravity, moving the way they look, jump to rise and sprint to sink. Always true while [`Player.Noclip`](ModWorld.md#player-noclip) is. Readable anywhere; setting it is server only and does nothing elsewhere.

<a name="player-noclip"></a>

#### Noclip

```csharp
bool Noclip { get; set; }
```

Whether they fly through everything, walls and ground included, colliding with nothing. Turning it off leaves them flying if [`Player.Flying`](ModWorld.md#player-flying) was on before. Readable anywhere; setting it is server only and does nothing elsewhere.

<a name="player-flyspeed"></a>

#### FlySpeed

```csharp
float FlySpeed { get; set; }
```

How fast they fly, in meters a second (15 to start with), kept between 1 and 200. Readable anywhere; setting it is server only and does nothing elsewhere.

### Methods

<a name="player-getlookhit"></a>

#### GetLookHit

```csharp
WorldHit GetLookHit(float maxDistance)
```

Like [`Player.LookingAt`](ModWorld.md#player-lookingat), but out to `maxDistance` metres (at most 1000), e.g. for a tool that reaches further.

<a name="player-hold"></a>

#### Hold

```csharp
bool Hold(WorldItem item)
```

Puts an item into their hands, as if they'd used its "Hold" option: they must have empty hands and be alive and on foot, and the item small and light enough and free. False if they can't. Server only.

<a name="player-dropheld"></a>

#### DropHeld

```csharp
bool DropHeld()
```

Makes them let go of what they carry, where it is. False if they carry nothing. Server only.

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

Metres per second. Setting it throws the item (not a kinematic, attached or walking one). Reads right for vehicles being driven and walkers too, measured from how they move.

<a name="worlditem-hasdatastore"></a>

#### HasDataStore

```csharp
bool HasDataStore { get; }
```

Whether this item can hold data (only mods' own items and vehicles can).

<a name="worlditem-model"></a>

#### Model

```csharp
string Model { get; }
```

The model this item shows instead of its kind's (see [`WorldItem.SetModel()`](ModWorld.md#worlditem-setmodel)), or null.

<a name="worlditem-playinganimation"></a>

#### PlayingAnimation

```csharp
string PlayingAnimation { get; }
```

The clip playing (or that played last and stopped on its end), or null.

<a name="worlditem-animationspeed"></a>

#### AnimationSpeed

```csharp
float AnimationSpeed { get; set; }
```

How fast clips play: 1 as made, 0.5 half speed, negative backwards (-10 to 10). Set it to match a walk to how fast the item moves. Server only to set.

<a name="worlditem-iskinematic"></a>

#### IsKinematic

```csharp
bool IsKinematic { get; set; }
```

While true the item stays exactly where it's put: physics leaves it alone (setting [`WorldItem.Position`](ModWorld.md#worlditem-position) and [`WorldItem.Rotation`](ModWorld.md#worlditem-rotation) still moves it), and players can't drag or hold it, e.g. parts snapped onto a stand or cable laid on the ground. Saved with mod items. Readable anywhere; server only to set.

<a name="worlditem-attachedto"></a>

#### AttachedTo

```csharp
WorldItem AttachedTo { get; }
```

What it's attached to, or an invalid item. Server only.

<a name="worlditem-iswalker"></a>

#### IsWalker

```csharp
bool IsWalker { get; }
```

Whether [`WorldItem.SetWalker()`](ModWorld.md#worlditem-setwalker) has made it a walker.

<a name="worlditem-movestate"></a>

#### MoveState

```csharp
MoveState MoveState { get; }
```

What a walker is doing. Server only.

<a name="worlditem-isgrounded"></a>

#### IsGrounded

```csharp
bool IsGrounded { get; }
```

Whether a walker has its feet on something. Server only.

<a name="worlditem-isvehicle"></a>

#### IsVehicle

```csharp
bool IsVehicle { get; }
```

Whether this is a vehicle, the game's or a mod's.

<a name="worlditem-seatcount"></a>

#### SeatCount

```csharp
int SeatCount { get; }
```

How many seats it has; 0 for anything that isn't a vehicle.

<a name="worlditem-driver"></a>

#### Driver

```csharp
Player Driver { get; }
```

Who's driving, or an invalid player. Server only.

<a name="worlditem-controls"></a>

#### Controls

```csharp
VehicleControls Controls { get; }
```

The driver's throttle and steering right now (zero with nobody driving), e.g. for a mod's own engine sound or brake lights.

<a name="worlditem-hitchedto"></a>

#### HitchedTo

```csharp
WorldItem HitchedTo { get; }
```

What this vehicle is hitched to, or an invalid item.

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

<a name="worlditem-setmaterial"></a>

#### SetMaterial

```csharp
void SetMaterial(string slot, string materialId)
```

Shows a material on this item's parts whose model material is called `slot` ("\*" for every part), for everyone, and saves it. Use full ids, e.g. "game:cobble" or what [`ModMaterials.Register()`](ModMaterials.md#modmaterials-register) returned. null puts back what the item's kind uses. Server only.

<a name="worlditem-setcolor"></a>

#### SetColor

```csharp
void SetColor(string slot, Color color)
```

Tints the parts' color, e.g. to paint one car red. Server only.

<a name="worlditem-setglow"></a>

#### SetGlow

```csharp
void SetGlow(string slot, Color glow)
```

Makes the parts give off light of this color (black for none). Server only.

<a name="worlditem-resetlook"></a>

#### ResetLook

```csharp
void ResetLook(string slot)
```

Undoes [`WorldItem.SetMaterial()`](ModWorld.md#worlditem-setmaterial), [`WorldItem.SetColor()`](ModWorld.md#worlditem-setcolor) and [`WorldItem.SetGlow()`](ModWorld.md#worlditem-setglow) for a slot. Server only.

<a name="worlditem-addlight"></a>

#### AddLight

```csharp
void AddLight(LightSpec light)
```

Adds a light to just this item (or replaces one of the same name, its kind's included), for everyone, saved with it; see [`LightSpec`](ModItems.md#lightspec). At most 8 per item. Mod items and vehicles only; server only.

<a name="worlditem-removelight"></a>

#### RemoveLight

```csharp
void RemoveLight(string name)
```

Takes away a light [`WorldItem.AddLight()`](ModWorld.md#worlditem-addlight) added (its kind's can only be switched off). Server only.

<a name="worlditem-setlighton"></a>

#### SetLightOn

```csharp
void SetLightOn(string name, bool on)
```

Switches a light on or off, for everyone, saved. Server only.

<a name="worlditem-islighton"></a>

#### IsLightOn

```csharp
bool? IsLightOn(string name)
```

Whether [`WorldItem.SetLightOn()`](ModWorld.md#worlditem-setlighton) last switched a light on; null if it hasn't been switched (it's as its spec says).

<a name="worlditem-setlightcolor"></a>

#### SetLightColor

```csharp
void SetLightColor(string name, Color color)
```

Changes a light's colour, e.g. a status light going red. Server only.

<a name="worlditem-setlightintensity"></a>

#### SetLightIntensity

```csharp
void SetLightIntensity(string name, float intensity)
```

Changes a light's brightness (0 to 50). Server only.

<a name="worlditem-setdooropen"></a>

#### SetDoorOpen

```csharp
void SetDoorOpen(string node, bool open)
```

Opens or shuts one of a vehicle's doors (see [`VehicleSpec.Doors`](ModVehicles.md#vehiclespec-doors)) by its node, for everyone, and keeps it that way (saved) until set again. Server only.

<a name="worlditem-isdooropen"></a>

#### IsDoorOpen

```csharp
bool IsDoorOpen(string node)
```

Whether [`WorldItem.SetDoorOpen()`](ModWorld.md#worlditem-setdooropen) has opened a door.

<a name="worlditem-setmodel"></a>

#### SetModel

```csharp
void SetModel(string model)
```

Shows a model of this item's own instead of its kind's: a mesh made with [`ModMeshes.Register()`](ModMeshes.md#modmeshes-register), by the id that returned, e.g. a statue that's carved bit by bit, each its own mesh. Every player sees it and it's saved with the item, but the mesh itself isn't: each peer's copy of the mod makes it (and makes it again as it changes), and until it has, the item shows its kind's model. null goes back to the kind's. Mod items only; server only.

<a name="worlditem-playanimation"></a>

#### PlayAnimation

```csharp
void PlayAnimation(string clip, bool loop = true, float fade = 0.2)
```

Plays one of the model's animation clips, by its name in the glTF file (e.g. "Walk"), for everyone, cross-fading over `fade` seconds from what was playing. A clip that doesn't loop stops on its last frame. Players who join later see it from where it's got to. Nothing plays until this is called. Mod items and vehicles only; server only.

<a name="worlditem-stopanimation"></a>

#### StopAnimation

```csharp
void StopAnimation()
```

Stops the clip playing, leaving the model where the clip had it. Server only.

<a name="worlditem-getanimationnames"></a>

#### GetAnimationNames

```csharp
List<string> GetAnimationNames()
```

The model's clip names, as this peer has the model (empty while it loads).

<a name="worlditem-setnoderotation"></a>

#### SetNodeRotation

```csharp
void SetNodeRotation(string node, Quaternion localRotation)
```

Turns one named part of the model (a node in the glTF file, e.g. "Head", "ReelDrum" or a bone), relative to its parent, for everyone. It overrides any clip on that part. Fine to call every tick for a spinning fan, though [`WorldItem.PlayAnimation()`](ModWorld.md#worlditem-playanimation) is smoother for anything that loops. Server only.

<a name="worlditem-setnodeposition"></a>

#### SetNodePosition

```csharp
void SetNodePosition(string node, Vector3 localPosition)
```

Moves one named part of the model, relative to its parent, for everyone. Server only.

<a name="worlditem-resetnode"></a>

#### ResetNode

```csharp
void ResetNode(string node)
```

Gives a part back to the model (and its clips). Server only.

<a name="worlditem-hasnode"></a>

#### HasNode

```csharp
bool HasNode(string node)
```

Whether the model has a part of that name, as this peer has the model.

<a name="worlditem-setnodevisible"></a>

#### SetNodeVisible

```csharp
void SetNodeVisible(string node, bool visible)
```

Hides a model node and everything under it, for everyone, or shows it again, e.g. to swap a part or take a door off. Its colliders stay (see [`ItemSpec.VisualOnly`](ModItems.md#itemspec-visualonly) for parts that should never be solid). Saved with the item. Server only.

<a name="worlditem-isnodevisible"></a>

#### IsNodeVisible

```csharp
bool IsNodeVisible(string node)
```

Whether [`WorldItem.SetNodeVisible()`](ModWorld.md#worlditem-setnodevisible) has hidden a node.

<a name="worlditem-getnodeat"></a>

#### GetNodeAt

```csharp
string GetNodeAt(Vector3 point, params string[] names)
```

The name of the model node at a world point on this item: the mesh part whose box the point is in, or nearest to (within 25 cm), the smallest when several hold it. With `names`, the nearest of those found from that part up its parents, e.g. which of a machine's connectors was clicked. Null if none.

<a name="worlditem-getnodeposition"></a>

#### GetNodePosition

```csharp
Vector3 GetNodePosition(string node)
```

Where a named part of the model is in the world right now, as this peer shows it (clips and all), e.g. a hand to carry something in. The item's own position if there's no such part.

<a name="worlditem-getnoderotation"></a>

#### GetNodeRotation

```csharp
Quaternion GetNodeRotation(string node)
```

How a named part of the model is turned in the world right now, as this peer shows it.

<a name="worlditem-attachto"></a>

#### AttachTo

```csharp
bool AttachTo(WorldItem other, Vector3 localPosition, Quaternion localRotation)
```

Fixes this item to another item or vehicle, at a pose in its space (`localPosition` metres from its middle, turned by `localRotation`), so it moves with it: for carrying, towing, or parts on a stand. Players can't grab it meanwhile, and the two don't collide. Any player holding it lets go. Attachments aren't saved: after a reload the item is loose where it was (set [`WorldItem.IsKinematic`](ModWorld.md#worlditem-iskinematic) too for it to stay put). False if it can't (attaching to itself, or in a loop). Server only.

#### AttachTo

```csharp
bool AttachTo(WorldItem other)
```

Like [`WorldItem.AttachTo()`](ModWorld.md#worlditem-attachto), keeping where it is now relative to `other`.

<a name="worlditem-detach"></a>

#### Detach

```csharp
void Detach()
```

Lets go of what [`WorldItem.AttachTo()`](ModWorld.md#worlditem-attachto) fixed it to. Server only.

<a name="worlditem-setwalker"></a>

#### SetWalker

```csharp
void SetWalker(WalkerSpec spec)
```

Makes the item move like a character instead of a physics body: kinematic, upright, keeping its feet on the ground, stepping up steps, sliding along walls and falling off edges, with [`WorldItem.MoveTo()`](ModWorld.md#worlditem-moveto) to walk a path round trees and buildings. `spec` gives its size and pace; null turns walking off and gives it back to physics. Trees near a walker get colliders, as near a player, so it can't walk through them and can chop them. Saved with mod items. Server only.

<a name="worlditem-moveto"></a>

#### MoveTo

```csharp
void MoveTo(Vector3 target, float speed = 0, float stopDistance = 0.5)
```

Works out a path to `target` and walks it, looking again if it gets stuck or the target is further than one search reaches. `speed` 0 uses the walker's own. It stops within `stopDistance` metres: give a metre or so to walk up to something solid, like a tree or a workbench. Watch [`WorldItem.MoveState`](ModWorld.md#worlditem-movestate), or [`ModEvents.MoveFinished`](ModEvents.md#modevents-movefinished). Walkers only; server only.

<a name="worlditem-walk"></a>

#### Walk

```csharp
void Walk(Vector3 direction, float speed = 0)
```

Walks in a direction (flattened) until told otherwise, keeping to the ground and sliding along walls, e.g. to follow a player. A zero direction stops. Walkers only; server only.

<a name="worlditem-stopmoving"></a>

#### StopMoving

```csharp
void StopMoving()
```

Stops walking where it is. Server only.

<a name="worlditem-getseatoccupant"></a>

#### GetSeatOccupant

```csharp
Player GetSeatOccupant(int seat)
```

Who sits in a seat (0 is the driver's, then the order the spec lists them), or an invalid player for nobody. Server only, since clients only know themselves.

<a name="worlditem-ejectseat"></a>

#### EjectSeat

```csharp
void EjectSeat(int seat)
```

Puts whoever sits in a seat out of the vehicle. Server only.

<a name="worlditem-hitch"></a>

#### Hitch

```csharp
bool Hitch(WorldItem tow, Vector3 point, Vector3 towPoint)
```

Hitches this mod vehicle (a trailer) behind another vehicle, with a ball joint where `point` (in this vehicle's space, e.g. the end of its drawbar) meets `towPoint` (in the tow's space, e.g. its tow ball). It's moved so the points meet, then it's towed: its wheels roll freely, and it follows whoever drives the tow. Hitches aren't saved: hitch it again after a reload. False if it can't be (not a mod vehicle, the tow has no body, or a loop). Server only.

<a name="worlditem-unhitch"></a>

#### Unhitch

```csharp
void Unhitch()
```

Lets go of what [`WorldItem.Hitch()`](ModWorld.md#worlditem-hitch) hitched it to. Server only.

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
