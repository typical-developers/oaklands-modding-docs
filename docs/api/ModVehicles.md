# ModVehicles

[All docs](../index.md) · In this page: [`ModVehicles`](#modvehicles) · [`VehicleSpec`](#vehiclespec) · [`DoorSpec`](#doorspec) · [`FuelSpec`](#fuelspec) · [`NitrousSpec`](#nitrousspec) · [`SeatSpec`](#seatspec) · [`WheelSpec`](#wheelspec) · [`EngineSpec`](#enginespec) · [`SuspensionSpec`](#suspensionspec)

<a name="modvehicles"></a>

## ModVehicles

*class*

Vehicles added by mods. JSON files in a mod's `vehicles` folder are registered before [`Mod.OnLoad()`](Mod.md#mod-onload); scripts can add more with [`ModVehicles.Register()`](ModVehicles.md#modvehicles-register). Spawn them like items, with [`ModWorld.SpawnItem()`](ModWorld.md#modworld-spawnitem), and they can carry data and interactions like items too.

### Methods

<a name="modvehicles-register"></a>

#### Register

```csharp
string Register(VehicleSpec spec)
```

Adds a vehicle kind, or updates it if this mod already added one with the same id. Returns its full id, "modid:vehicleid". Register on every peer, like items.

<a name="vehiclespec"></a>

## VehicleSpec

*class*

Describes a vehicle, in its own space: metres, +Y up, +Z forward, with the origin on the ground under its middle. As JSON (vectors as [x, y, z]):

```json
{
  "id": "kart", "name": "Go-Kart", "model": "models/kart.glb", "mass": 250,
  "seats": [ { "name": "Driver", "position": [0, 0.45, -0.2] } ],
  "wheels": [
    { "position": [-0.55, 0.2, 0.7], "radius": 0.2, "steer": true, "drive": false, "visual": "WheelFL" },
    ...
  ],
  "engine": { "torque": 600, "maxSpeed": 18 }
}
```

### Fields

<a name="vehiclespec-id"></a>

#### Id

```csharp
string Id
```

The id within the mod: lowercase letters, digits and '\_', e.g. "kart".

<a name="vehiclespec-name"></a>

#### Name

```csharp
string Name
```

What players see, e.g. "Go-Kart".

<a name="vehiclespec-model"></a>

#### Model

```csharp
string Model
```

A .glb or .gltf file, relative to the mod's folder.

<a name="vehiclespec-mass"></a>

#### Mass

```csharp
float Mass = 1000f
```

Kilograms.

<a name="vehiclespec-collider"></a>

#### Collider

```csharp
ItemColliderShape Collider = ItemColliderShape.Box
```

Box: one box round the body. Convex: a hull per mesh.

<a name="vehiclespec-centerofmass"></a>

#### CenterOfMass

```csharp
Vector3 CenterOfMass = new(0f, 0.3f, 0f)
```

Lower rolls over less.

<a name="vehiclespec-seats"></a>

#### Seats

```csharp
List<SeatSpec> Seats = new()
```

The first is the driver's. At most 6.

<a name="vehiclespec-wheels"></a>

#### Wheels

```csharp
List<WheelSpec> Wheels = new()
```

Every wheel, driven or not.

<a name="vehiclespec-engine"></a>

#### Engine

```csharp
EngineSpec Engine = new()
```

How it drives.

<a name="vehiclespec-suspension"></a>

#### Suspension

```csharp
SuspensionSpec Suspension = new()
```

How the wheels spring.

<a name="vehiclespec-camerapivot"></a>

#### CameraPivot

```csharp
Vector3 CameraPivot = new(0f, 1.5f, -1f)
```

Where the third-person camera orbits.

<a name="vehiclespec-cameradistance"></a>

#### CameraDistance

```csharp
float CameraDistance = 7f
```

How far the third-person camera sits from the pivot, in metres.

<a name="vehiclespec-interactions"></a>

#### Interactions

```csharp
List<InteractionSpec> Interactions = new()
```

Extra prompt options, like items'. At most 4.

<a name="vehiclespec-materials"></a>

#### Materials

```csharp
Dictionary<string, string> Materials = new()
```

Materials for the model's parts, by the names its materials have in the model file, e.g. `{ "Body": "paint", "Glass": "game:glass" }`; "\*" covers every part not named. A plain id is this mod's own material. Parts not listed keep the model's own materials.

<a name="vehiclespec-nitrous"></a>

#### Nitrous

```csharp
NitrousSpec Nitrous
```

A nitrous tank: the driver holds Sprint (Shift) to boost, with flames out of the exhausts and a tank gauge on the HUD. Leave it out for no nitrous.

<a name="vehiclespec-fuel"></a>

#### Fuel

```csharp
FuelSpec Fuel
```

A fuel tank: driving under power burns fuel, the dashboard shows a fuel gauge, and once it's empty the engine stops pulling (it still steers, brakes and rolls). Leave it out for a vehicle that never needs fuel, which is the default. There's no way to refuel yet.

<a name="vehiclespec-parkingbrake"></a>

#### ParkingBrake

```csharp
float ParkingBrake = -1f
```

Braking torque per wheel, in newton-metres, while nobody drives it, so it doesn't roll away when parked. 0 lets it roll freely, e.g. a trailer; -1 (the default) is 4000, or 0 for a vehicle with no seats.

<a name="vehiclespec-lights"></a>

#### Lights

```csharp
List<LightSpec> Lights = new()
```

Lights, e.g. headlights; see [`LightSpec`](ModItems.md#lightspec). At most 8.

<a name="vehiclespec-doors"></a>

#### Doors

```csharp
List<DoorSpec> Doors = new()
```

Doors (or hatches) that swing open while someone gets into or out of their seat; see [`DoorSpec`](ModVehicles.md#doorspec). Looking at a seat's door shows that seat's prompt, so a closed cab can still be got into. Works best with `"collider": "Convex"`.

<a name="doorspec"></a>

## DoorSpec

*class*

A door on a vehicle, e.g. `{ "node": "DoorL", "seat": 0, "axis": [0, 1, 0], "openAngle": -70 }`. The node turns about its own origin, so put the origin on the hinge in the model. Scripts can also open and shut it with [`WorldItem.SetDoorOpen()`](ModWorld.md#worlditem-setdooropen).

### Fields

<a name="doorspec-node"></a>

#### Node

```csharp
string Node
```

The model node to swing (and everything under it).

<a name="doorspec-seat"></a>

#### Seat

```csharp
int Seat = -1
```

Which seat it's for (0 is the driver's), or -1 for none.

<a name="doorspec-axis"></a>

#### Axis

```csharp
Vector3 Axis = Vector3.up
```

The hinge's axis, in the node's own space.

<a name="doorspec-openangle"></a>

#### OpenAngle

```csharp
float OpenAngle = 70f
```

Degrees it turns to be fully open; the sign picks the way.

<a name="doorspec-swingseconds"></a>

#### SwingSeconds

```csharp
float SwingSeconds = 0.35f
```

Seconds to swing fully open or shut.

<a name="fuelspec"></a>

## FuelSpec

*class*

A vehicle's fuel tank, e.g. `{ "range": 40000 }`.

### Fields

<a name="fuelspec-range"></a>

#### Range

```csharp
float Range = 40000f
```

How far it drives on a full tank, in metres. Driving at part throttle goes further.

<a name="nitrousspec"></a>

## NitrousSpec

*class*

A vehicle's nitrous tank.

### Fields

<a name="nitrousspec-capacity"></a>

#### Capacity

```csharp
float Capacity = 4f
```

Seconds of boost in a full tank.

<a name="nitrousspec-acceleration"></a>

#### Acceleration

```csharp
float Acceleration = 8f
```

Extra forward push while boosting, in metres per second per second.

<a name="nitrousspec-maxspeed"></a>

#### MaxSpeed

```csharp
float MaxSpeed = 30f
```

The boost stops pushing above this speed, in metres per second.

<a name="nitrousspec-refilltime"></a>

#### RefillTime

```csharp
float RefillTime = 15f
```

Seconds an empty tank takes to refill. It starts a couple of seconds after the boost ends.

<a name="nitrousspec-exhausts"></a>

#### Exhausts

```csharp
List<Vector3> Exhausts = new()
```

Where the flames come out, at the end of each exhaust pipe; they point straight back. Empty: two at the back of the model.

<a name="seatspec"></a>

## SeatSpec

*class*

A seat in a vehicle.

### Fields

<a name="seatspec-name"></a>

#### Name

```csharp
string Name = "Seat"
```

What the prompt to sit in it says, e.g. "Driver".

<a name="seatspec-position"></a>

#### Position

```csharp
Vector3 Position
```

Where the sitting player's middle goes.

<a name="wheelspec"></a>

## WheelSpec

*class*

A vehicle's wheel.

### Fields

<a name="wheelspec-position"></a>

#### Position

```csharp
Vector3 Position
```

The wheel's middle, resting on its suspension.

<a name="wheelspec-radius"></a>

#### Radius

```csharp
float Radius = 0.4f
```

In metres.

<a name="wheelspec-steer"></a>

#### Steer

```csharp
bool Steer
```

Whether it turns with the steering.

<a name="wheelspec-drive"></a>

#### Drive

```csharp
bool Drive = true
```

Whether the engine turns it.

<a name="wheelspec-visual"></a>

#### Visual

```csharp
string Visual
```

Name of the model's node to spin and steer with this wheel, e.g. "WheelFL". Everything under it moves with the wheel.

<a name="enginespec"></a>

## EngineSpec

*class*

How a vehicle drives.

### Fields

<a name="enginespec-torque"></a>

#### Torque

```csharp
float Torque = 3000f
```

Newton-metres per driven wheel.

<a name="enginespec-reversetorque"></a>

#### ReverseTorque

```csharp
float ReverseTorque = 2000f
```

Newton-metres per driven wheel, backwards.

<a name="enginespec-maxspeed"></a>

#### MaxSpeed

```csharp
float MaxSpeed = 25f
```

Metres per second.

<a name="enginespec-maxreversespeed"></a>

#### MaxReverseSpeed

```csharp
float MaxReverseSpeed = 8f
```

Metres per second, backwards.

<a name="enginespec-brake"></a>

#### Brake

```csharp
float Brake = 4000f
```

Braking torque per wheel, in newton-metres.

<a name="enginespec-steerangle"></a>

#### SteerAngle

```csharp
float SteerAngle = 35f
```

Degrees, at low speed.

<a name="suspensionspec"></a>

## SuspensionSpec

*class*

How a vehicle's wheels spring.

### Fields

<a name="suspensionspec-distance"></a>

#### Distance

```csharp
float Distance = 0.3f
```

How far the wheels travel, in metres.

<a name="suspensionspec-spring"></a>

#### Spring

```csharp
float Spring = 35000f
```

Stiffness: higher bounces less but feels harder.

<a name="suspensionspec-damper"></a>

#### Damper

```csharp
float Damper = 4500f
```

How quickly bouncing dies down.
