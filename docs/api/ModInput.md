# ModInput

[All docs](../index.md) · In this page: [`ModInput`](#modinput) · [`ModKey`](#modkey)

<a name="modinput"></a>

## ModInput

*class*

The keyboard and mouse buttons, on clients. Keys read as up while the player is typing (in chat or a text field), so a mod's shortcut doesn't fire mid-sentence. Read them every frame from [`ModEvents.Tick`](ModEvents.md#modevents-tick).

### Methods

<a name="modinput-isdown"></a>

#### IsDown

```csharp
bool IsDown(ModKey key)
```

Held down right now.

<a name="modinput-waspressed"></a>

#### WasPressed

```csharp
bool WasPressed(ModKey key)
```

Went down this frame.

<a name="modinput-wasreleased"></a>

#### WasReleased

```csharp
bool WasReleased(ModKey key)
```

Came up this frame.

<a name="modkey"></a>

## ModKey

*enum*

A key or mouse button, for [`ModInput`](ModInput.md#modinput).

| Value | Description |
| --- | --- |
| `None` |  |
| `A` |  |
| `B` |  |
| `C` |  |
| `D` |  |
| `E` |  |
| `F` |  |
| `G` |  |
| `H` |  |
| `I` |  |
| `J` |  |
| `K` |  |
| `L` |  |
| `M` |  |
| `N` |  |
| `O` |  |
| `P` |  |
| `Q` |  |
| `R` |  |
| `S` |  |
| `T` |  |
| `U` |  |
| `V` |  |
| `W` |  |
| `X` |  |
| `Y` |  |
| `Z` |  |
| `Digit0` |  |
| `Digit1` |  |
| `Digit2` |  |
| `Digit3` |  |
| `Digit4` |  |
| `Digit5` |  |
| `Digit6` |  |
| `Digit7` |  |
| `Digit8` |  |
| `Digit9` |  |
| `F1` |  |
| `F2` |  |
| `F3` |  |
| `F4` |  |
| `F5` |  |
| `F6` |  |
| `F7` |  |
| `F8` |  |
| `F9` |  |
| `F10` |  |
| `F11` |  |
| `F12` |  |
| `Space` |  |
| `Enter` |  |
| `Tab` |  |
| `Backspace` |  |
| `Escape` |  |
| `Up` |  |
| `Down` |  |
| `Left` |  |
| `Right` |  |
| `LeftShift` |  |
| `RightShift` |  |
| `LeftCtrl` |  |
| `RightCtrl` |  |
| `LeftAlt` |  |
| `RightAlt` |  |
| `Minus` |  |
| `Equals` |  |
| `LeftBracket` |  |
| `RightBracket` |  |
| `Semicolon` |  |
| `Quote` |  |
| `Comma` |  |
| `Period` |  |
| `Slash` |  |
| `Backslash` |  |
| `Backquote` |  |
| `Insert` |  |
| `Delete` |  |
| `Home` |  |
| `End` |  |
| `PageUp` |  |
| `PageDown` |  |
| `LeftMouse` |  |
| `RightMouse` |  |
| `MiddleMouse` |  |
