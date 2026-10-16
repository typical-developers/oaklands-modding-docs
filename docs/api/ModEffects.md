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
