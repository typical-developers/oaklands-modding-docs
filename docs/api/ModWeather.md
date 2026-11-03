# ModWeather

[All docs](../index.md) · In this page: [`WeatherStage`](#weatherstage) · [`WeatherInfo`](#weatherinfo) · [`ModWeather`](#modweather)

<a name="weatherstage"></a>

## WeatherStage

*enum*

How far along a shower or storm is.

| Value | Description |
| --- | --- |
| <a name="weatherstage-forming"></a>`Forming` | Clouds building up; little or no rain yet. |
| <a name="weatherstage-mature"></a>`Mature` | Raining hardest (and, for storms, the most lightning). |
| <a name="weatherstage-fading"></a>`Fading` | Dying away, or clearing after being stopped. |

<a name="weatherinfo"></a>

## WeatherInfo

*struct*

One shower, storm or fog bank going on in the world.

### Properties

<a name="weatherinfo-id"></a>

#### Id

```csharp
int Id { get; }
```

Identifies it while the world runs (for [`ModWeather.Stop()`](ModWeather.md#modweather-stop)).

<a name="weatherinfo-type"></a>

#### Type

```csharp
string Type { get; }
```

Its kind, one of [`ModWeather.Types`](ModWeather.md#modweather-types), e.g. "thunderstorm".

<a name="weatherinfo-position"></a>

#### Position

```csharp
Vector3 Position { get; }
```

Its center on the ground (y is 0).

<a name="weatherinfo-velocity"></a>

#### Velocity

```csharp
Vector3 Velocity { get; }
```

How it drifts, in meters per second (y is 0).

<a name="weatherinfo-radius"></a>

#### Radius

```csharp
float Radius { get; }
```

Meters across its clouds, from the center.

<a name="weatherinfo-stage"></a>

#### Stage

```csharp
WeatherStage Stage { get; }
```

How far along it is.

<a name="weatherinfo-life"></a>

#### Life

```csharp
float Life { get; }
```

0 when it starts forming up to 1 when it's gone.

<a name="weatherinfo-intensity"></a>

#### Intensity

```csharp
float Intensity { get; }
```

1 is normal; more is heavier rain and more lightning.

<a name="weatherinfo-natural"></a>

#### Natural

```csharp
bool Natural { get; }
```

Whether it formed on its own rather than being started.

<a name="weatherinfo-stopping"></a>

#### Stopping

```csharp
bool Stopping { get; }
```

Whether it was stopped and is clearing away.

<a name="modweather"></a>

## ModWeather

*class*

The world's weather: showers, rain, thunderstorms and fog that form, drift with the wind and die away, the same for every player. Reading works everywhere; changing the weather is server only.

```csharp
int storm = Weather.Start("thunderstorm", player.Position);
Timers.After(300f, () => Weather.Stop(storm));
```

### Properties

<a name="modweather-types"></a>

#### Types

```csharp
List<string> Types { get; }
```

The kinds of weather there are, e.g. "shower", "rain", "thunderstorm", "fog".

<a name="modweather-current"></a>

#### Current

```csharp
List<WeatherInfo> Current { get; }
```

The showers, storms and fog banks going on now.

<a name="modweather-natural"></a>

#### Natural

```csharp
bool Natural { get; set; }
```

Whether weather forms on its own every so often. Saved with the world. Server only to set.

<a name="modweather-wind"></a>

#### Wind

```csharp
Vector3 Wind { get; }
```

The wind clouds and storms drift on, in meters per second (y is 0).

### Methods

<a name="modweather-rainat"></a>

#### RainAt

```csharp
float RainAt(Vector3 position)
```

0 (dry) to 1 (pouring), how hard it's raining at a spot. Shelter isn't counted.

<a name="modweather-start"></a>

#### Start

```csharp
int Start(string type, Vector3 position, float intensity = 1, bool overhead = true, bool moving = true)
```

Starts weather of a kind (see [`ModWeather.Types`](ModWeather.md#modweather-types)) and returns its [`WeatherInfo.Id`](ModWeather.md#weatherinfo-id). With `overhead` it rolls in over `position` within a minute, already grown; otherwise it forms upwind and drifts over the spot, the way weather does on its own. With `moving` false it stays where it is. `intensity` runs from 0.1 to 2 (1 is normal). Server only. A world has room for a few at once; starting one more clears the oldest.

<a name="modweather-stop"></a>

#### Stop

```csharp
bool Stop(int id)
```

Starts one clearing away (it takes about a minute). False if there's none with that id. Server only.

<a name="modweather-stopall"></a>

#### StopAll

```csharp
int StopAll()
```

Clears all weather away and returns how many were stopped. Server only.

<a name="modweather-strike"></a>

#### Strike

```csharp
void Strike(Vector3 position)
```

A lightning bolt down to a spot, seen and heard by everyone. It's only a sight and a sound: it doesn't hurt anything. Server only.
