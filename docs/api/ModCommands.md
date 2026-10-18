# ModCommands

[All docs](../index.md) · In this page: [`ModCommands`](#modcommands) · [`ModCommand`](#modcommand) · [`CommandArg`](#commandarg) · [`CommandCompletion`](#commandcompletion) · [`CommandCall`](#commandcall)

<a name="modcommands"></a>

## ModCommands

*class*

Chat commands, typed as "/name arguments". Commands run on the server. Describe the arguments and players get suggestions as they type, worked out on the server so they can depend on anything the mod knows:

```csharp
Commands.Register("spawn", "Spawn items.", Spawn)
    .Arg(CommandArg.Item())
    .Arg(CommandArg.Number("count", 1, 20).Optional())
    .HostOnly();
```

Arguments are checked before the handler runs; the player sees the usage line ("/spawn &lt;item> [count]") if they got them wrong.

### Methods

<a name="modcommands-register"></a>

#### Register

```csharp
ModCommand Register(string name, string description, Action<CommandCall> handler)
```

Adds a command. `name` is what players type after the slash, e.g. "spawn" for "/spawn". Letters, digits, '-' and '\_'. Returns the command, to describe its arguments. If the game or another running mod has a command with the same name, players type this one with the mod's id in front, e.g. "/my\_mod:spawn" (that always works, clash or not).

<a name="modcommands-unregister"></a>

#### Unregister

```csharp
void Unregister(string name)
```

<a name="modcommand"></a>

## ModCommand

*class*

A registered command, for describing its arguments.

### Properties

<a name="modcommand-name"></a>

#### Name

```csharp
string Name { get; }
```

<a name="modcommand-description"></a>

#### Description

```csharp
string Description { get; }
```

<a name="modcommand-ishostonly"></a>

#### IsHostOnly

```csharp
bool IsHostOnly { get; }
```

Only the host may use it, and only the host sees it suggested.

<a name="modcommand-usage"></a>

#### Usage

```csharp
string Usage { get; }
```

E.g. "/spawn &lt;item> [count]".

### Methods

<a name="modcommand-arg"></a>

#### Arg

```csharp
ModCommand Arg(CommandArg arg)
```

Adds the next argument. Optional ones must come last.

<a name="modcommand-hostonly"></a>

#### HostOnly

```csharp
ModCommand HostOnly()
```

<a name="commandarg"></a>

## CommandArg

*class*

One argument of a command: what it accepts, and what to suggest while it's typed.

### Properties

<a name="commandarg-name"></a>

#### Name

```csharp
string Name { get; }
```

<a name="commandarg-isoptional"></a>

#### IsOptional

```csharp
bool IsOptional { get; }
```

### Methods

<a name="commandarg-word"></a>

#### Word

```csharp
static CommandArg Word(string name)
```

One word, anything.

<a name="commandarg-text"></a>

#### Text

```csharp
static CommandArg Text(string name)
```

The rest of the line, spaces and all. Must be the last argument.

<a name="commandarg-number"></a>

#### Number

```csharp
static CommandArg Number(string name, float min = -3.40282347E+38, float max = 3.40282347E+38)
```

A number between `min` and `max`.

<a name="commandarg-integer"></a>

#### Integer

```csharp
static CommandArg Integer(string name, int min = -2147483648, int max = 2147483647)
```

A whole number between `min` and `max`.

<a name="commandarg-item"></a>

#### Item

```csharp
static CommandArg Item(string name = "item")
```

An item id, e.g. "axe"; suggests every item.

<a name="commandarg-player"></a>

#### Player

```csharp
static CommandArg Player(string name = "player")
```

A connected player's name; suggests everyone online.

<a name="commandarg-choice"></a>

#### Choice

```csharp
static CommandArg Choice(string name, params string[] options)
```

One of a fixed set of words.

<a name="commandarg-custom"></a>

#### Custom

```csharp
static CommandArg Custom(string name, Func<CommandCompletion, IEnumerable<string>> suggest)
```

One word, with suggestions from the mod as the player types, e.g. the names of saved waypoints. Runs on the server for every keystroke (well, every pause), so keep it quick.

<a name="commandarg-suggest"></a>

#### Suggest

```csharp
CommandArg Suggest(Func<CommandCompletion, IEnumerable<string>> suggest)
```

Suggests these as well as whatever the argument suggests itself.

<a name="commandarg-optional"></a>

#### Optional

```csharp
CommandArg Optional()
```

<a name="commandcompletion"></a>

## CommandCompletion

*class*

What a player has typed so far, for suggesting the rest.

### Properties

<a name="commandcompletion-player"></a>

#### Player

```csharp
Player Player { get; }
```

<a name="commandcompletion-argumentindex"></a>

#### ArgumentIndex

```csharp
int ArgumentIndex { get; }
```

Which argument is being typed, from 0.

<a name="commandcompletion-prefix"></a>

#### Prefix

```csharp
string Prefix { get; }
```

What's been typed of it so far.

<a name="commandcompletion-arguments"></a>

#### Arguments

```csharp
string[] Arguments { get; }
```

The arguments before it.

<a name="commandcall"></a>

## CommandCall

*class*

One use of a chat command.

### Properties

<a name="commandcall-player"></a>

#### Player

```csharp
Player Player { get; }
```

Who typed it.

<a name="commandcall-name"></a>

#### Name

```csharp
string Name { get; }
```

The command's name, without the slash.

<a name="commandcall-argumenttext"></a>

#### ArgumentText

```csharp
string ArgumentText { get; }
```

Everything after the name, e.g. "cone 3".

<a name="commandcall-arguments"></a>

#### Arguments

```csharp
string[] Arguments { get; }
```

The arguments split on spaces, e.g. ["cone", "3"].

### Methods

<a name="commandcall-has"></a>

#### Has

```csharp
bool Has(int index)
```

Whether argument `index` was given.

<a name="commandcall-getstring"></a>

#### GetString

```csharp
string GetString(int index, string fallback = null)
```

<a name="commandcall-getint"></a>

#### GetInt

```csharp
int GetInt(int index, int fallback = 0)
```

<a name="commandcall-getfloat"></a>

#### GetFloat

```csharp
float GetFloat(int index, float fallback = 0)
```

<a name="commandcall-gettext"></a>

#### GetText

```csharp
string GetText(int index)
```

The rest of the line from argument `index`, spaces and all.

<a name="commandcall-getplayer"></a>

#### GetPlayer

```csharp
Player GetPlayer(int index)
```

The online player named by argument `index`, or an invalid one.

<a name="commandcall-reply"></a>

#### Reply

```csharp
void Reply(string text)
```

Shows a line in chat to the player who typed the command.
