// An example scripted mod. Save this file while the game is running and the
// mod reloads by itself.
//
// Shows: chat commands with arguments and suggestions as you type, server
// events, timers, a HUD panel, a menu that frees the mouse, settings in the
// game's Settings screen, network messages from clients to the server, and
// planting the birch species from trees/birch.json.
public sealed class TimberMod : Mod
{
    private readonly Dictionary<string, int> _felled = new();
    private int _total;

    // Client side.
    private UIPanel _counter;
    private UILabel _counterText;
    private UIPanel _menu;

    public override void OnLoad()
    {
        Settings.AddToggle("show_counter", "Show Tree Counter",
            true, "Shows how many trees have fallen this session, in the corner.");
        Settings.AddChoice("counter_corner", "Counter Corner",
            new[] { "Top Left", "Top Right", "Bottom Right" }, 0, "Where the tree counter sits. Top Left by default, clear of the minimap.");
        Settings.Changed += _ => PlaceCounter();

        Events.TreeFelled += OnTreeFelled;
        Events.PlayerJoined += player => player.SendMessage($"Welcome, {player.Name}! This world runs Timber! - type /help.");

        Commands.Register("trees", "How many trees have fallen this session.", ShowTrees);

        Commands.Register("spawn", "Spawn items in front of you.", Spawn)
            .Arg(CommandArg.Item())
            .Arg(CommandArg.Integer("count", 1, 20).Optional())
            .HostOnly();

        Commands.Register("plant", "Plant a tree in front of you.", Plant)
            .Arg(CommandArg.Custom("species", _ => Trees.GetSpeciesIds()))
            .Arg(CommandArg.Integer("stage", 0, 3).Optional())
            .HostOnly();

        Commands.Register("launch", "Throw everything near you into the air.", Launch)
            .Arg(CommandArg.Number("power", 1, 30).Optional())
            .HostOnly();

        Commands.Register("whisper", "Send someone a private message.", Whisper)
            .Arg(CommandArg.Player())
            .Arg(CommandArg.Text("message"));

        // The spawn menu (F6) asks the server; only the host may spawn.
        Network.On("spawn", message =>
        {
            if (!message.Sender.IsHost)
            {
                message.Sender.SendMessage("Only the host can spawn items.");
                return;
            }

            SpawnFor(message.Sender, message.Data.GetString("id"), 1);
        });

        Events.ClientStarted += BuildUI;
        Events.ClientStopped += () =>
        {
            _counter?.Destroy();
            _menu?.Destroy();
            _counter = null;
            _menu = null;
        };

        Events.Tick += _ =>
        {
            if (_menu != null && Input.WasPressed(ModKey.F6))
                _menu.Toggle();
        };

        // Joining a world this mod didn't see start (e.g. after a reload).
        if (Context.IsClient)
            BuildUI();
    }

    // Trees.

    private void OnTreeFelled(TreeFelledEvent felled)
    {
        _felled.TryGetValue(felled.Species, out int count);
        _felled[felled.Species] = count + 1;
        _total++;

        // Every tenth tree, tell everyone - after the crash has died down.
        if (_total % 10 == 0)
            Timers.After(2f, () => Chat.Broadcast($"Timber! That's {_total} trees down."));

        // The host's own counter; other players' counters count what they hear about.
        if (_counterText != null)
            _counterText.Text = CounterText();
    }

    private string CounterText() => $"Trees down: {_total}";

    private void ShowTrees(CommandCall call)
    {
        if (_total == 0)
        {
            call.Reply("No trees have fallen yet.");
            return;
        }

        string bySpecies = string.Join(", ", _felled
            .OrderByDescending(pair => pair.Value)
            .Select(pair => $"{pair.Value} {pair.Key}"));

        call.Reply($"{_total} trees down: {bySpecies}.");
    }

    private void Plant(CommandCall call)
    {
        string species = call.GetString(0);
        int stage = call.Has(1) ? call.GetInt(1) : -1;
        Vector3 spot = World.GroundAt(call.Player.Position + call.Player.Forward * 4f);

        if (World.PlantTree(species, spot, UnityEngine.Random.Range(0f, 360f), stage))
            call.Reply($"Planted {species}.");
        else
            call.Reply($"Couldn't plant {species} there.");
    }

    // Items.

    private void Spawn(CommandCall call) => SpawnFor(call.Player, call.GetString(0), call.GetInt(1, 1));

    private void SpawnFor(Player player, string id, int count)
    {
        if (!Items.Exists(id))
        {
            player.SendMessage($"There's no item '{id}'.");
            return;
        }

        Vector3 front = player.Position + player.Forward * 2.5f;
        Quaternion facing = Quaternion.LookRotation(player.Forward);

        for (int i = 0; i < count; i++)
            World.SpawnItem(id, front + Vector3.up * (1f + i * 0.8f), facing);

        player.SendMessage($"Spawned {count} {id}.");
    }

    private void Launch(CommandCall call)
    {
        float power = call.GetFloat(0, 9f);
        List<WorldItem> items = World.FindItems(call.Player.Position, 8f);

        foreach (WorldItem item in items)
            item.Velocity = Vector3.up * power + UnityEngine.Random.insideUnitSphere * 3f;

        call.Reply($"Launched {items.Count} things.");
    }

    private void Whisper(CommandCall call)
    {
        Player to = call.GetPlayer(0);
        to.SendMessage($"{call.Player.Name} whispers: {call.GetText(1)}");
        call.Reply($"You whisper to {to.Name}: {call.GetText(1)}");
    }

    // UI.

    private void BuildUI()
    {
        if (_counter != null)
            return;

        _counterText = UI.Label(CounterText()).FontSize(18).Bold().TextColor(Color.white);
        _counter = UI.Panel("Tree Counter")
            .Padding(8, 14)
            .Background(new Color(0f, 0f, 0f, 0.45f))
            .Rounded(10);
        _counter.Add(_counterText);
        PlaceCounter();

        _menu = UI.Panel("Spawn Menu")
            .Anchor(UIAnchor.Center)
            .Width(360)
            .Padding(16)
            .Background(new Color(0.08f, 0.09f, 0.1f, 0.92f))
            .Rounded(14);

        _menu.Add(UI.Label("Spawn an item").FontSize(22).Bold().TextColor(Color.white).Margin(0, 0));
        _menu.Add(UI.Label("Press F6 to close. Only the host can spawn.").FontSize(13).TextColor(new Color(1f, 1f, 1f, 0.6f)).Margin(4, 0));

        UIScroll list = UI.Scroll().Height(300).Margin(8, 0);

        foreach (string id in Items.GetIds().OrderBy(id => id))
        {
            string chosen = id;
            list.Add(UI.Button(id, () => Network.SendToServer("spawn", new NetData().Set("id", chosen)))
                .Class("form-button").FontSize(16)
                .Margin(3, 0));
        }

        _menu.Add(list);
        _menu.Add(UI.Button("Close", () => _menu.Hide()).Class("form-button").Margin(6, 0));
        _menu.CapturesMouse = true;
        _menu.Hide();
    }

    private void PlaceCounter()
    {
        if (_counter == null)
            return;

        _counter.Visible = Settings.GetBool("show_counter");

        UIAnchor corner = Settings.GetChoice("counter_corner") switch
        {
            0 => UIAnchor.TopLeft,
            2 => UIAnchor.BottomRight,
            _ => UIAnchor.TopRight
        };

        _counter.Anchor(corner, 20, 20);
    }
}
