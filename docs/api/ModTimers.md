# ModTimers

[All docs](../index.md) · In this page: [`ModTimers`](#modtimers) · [`ModTimer`](#modtimer)

<a name="modtimers"></a>

## ModTimers

*class*

Runs code later, once or repeatedly, on the main thread.

### Methods

<a name="modtimers-after"></a>

#### After

```csharp
ModTimer After(float seconds, Action action)
```

Runs `action` once, after a delay.

<a name="modtimers-every"></a>

#### Every

```csharp
ModTimer Every(float seconds, Action action)
```

Runs `action` every `seconds` until cancelled.

<a name="modtimer"></a>

## ModTimer

*class*

A timer from [`ModTimers`](ModTimers.md#modtimers); cancel it to stop it.

### Properties

<a name="modtimer-iscancelled"></a>

#### IsCancelled

```csharp
bool IsCancelled { get; }
```

True once [`ModTimer.Cancel()`](ModTimers.md#modtimer-cancel) has been called.

### Methods

<a name="modtimer-cancel"></a>

#### Cancel

```csharp
void Cancel()
```

Stops the timer. Safe to call more than once.
