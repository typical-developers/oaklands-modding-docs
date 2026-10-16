# Mod

[All docs](../index.md)

<a name="mod"></a>

## Mod

*abstract class*

The entry point of a scripted mod. A mod's scripts must contain exactly one public, non-abstract class deriving from this. The game creates it when the mod loads and calls [`Mod.OnLoad()`](Mod.md#mod-onload); when the mod is reloaded or turned off, [`Mod.OnUnload()`](Mod.md#mod-onunload) runs and everything the mod registered (commands, items, timers, event handlers) is removed.

```csharp
public sealed class HelloMod : Mod
{
    public override void OnLoad()
    {
        Commands.Register("hello", "Says hello.", call => call.Reply("Hi " + call.Player.Name));
    }
}
```

### Properties

<a name="mod-context"></a>

#### Context

```csharp
ModContext Context { get; }
```

This mod's view of the game.

<a name="mod-log"></a>

#### Log

```csharp
protected ModLog Log { get; }
```

Shortcut for [`ModContext.Log`](ModContext.md#modcontext-log).

<a name="mod-events"></a>

#### Events

```csharp
protected ModEvents Events { get; }
```

Shortcut for [`ModContext.Events`](ModContext.md#modcontext-events).

<a name="mod-commands"></a>

#### Commands

```csharp
protected ModCommands Commands { get; }
```

Shortcut for [`ModContext.Commands`](ModContext.md#modcontext-commands).

<a name="mod-items"></a>

#### Items

```csharp
protected ModItems Items { get; }
```

Shortcut for [`ModContext.Items`](ModContext.md#modcontext-items).

<a name="mod-vehicles"></a>

#### Vehicles

```csharp
protected ModVehicles Vehicles { get; }
```

Shortcut for [`ModContext.Vehicles`](ModContext.md#modcontext-vehicles).

<a name="mod-trees"></a>

#### Trees

```csharp
protected ModTrees Trees { get; }
```

Shortcut for [`ModContext.Trees`](ModContext.md#modcontext-trees).

<a name="mod-world"></a>

#### World

```csharp
protected ModWorld World { get; }
```

Shortcut for [`ModContext.World`](ModContext.md#modcontext-world).

<a name="mod-chat"></a>

#### Chat

```csharp
protected ModChat Chat { get; }
```

Shortcut for [`ModContext.Chat`](ModContext.md#modcontext-chat).

<a name="mod-timers"></a>

#### Timers

```csharp
protected ModTimers Timers { get; }
```

Shortcut for [`ModContext.Timers`](ModContext.md#modcontext-timers).

<a name="mod-network"></a>

#### Network

```csharp
protected ModNetwork Network { get; }
```

Shortcut for [`ModContext.Network`](ModContext.md#modcontext-network).

<a name="mod-effects"></a>

#### Effects

```csharp
protected ModEffects Effects { get; }
```

Shortcut for [`ModContext.Effects`](ModContext.md#modcontext-effects).

<a name="mod-ui"></a>

#### UI

```csharp
protected ModUI UI { get; }
```

Shortcut for [`ModContext.UI`](ModContext.md#modcontext-ui).

<a name="mod-input"></a>

#### Input

```csharp
protected ModInput Input { get; }
```

Shortcut for [`ModContext.Input`](ModContext.md#modcontext-input).

<a name="mod-settings"></a>

#### Settings

```csharp
protected ModSettings Settings { get; }
```

Shortcut for [`ModContext.Settings`](ModContext.md#modcontext-settings).

### Methods

<a name="mod-onload"></a>

#### OnLoad

```csharp
virtual void OnLoad()
```

Runs once when the mod loads, on the server and on every client. Register commands, items and event handlers here.

<a name="mod-onunload"></a>

#### OnUnload

```csharp
virtual void OnUnload()
```

Runs before the mod is unloaded or reloaded. Anything registered through [`Mod.Context`](Mod.md#mod-context) is cleaned up for you.
