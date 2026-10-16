# ModLog

[All docs](../index.md) · In this page: [`ModLogLevel`](#modloglevel) · [`ModLog`](#modlog)

<a name="modloglevel"></a>

## ModLogLevel

*enum*

How serious a log line is.

| Value | Description |
| --- | --- |
| `Info` |  |
| `Warning` |  |
| `Error` |  |

<a name="modlog"></a>

## ModLog

*class*

Writes to the game's log, tagged with the mod's id.

### Methods

<a name="modlog-info"></a>

#### Info

```csharp
void Info(string message)
```

<a name="modlog-warn"></a>

#### Warn

```csharp
void Warn(string message)
```

<a name="modlog-error"></a>

#### Error

```csharp
void Error(string message)
```
