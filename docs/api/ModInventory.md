# ModInventory

[All docs](../index.md) · In this page: [`ModInventory`](#modinventory) · [`InventoryEntry`](#inventoryentry) · [`InventoryCategory`](#inventorycategory) · [`InventorySpec`](#inventoryspec) · [`InventoryItemSpec`](#inventoryitemspec)

<a name="modinventory"></a>

## ModInventory

*class*

Players' inventories, and items that only live in them. Mod items and tools are storable by default (see [`ItemSpec.Inventory`](ModItems.md#itemspec-inventory)); items with no model, like nails or coins, go in a mod's `inventory` folder or [`ModInventory.Register()`](ModInventory.md#modinventory-register). Changing what a player carries is server only.

### Methods

<a name="modinventory-register"></a>

#### Register

```csharp
string Register(InventoryItemSpec spec)
```

Adds an inventory-only item kind, or updates this mod's one with the same id. Returns its full id, "modid:itemid". Register on every peer, like items.

<a name="modinventory-exists"></a>

#### Exists

```csharp
bool Exists(string itemId)
```

Whether an inventory item exists, e.g. "log", "planks" or "nails:nail". Mod items and tools use their item id.

<a name="modinventory-give"></a>

#### Give

```csharp
int Give(Player player, string itemId, int count = 1)
```

Puts items in a player's inventory, topping up stacks first. Returns how many fit. Server only.

<a name="modinventory-take"></a>

#### Take

```csharp
bool Take(Player player, string itemId, int count = 1)
```

Takes items from a player, as a recipe would: all of them, or none (returning false) if they don't carry that many. Server only.

<a name="modinventory-count"></a>

#### Count

```csharp
int Count(Player player, string itemId)
```

How many of an item a player carries, where recipes look: pockets, bags, the tool belt and tool slots. Server only.

<a name="modinventory-getitems"></a>

#### GetItems

```csharp
List<InventoryEntry> GetItems(Player player)
```

Everything a player carries, one entry per stack. Server only.

<a name="inventoryentry"></a>

## InventoryEntry

*struct*

One stack in a player's inventory.

### Properties

<a name="inventoryentry-itemid"></a>

#### ItemId

```csharp
string ItemId { get; }
```

The item's id, e.g. "planks" or "nails:nail".

<a name="inventoryentry-count"></a>

#### Count

```csharp
int Count { get; }
```

<a name="inventorycategory"></a>

## InventoryCategory

*enum*

The groups items are sorted into.

| Value | Description |
| --- | --- |
| `Misc` |  |
| `Tool` |  |
| `Wood` |  |
| `Food` |  |
| `Medical` |  |
| `Gear` |  |

<a name="inventoryspec"></a>

## InventorySpec

*class*

How a mod's item or tool sits in the inventory, as the `inventory` part of its spec, e.g. `"inventory": { "size": [2, 1], "weight": 1.5, "category": "Tool" }`.

### Fields

<a name="inventoryspec-storable"></a>

#### Storable

```csharp
bool Storable = true
```

False to keep it out of inventories: it can only be dragged around.

<a name="inventoryspec-size"></a>

#### Size

```csharp
Vector2Int Size = Vector2Int.one
```

Cells it takes up, [width, height]. 1 to 6 each.

<a name="inventoryspec-weight"></a>

#### Weight

```csharp
float Weight = 1f
```

Kilograms, as the inventory counts it.

<a name="inventoryspec-category"></a>

#### Category

```csharp
InventoryCategory Category = InventoryCategory.Misc
```

<a name="inventoryspec-belt"></a>

#### Belt

```csharp
bool Belt
```

Allowed in the tool belt. Tools always are.

<a name="inventoryspec-description"></a>

#### Description

```csharp
string Description
```

Shown when the item's card is open.

<a name="inventoryitemspec"></a>

## InventoryItemSpec

*class*

An item with no model that only lives in inventories, e.g. a mod's `inventory/nail.json`: `{ "id": "nail", "name": "Nails", "icon": "icons/nail.png", "maxStack": 50 }`. It can't be dropped into the world.

### Fields

<a name="inventoryitemspec-id"></a>

#### Id

```csharp
string Id
```

The id within the mod: lowercase letters, digits and '\_'.

<a name="inventoryitemspec-name"></a>

#### Name

```csharp
string Name
```

What players see.

<a name="inventoryitemspec-description"></a>

#### Description

```csharp
string Description
```

<a name="inventoryitemspec-icon"></a>

#### Icon

```csharp
string Icon
```

A square .png, relative to the mod's folder. Optional.

<a name="inventoryitemspec-size"></a>

#### Size

```csharp
Vector2Int Size = Vector2Int.one
```

Cells it takes up, [width, height]. 1 to 6 each.

<a name="inventoryitemspec-maxstack"></a>

#### MaxStack

```csharp
int MaxStack = 1
```

How many fit in one stack, 1 to 999.

<a name="inventoryitemspec-weight"></a>

#### Weight

```csharp
float Weight = 0.1f
```

Kilograms each.

<a name="inventoryitemspec-category"></a>

#### Category

```csharp
InventoryCategory Category = InventoryCategory.Misc
```

<a name="inventoryitemspec-belt"></a>

#### Belt

```csharp
bool Belt
```

Allowed in the tool belt.
