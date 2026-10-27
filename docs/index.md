# Oaklands modding

A mod is a folder with a `mod.json` in it. JSON files and models add items, vehicles, tree species and materials with no code at all; C# scripts can do much more, from chat commands and synced item data to their own UI and settings. Scripts run in a sandbox, so players can install mods without worrying about what they might do to their computer.

- **[Content files](content.md)**: a mod's folder, `mod.json`, and the JSON for items, vehicles and trees.
- **[Scripts and the sandbox](sandbox.md)**: what scripts can use, and the limits they run under.
- **[Examples](examples.md)**: example mods to copy from.
- **[API reference](#api-reference)**: everything scripts reach the game through.

## Where mods go

Each mod is a folder in a `Mods` folder: either beside the game, or in the player's data folder (Unity's `Application.persistentDataPath`). Folders whose names start with `.` or `_` are skipped. When working on the game itself in the Unity editor, it's the `Mods` folder in the project.

Installing a mod doesn't turn it on everywhere: each world picks the mods it loads, under **Manage Mods** when creating it or in its settings. A mod's scripts and content load when the world opens, and reload by themselves while it's open whenever the mod's files change.

## API reference

Scripts reach the game through the `Oaklands.Modding` namespace, which every script already uses. Start with [`Mod`](api/Mod.md#mod), the class a mod's script derives from, and [`ModContext`](api/ModContext.md#modcontext), which leads to everything else.

### [BuildingPart](api/BuildingPart.md)

- [`BuildingPart`](api/BuildingPart.md#buildingpart): One part built with the hammer: a blueprint until it's filled with material (logs dragged into it, say), then solid.

### [ImageBuilder](api/ImageBuilder.md)

- [`ImageBuilder`](api/ImageBuilder.md#imagebuilder): A picture drawn in code, pixel by pixel or with simple shapes, then [`ModImages.Register()`](api/ModImages.md#modimages-register)ed and used anywhere a mod's image file goes: an item's or tool's icon, a material's texture, a UI image.

### [Line](api/Line.md)

- [`Line`](api/Line.md#line): A line drawn in the world as a round tube through points: a cable, a rope, a laser.

### [MeshBuilder](api/MeshBuilder.md)

- [`MeshBuilder`](api/MeshBuilder.md#meshbuilder): A 3D model built in code: add shapes, or vertices and triangles yourself, then [`ModMeshes.Register()`](api/ModMeshes.md#modmeshes-register) it and use what that returns as an item's, tool's or vehicle's `Model`:

### [Mod](api/Mod.md)

- [`Mod`](api/Mod.md#mod): The entry point of a scripted mod.

### [ModChat](api/ModChat.md)

- [`ModChat`](api/ModChat.md#modchat): The chat everyone sees.

### [ModCommands](api/ModCommands.md)

- [`ModCommands`](api/ModCommands.md#modcommands): Chat commands, typed as "/name arguments".
- [`ModCommand`](api/ModCommands.md#modcommand): A registered command, for describing its arguments.
- [`CommandArg`](api/ModCommands.md#commandarg): One argument of a command: what it accepts, and what to suggest while it's typed.
- [`CommandCompletion`](api/ModCommands.md#commandcompletion): What a player has typed so far, for suggesting the rest.
- [`CommandCall`](api/ModCommands.md#commandcall): One use of a chat command.

### [ModContext](api/ModContext.md)

- [`ModContext`](api/ModContext.md#modcontext): One loaded mod's view of the game.

### [ModCrafting](api/ModCrafting.md)

- [`ModCrafting`](api/ModCrafting.md#modcrafting): Recipes in the inventory's Crafting tab.
- [`StationIcon`](api/ModCrafting.md#stationicon): The picture a station kind gets in the Crafting tab.
- [`StationSpec`](api/ModCrafting.md#stationspec): A kind of crafting station, e.g. a mod's `stations/fabricator.json`: `{ "id": "fabricator", "name": "Droid Fabricator", "icon": "Forge", "range": 4 }`.
- [`RecipeCategory`](api/ModCrafting.md#recipecategory): The groups recipes are listed under.
- [`RecipeSpec`](api/ModCrafting.md#recipespec): Describes a recipe, e.g. a mod's `recipes/pickaxe.json`:
- [`CraftedEvent`](api/ModCrafting.md#craftedevent): A player making a recipe once, for [`ModEvents.Crafted`](api/ModEvents.md#modevents-crafted).

### [ModEffects](api/ModEffects.md)

- [`ModEffects`](api/ModEffects.md#modeffects): Sounds, lines and other things players see and hear.

### [ModEvents](api/ModEvents.md)

- [`ModEvents`](api/ModEvents.md#modevents): Things happening in the game that a mod can react to.
- [`ChatMessageEvent`](api/ModEvents.md#chatmessageevent): A chat message being sent, for [`ModEvents.ChatMessage`](api/ModEvents.md#modevents-chatmessage).
- [`InteractEvent`](api/ModEvents.md#interactevent): A player using one of a mod item's or vehicle's interactions.
- [`HeldItemUseEvent`](api/ModEvents.md#helditemuseevent): A click with a carried item, for [`ModEvents.HeldItemUsed`](api/ModEvents.md#modevents-helditemused).
- [`SeatEvent`](api/ModEvents.md#seatevent): Someone getting into or out of a seat, for [`ModEvents.SeatChanged`](api/ModEvents.md#modevents-seatchanged).
- [`TreeFelledEvent`](api/ModEvents.md#treefelledevent): A tree coming down, for [`ModEvents.TreeFelled`](api/ModEvents.md#modevents-treefelled).

### [ModImages](api/ModImages.md)

- [`ModImages`](api/ModImages.md#modimages): Pictures made in code with [`ImageBuilder`](api/ImageBuilder.md#imagebuilder), used by the name [`ModImages.Register()`](api/ModImages.md#modimages-register) returns ("image:name") wherever a mod's image file goes: `Icon` in item, tool and inventory specs, `Texture`, `NormalMap` and `EmissionMap` in material specs, and [`ModUI.Image()`](api/ModUI.md#modui-image).

### [ModInput](api/ModInput.md)

- [`ModInput`](api/ModInput.md#modinput): The keyboard and mouse buttons, on clients.
- [`ModKey`](api/ModInput.md#modkey): A key or mouse button, for [`ModInput`](api/ModInput.md#modinput).

### [ModInventory](api/ModInventory.md)

- [`ModInventory`](api/ModInventory.md#modinventory): Players' inventories, and items that only live in them.
- [`InventoryEntry`](api/ModInventory.md#inventoryentry): One stack in a player's inventory.
- [`InventoryCategory`](api/ModInventory.md#inventorycategory): The groups items are sorted into.
- [`InventorySpec`](api/ModInventory.md#inventoryspec): How a mod's item or tool sits in the inventory, as the `inventory` part of its spec, e.g. `"inventory": { "size": [2, 1], "weight": 1.5, "category": "Tool" }`.
- [`InventoryItemSpec`](api/ModInventory.md#inventoryitemspec): An item with no model that only lives in inventories, e.g. a mod's `inventory/nail.json`: `{ "id": "nail", "name": "Nails", "icon": "icons/nail.png", "maxStack": 50 }`.

### [ModItems](api/ModItems.md)

- [`ModItems`](api/ModItems.md#moditems): Item kinds: the game's own and those added by mods.
- [`ItemColliderShape`](api/ModItems.md#itemcollidershape): How an item's or vehicle's shape is made solid.
- [`CarryClicks`](api/ModItems.md#carryclicks): What the mouse buttons do while a player carries an item.
- [`CarrySpec`](api/ModItems.md#carryspec): How players carry an item in both hands, e.g. `"carry": { "clicks": "Mod", "prompts": true }`.
- [`LightKind`](api/ModItems.md#lightkind): What a [`LightSpec`](api/ModItems.md#lightspec) gives off.
- [`LightSpec`](api/ModItems.md#lightspec): A light on an item or vehicle that lights up what's around it, e.g. `{ "name": "head", "kind": "Spot", "position": [0, 0.6, 1.2], "color": [1, 0.95, 0.8], "range": 25, "angle": 50 }`.
- [`ItemSpec`](api/ModItems.md#itemspec): Describes an item kind.
- [`InteractionSpec`](api/ModItems.md#interactionspec): One option in an item's prompt, e.g. `{ "id": "light", "label": "Turn On", "labelKey": "lightLabel" }`.

### [ModLog](api/ModLog.md)

- [`ModLogLevel`](api/ModLog.md#modloglevel): How serious a log line is.
- [`ModLog`](api/ModLog.md#modlog): Writes to the game's log, tagged with the mod's id.

### [ModMaterials](api/ModMaterials.md)

- [`ModMaterials`](api/ModMaterials.md#modmaterials): Materials: how the surfaces of items and vehicles look.
- [`MaterialSpec`](api/ModMaterials.md#materialspec): Describes a material.

### [ModMeshes](api/ModMeshes.md)

- [`ModMeshes`](api/ModMeshes.md#modmeshes): Models made in code with [`MeshBuilder`](api/MeshBuilder.md#meshbuilder): for an item, tool or vehicle kind's `Model`, or for one item with [`WorldItem.SetModel()`](api/ModWorld.md#worlditem-setmodel).

### [ModNetwork](api/ModNetwork.md)

- [`ModNetwork`](api/ModNetwork.md#modnetwork): Messages between this mod's code on the server and on clients.
- [`NetMessage`](api/ModNetwork.md#netmessage): A message from this mod's code on another peer, for [`ModNetwork.On()`](api/ModNetwork.md#modnetwork-on).

### [ModSettings](api/ModSettings.md)

- [`ModSettings`](api/ModSettings.md#modsettings): The player's own options for a mod, shown in the game's Settings screen under "Mods" and saved with the game's settings.

### [ModTimers](api/ModTimers.md)

- [`ModTimers`](api/ModTimers.md#modtimers): Runs code later, once or repeatedly, on the main thread.
- [`ModTimer`](api/ModTimers.md#modtimer): A timer from [`ModTimers`](api/ModTimers.md#modtimers); cancel it to stop it.

### [ModTools](api/ModTools.md)

- [`ModTools`](api/ModTools.md#modtools): Tools added by mods: held in the hand from the hotbar like the axe, swung with the left mouse button (and, if they have one, a second use on the right).
- [`ToolSwing`](api/ModTools.md#toolswing): How a tool moves when it's used.
- [`ToolSpec`](api/ModTools.md#toolspec): Describes a tool, e.g. a mod's `tools/pickaxe.json`:
- [`ToolHold`](api/ModTools.md#toolhold): A tool's place in the hand, in metres and degrees.
- [`ToolHitKind`](api/ModTools.md#toolhitkind): What a tool's use struck, for [`ToolUseEvent.HitKind`](api/ModTools.md#tooluseevent-hitkind).
- [`ToolUseEvent`](api/ModTools.md#tooluseevent): A player using a tool, for [`ModTools.OnUse()`](api/ModTools.md#modtools-onuse).

### [ModTrees](api/ModTrees.md)

- [`ModTrees`](api/ModTrees.md#modtrees): Tree species added by mods.
- [`TreeSpeciesSpec`](api/ModTrees.md#treespeciesspec): Describes a tree species.

### [ModUI](api/ModUI.md)

- [`ModUI`](api/ModUI.md#modui): On-screen UI for mods, built in code.
- [`UIElement`](api/ModUI.md#uielement): Anything on screen.
- [`UIBox`](api/ModUI.md#uibox): A plain container.
- [`UIPanel`](api/ModUI.md#uipanel): A top-level piece of UI.
- [`UILabel`](api/ModUI.md#uilabel): Text on screen.
- [`UIButton`](api/ModUI.md#uibutton): A button.
- [`UITextField`](api/ModUI.md#uitextfield): A box players type into.
- [`UIToggle`](api/ModUI.md#uitoggle): A checkbox with a label.
- [`UISlider`](api/ModUI.md#uislider): A slider between two numbers.
- [`UIImage`](api/ModUI.md#uiimage): A picture from the mod's folder (PNG or JPG), scaled to fit.
- [`UIScroll`](api/ModUI.md#uiscroll): A box that scrolls when its children don't fit.
- [`UIBar`](api/ModUI.md#uibar): A bar that fills from left to right, e.g. for progress or health.
- [`UIAnchor`](api/ModUI.md#uianchor): Which part of the screen a panel sits in.
- [`UIAlign`](api/ModUI.md#uialign): How a box lines up its children.
- [`UIStyle`](api/ModUI.md#uistyle): Chainable styling for any element, e.g. `UI.Label("Hi").FontSize(20).TextColor(Color.yellow).Margin(4)`.

### [ModVehicles](api/ModVehicles.md)

- [`ModVehicles`](api/ModVehicles.md#modvehicles): Vehicles added by mods.
- [`VehicleSpec`](api/ModVehicles.md#vehiclespec): Describes a vehicle, in its own space: metres, +Y up, +Z forward, with the origin on the ground under its middle.
- [`DoorSpec`](api/ModVehicles.md#doorspec): A door on a vehicle, e.g. `{ "node": "DoorL", "seat": 0, "axis": [0, 1, 0], "openAngle": -70 }`.
- [`FuelSpec`](api/ModVehicles.md#fuelspec): A vehicle's fuel tank, e.g. `{ "range": 40000 }`.
- [`NitrousSpec`](api/ModVehicles.md#nitrousspec): A vehicle's nitrous tank.
- [`SeatSpec`](api/ModVehicles.md#seatspec): A seat in a vehicle.
- [`WheelSpec`](api/ModVehicles.md#wheelspec): A vehicle's wheel.
- [`EngineSpec`](api/ModVehicles.md#enginespec): How a vehicle drives.
- [`SuspensionSpec`](api/ModVehicles.md#suspensionspec): How a vehicle's wheels spring.

### [ModWorld](api/ModWorld.md)

- [`ModWorld`](api/ModWorld.md#modworld): Players and items in the world.
- [`GroundFilter`](api/ModWorld.md#groundfilter): What [`ModWorld.GroundAt()`](api/ModWorld.md#modworld-groundat) stands things on.
- [`Player`](api/ModWorld.md#player): A connected player.
- [`WorldItem`](api/ModWorld.md#worlditem): An item in the world.

### [ModWorldData](api/ModWorldData.md)

- [`ModWorldData`](api/ModWorldData.md#modworlddata): Named values this mod keeps for the whole world, saved with it, e.g. "starter kit given" or which droid belongs to whom.

### [NetData](api/NetData.md)

- [`NetData`](api/NetData.md#netdata): A small bag of named values, sent in network messages: numbers, text, true/false, vectors, rotations and colors.

### [Paths](api/Paths.md)

- [`WalkerSpec`](api/Paths.md#walkerspec): The size and pace of something that walks: for [`WorldItem.SetWalker()`](api/ModWorld.md#worlditem-setwalker) and [`ModWorld.FindPath()`](api/ModWorld.md#modworld-findpath).
- [`MoveState`](api/Paths.md#movestate): What a walking item is doing (see [`WorldItem.MoveState`](api/ModWorld.md#worlditem-movestate)).
- [`PathRequest`](api/Paths.md#pathrequest): A path being worked out by [`ModWorld.FindPath()`](api/ModWorld.md#modworld-findpath).

### [Sound](api/Sound.md)

- [`Sound`](api/Sound.md#sound): A sound a mod made with [`ModEffects.CreateSound()`](api/ModEffects.md#modeffects-createsound) or [`ModEffects.CreateSoundForAll()`](api/ModEffects.md#modeffects-createsoundforall), which it can start, stop and change while it plays, e.g. an engine whose pitch follows its speed.

### [WorldItem.Vehicle](api/WorldItem.Vehicle.md)

- [`VehicleControls`](api/WorldItem.Vehicle.md#vehiclecontrols): A driver's controls, for [`WorldItem.Controls`](api/ModWorld.md#worlditem-controls).

### [WorldTree](api/WorldTree.md)

- [`WorldTree`](api/WorldTree.md#worldtree): A tree in the world, standing or cut down to a stump, from [`ModWorld.FindTrees()`](api/ModWorld.md#modworld-findtrees) or [`WorldHit.Tree`](api/WorldTree.md#worldhit-tree).
- [`ChopResult`](api/WorldTree.md#chopresult): What a chop into a tree did, from [`ModWorld.ChopTree()`](api/ModWorld.md#modworld-choptree).
- [`WorldHit`](api/WorldTree.md#worldhit): What a ray hit, from [`ModWorld.Raycast()`](api/ModWorld.md#modworld-raycast).
