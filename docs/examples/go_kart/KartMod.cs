// The kart itself is all JSON (vehicles/kart.json). This script gives its
// "Honk" option something to do: a horn everyone hears, and a count kept on
// each kart (saved with it) that shows in the option's label.
public sealed class KartMod : Mod
{
    private const string KartId = "go_kart:kart";

    public override void OnLoad()
    {
        Items.OnInteract(KartId, "honk", Honk);

        // Answer straight away on the honker's own machine, so it feels
        // instant; the server plays it for everyone else.
        Events.InteractedLocally += used =>
        {
            if (used.Item.ItemId == KartId && used.InteractionId == "honk")
                Log.Info("Beep beep!");
        };
    }

    private void Honk(InteractEvent used)
    {
        WorldItem kart = used.Item;
        int honks = kart.GetInt("honks") + 1;

        kart.SetData("honks", honks);
        kart.SetData("honkLabel", $"Honk ({honks})");

        // Effects.PlaySoundForAll("sounds/horn.wav", kart.Position, 0.8f, UnityEngine.Random.Range(0.95f, 1.05f));
    }
}
