# ModEvents

[All docs](../index.md) · In this page: [`ModEvents`](#modevents) · [`ChatMessageEvent`](#chatmessageevent) · [`InteractEvent`](#interactevent) · [`HeldItemUseEvent`](#helditemuseevent) · [`SeatEvent`](#seatevent) · [`TreeFelledEvent`](#treefelledevent)

<a name="modevents"></a>

## ModEvents

*class*

Things happening in the game that a mod can react to. Apart from [`ModEvents.Tick`](ModEvents.md#modevents-tick), these are only raised on the server.

### Events

<a name="modevents-tick"></a>

#### Tick

```csharp
event Action<float> Tick
```

Every frame, with the seconds since the last one.

<a name="modevents-playerjoined"></a>

#### PlayerJoined

```csharp
event Action<Player> PlayerJoined
```

A player connected to the server.

<a name="modevents-playerleft"></a>

#### PlayerLeft

```csharp
event Action<Player> PlayerLeft
```

A player left the server.

<a name="modevents-chatmessage"></a>

#### ChatMessage

```csharp
event Action<ChatMessageEvent> ChatMessage
```

A player sent a chat message. Set [`ChatMessageEvent.Cancelled`](ModEvents.md#chatmessageevent-cancelled) to stop it being shown.

<a name="modevents-itempickedup"></a>

#### ItemPickedUp

```csharp
event Action<WorldItem, Player> ItemPickedUp
```

A player picked up a tool from the world.

<a name="modevents-treefelled"></a>

#### TreeFelled

```csharp
event Action<TreeFelledEvent> TreeFelled
```

A standing tree was chopped through.

<a name="modevents-serverstarted"></a>

#### ServerStarted

```csharp
event Action ServerStarted
```

This copy of the game started hosting or running a server (a world was opened). Server-side setup goes here, since mods load at the main menu, before there's a server.

<a name="modevents-serverstopped"></a>

#### ServerStopped

```csharp
event Action ServerStopped
```

The server stopped (the world was closed).

<a name="modevents-clientstarted"></a>

#### ClientStarted

```csharp
event Action ClientStarted
```

This copy of the game joined a world, its own or someone else's.

<a name="modevents-clientstopped"></a>

#### ClientStopped

```csharp
event Action ClientStopped
```

This copy of the game left the world.

<a name="modevents-interacted"></a>

#### Interacted

```csharp
event Action<InteractEvent> Interacted
```

A player used one of a mod item's interactions (see [`ItemSpec.Interactions`](ModItems.md#itemspec-interactions)). Server only; [`ModItems.OnInteract()`](ModItems.md#moditems-oninteract) is a shortcut for one item.

<a name="modevents-interactedlocally"></a>

#### InteractedLocally

```csharp
event Action<InteractEvent> InteractedLocally
```

The local player used an interaction, on their own client, at the moment they used it: for instant feedback like a click sound. The server decides what actually happens.

<a name="modevents-itemdatachanged"></a>

#### ItemDataChanged

```csharp
event Action<WorldItem, string> ItemDataChanged
```

A value in an item's data changed (on the server and every client).

<a name="modevents-toolused"></a>

#### ToolUsed

```csharp
event Action<ToolUseEvent> ToolUsed
```

A player used a mod's tool. Server only; [`ModTools.OnUse()`](ModTools.md#modtools-onuse) is a shortcut for one tool.

<a name="modevents-toolusedlocally"></a>

#### ToolUsedLocally

```csharp
event Action<ToolUseEvent> ToolUsedLocally
```

The local player used a mod's tool, on their own client, as they swung it: for instant feedback like a sound. The hit is worked out here, so it can differ slightly from the server's.

<a name="modevents-crafted"></a>

#### Crafted

```csharp
event Action<CraftedEvent> Crafted
```

A player made a recipe once (the game's or a mod's). Server only.

<a name="modevents-worldloaded"></a>

#### WorldLoaded

```csharp
event Action WorldLoaded
```

The world's save has been read (or a new world has started), after [`ModEvents.ServerStarted`](ModEvents.md#modevents-serverstarted): its items are in place and [`ModContext.WorldData`](ModContext.md#modcontext-worlddata) holds what was saved. Raised for a mod that loads while a world is already open too. Server only.

<a name="modevents-movefinished"></a>

#### MoveFinished

```csharp
event Action<WorldItem, MoveState> MoveFinished
```

A walking item stopped going where [`WorldItem.MoveTo()`](ModWorld.md#worlditem-moveto) sent it: it [`MoveState.Arrived`](Paths.md#movestate-arrived), found [`MoveState.NoPath`](Paths.md#movestate-nopath) or got [`MoveState.Stuck`](Paths.md#movestate-stuck), or was stopped. Server only.

<a name="modevents-hoverchanged"></a>

#### HoverChanged

```csharp
event Action<WorldItem> HoverChanged
```

The thing under the local player's crosshair (within reach) changed, on their own client: the item now, or an invalid one for nothing (or something that isn't an item, like a tree). Read [`Player.LookingAt`](ModWorld.md#player-lookingat) on [`ModContext.LocalPlayer`](ModContext.md#modcontext-localplayer) for the point and node. Client only.

<a name="modevents-helditemused"></a>

#### HeldItemUsed

```csharp
event Action<HeldItemUseEvent> HeldItemUsed
```

A player clicked while carrying a mod item whose [`CarrySpec.Clicks`](ModItems.md#carryspec-clicks) is [`CarryClicks.Mod`](ModItems.md#carryclicks-mod): the left button, or the right ([`HeldItemUseEvent.Alt`](ModEvents.md#helditemuseevent-alt)). Server only.

<a name="modevents-helditemusedlocally"></a>

#### HeldItemUsedLocally

```csharp
event Action<HeldItemUseEvent> HeldItemUsedLocally
```

Like [`ModEvents.HeldItemUsed`](ModEvents.md#modevents-helditemused), on the clicking player's own client at once, for instant feedback.

<a name="modevents-seatchanged"></a>

#### SeatChanged

```csharp
event Action<SeatEvent> SeatChanged
```

A player got into or out of a seat of a vehicle (the game's or a mod's). Server only.

<a name="chatmessageevent"></a>

## ChatMessageEvent

*class*

A chat message being sent, for [`ModEvents.ChatMessage`](ModEvents.md#modevents-chatmessage).

### Properties

<a name="chatmessageevent-player"></a>

#### Player

```csharp
Player Player { get; }
```

Who sent it.

<a name="chatmessageevent-text"></a>

#### Text

```csharp
string Text { get; }
```

What they wrote.

<a name="chatmessageevent-cancelled"></a>

#### Cancelled

```csharp
bool Cancelled { get; set; }
```

Set to true to stop the message being shown to anyone.

<a name="interactevent"></a>

## InteractEvent

*struct*

A player using one of a mod item's or vehicle's interactions.

### Properties

<a name="interactevent-item"></a>

#### Item

```csharp
WorldItem Item { get; }
```

What was used: a mod's item or vehicle.

<a name="interactevent-player"></a>

#### Player

```csharp
Player Player { get; }
```

Who used it. On the client, the local player.

<a name="interactevent-interactionid"></a>

#### InteractionId

```csharp
string InteractionId { get; }
```

The interaction's id from the item's spec, e.g. "honk".

<a name="interactevent-haspoint"></a>

#### HasPoint

```csharp
bool HasPoint { get; }
```

Whether [`InteractEvent.Point`](ModEvents.md#interactevent-point) and [`InteractEvent.Normal`](ModEvents.md#interactevent-normal) are known: where the player's crosshair met the item. The server checks it's on the item and in their reach.

<a name="interactevent-point"></a>

#### Point

```csharp
Vector3 Point { get; }
```

Where the player's crosshair met the item, in the world.

<a name="interactevent-normal"></a>

#### Normal

```csharp
Vector3 Normal { get; }
```

The surface's normal at [`InteractEvent.Point`](ModEvents.md#interactevent-point).

<a name="interactevent-node"></a>

#### Node

```csharp
string Node { get; }
```

The model node the player aimed at: one of the interaction's [`InteractionSpec.Nodes`](ModItems.md#interactionspec-nodes) when it names any, else the name of the mesh part under the crosshair. Null if unknown.

<a name="helditemuseevent"></a>

## HeldItemUseEvent

*class*

A click with a carried item, for [`ModEvents.HeldItemUsed`](ModEvents.md#modevents-helditemused).

### Properties

<a name="helditemuseevent-item"></a>

#### Item

```csharp
WorldItem Item { get; }
```

The item in their hands.

<a name="helditemuseevent-player"></a>

#### Player

```csharp
Player Player { get; }
```

Who clicked. On a client, the local player.

<a name="helditemuseevent-alt"></a>

#### Alt

```csharp
bool Alt { get; }
```

True for the right mouse button.

<a name="helditemuseevent-aimorigin"></a>

#### AimOrigin

```csharp
Vector3 AimOrigin { get; }
```

Their eyes as they clicked.

<a name="helditemuseevent-aimdirection"></a>

#### AimDirection

```csharp
Vector3 AimDirection { get; }
```

Which way they looked, up and down included.

<a name="helditemuseevent-hit"></a>

#### Hit

```csharp
WorldHit Hit { get; }
```

What their crosshair was on, up to 10 m away (seeing through the carried item and the player), with its point, normal and [`WorldHit.Node`](WorldTree.md#worldhit-node).

<a name="seatevent"></a>

## SeatEvent

*struct*

Someone getting into or out of a seat, for [`ModEvents.SeatChanged`](ModEvents.md#modevents-seatchanged).

### Properties

<a name="seatevent-vehicle"></a>

#### Vehicle

```csharp
WorldItem Vehicle { get; }
```

The vehicle.

<a name="seatevent-seat"></a>

#### Seat

```csharp
int Seat { get; }
```

Which seat: 0 is the driver's, then in the order the spec lists them.

<a name="seatevent-player"></a>

#### Player

```csharp
Player Player { get; }
```

Who got in or out.

<a name="seatevent-entered"></a>

#### Entered

```csharp
bool Entered { get; }
```

True for getting in, false for getting out.

<a name="seatevent-isdriver"></a>

#### IsDriver

```csharp
bool IsDriver { get; }
```

Whether it's the driver's seat.

<a name="treefelledevent"></a>

## TreeFelledEvent

*struct*

A tree coming down, for [`ModEvents.TreeFelled`](ModEvents.md#modevents-treefelled).

### Properties

<a name="treefelledevent-species"></a>

#### Species

```csharp
string Species { get; }
```

The tree's species, e.g. "Oak".

<a name="treefelledevent-position"></a>

#### Position

```csharp
Vector3 Position { get; }
```

Where the tree stood.
