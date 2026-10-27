// An example scripted mod: a little worker bot. Save this file while the
// game is running and the mod reloads by itself.
//
// Shows: a model and an icon drawn in code (Meshes, Images), an item that
// walks like a character and finds its way round things (SetWalker, MoveTo),
// chopping trees from a script (FindTrees, ChopTree), a rope drawn on each
// client (Effects.CreateLine), and values kept with the world (WorldData).
//
// Type /bot to get one. Look at it for Follow Me, Chop Trees and Stay.
public sealed class WorkerBotMod : Mod
{
    private const string BotId = "worker_bot:bot";

    // What each bot is doing, kept in its data so it's saved with it and
    // every client can read it (for the tether).
    private const string JobKey = "job";
    private const string OwnerKey = "owner";

    private readonly Dictionary<WorldItem, WorldTree> _targets = new();
    private readonly Dictionary<WorldItem, Vector3> _followGoals = new();
    private readonly Dictionary<WorldItem, Line> _tethers = new();

    // WorldData holds what was saved only once the world has loaded.
    private bool _loaded;

    public override void OnLoad()
    {
        string model = Meshes.Register("bot", BuildModel());
        string icon = Images.Register("bot", DrawIcon());

        Items.Register(new ItemSpec
        {
            Id = "bot",
            Name = "Worker Bot",
            Model = model,
            Icon = icon,
            Mass = 40f,
            Materials = new Dictionary<string, string> { ["Body"] = "game:lightmetal" },
            Interactions = new List<InteractionSpec>
            {
                new() { Id = "follow", Label = "Follow Me" },
                new() { Id = "chop", Label = "Chop Trees" },
                new() { Id = "stay", Label = "Stay" }
            }
        });

        Commands.Register("bot", "Gives you a worker bot.", call => SpawnFor(call.Player));

        Items.OnInteract(BotId, null, used =>
        {
            used.Item.SetData(JobKey, used.InteractionId);
            used.Item.SetData(OwnerKey, used.Player.Id);
            used.Item.StopMoving();
            used.Player.SendMessage(used.InteractionId switch
            {
                "follow" => "The bot follows you.",
                "chop" => "The bot looks for trees.",
                _ => "The bot stays put."
            });
        });

        // One free bot each, the first time they play in this world.
        Events.WorldLoaded += () =>
        {
            _loaded = true;

            foreach (Player player in World.GetPlayers())
                GiveFirstBot(player);
        };

        Events.PlayerJoined += player =>
        {
            if (_loaded)
                GiveFirstBot(player);
        };

        Events.ServerStopped += () => _loaded = false;

        // A tree it can't get to: pick another next time.
        Events.MoveFinished += (bot, state) =>
        {
            if (state == MoveState.NoPath || state == MoveState.Stuck)
                _targets.Remove(bot);
        };

        Timers.Every(0.5f, Work);
        Events.Tick += _ => DrawTethers();
    }

    private void GiveFirstBot(Player player)
    {
        string key = "given:" + player.Id;

        if (WorldData.GetBool(key))
            return;

        WorldData.SetBool(key, true);
        SpawnFor(player);
    }

    private void SpawnFor(Player player)
    {
        WorldItem bot = World.SpawnItem(BotId, player.Position + player.Forward * 2f + Vector3.up * 0.2f);
        bot.SetWalker(new WalkerSpec { Radius = 0.35f, Height = 1.1f, Speed = 3f });
        bot.SetData(JobKey, "follow");
        bot.SetData(OwnerKey, player.Id);
    }

    // ---------------------------------------------------------------
    // Server: what each bot does, twice a second

    private void Work()
    {
        if (!Context.IsServer)
            return;

        foreach (WorldItem bot in World.FindItemsById(BotId))
        {
            // Bots from before a reload are still walkers: it's saved.
            if (!bot.IsWalker)
                bot.SetWalker(new WalkerSpec { Radius = 0.35f, Height = 1.1f, Speed = 3f });

            switch (bot.GetString(JobKey))
            {
                case "follow":
                    Follow(bot);
                    break;
                case "chop":
                    Chop(bot);
                    break;
            }
        }
    }

    private Player OwnerOf(WorldItem bot)
    {
        string id = bot.GetString(OwnerKey);
        return World.GetPlayers().FirstOrDefault(p => p.Id == id);
    }

    private void Follow(WorldItem bot)
    {
        Player owner = OwnerOf(bot);

        if (!owner.IsValid)
            return;

        float distance = Vector3.Distance(bot.Position, owner.Position);

        if (distance <= 3f || bot.MoveState == MoveState.Planning)
            return;

        // Already on the way there, or found no way there: only look again
        // once they've moved on.
        if (_followGoals.TryGetValue(bot, out Vector3 goal) &&
            Vector3.Distance(goal, owner.Position) < 2f &&
            bot.MoveState != MoveState.Idle && bot.MoveState != MoveState.Arrived)
        {
            return;
        }

        _followGoals[bot] = owner.Position;
        bot.MoveTo(owner.Position, distance > 10f ? 5f : 0f, 2f);
    }

    private void Chop(WorldItem bot)
    {
        if (!_targets.TryGetValue(bot, out WorldTree tree) || !tree.IsStanding)
        {
            // The nearest standing tree.
            tree = World.FindTrees(bot.Position, 40f)
                .Where(t => t.IsStanding)
                .OrderBy(t => Vector3.Distance(t.Position, bot.Position))
                .FirstOrDefault();

            if (!tree.IsValid)
                return;

            _targets[bot] = tree;
            bot.MoveTo(tree.Position, 0f, 1.3f);
            return;
        }

        if (bot.MoveState == MoveState.Walking || bot.MoveState == MoveState.Planning)
            return;

        if (Vector3.Distance(bot.Position, tree.Position) > 2.5f)
        {
            bot.MoveTo(tree.Position, 0f, 1.3f);
            return;
        }

        // Face it and swing: a bot is about as strong as an axe.
        Vector3 toward = tree.Position - bot.Position;
        toward.y = 0f;

        if (toward.sqrMagnitude > 0.01f)
            bot.Rotation = Quaternion.LookRotation(toward);

        if (World.ChopTree(tree, 12f, bot.Position) == ChopResult.Felled)
            _targets.Remove(bot);
    }

    // ---------------------------------------------------------------
    // Clients: a rope from a following bot to its owner

    private void DrawTethers()
    {
        if (!Context.IsClient)
            return;

        Player me = Context.LocalPlayer;
        var seen = new HashSet<WorldItem>();

        foreach (WorldItem bot in World.FindItems(me.Position, 60f))
        {
            if (bot.ItemId != BotId || bot.GetString(JobKey) != "follow" || bot.GetString(OwnerKey) != me.Id)
                continue;

            seen.Add(bot);

            if (!_tethers.TryGetValue(bot, out Line rope) || !rope.IsValid)
            {
                rope = Effects.CreateLine();
                rope.Width = 0.03f;
                rope.Color = new Color(1f, 0.8f, 0.2f);
                _tethers[bot] = rope;
            }

            rope.SetRope(bot.Position + Vector3.up * 0.9f, me.Position + Vector3.up * 1f, 0.4f);
        }

        foreach (WorldItem gone in _tethers.Keys.Where(b => !seen.Contains(b)).ToList())
        {
            _tethers[gone].Destroy();
            _tethers.Remove(gone);
        }
    }

    // ---------------------------------------------------------------
    // The look, made in code

    private static MeshBuilder BuildModel()
    {
        var mesh = new MeshBuilder();

        // Origin on the ground, +Z forward.
        mesh.Material = "Body";
        mesh.AddBox(new Vector3(0f, 0.55f, 0f), new Vector3(0.6f, 0.6f, 0.45f), Color.white);
        mesh.AddBox(new Vector3(0f, 1f, 0f), new Vector3(0.4f, 0.3f, 0.35f), Color.white);
        mesh.AddCylinder(new Vector3(-0.18f, 0f, 0f), new Vector3(-0.18f, 0.3f, 0f), 0.08f, Color.gray);
        mesh.AddCylinder(new Vector3(0.18f, 0f, 0f), new Vector3(0.18f, 0.3f, 0f), 0.08f, Color.gray);
        mesh.AddCylinder(new Vector3(0f, 1.15f, 0f), new Vector3(0f, 1.3f, 0f), 0.015f, Color.gray, sides: 6);

        mesh.Material = "Eye";
        mesh.AddSphere(new Vector3(-0.09f, 1.03f, 0.18f), 0.045f, new Color(0.2f, 0.9f, 1f), segments: 8);
        mesh.AddSphere(new Vector3(0.09f, 1.03f, 0.18f), 0.045f, new Color(0.2f, 0.9f, 1f), segments: 8);
        mesh.AddSphere(new Vector3(0f, 1.32f, 0f), 0.035f, new Color(1f, 0.3f, 0.2f), segments: 8);
        return mesh;
    }

    private static ImageBuilder DrawIcon()
    {
        var image = new ImageBuilder(32, 32);
        var body = new Color32(150, 160, 170, 255);
        var dark = new Color32(60, 66, 74, 255);
        var eye = new Color32(60, 230, 255, 255);

        image.FillRect(8, 14, 16, 12, body);
        image.DrawRect(8, 14, 16, 12, dark);
        image.FillRect(10, 5, 12, 8, body);
        image.DrawRect(10, 5, 12, 8, dark);
        image.FillCircle(13, 8, 1, eye);
        image.FillCircle(18, 8, 1, eye);
        image.DrawLine(16, 1, 16, 4, dark);
        image.SetPixel(16, 0, new Color32(255, 80, 60, 255));
        image.FillRect(10, 26, 3, 4, dark);
        image.FillRect(19, 26, 3, 4, dark);
        return image;
    }
}
