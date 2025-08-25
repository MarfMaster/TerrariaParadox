using Microsoft.Xna.Framework;
using MLib.Common.Tiles;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Dusts.Tiles.Blocks;

namespace TerrariaParadox.Content.Tiles.Blocks;

public class AssecsandstoneBlockTile : ModdedBlockTile
{
    public override bool SolidBlock => true;
    public override bool MergesWithDirt => true;
    public override int OnMineDustType => ModContent.DustType<AssecsandstoneDust>();
    public override ushort VanillaFallbackTileAndMerge => TileID.Sandstone;
    public override SoundStyle TileMineSound => SoundID.Dig;
    public override Color MapColor => new(45, 59, 93);
    public override bool MergesWithItself => true;
    public override bool NameShowsOnMapHover => false;

    public override void CustomSetStaticDefaults()
    {
        TileID.Sets.Conversion.Sandstone[Type] = true;
        TileID.Sets.isDesertBiomeSand[Type] = true;
        TileID.Sets.ForAdvancedCollision.ForSandshark[Type] = true;
        TileID.Sets.CanBeClearedDuringGeneration[Type] = false;
        TileID.Sets.SandBiome[Type] = 1;
        Main.tileMerge[ModContent.TileType<AssecsandBlockTile>()][Type] = true;
        Main.tileMerge[Type][ModContent.TileType<AssecsandBlockTile>()] = true;
        Main.tileMerge[ModContent.TileType<HardenedAssecsandBlockTile>()][Type] = true;
        Main.tileMerge[Type][ModContent.TileType<HardenedAssecsandBlockTile>()] = true;
        Main.tileMerge[Type][TileID.DesertFossil] = true;
        Main.tileMerge[TileID.DesertFossil][Type] = true;
    }
}