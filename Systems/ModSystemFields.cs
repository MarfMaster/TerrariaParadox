using System.Collections.Generic;
using Terraria.ModLoader;

namespace TerrariaParadox;

public partial class ParadoxSystem : ModSystem
{
    private static int[] wallHammerRequirement;

    public static Dictionary<ushort, ushort> AssimilatedBlocks;
    public static Dictionary<ushort, ushort> AssimilatedWalls;
    public static int[] WallHammerRequirement => wallHammerRequirement;
    public static bool[] TileTransformsOnKill { get; private set; }
}