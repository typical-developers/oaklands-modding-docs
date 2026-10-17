
public sealed class AdminCommands : Mod
{

    public override void OnLoad()
    {
        Commands.Register("spawn", "Spawn items in front of you.", Spawn)
            .Arg(CommandArg.Item())
            .Arg(CommandArg.Integer("count", 1, 20).Optional())
            .HostOnly();

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
}
