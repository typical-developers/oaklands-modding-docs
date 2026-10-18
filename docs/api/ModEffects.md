# ModEffects

[All docs](../index.md)

<a name="modeffects"></a>

## ModEffects

*class*

Sounds and other things players see and hear.

### Methods

<a name="modeffects-playsound"></a>

#### PlaySound

```csharp
void PlaySound(string path, Vector3 position, float volume = 1, float pitch = 1)
```

Plays a sound file from the mod's folder (.ogg or .wav, e.g. "sounds/honk.ogg") at a point in the world, for this player only. Does nothing on a server with no player.

<a name="modeffects-playsoundforall"></a>

#### PlaySoundForAll

```csharp
void PlaySoundForAll(string path, Vector3 position, float volume = 1, float pitch = 1)
```

Plays a sound for every player. Server only.

<a name="modeffects-createsound"></a>

#### CreateSound

```csharp
Sound CreateSound(string path)
```

Makes a sound from a file in the mod's folder (.ogg or .wav) that this player hears, to start, stop and change as it plays. It's silent until [`Sound.Play()`](Sound.md#sound-play). Keep it and reuse it rather than making a new one each time, and destroy it when done (or set [`Sound.DestroyWhenDone`](Sound.md#sound-destroywhendone)): a mod can have at most 64. Does nothing (gives an invalid sound) on a server with no player.

<a name="modeffects-createsoundforall"></a>

#### CreateSoundForAll

```csharp
Sound CreateSoundForAll(string path)
```

Like [`ModEffects.CreateSound()`](ModEffects.md#modeffects-createsound), but every player hears it: whatever the server does with it (playing, volume, attaching...) reaches everyone, and players who join later hear it from where it's at. Server only.

<a name="modeffects-destroyallsounds"></a>

#### DestroyAllSounds

```csharp
void DestroyAllSounds()
```

Destroys every sound this mod has made with [`ModEffects.CreateSound()`](ModEffects.md#modeffects-createsound) or [`ModEffects.CreateSoundForAll()`](ModEffects.md#modeffects-createsoundforall).
