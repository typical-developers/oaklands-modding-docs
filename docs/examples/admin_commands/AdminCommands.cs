using System.Globalization;

public sealed class AdminCommands : Mod
{

    public override void OnLoad()
    {
        Commands.Register("spawn", "Spawn items in front of you.", Spawn)
            .Arg(CommandArg.Item())
            .Arg(CommandArg.Integer("count", 1, 20).Optional())
            .HostOnly();

        Commands.Register("time", "Show the time of day, or set it, e.g. noon, 18, 6:30 or 7pm.", TimeOfDay)
            .Arg(CommandArg.Custom("time", SuggestTimes).Optional())
            .Arg(CommandArg.Choice("am/pm", "am", "pm").Optional());

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

    // Time of day.

    private static readonly Dictionary<string, float> NamedTimes = new()
    {
        ["sunrise"] = 6f,
        ["morning"] = 9f,
        ["noon"] = 12f,
        ["day"] = 12f,
        ["afternoon"] = 15f,
        ["sunset"] = 18f,
        ["evening"] = 20f,
        ["night"] = 22f,
        ["midnight"] = 0f
    };

    // Anyone can ask the time; only the host can change it.
    private void TimeOfDay(CommandCall call)
    {
        if (!call.Has(0))
        {
            call.Reply($"It's {FormatTime(World.TimeOfDay)}.");
            return;
        }

        if (!call.Player.IsHost)
        {
            call.Reply("Only the host can change the time.");
            return;
        }

        string text = call.GetString(0) + call.GetString(1, "");

        if (!TryParseTime(text, out float hours))
        {
            call.Reply($"'{call.ArgumentText}' isn't a time. Try noon, 18, 6:30 or 7pm.");
            return;
        }

        World.TimeOfDay = hours;
        Chat.Broadcast($"{call.Player.Name} set the time to {FormatTime(hours)}.");
    }

    private static IEnumerable<string> SuggestTimes(CommandCompletion completion) => NamedTimes.Keys;

    // "noon", "18", "18.5", "6:30", "7pm", "7:15am".
    private static bool TryParseTime(string text, out float hours)
    {
        hours = 0f;
        text = text.Trim().ToLowerInvariant();

        if (NamedTimes.TryGetValue(text, out hours))
            return true;

        bool am = text.EndsWith("am");
        bool pm = text.EndsWith("pm");

        if (am || pm)
            text = text.Substring(0, text.Length - 2);

        string[] parts = text.Split(':');
        int minutes = 0;

        if (parts.Length > 2 ||
            !float.TryParse(parts[0], NumberStyles.Float, CultureInfo.InvariantCulture, out hours))
        {
            return false;
        }

        if (parts.Length == 2 &&
            (parts[1].Length != 2 ||
             !int.TryParse(parts[1], NumberStyles.None, CultureInfo.InvariantCulture, out minutes) ||
             minutes > 59))
        {
            return false;
        }

        if (am || pm)
        {
            // 12-hour clock: 12am is midnight, 12pm is noon.
            if (hours < 1f || hours >= 13f)
                return false;

            hours %= 12f;

            if (pm)
                hours += 12f;
        }
        else if (hours < 0f || hours > 24f)
        {
            return false;
        }

        hours = Mathf.Repeat(hours + minutes / 60f, 24f);
        return true;
    }

    // "2:45 PM", like the minimap.
    private static string FormatTime(float hours)
    {
        int minutes = Mathf.FloorToInt(Mathf.Repeat(hours, 24f) * 60f);
        int hour = minutes / 60;
        int minute = minutes % 60;
        int shown = hour % 12 == 0 ? 12 : hour % 12;
        return $"{shown}:{minute:00} {(hour < 12 ? "AM" : "PM")}";
    }
}
