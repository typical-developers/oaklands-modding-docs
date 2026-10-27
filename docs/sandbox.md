# Scripts and the sandbox

[All docs](index.md)

Every `.cs` file in a mod's folder is compiled together when a world that uses the mod opens, and again whenever one of them changes. Exactly one public class must derive from [`Mod`](api/Mod.md#mod); the game creates it and calls it as things happen. Compile errors, and anything a script throws, show in the console prefixed `[Mods]`. Everything logged about a mod (its `Log` calls, compile errors, what its scripts throw and problems with its files) also goes to `Logs/Mods/<mod id>.log` in the game's data folder, beside `Worlds` and the game's own `Player.log`, started afresh each session; a host playing their world is told in chat when a mod has an error.

## What scripts compile against

Only these assemblies: `mscorlib`, `netstandard`, `System`, `System.Core`, `UnityEngine.CoreModule`, `Oaklands.Modding.Api`. Every script gets these `using`s without writing them:

```csharp
global using System;
global using System.Collections.Generic;
global using System.Linq;
global using UnityEngine;
global using Oaklands.Modding;
```

## The language

Scripts are C# 12.0, compiled in Release with nullable annotations off, against the same .NET Standard 2.1 library the game runs on (Unity's Mono), less what the sandbox refuses. `OAKLANDS_MOD` is defined, for `#if`. Features the compiler does by itself work: records, `init`, pattern matching, switch expressions, local functions, tuples, `foreach` over the API's lists. Anything built on threads (`async`/`await`, `Task`) is refused; use [`Timers`](api/ModTimers.md#modtimers) and callbacks instead.

The API's handles, [`Player`](api/ModWorld.md#player), [`WorldItem`](api/ModWorld.md#worlditem), [`WorldTree`](api/WorldTree.md#worldtree) and [`BuildingPart`](api/BuildingPart.md#buildingpart), are `readonly struct`s: small values that name something in the game rather than hold it, so they're cheap to copy and keep in lists or dictionaries, compare equal when they name the same thing, and become invalid (check `IsValid`) when it goes. `default` is the invalid one.

## What scripts may use

Anything not listed here is refused when the mod is compiled, with an error saying what and where. That rules out files, the network, threads, reflection and Unity objects: scripts reach the game only through the mod API. The check is made on the compiled code, so it can't be got round with tricks in the source.

### The mod API

Every public member of the [`Oaklands.Modding`](index.md#api-reference) namespace.

### Whole namespaces

- `System.Collections.Generic`
- `System.Collections.ObjectModel`
- `System.Linq`

Except these types in them, which could start threads or compile code:

`System.Linq.ParallelEnumerable`, `System.Linq.ParallelQuery`, `System.Linq.ParallelQuery<T>`, `System.Linq.OrderedParallelQuery<T>`, `System.Linq.ParallelExecutionMode`, `System.Linq.ParallelMergeOptions`, `System.Linq.Queryable`, `System.Linq.IQueryable`, `System.Linq.IQueryable<T>`, `System.Linq.IQueryProvider`, `System.Linq.IOrderedQueryable`, `System.Linq.IOrderedQueryable<T>`, `System.Linq.EnumerableQuery`, `System.Linq.EnumerableQuery<T>`.

### Other types

"Everything" means every member of the type; otherwise only the members listed. "The type only" means it can appear in signatures, e.g. as a base class, but none of its members can be used.

#### System

| Type | Allowed |
| --- | --- |
| `Void` | everything |
| `Boolean` | everything |
| `Byte` | everything |
| `SByte` | everything |
| `Char` | everything |
| `Int16` | everything |
| `UInt16` | everything |
| `Int32` | everything |
| `UInt32` | everything |
| `Int64` | everything |
| `UInt64` | everything |
| `Single` | everything |
| `Double` | everything |
| `Decimal` | everything |
| `String` | everything |
| `Math` | everything |
| `MathF` | everything |
| `Nullable` | everything |
| `Nullable<T>` | everything |
| `Enum` | everything |
| `ValueType` | everything |
| `Array` | everything |
| `TimeSpan` | everything |
| `DateTime` | everything |
| `DayOfWeek` | everything |
| `Guid` | everything |
| `Random` | everything |
| `BitConverter` | everything |
| `Convert` | everything |
| `HashCode` | everything |
| `Index` | everything |
| `Range` | everything |
| `Span<T>` | everything |
| `ReadOnlySpan<T>` | everything |
| `MidpointRounding` | everything |
| `StringComparison` | everything |
| `StringComparer` | everything |
| `StringSplitOptions` | everything |
| `IFormatProvider` | everything |
| `IComparable` | everything |
| `IComparable<T>` | everything |
| `IEquatable<T>` | everything |
| `IFormattable` | everything |
| `IConvertible` | everything |
| `IDisposable` | everything |
| `ICloneable` | everything |
| `Tuple` | everything |
| `Tuple<T>` | everything |
| `Tuple<T1, T2>` | everything |
| `Tuple<T1, T2, T3>` | everything |
| `Tuple<T1, T2, T3, T4>` | everything |
| `Tuple<T1, T2, T3, T4, T5>` | everything |
| `Tuple<T1, T2, T3, T4, T5, T6>` | everything |
| `Tuple<T1, T2, T3, T4, T5, T6, T7>` | everything |
| `Tuple<T1, T2, T3, T4, T5, T6, T7, T8>` | everything |
| `ValueTuple` | everything |
| `ValueTuple<T>` | everything |
| `ValueTuple<T1, T2>` | everything |
| `ValueTuple<T1, T2, T3>` | everything |
| `ValueTuple<T1, T2, T3, T4>` | everything |
| `ValueTuple<T1, T2, T3, T4, T5>` | everything |
| `ValueTuple<T1, T2, T3, T4, T5, T6>` | everything |
| `ValueTuple<T1, T2, T3, T4, T5, T6, T7>` | everything |
| `ValueTuple<T1, T2, T3, T4, T5, T6, T7, T8>` | everything |
| `Action` | everything |
| `Action<T>` | everything |
| `Action<T1, T2>` | everything |
| `Action<T1, T2, T3>` | everything |
| `Action<T1, T2, T3, T4>` | everything |
| `Action<T1, T2, T3, T4, T5>` | everything |
| `Action<T1, T2, T3, T4, T5, T6>` | everything |
| `Action<T1, T2, T3, T4, T5, T6, T7>` | everything |
| `Action<T1, T2, T3, T4, T5, T6, T7, T8>` | everything |
| `Func<T>` | everything |
| `Func<T1, T2>` | everything |
| `Func<T1, T2, T3>` | everything |
| `Func<T1, T2, T3, T4>` | everything |
| `Func<T1, T2, T3, T4, T5>` | everything |
| `Func<T1, T2, T3, T4, T5, T6>` | everything |
| `Func<T1, T2, T3, T4, T5, T6, T7>` | everything |
| `Func<T1, T2, T3, T4, T5, T6, T7, T8>` | everything |
| `Func<T1, T2, T3, T4, T5, T6, T7, T8, T9>` | everything |
| `Predicate<T>` | everything |
| `Comparison<T>` | everything |
| `Converter<T1, T2>` | everything |
| `EventHandler` | everything |
| `EventHandler<T>` | everything |
| `EventArgs` | everything |
| `Delegate` | `Combine`, `Remove`, `operator ==`, `operator !=`, `Equals`, `GetHashCode` |
| `MulticastDelegate` | the type only |
| `Object` | constructors, `ToString`, `Equals`, `GetHashCode`, `GetType`, `ReferenceEquals`, `MemberwiseClone` |
| `Type` | `GetTypeFromHandle`, `operator ==`, `operator !=`, `Equals` |
| `RuntimeTypeHandle` | the type only |
| `RuntimeFieldHandle` | the type only |
| `Exception` | constructors, `Message` (get), `InnerException` (get), `StackTrace` (get), `ToString` |
| `SystemException` | constructors |
| `ArgumentException` | constructors, `Message` (get), `ParamName` (get) |
| `ArgumentNullException` | constructors |
| `ArgumentOutOfRangeException` | constructors, `Message` (get) |
| `InvalidOperationException` | constructors |
| `NotSupportedException` | constructors |
| `NotImplementedException` | constructors |
| `IndexOutOfRangeException` | constructors |
| `NullReferenceException` | constructors |
| `InvalidCastException` | constructors |
| `FormatException` | constructors |
| `OverflowException` | constructors |
| `DivideByZeroException` | constructors |
| `ObjectDisposedException` | constructors |
| `Attribute` | constructors |
| `AttributeUsageAttribute` | everything |
| `AttributeTargets` | everything |
| `FlagsAttribute` | everything |
| `ObsoleteAttribute` | everything |
| `ParamArrayAttribute` | everything |
| `Environment` | `CurrentManagedThreadId` (get), `NewLine` (get) |

#### System.Collections

| Type | Allowed |
| --- | --- |
| `IEnumerable` | everything |
| `IEnumerator` | everything |
| `IEqualityComparer` | everything |
| `IComparer` | everything |
| `IStructuralEquatable` | everything |
| `IStructuralComparable` | everything |

#### System.Diagnostics

| Type | Allowed |
| --- | --- |
| `ConditionalAttribute` | everything |
| `DebuggableAttribute` | constructors |
| `DebuggableAttribute.DebuggingModes` | everything |
| `DebuggerBrowsableAttribute` | everything |
| `DebuggerBrowsableState` | everything |
| `DebuggerDisplayAttribute` | everything |
| `DebuggerHiddenAttribute` | everything |
| `DebuggerStepThroughAttribute` | everything |

#### System.Globalization

| Type | Allowed |
| --- | --- |
| `NumberStyles` | everything |
| `CultureInfo` | `InvariantCulture` (get) |

#### System.Reflection

| Type | Allowed |
| --- | --- |
| `DefaultMemberAttribute` | constructors |

#### System.Runtime.CompilerServices

| Type | Allowed |
| --- | --- |
| `CompilationRelaxationsAttribute` | everything |
| `RuntimeCompatibilityAttribute` | everything |
| `CompilerGeneratedAttribute` | everything |
| `ExtensionAttribute` | everything |
| `IsReadOnlyAttribute` | everything |
| `IsByRefLikeAttribute` | everything |
| `IsUnmanagedAttribute` | everything |
| `IteratorStateMachineAttribute` | everything |
| `NullableAttribute` | everything |
| `NullableContextAttribute` | everything |
| `NullablePublicOnlyAttribute` | everything |
| `RefSafetyRulesAttribute` | everything |
| `ScopedRefAttribute` | everything |
| `TupleElementNamesAttribute` | everything |
| `RuntimeHelpers` | `InitializeArray`, `GetHashCode`, `EnsureSufficientExecutionStack` |

#### System.Text

| Type | Allowed |
| --- | --- |
| `StringBuilder` | everything |

#### System.Threading

| Type | Allowed |
| --- | --- |
| `Interlocked` | `CompareExchange` |

#### UnityEngine

| Type | Allowed |
| --- | --- |
| `Vector2` | everything |
| `Vector3` | everything |
| `Vector4` | everything |
| `Vector2Int` | everything |
| `Vector3Int` | everything |
| `Quaternion` | everything |
| `Matrix4x4` | everything |
| `Color` | everything |
| `Color32` | everything |
| `Mathf` | everything |
| `Bounds` | everything |
| `Rect` | everything |
| `Ray` | everything |
| `Plane` | everything |
| `Random` | everything |

## Limits

Mod code is rewritten as it's loaded to check these as it runs, so a mistake like an endless loop stops that one callback instead of freezing the game. Going over a limit throws an exception the mod can't catch, which ends the callback.

| Limit | Value |
| --- | --- |
| Time one callback (an event handler, command, timer...) may run | 100 ms |
| Time [`Mod.OnLoad`](api/Mod.md#mod-onload) may run | 2000 ms |
| How deep mod methods may call each other | 400 calls |
| The longest array made in one go | 4,194,304 items |
| How much one callback may grow the memory in use | 256 MB |
| Errors before the mod's scripts are turned off | 25 (going over a limit counts as 9) |

A mod whose scripts were turned off keeps its content. Saving a change to any of its files loads it again.
