using System.Collections.Generic;
using MLib.Common.Utilities;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.NPCs.Hostile;
using TerrariaParadox.Content.NPCs.Hostile.Miniboss;
using TerrariaParadox.Content.NPCs.Hostile.Worms;
using TerrariaParadox.Content.Tiles.Blocks;
using TerrariaParadox.Content.Tiles.Plants;
using TerrariaParadox.Content.Tiles.Walls;

namespace TerrariaParadox;

// Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
public partial class TerrariaParadox : Mod
{
    #region Unload

    public override void Unload()
    {
        ParadoxSystem.AssimilatedBlocks.Clear();
        ParadoxSystem.AssimilatedWalls.Clear();
    }

    #endregion


    #region Load

    public override void Load()
    {
        ApplyMethodSwaps();
        ParadoxSystem.AssimilatedBlocks = new Dictionary<ushort, ushort>
        {
            { TileID.Grass, (ushort)ModContent.TileType<FlippedGrassBlock>() },
            { TileID.JungleGrass, (ushort)ModContent.TileType<FlippedJungleGrassBlock>() },
            { TileID.MushroomGrass, (ushort)ModContent.TileType<FlippedJungleGrassBlock>() },
            { TileID.Stone, (ushort)ModContent.TileType<AssecstoneBlockTile>() },
            { TileID.IceBlock, (ushort)ModContent.TileType<MurkyIceBlockTile>() },
            { TileID.Sand, (ushort)ModContent.TileType<AssecsandBlockTile>() },
            { TileID.HardenedSand, (ushort)ModContent.TileType<HardenedAssecsandBlockTile>() },
            { TileID.Sandstone, (ushort)ModContent.TileType<AssecsandstoneBlockTile>() },
            { TileID.Plants, (ushort)ModContent.TileType<FlippedGrassPlants>() },
            { TileID.Plants2, (ushort)ModContent.TileType<FlippedGrassPlants>() },
            { TileID.Vines, (ushort)ModContent.TileType<FlippedVine>()},
            { TileID.JungleThorns, (ushort)ModContent.TileType<FlippedThorns>() },
        };
        ParadoxSystem.AssimilatedWalls = new Dictionary<ushort, ushort>
        {
            { WallID.Stone, (ushort)ModContent.WallType<AssecstoneWallTileUnsafe>() },

            { WallID.Cave1Echo, (ushort)ModContent.WallType<AssecstoneWallTileUnsafe>() },
            { WallID.CaveUnsafe, (ushort)ModContent.WallType<AssecstoneWallTileUnsafe>() },
            { WallID.Cave2Echo, (ushort)ModContent.WallType<AssecstoneWallTileUnsafe>() },
            { WallID.Cave2Unsafe, (ushort)ModContent.WallType<AssecstoneWallTileUnsafe>() },
            { WallID.Cave3Echo, (ushort)ModContent.WallType<AssecstoneWallTileUnsafe>() },
            { WallID.Cave3Unsafe, (ushort)ModContent.WallType<AssecstoneWallTileUnsafe>() },
            { WallID.Cave4Echo, (ushort)ModContent.WallType<AssecstoneWallTileUnsafe>() },
            { WallID.Cave4Unsafe, (ushort)ModContent.WallType<AssecstoneWallTileUnsafe>() },
            { WallID.Cave5Echo, (ushort)ModContent.WallType<AssecstoneWallTileUnsafe>() },
            { WallID.Cave5Unsafe, (ushort)ModContent.WallType<AssecstoneWallTileUnsafe>() },
            { WallID.Cave6Echo, (ushort)ModContent.WallType<AssecstoneWallTileUnsafe>() },
            { WallID.Cave6Unsafe, (ushort)ModContent.WallType<AssecstoneWallTileUnsafe>() },
            { WallID.Cave7Echo, (ushort)ModContent.WallType<AssecstoneWallTileUnsafe>() },
            { WallID.Cave7Unsafe, (ushort)ModContent.WallType<AssecstoneWallTileUnsafe>() },
            { WallID.Cave8Echo, (ushort)ModContent.WallType<AssecstoneWallTileUnsafe>() },
            { WallID.Cave8Unsafe, (ushort)ModContent.WallType<AssecstoneWallTileUnsafe>() },
            { WallID.Sandstone, (ushort)ModContent.WallType<AssecsandstoneWallTileUnsafe>() },
            { WallID.SandstoneEcho, (ushort)ModContent.WallType<AssecsandstoneWallTileUnsafe>() },
            { WallID.Grass, (ushort)ModContent.WallType<FlippedGrassWallTileUnsafe>() },
            { WallID.GrassUnsafe, (ushort)ModContent.WallType<FlippedGrassWallTileUnsafe>() },
            { WallID.HardenedSand, (ushort)ModContent.WallType<HardenedAssecsandWallTileUnsafe>() },
            { WallID.HardenedSandEcho, (ushort)ModContent.WallType<HardenedAssecsandWallTileUnsafe>() }
        };
        ParadoxNPC.FlipsideEnemies = new List<int>()
        {
            ModContent.NPCType<Swarm>(),
            ModContent.NPCType<AssimilatedDemonEye>(),
            ModContent.NPCType<Flatworm>(),
            ModContent.NPCType<WalkingHive>()
        };
    }

    private void LoadClient()
    {
    }

    #endregion
}