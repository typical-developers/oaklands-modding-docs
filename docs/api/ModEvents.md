# ModEvents

[All docs](../index.md) · In this page: [`ModEvents`](#modevents) · [`ChatMessageEvent`](#chatmessageevent) · [`InteractEvent`](#interactevent) · [`TreeFelledEvent`](#treefelledevent)

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
