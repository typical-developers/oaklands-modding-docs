# ModUI

[All docs](../index.md) · In this page: [`ModUI`](#modui) · [`UIElement`](#uielement) · [`UIBox`](#uibox) · [`UIPanel`](#uipanel) · [`UILabel`](#uilabel) · [`UIButton`](#uibutton) · [`UITextField`](#uitextfield) · [`UIToggle`](#uitoggle) · [`UISlider`](#uislider) · [`UIImage`](#uiimage) · [`UIScroll`](#uiscroll) · [`UIBar`](#uibar) · [`UIAnchor`](#uianchor) · [`UIAlign`](#uialign) · [`UIStyle`](#uistyle)

<a name="modui"></a>

## ModUI

*class*

On-screen UI for mods, built in code. Make a [`UIPanel`](ModUI.md#uipanel), add elements to it, and style them with the methods in [`UIStyle`](ModUI.md#uistyle); elements can also use the game's own style classes with [`UIElement.AddClass()`](ModUI.md#uielement-addclass) (e.g. "form-button").

```csharp
var panel = UI.Panel().Anchor(UIAnchor.TopRight, 16, 16).Padding(12).Background(new Color(0, 0, 0, 0.6f));
var count = UI.Label("Trees: 0").FontSize(18);
panel.Add(count);
Events.TreeFelled += _ => count.Text = "Trees: " + ++felled;
```

UI only shows on clients (see [`ModContext.IsClient`](ModContext.md#modcontext-isclient), or the main menu). Everything a mod made is removed when it unloads.

### Properties

<a name="modui-istyping"></a>

#### IsTyping

```csharp
bool IsTyping { get; }
```

Whether the player is typing (in chat or a text field), so keys shouldn't trigger anything.

### Methods

<a name="modui-panel"></a>

#### Panel

```csharp
UIPanel Panel(string name = null)
```

A new top-level panel. Unless placed with [`UIStyle.Anchor()`](ModUI.md#uistyle-anchor), it sits in the top-left corner.

<a name="modui-box"></a>

#### Box

```csharp
UIBox Box()
```

<a name="modui-label"></a>

#### Label

```csharp
UILabel Label(string text = "")
```

<a name="modui-button"></a>

#### Button

```csharp
UIButton Button(string text = "", Action onClick = null)
```

<a name="modui-textfield"></a>

#### TextField

```csharp
UITextField TextField(string placeholder = null)
```

<a name="modui-toggle"></a>

#### Toggle

```csharp
UIToggle Toggle(string label = "", bool value = false)
```

<a name="modui-slider"></a>

#### Slider

```csharp
UISlider Slider(float min = 0, float max = 1, float value = 0)
```

<a name="modui-image"></a>

#### Image

```csharp
UIImage Image(string path = null)
```

<a name="modui-scroll"></a>

#### Scroll

```csharp
UIScroll Scroll()
```

<a name="modui-bar"></a>

#### Bar

```csharp
UIBar Bar(float value = 0)
```

<a name="uielement"></a>

## UIElement

*abstract class*

Anything on screen. Style it with the chainable methods in [`UIStyle`](ModUI.md#uistyle).

### Properties

<a name="uielement-name"></a>

#### Name

```csharp
string Name { get; set; }
```

<a name="uielement-visible"></a>

#### Visible

```csharp
bool Visible { get; set; }
```

### Methods

<a name="uielement-add"></a>

#### Add

```csharp
UIElement Add(UIElement child)
```

Adds a child element. Returns this element, for chaining.

<a name="uielement-remove"></a>

#### Remove

```csharp
void Remove()
```

Takes this element off the screen. It can be added again.

<a name="uielement-clear"></a>

#### Clear

```csharp
void Clear()
```

Removes every child.

<a name="uielement-addclass"></a>

#### AddClass

```csharp
void AddClass(string name)
```

Adds a style class: the game's own (e.g. "form-button", "screen-title") or any name for [`UIElement.RemoveClass()`](ModUI.md#uielement-removeclass).

<a name="uielement-removeclass"></a>

#### RemoveClass

```csharp
void RemoveClass(string name)
```

<a name="uielement-hasclass"></a>

#### HasClass

```csharp
bool HasClass(string name)
```

<a name="uielement-setonclick"></a>

#### SetOnClick

```csharp
void SetOnClick(Action action)
```

Runs `action` when the element is clicked.

<a name="uibox"></a>

## UIBox

*class* · derives from [`UIElement`](ModUI.md#uielement)

A plain container. Lays its children out in a column, or a row with [`UIStyle.Row()`](ModUI.md#uistyle-row).

<a name="uipanel"></a>

## UIPanel

*class* · derives from [`UIBox`](ModUI.md#uibox)

A top-level piece of UI. Panels sit above the game's HUD and can take the mouse ([`UIPanel.CapturesMouse`](ModUI.md#uipanel-capturesmouse)) for buttons and fields.

### Properties

<a name="uipanel-capturesmouse"></a>

#### CapturesMouse

```csharp
bool CapturesMouse { get; set; }
```

While true and the panel is visible, the cursor is free and the player's controls are paused, so the panel can be clicked.

### Methods

<a name="uipanel-show"></a>

#### Show

```csharp
void Show()
```

<a name="uipanel-hide"></a>

#### Hide

```csharp
void Hide()
```

<a name="uipanel-toggle"></a>

#### Toggle

```csharp
void Toggle()
```

<a name="uipanel-destroy"></a>

#### Destroy

```csharp
void Destroy()
```

Removes the panel for good.

<a name="uilabel"></a>

## UILabel

*class* · derives from [`UIElement`](ModUI.md#uielement)

Text on screen.

### Properties

<a name="uilabel-text"></a>

#### Text

```csharp
string Text { get; set; }
```

<a name="uibutton"></a>

## UIButton

*class* · derives from [`UIElement`](ModUI.md#uielement)

A button. Its text is a label styled like the game's buttons' labels, so `.Class("form-button")` gives the game's own button look.

### Properties

<a name="uibutton-text"></a>

#### Text

```csharp
string Text { get; set; }
```

<a name="uibutton-enabled"></a>

#### Enabled

```csharp
bool Enabled { get; set; }
```

### Methods

<a name="uibutton-onclick"></a>

#### OnClick

```csharp
UIButton OnClick(Action action)
```

What a click does. Returns the button, for chaining.

<a name="uitextfield"></a>

## UITextField

*class* · derives from [`UIElement`](ModUI.md#uielement)

A box players type into.

### Properties

<a name="uitextfield-value"></a>

#### Value

```csharp
string Value { get; set; }
```

<a name="uitextfield-maxlength"></a>

#### MaxLength

```csharp
int MaxLength { get; set; }
```

### Methods

<a name="uitextfield-onchanged"></a>

#### OnChanged

```csharp
UITextField OnChanged(Action<string> action)
```

Runs on every edit, with the new text.

<a name="uitextfield-onsubmit"></a>

#### OnSubmit

```csharp
UITextField OnSubmit(Action<string> action)
```

Runs when Enter is pressed in the field.

<a name="uitextfield-focus"></a>

#### Focus

```csharp
void Focus()
```

<a name="uitoggle"></a>

## UIToggle

*class* · derives from [`UIElement`](ModUI.md#uielement)

A checkbox with a label.

### Properties

<a name="uitoggle-value"></a>

#### Value

```csharp
bool Value { get; set; }
```

### Methods

<a name="uitoggle-onchanged"></a>

#### OnChanged

```csharp
UIToggle OnChanged(Action<bool> action)
```

<a name="uislider"></a>

## UISlider

*class* · derives from [`UIElement`](ModUI.md#uielement)

A slider between two numbers.

### Properties

<a name="uislider-value"></a>

#### Value

```csharp
float Value { get; set; }
```

### Methods

<a name="uislider-onchanged"></a>

#### OnChanged

```csharp
UISlider OnChanged(Action<float> action)
```

<a name="uiimage"></a>

## UIImage

*class* · derives from [`UIElement`](ModUI.md#uielement)

A picture from the mod's folder (PNG or JPG), scaled to fit.

### Methods

<a name="uiimage-setimage"></a>

#### SetImage

```csharp
UIImage SetImage(string path)
```

Shows an image file, e.g. "ui/logo.png". Null clears it.

<a name="uiimage-tint"></a>

#### Tint

```csharp
UIImage Tint(Color color)
```

<a name="uiscroll"></a>

## UIScroll

*class* · derives from [`UIElement`](ModUI.md#uielement)

A box that scrolls when its children don't fit.

### Methods

<a name="uiscroll-scrolltoend"></a>

#### ScrollToEnd

```csharp
void ScrollToEnd()
```

<a name="uibar"></a>

## UIBar

*class* · derives from [`UIElement`](ModUI.md#uielement)

A bar that fills from left to right, e.g. for progress or health.

### Properties

<a name="uibar-value"></a>

#### Value

```csharp
float Value { get; set; }
```

From 0 (empty) to 1 (full).

### Methods

<a name="uibar-fillcolor"></a>

#### FillColor

```csharp
UIBar FillColor(Color color)
```

<a name="uianchor"></a>

## UIAnchor

*enum*

Which part of the screen a panel sits in.

| Value | Description |
| --- | --- |
| `TopLeft` |  |
| `Top` |  |
| `TopRight` |  |
| `Left` |  |
| `Center` |  |
| `Right` |  |
| `BottomLeft` |  |
| `Bottom` |  |
| `BottomRight` |  |

<a name="uialign"></a>

## UIAlign

*enum*

How a box lines up its children.

| Value | Description |
| --- | --- |
| `Start` |  |
| `Center` |  |
| `End` |  |
| `Stretch` |  |
| `SpaceBetween` |  |

<a name="uistyle"></a>

## UIStyle

*static class*

Chainable styling for any element, e.g. `UI.Label("Hi").FontSize(20).TextColor(Color.yellow).Margin(4)`. Sizes are in UI pixels.

### Methods

<a name="uistyle-size"></a>

#### Size

```csharp
static T Size<T>(this T e, float width, float height)
```

<a name="uistyle-width"></a>

#### Width

```csharp
static T Width<T>(this T e, float width)
```

<a name="uistyle-height"></a>

#### Height

```csharp
static T Height<T>(this T e, float height)
```

<a name="uistyle-widthpercent"></a>

#### WidthPercent

```csharp
static T WidthPercent<T>(this T e, float percent)
```

Width as a share of the parent, 0 to 100.

<a name="uistyle-heightpercent"></a>

#### HeightPercent

```csharp
static T HeightPercent<T>(this T e, float percent)
```

<a name="uistyle-minsize"></a>

#### MinSize

```csharp
static T MinSize<T>(this T e, float width, float height)
```

<a name="uistyle-grow"></a>

#### Grow

```csharp
static T Grow<T>(this T e, float amount = 1)
```

Takes up spare room in its row or column.

<a name="uistyle-padding"></a>

#### Padding

```csharp
static T Padding<T>(this T e, float all)
```

#### Padding

```csharp
static T Padding<T>(this T e, float vertical, float horizontal)
```

<a name="uistyle-margin"></a>

#### Margin

```csharp
static T Margin<T>(this T e, float all)
```

#### Margin

```csharp
static T Margin<T>(this T e, float vertical, float horizontal)
```

<a name="uistyle-row"></a>

#### Row

```csharp
static T Row<T>(this T e)
```

Lays children out left to right.

<a name="uistyle-column"></a>

#### Column

```csharp
static T Column<T>(this T e)
```

Lays children out top to bottom (the default).

<a name="uistyle-align"></a>

#### Align

```csharp
static T Align<T>(this T e, UIAlign along, UIAlign across = Stretch)
```

Where children sit: `along` the row or column, and `across` it.

<a name="uistyle-anchor"></a>

#### Anchor

```csharp
static T Anchor<T>(this T e, UIAnchor anchor, float x = 0, float y = 0)
```

Pins the element to a corner, edge or the middle of its parent (the screen, for panels), `x` and `y` pixels in from it.

<a name="uistyle-background"></a>

#### Background

```csharp
static T Background<T>(this T e, Color color)
```

<a name="uistyle-border"></a>

#### Border

```csharp
static T Border<T>(this T e, Color color, float width = 1)
```

<a name="uistyle-rounded"></a>

#### Rounded

```csharp
static T Rounded<T>(this T e, float radius)
```

<a name="uistyle-opacity"></a>

#### Opacity

```csharp
static T Opacity<T>(this T e, float opacity)
```

<a name="uistyle-fontsize"></a>

#### FontSize

```csharp
static T FontSize<T>(this T e, float size)
```

<a name="uistyle-textcolor"></a>

#### TextColor

```csharp
static T TextColor<T>(this T e, Color color)
```

<a name="uistyle-bold"></a>

#### Bold

```csharp
static T Bold<T>(this T e, bool bold = true)
```

<a name="uistyle-textalign"></a>

#### TextAlign

```csharp
static T TextAlign<T>(this T e, UIAlign align)
```

<a name="uistyle-wrap"></a>

#### Wrap

```csharp
static T Wrap<T>(this T e, bool wrap = true)
```

Lets the text wrap onto more lines instead of running on.

<a name="uistyle-class"></a>

#### Class

```csharp
static T Class<T>(this T e, string name)
```

Adds one of the game's style classes; see [`UIElement.AddClass()`](ModUI.md#uielement-addclass).

<a name="uistyle-clicked"></a>

#### Clicked

```csharp
static T Clicked<T>(this T e, Action action)
```

Runs `action` when the element is clicked.

<a name="uistyle-with"></a>

#### With

```csharp
static T With<T>(this T e, params UIElement[] children)
```

Adds children, returning this element.
