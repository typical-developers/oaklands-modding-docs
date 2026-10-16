# NetData

[All docs](../index.md)

<a name="netdata"></a>

## NetData

*class*

A small bag of named values, sent in network messages: numbers, text, true/false, vectors, rotations and colours.

```csharp
var data = new NetData().Set("speed", 4.5f).Set("label", "Fast");
Network.SendToAll("boost", data);
```

### Constructors

<a name="netdata-new"></a>

#### new NetData

```csharp
NetData()
```

### Fields

<a name="netdata-maxlength"></a>

#### MaxLength

```csharp
const int MaxLength = 8192
```

Most characters all the values together may take.

### Properties

<a name="netdata-count"></a>

#### Count

```csharp
int Count { get; }
```

<a name="netdata-keys"></a>

#### Keys

```csharp
IEnumerable<string> Keys { get; }
```

### Methods

<a name="netdata-has"></a>

#### Has

```csharp
bool Has(string key)
```

<a name="netdata-set"></a>

#### Set

```csharp
NetData Set(string key, int value)
```

#### Set

```csharp
NetData Set(string key, float value)
```

#### Set

```csharp
NetData Set(string key, double value)
```

#### Set

```csharp
NetData Set(string key, bool value)
```

#### Set

```csharp
NetData Set(string key, string value)
```

#### Set

```csharp
NetData Set(string key, Vector3 value)
```

#### Set

```csharp
NetData Set(string key, Quaternion value)
```

#### Set

```csharp
NetData Set(string key, Color value)
```

<a name="netdata-getint"></a>

#### GetInt

```csharp
int GetInt(string key, int fallback = 0)
```

<a name="netdata-getfloat"></a>

#### GetFloat

```csharp
float GetFloat(string key, float fallback = 0)
```

<a name="netdata-getbool"></a>

#### GetBool

```csharp
bool GetBool(string key, bool fallback = false)
```

<a name="netdata-getstring"></a>

#### GetString

```csharp
string GetString(string key, string fallback = null)
```

<a name="netdata-getvector3"></a>

#### GetVector3

```csharp
Vector3 GetVector3(string key, Vector3 fallback = default(Vector3))
```

<a name="netdata-getquaternion"></a>

#### GetQuaternion

```csharp
Quaternion GetQuaternion(string key, Quaternion fallback = default(Quaternion))
```

<a name="netdata-getcolor"></a>

#### GetColor

```csharp
Color GetColor(string key, Color fallback = default(Color))
```
