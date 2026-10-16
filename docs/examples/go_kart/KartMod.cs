// The kart itself is all JSON (vehicles/kart.json). This script gives its
// "Honk" option something to do: a horn everyone hears, and a count kept on
// each kart (saved with it) that shows in the option's label.
public sealed class KartMod : Mod
{
    private const string KartId = "go_kart:kart";

    public override void OnLoad()
    {
        Items.OnInteract(KartId, "honk", Honk);
        Items.OnInteract(KartId, "repaint", Repaint);

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

        // The headlights are the paint here: the body glows for a moment.
        kart.SetGlow("Body", new Color(1f, 0.45f, 0.1f) * 2f);
        Timers.After(0.3f, () =>
        {
            if (kart.IsValid)
                kart.SetGlow("Body", Color.black);
        });
    }

    // A new colour for this kart only. Look changes are item data, so every
    // player sees it and it's saved with the kart.
    private void Repaint(InteractEvent used)
    {
        Color colour = Color.HSVToRGB(UnityEngine.Random.value, 0.8f, 0.9f);
        used.Item.SetColor("Body", colour);
    }
}
