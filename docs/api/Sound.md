# Sound

[All docs](../index.md)

<a name="sound"></a>

## Sound

*struct* · implements `IEquatable<Sound>`

A sound a mod made with [`ModEffects.CreateSound()`](ModEffects.md#modeffects-createsound) or [`ModEffects.CreateSoundForAll()`](ModEffects.md#modeffects-createsoundforall), which it can start, stop and change while it plays, e.g. an engine whose pitch follows its speed. Becomes invalid once destroyed; reading an invalid sound gives defaults and changing it does nothing.

### Properties

<a name="sound-isvalid"></a>

#### IsValid

```csharp
bool IsValid { get; }
```

False once the sound has been destroyed (or the mod unloaded).

<a name="sound-isforall"></a>

#### IsForAll

```csharp
bool IsForAll { get; }
```

Whether every player hears it (made with [`ModEffects.CreateSoundForAll()`](ModEffects.md#modeffects-createsoundforall)): changes made on the server reach everyone, including players who join later.

<a name="sound-path"></a>

#### Path

```csharp
string Path { get; }
```

The file it plays, as the mod named it, e.g. "sounds/engine.ogg".

<a name="sound-volume"></a>

#### Volume

```csharp
float Volume { get; set; }
```

From 0 (silent) to 1. Default 1.

<a name="sound-pitch"></a>

#### Pitch

```csharp
float Pitch { get; set; }
```

Playback speed, which also changes how high it sounds: 2 is twice as fast and an octave up. From 0.1 to 3. Default 1.

<a name="sound-loop"></a>

#### Loop

```csharp
bool Loop { get; set; }
```

Whether it starts again from the beginning when it reaches the end. Default false.

<a name="sound-is3d"></a>

#### Is3D

```csharp
bool Is3D { get; set; }
```

Whether it comes from a point in the world, getting quieter with distance (true, the default), or plays at the same volume wherever players are, like music (false).

<a name="sound-mindistance"></a>

#### MinDistance

```csharp
float MinDistance { get; set; }
```

Metres within which a 3D sound is at full volume. Default 2.

<a name="sound-maxdistance"></a>

#### MaxDistance

```csharp
float MaxDistance { get; set; }
```

Metres beyond which a 3D sound can't be heard. Default 60, at most 500.

<a name="sound-position"></a>

#### Position

```csharp
Vector3 Position { get; set; }
```

Where a 3D sound comes from. While attached (see [`Sound.AttachTo()`](Sound.md#sound-attachto)) this follows what it's attached to, and setting it detaches it.

<a name="sound-time"></a>

#### Time

```csharp
float Time { get; set; }
```

Seconds into the file it's at. Setting it jumps there, whether it's playing, paused or stopped.

<a name="sound-length"></a>

#### Length

```csharp
float Length { get; }
```

The file's length in seconds, at normal pitch; 0 if it couldn't be read.

<a name="sound-isplaying"></a>

#### IsPlaying

```csharp
bool IsPlaying { get; }
```

Whether it's playing (not paused or stopped). A sound started before its file has loaded counts as playing, and is heard from the right point once it has.

<a name="sound-ispaused"></a>

#### IsPaused

```csharp
bool IsPaused { get; }
```

Whether [`Sound.Pause()`](Sound.md#sound-pause) stopped it where it was.

<a name="sound-destroywhendone"></a>

#### DestroyWhenDone

```csharp
bool DestroyWhenDone { get; set; }
```

Destroys the sound once it finishes playing (not when it's stopped or paused), for one-off sounds the mod doesn't want to keep track of. Default false.

### Methods

<a name="sound-play"></a>

#### Play

```csharp
void Play()
```

Starts it from the beginning (or from where [`Sound.Time`](Sound.md#sound-time) was set while stopped).

<a name="sound-pause"></a>

#### Pause

```csharp
void Pause()
```

Stops it where it is, to carry on with [`Sound.Resume()`](Sound.md#sound-resume).

<a name="sound-resume"></a>

#### Resume

```csharp
void Resume()
```

Carries on after [`Sound.Pause()`](Sound.md#sound-pause). Starts it if it was stopped.

<a name="sound-stop"></a>

#### Stop

```csharp
void Stop(float fadeSeconds = 0)
```

Stops it and goes back to the beginning, fading out over `fadeSeconds` first if given. The volume is put back once it has stopped, ready to play again.

<a name="sound-fadeto"></a>

#### FadeTo

```csharp
void FadeTo(float volume, float seconds)
```

Changes [`Sound.Volume`](Sound.md#sound-volume) smoothly to `volume` over `seconds`. Reading Volume during a fade gives where it is now.

<a name="sound-attachto"></a>

#### AttachTo

```csharp
bool AttachTo(WorldItem item, Vector3 offset = default(Vector3))
```

Makes the sound follow an item or vehicle, `offset` metres from its centre in its own directions (so (0, 0, 1) is a metre in front of it). The sound is destroyed if the item goes away. False if the item isn't here.

#### AttachTo

```csharp
bool AttachTo(Player player, Vector3 offset = default(Vector3))
```

Makes the sound follow a player, `offset` metres from their feet in their own directions. The sound is destroyed if they leave. On a client only [`ModContext.LocalPlayer`](ModContext.md#modcontext-localplayer) can be used. False if the player isn't here.

<a name="sound-detach"></a>

#### Detach

```csharp
void Detach()
```

Stops following what it's attached to, staying where it is now.

<a name="sound-destroy"></a>

#### Destroy

```csharp
void Destroy()
```

Stops the sound and frees it. Safe to call more than once.

### Operators

<a name="sound-op_equality"></a>

#### operator ==

```csharp
static bool operator ==(Sound a, Sound b)
```

<a name="sound-op_inequality"></a>

#### operator !=

```csharp
static bool operator !=(Sound a, Sound b)
```
