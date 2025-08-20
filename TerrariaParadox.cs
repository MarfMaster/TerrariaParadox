using System;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Tiles.Blocks;

namespace TerrariaParadox;

// Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
public partial class TerrariaParadox : Mod
{
    private static int[] wallHammerRequirement;

    public static List<int> InfestedBlocks;
    public static int[] WallHammerRequirement => wallHammerRequirement;
    public static bool[] TileTransformsOnKill { get; private set; }

    #region Unload

    public override void Unload()
    {
        UnpopulateArrays();
    }

    #endregion

    internal static void PopulateArrays()
    {
        Array.Resize(ref wallHammerRequirement, WallLoader.WallCount);
        TileTransformsOnKill = TileID.Sets.Factory.CreateBoolSet(false);
        InfestedBlocks = new List<int>
        {
            ModContent.TileType<FlippedGrassBlock>(),
            ModContent.TileType<FlippedJungleGrassBlock>(),
            ModContent.TileType<AssecstoneBlockTile>(),
            ModContent.TileType<MurkyIceBlockTile>(),
            ModContent.TileType<AssecsandBlockTile>(),
            ModContent.TileType<HardenedAssecsandBlockTile>(),
            ModContent.TileType<AssecsandstoneBlockTile>()
        };
    }

    private void UnpopulateArrays()
    {
        InfestedBlocks = null;
    }

    #region Load

    public override void Load()
    {
        ApplyMethodSwaps();
    }

    private void LoadClient()
    {
    }

    #endregion
}