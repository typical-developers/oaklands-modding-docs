# ModNetwork

[All docs](../index.md) · In this page: [`ModNetwork`](#modnetwork) · [`NetMessage`](#netmessage)

<a name="modnetwork"></a>

## ModNetwork

*class*

Messages between this mod's code on the server and on clients. Each message has a name, some [`NetData`](NetData.md#netdata), and optionally the item it's about. Messages only reach the same mod on the other side, which must be installed there too.

```csharp
// Client: ask the server.
Network.SendToServer("ring", new NetData().Set("volume", 0.5f), bell);
// Server: answer everyone.
Network.On("ring", message => Network.SendToAll("rang", message.Data, message.Item));
```

Clients can send a burst of messages, then about 20 a second; the server drops anything beyond that.

### Methods

<a name="modnetwork-on"></a>

#### On

```csharp
void On(string name, Action<NetMessage> handler)
```

Handles messages called `name` arriving on this side: from clients when this is the server, from the server when this is a client. On the host both happen.

<a name="modnetwork-sendtoserver"></a>

#### SendToServer

```csharp
void SendToServer(string name, NetData data = null, WorldItem about = default(WorldItem))
```

Client to server.

<a name="modnetwork-sendto"></a>

#### SendTo

```csharp
void SendTo(Player player, string name, NetData data = null, WorldItem about = default(WorldItem))
```

Server to one client.

<a name="modnetwork-sendtoall"></a>

#### SendToAll

```csharp
void SendToAll(string name, NetData data = null, WorldItem about = default(WorldItem))
```

Server to every client.

<a name="netmessage"></a>

## NetMessage

*class*

A message from this mod's code on another peer, for [`ModNetwork.On()`](ModNetwork.md#modnetwork-on).

### Properties

<a name="netmessage-name"></a>

#### Name

```csharp
string Name { get; }
```

What it's called, as given to the send method.

<a name="netmessage-data"></a>

#### Data

```csharp
NetData Data { get; }
```

The values sent with it.

<a name="netmessage-sender"></a>

#### Sender

```csharp
Player Sender { get; }
```

Who sent it, on the server. On clients messages come from the server, so this isn't valid.

<a name="netmessage-item"></a>

#### Item

```csharp
WorldItem Item { get; }
```

The item it's about, if any (check [`WorldItem.IsValid`](ModWorld.md#worlditem-isvalid)).
