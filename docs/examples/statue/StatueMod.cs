// An example scripted mod: a stone block you carve with a chisel. Save this
// file while the game is running and the mod reloads by itself.
//
// Shows: one model per item made in code and changed as it's worked on
// (Meshes.Register again under the same name, WorldItem.SetModel), a tool
// whose model and icon are drawn in code, and keeping the carving in the
// item's data so it reaches every player and is saved with the world.
//
// Type /statue for a block and a chisel. Hold the chisel and click the
// block to chip bits off.
public sealed class StatueMod : Mod
{
    private const string StatueId = "statue:statue";
    private const string ChiselId = "statue:chisel";

    // The block is Width x Height x Depth cubes of Cube metres, its bottom
    // in the middle on the ground.
    private const int Width = 12, Height = 18, Depth = 12;
    private const float Cube = 0.08f;
    private const int Cubes = Width * Height * Depth;

    // In each statue's data: its own name, and which cubes are gone (one
    // bit each, base64: 432 characters, well inside an item's 1024).
    private const string KeyKey = "key";
    private const string CarvedKey = "carved";

    // What each peer last built each statue's mesh from, by key.
    private readonly Dictionary<string, string> _built = new();
    private readonly System.Random _random = new();

    public override void OnLoad()
    {
        Items.Register(new ItemSpec
        {
            Id = "statue",
            Name = "Stone Block",
            Model = Meshes.Register("block", BuildMesh(new bool[Cubes])),
            Mass = 300f,
            Collider = ItemColliderShape.Convex,
            Inventory = new InventorySpec { Storable = false }
        });

        Tools.Register(new ToolSpec
        {
            Id = "chisel",
            Name = "Chisel",
            Model = Meshes.Register("chisel", BuildChisel()),
            Icon = Images.Register("chisel", DrawChiselIcon()),
            Swing = ToolSwing.Stab,
            UseSeconds = 0.3f,
            Range = 2.5f
        });

        Commands.Register("statue", "Gives you a block of stone to carve, and a chisel.", call =>
        {
            Player player = call.Player;
            WorldItem block = World.SpawnItem(StatueId, World.GroundAt(player.Position + player.Forward * 2f),
                Quaternion.LookRotation(-player.Forward));

            // Its own name, for its own mesh. Kinematic: a statue stays put.
            block.SetData(KeyKey, _random.Next(0x10000000, int.MaxValue).ToString("x8"));
            block.SetData(CarvedKey, Encode(new bool[Cubes]));
            block.IsKinematic = true;

            if (Inventory.Count(player, ChiselId) == 0)
                Inventory.Give(player, ChiselId);
        });

        Tools.OnUse(ChiselId, Chip);

        // Every peer keeps each statue's mesh up to date with its data; the
        // server also points the statue at it.
        Events.Tick += _ => UpdateMeshes();

        // Statues that are gone don't need their meshes.
        Timers.Every(10f, ForgetGone);
    }

    // ---------------------------------------------------------------
    // Server: a chip off

    private void Chip(ToolUseEvent used)
    {
        WorldItem statue = used.HitItem;

        if (used.HitKind != ToolHitKind.Item || statue.ItemId != StatueId)
            return;

        bool[] gone = Decode(statue.GetString(CarvedKey));

        // Into the block's own space. Its collider is the hull round what's
        // left, so walk on along the aim to the first cube still there.
        Quaternion back = Quaternion.Inverse(statue.Rotation);
        Vector3 origin = back * (used.AimOrigin - statue.Position);
        Vector3 direction = back * used.AimDirection;

        for (float distance = used.HitDistance - 0.05f; distance < used.HitDistance + 1.5f; distance += Cube / 3f)
        {
            Vector3 point = origin + direction * distance;

            if (!TryCell(point, out int x, out int y, out int z) || gone[Index(x, y, z)])
                continue;

            // A small round chip: this cube and the nearest round it.
            for (int dz = -1; dz <= 1; dz++)
            for (int dy = -1; dy <= 1; dy++)
            for (int dx = -1; dx <= 1; dx++)
            {
                if (Mathf.Abs(dx) + Mathf.Abs(dy) + Mathf.Abs(dz) > 1)
                    continue;

                int cx = x + dx, cy = y + dy, cz = z + dz;

                if (cx >= 0 && cy >= 0 && cz >= 0 && cx < Width && cy < Height && cz < Depth)
                    gone[Index(cx, cy, cz)] = true;
            }

            statue.SetData(CarvedKey, Encode(gone));
            return;
        }
    }

    // ---------------------------------------------------------------
    // Every peer: meshes from the data

    private void UpdateMeshes()
    {
        foreach (WorldItem statue in World.FindItemsById(StatueId))
        {
            string key = statue.GetString(KeyKey);
            string carved = statue.GetString(CarvedKey);

            if (key == null || carved == null)
                continue;

            // Made again under the same name: refilled in place, so the
            // statue only rebuilds its collider.
            if (!_built.TryGetValue(key, out string built) || built != carved)
            {
                string model = Meshes.Register("statue_" + key, BuildMesh(Decode(carved)));
                _built[key] = carved;

                if (Context.IsServer && statue.Model != model)
                    statue.SetModel(model);
            }
        }
    }

    private void ForgetGone()
    {
        var present = new HashSet<string>(World.FindItemsById(StatueId).Select(s => s.GetString(KeyKey)));

        foreach (string key in _built.Keys.Where(k => !present.Contains(k)).ToList())
        {
            Meshes.Remove("statue_" + key);
            _built.Remove(key);
        }
    }

    // ---------------------------------------------------------------
    // The models

    private static MeshBuilder BuildMesh(bool[] gone)
    {
        var mesh = new MeshBuilder();
        var origin = new Vector3(-Width * Cube * 0.5f, 0f, -Depth * Cube * 0.5f);

        for (int z = 0; z < Depth; z++)
        for (int y = 0; y < Height; y++)
        for (int x = 0; x < Width; x++)
        {
            if (gone[Index(x, y, z)])
                continue;

            // Marble, each cube a shade apart.
            float shade = 0.82f + 0.12f * Mathf.Abs(Mathf.Sin(x * 12.9898f + y * 78.233f + z * 37.719f));
            var colour = new Color(shade, shade * 0.98f, shade * 0.95f);
            Vector3 corner = origin + new Vector3(x, y, z) * Cube;

            // Only the faces that show: no cube beside them.
            for (int face = 0; face < 6; face++)
            {
                (int nx, int ny, int nz) = Normals[face];

                if (Solid(gone, x + nx, y + ny, z + nz))
                    continue;

                int[] c = Faces[face];
                mesh.AddQuad(corner + Corners[c[0]] * Cube, corner + Corners[c[1]] * Cube,
                    corner + Corners[c[2]] * Cube, corner + Corners[c[3]] * Cube, colour);
            }
        }

        return mesh;
    }

    // A cube's corners, and each face's four, clockwise seen from outside.
    private static readonly Vector3[] Corners =
    {
        new(0, 0, 0), new(1, 0, 0), new(0, 1, 0), new(1, 1, 0),
        new(0, 0, 1), new(1, 0, 1), new(0, 1, 1), new(1, 1, 1)
    };

    private static readonly (int, int, int)[] Normals =
    {
        (0, 0, 1), (0, 0, -1), (1, 0, 0), (-1, 0, 0), (0, 1, 0), (0, -1, 0)
    };

    private static readonly int[][] Faces =
    {
        new[] { 5, 7, 6, 4 }, // +Z
        new[] { 0, 2, 3, 1 }, // -Z
        new[] { 1, 3, 7, 5 }, // +X
        new[] { 4, 6, 2, 0 }, // -X
        new[] { 2, 6, 7, 3 }, // +Y
        new[] { 4, 0, 1, 5 }  // -Y
    };

    private static MeshBuilder BuildChisel()
    {
        // Held upright: the grip at the origin, the blade up.
        var mesh = new MeshBuilder();
        mesh.Material = "Handle";
        mesh.AddCylinder(new Vector3(0f, -0.06f, 0f), new Vector3(0f, 0.1f, 0f), 0.018f, new Color(0.55f, 0.36f, 0.2f), sides: 8);
        mesh.Material = "Blade";
        mesh.AddBox(new Vector3(0f, 0.16f, 0f), new Vector3(0.016f, 0.12f, 0.016f), new Color(0.7f, 0.72f, 0.75f));
        mesh.AddBox(new Vector3(0f, 0.225f, 0f), new Vector3(0.03f, 0.012f, 0.008f), new Color(0.8f, 0.82f, 0.85f));
        return mesh;
    }

    private static ImageBuilder DrawChiselIcon()
    {
        var image = new ImageBuilder(32, 32);
        var wood = new Color32(140, 92, 52, 255);
        var steel = new Color32(180, 186, 194, 255);
        var dark = new Color32(70, 60, 54, 255);

        // On a slant, blade at the top right.
        for (int i = 0; i < 12; i++)
            image.FillCircle(5 + i, 26 - i, 2, wood);

        image.DrawLine(16, 15, 26, 5, steel);
        image.DrawLine(17, 15, 27, 5, steel);
        image.DrawLine(16, 16, 27, 5, steel);
        image.DrawLine(25, 3, 29, 7, dark);
        return image;
    }

    // ---------------------------------------------------------------
    // The grid

    private static int Index(int x, int y, int z) => (z * Height + y) * Width + x;

    private static bool Solid(bool[] gone, int x, int y, int z) =>
        x >= 0 && y >= 0 && z >= 0 && x < Width && y < Height && z < Depth && !gone[Index(x, y, z)];

    private static bool TryCell(Vector3 local, out int x, out int y, out int z)
    {
        x = Mathf.FloorToInt(local.x / Cube + Width * 0.5f);
        y = Mathf.FloorToInt(local.y / Cube);
        z = Mathf.FloorToInt(local.z / Cube + Depth * 0.5f);
        return x >= 0 && y >= 0 && z >= 0 && x < Width && y < Height && z < Depth;
    }

    private static string Encode(bool[] gone)
    {
        var bytes = new byte[(Cubes + 7) / 8];

        for (int i = 0; i < Cubes; i++)
        {
            if (gone[i])
                bytes[i / 8] |= (byte)(1 << (i % 8));
        }

        return Convert.ToBase64String(bytes);
    }

    private static bool[] Decode(string text)
    {
        var gone = new bool[Cubes];

        if (string.IsNullOrEmpty(text))
            return gone;

        byte[] bytes = Convert.FromBase64String(text);

        for (int i = 0; i < Cubes && i / 8 < bytes.Length; i++)
            gone[i] = (bytes[i / 8] & (1 << (i % 8))) != 0;

        return gone;
    }
}
