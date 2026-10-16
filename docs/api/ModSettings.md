# ModSettings

[All docs](../index.md)

<a name="modsettings"></a>

## ModSettings

*class*

The player's own options for a mod, shown in the game's Settings screen under "Mods" and saved with the game's settings. Add them in [`Mod.OnLoad()`](Mod.md#mod-onload), then read them whenever they're needed:

```csharp
Settings.AddToggle("show_counter", "Show Tree Counter", true, "Shows how many trees have fallen.");
Settings.Changed += id => counter.Visible = Settings.GetBool("show_counter");
```

Settings belong to each player's copy of the game: on the server they're the host's.

### Events

<a name="modsettings-changed"></a>

#### Changed

```csharp
event Action<string> Changed
```

One of this mod's settings changed (its id). The player applied new settings.

### Methods

<a name="modsettings-addtoggle"></a>

#### AddToggle

```csharp
void AddToggle(string id, string label, bool defaultValue, string description = null)
```

<a name="modsettings-addslider"></a>

#### AddSlider

```csharp
void AddSlider(string id, string label, float min, float max, float defaultValue, float step = 0, string description = null)
```

A slider from `min` to `max`, moving in `step`s (0 for smooth).

<a name="modsettings-addchoice"></a>

#### AddChoice

```csharp
void AddChoice(string id, string label, string[] options, int defaultIndex = 0, string description = null)
```

Picks one of `options`; read it as an index with [`ModSettings.GetChoice()`](ModSettings.md#modsettings-getchoice).

<a name="modsettings-getbool"></a>

#### GetBool

```csharp
bool GetBool(string id)
```

<a name="modsettings-getfloat"></a>

#### GetFloat

```csharp
float GetFloat(string id)
```

<a name="modsettings-getchoice"></a>

#### GetChoice

```csharp
int GetChoice(string id)
```

The chosen option's index.

<a name="modsettings-getchoicetext"></a>

#### GetChoiceText

```csharp
string GetChoiceText(string id)
```

The chosen option's text.
