// An example gameplay mod. Save this file while the game is running and the
// mod reloads by itself.
//
// Shows: a tool (tools/pickaxe.json) and what happens when it's used, an
// item that only lives in inventories (inventory/stone.json), recipes from
// a file (recipes/pickaxe.json) and from code, giving and counting what a
// player carries, and the Crafted event.
public sealed class StoneTools : Mod
{
    private const string Pickaxe = "stone_tools:pickaxe";
    private const string Stone = "stone_tools:stone";

    // Swings at the ground since each player's last stone, by player id.
    private readonly Dictionary<string, int> _swings = new();

    public override void OnLoad()
    {
        // Content can come from code as well as files.
        Inventory.Register(new InventoryItemSpec
        {
            Id = "stone_brick",
            Name = "Stone Brick",
            Description = "Three stones, squared up at a workbench.",
            Icon = "icons/stone.png",
            MaxStack = 10,
            Weight = 4f
        });

        Crafting.AddRecipe(new RecipeSpec
        {
            Id = "stone_brick",
            Output = "stone_brick",
            Station = "workbench",
            Category = RecipeCategory.Materials,
            Seconds = 2f,
            Ingredients = { ["stone"] = 3 }
        });

        Tools.OnUse(Pickaxe, OnPickaxe);

        Events.Crafted += crafted =>
        {
            if (crafted.Output == Pickaxe)
                crafted.Player.SendMessage("Put the pickaxe in your hotbar and swing it at the ground to mine stone.");
        };

        Commands.Register("stone", "How much stone you're carrying.", call =>
            call.Reply($"You have {Inventory.Count(call.Player, Stone)} stone."));
    }

    // Server only, like every tool use: the game has already checked the
    // pickaxe's cooldown and worked out what the swing hit.
    private void OnPickaxe(ToolUseEvent used)
    {
        if (used.HitKind != ToolHitKind.Ground)
            return;

        // Every third swing at the ground breaks a stone loose.
        _swings.TryGetValue(used.Player.Id, out int swings);
        swings++;

        if (swings < 3)
        {
            _swings[used.Player.Id] = swings;
            return;
        }

        _swings[used.Player.Id] = 0;

        if (Inventory.Give(used.Player, Stone) == 0)
            used.Player.SendMessage("No room for any more stone.");
    }
}
