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
| <a name="modkey-none"></a>`None` |  |
| <a name="modkey-a"></a>`A` |  |
| <a name="modkey-b"></a>`B` |  |
| <a name="modkey-c"></a>`C` |  |
| <a name="modkey-d"></a>`D` |  |
| <a name="modkey-e"></a>`E` |  |
| <a name="modkey-f"></a>`F` |  |
| <a name="modkey-g"></a>`G` |  |
| <a name="modkey-h"></a>`H` |  |
| <a name="modkey-i"></a>`I` |  |
| <a name="modkey-j"></a>`J` |  |
| <a name="modkey-k"></a>`K` |  |
| <a name="modkey-l"></a>`L` |  |
| <a name="modkey-m"></a>`M` |  |
| <a name="modkey-n"></a>`N` |  |
| <a name="modkey-o"></a>`O` |  |
| <a name="modkey-p"></a>`P` |  |
| <a name="modkey-q"></a>`Q` |  |
| <a name="modkey-r"></a>`R` |  |
| <a name="modkey-s"></a>`S` |  |
| <a name="modkey-t"></a>`T` |  |
| <a name="modkey-u"></a>`U` |  |
| <a name="modkey-v"></a>`V` |  |
| <a name="modkey-w"></a>`W` |  |
| <a name="modkey-x"></a>`X` |  |
| <a name="modkey-y"></a>`Y` |  |
| <a name="modkey-z"></a>`Z` |  |
| <a name="modkey-digit0"></a>`Digit0` |  |
| <a name="modkey-digit1"></a>`Digit1` |  |
| <a name="modkey-digit2"></a>`Digit2` |  |
| <a name="modkey-digit3"></a>`Digit3` |  |
| <a name="modkey-digit4"></a>`Digit4` |  |
| <a name="modkey-digit5"></a>`Digit5` |  |
| <a name="modkey-digit6"></a>`Digit6` |  |
| <a name="modkey-digit7"></a>`Digit7` |  |
| <a name="modkey-digit8"></a>`Digit8` |  |
| <a name="modkey-digit9"></a>`Digit9` |  |
| <a name="modkey-f1"></a>`F1` |  |
| <a name="modkey-f2"></a>`F2` |  |
| <a name="modkey-f3"></a>`F3` |  |
| <a name="modkey-f4"></a>`F4` |  |
| <a name="modkey-f5"></a>`F5` |  |
| <a name="modkey-f6"></a>`F6` |  |
| <a name="modkey-f7"></a>`F7` |  |
| <a name="modkey-f8"></a>`F8` |  |
| <a name="modkey-f9"></a>`F9` |  |
| <a name="modkey-f10"></a>`F10` |  |
| <a name="modkey-f11"></a>`F11` |  |
| <a name="modkey-f12"></a>`F12` |  |
| <a name="modkey-space"></a>`Space` |  |
| <a name="modkey-enter"></a>`Enter` |  |
| <a name="modkey-tab"></a>`Tab` |  |
| <a name="modkey-backspace"></a>`Backspace` |  |
| <a name="modkey-escape"></a>`Escape` |  |
| <a name="modkey-up"></a>`Up` |  |
| <a name="modkey-down"></a>`Down` |  |
| <a name="modkey-left"></a>`Left` |  |
| <a name="modkey-right"></a>`Right` |  |
| <a name="modkey-leftshift"></a>`LeftShift` |  |
| <a name="modkey-rightshift"></a>`RightShift` |  |
| <a name="modkey-leftctrl"></a>`LeftCtrl` |  |
| <a name="modkey-rightctrl"></a>`RightCtrl` |  |
| <a name="modkey-leftalt"></a>`LeftAlt` |  |
| <a name="modkey-rightalt"></a>`RightAlt` |  |
| <a name="modkey-minus"></a>`Minus` |  |
| <a name="modkey-equals"></a>`Equals` |  |
| <a name="modkey-leftbracket"></a>`LeftBracket` |  |
| <a name="modkey-rightbracket"></a>`RightBracket` |  |
| <a name="modkey-semicolon"></a>`Semicolon` |  |
| <a name="modkey-quote"></a>`Quote` |  |
| <a name="modkey-comma"></a>`Comma` |  |
| <a name="modkey-period"></a>`Period` |  |
| <a name="modkey-slash"></a>`Slash` |  |
| <a name="modkey-backslash"></a>`Backslash` |  |
| <a name="modkey-backquote"></a>`Backquote` |  |
| <a name="modkey-insert"></a>`Insert` |  |
| <a name="modkey-delete"></a>`Delete` |  |
| <a name="modkey-home"></a>`Home` |  |
| <a name="modkey-end"></a>`End` |  |
| <a name="modkey-pageup"></a>`PageUp` |  |
| <a name="modkey-pagedown"></a>`PageDown` |  |
| <a name="modkey-leftmouse"></a>`LeftMouse` |  |
| <a name="modkey-rightmouse"></a>`RightMouse` |  |
| <a name="modkey-middlemouse"></a>`MiddleMouse` |  |
