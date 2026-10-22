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
- [`RecipeCategory`](api/ModCrafting.md#recipecategory): The groups recipes are listed under.
- [`RecipeSpec`](api/ModCrafting.md#recipespec): Describes a recipe, e.g. a mod's `recipes/pickaxe.json`:
- [`CraftedEvent`](api/ModCrafting.md#craftedevent): A player making a recipe once, for [`ModEvents.Crafted`](api/ModEvents.md#modevents-crafted).

### [ModEffects](api/ModEffects.md)

- [`ModEffects`](api/ModEffects.md#modeffects): Sounds and other things players see and hear.

### [ModEvents](api/ModEvents.md)

- [`ModEvents`](api/ModEvents.md#modevents): Things happening in the game that a mod can react to.
- [`ChatMessageEvent`](api/ModEvents.md#chatmessageevent): A chat message being sent, for [`ModEvents.ChatMessage`](api/ModEvents.md#modevents-chatmessage).
- [`InteractEvent`](api/ModEvents.md#interactevent): A player using one of a mod item's or vehicle's interactions.
- [`TreeFelledEvent`](api/ModEvents.md#treefelledevent): A tree coming down, for [`ModEvents.TreeFelled`](api/ModEvents.md#modevents-treefelled).

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
- [`ItemSpec`](api/ModItems.md#itemspec): Describes an item kind.
- [`InteractionSpec`](api/ModItems.md#interactionspec): One option in an item's prompt, e.g. `{ "id": "light", "label": "Turn On", "labelKey": "lightLabel" }`.

### [ModLog](api/ModLog.md)

- [`ModLogLevel`](api/ModLog.md#modloglevel): How serious a log line is.
- [`ModLog`](api/ModLog.md#modlog): Writes to the game's log, tagged with the mod's id.

### [ModMaterials](api/ModMaterials.md)

- [`ModMaterials`](api/ModMaterials.md#modmaterials): Materials: how the surfaces of items and vehicles look.
- [`MaterialSpec`](api/ModMaterials.md#materialspec): Describes a material.

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
- [`NitrousSpec`](api/ModVehicles.md#nitrousspec): A vehicle's nitrous tank.
- [`SeatSpec`](api/ModVehicles.md#seatspec): A seat in a vehicle.
- [`WheelSpec`](api/ModVehicles.md#wheelspec): A vehicle's wheel.
- [`EngineSpec`](api/ModVehicles.md#enginespec): How a vehicle drives.
- [`SuspensionSpec`](api/ModVehicles.md#suspensionspec): How a vehicle's wheels spring.

### [ModWorld](api/ModWorld.md)

- [`ModWorld`](api/ModWorld.md#modworld): Players and items in the world.
- [`Player`](api/ModWorld.md#player): A connected player.
- [`WorldItem`](api/ModWorld.md#worlditem): An item in the world.

### [NetData](api/NetData.md)

- [`NetData`](api/NetData.md#netdata): A small bag of named values, sent in network messages: numbers, text, true/false, vectors, rotations and colours.

### [Sound](api/Sound.md)

- [`Sound`](api/Sound.md#sound): A sound a mod made with [`ModEffects.CreateSound()`](api/ModEffects.md#modeffects-createsound) or [`ModEffects.CreateSoundForAll()`](api/ModEffects.md#modeffects-createsoundforall), which it can start, stop and change while it plays, e.g. an engine whose pitch follows its speed.
