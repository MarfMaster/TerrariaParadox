using System.Collections.Generic;
using Terraria.ModLoader;

namespace TerrariaParadox;

public partial class ParadoxSystem : ModSystem
{
    public static Dictionary<ushort, ushort> AssimilatedBlocks;
    public static Dictionary<ushort, ushort> AssimilatedWalls;
    
    public static bool[] TileTransformsOnKill { get; private set; }
    
    private static int[] _minHammer;
    public static int[] MinHammer => _minHammer;
    public static float[] FlippedBlockSpawnChance { get; private set; }
}