using Microsoft.Xna.Framework;
using MLib.Common.Tiles;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Dusts.Tiles.Blocks;

namespace TerrariaParadox.Content.Tiles.Blocks;

public class HardenedAssecsandBlockTile : ModdedBlockTile
{
    public override bool SolidBlock => true;
    public override bool MergesWithDirt => true;
    public override int OnMineDustType => ModContent.DustType<HardenedAssecsandDust>();
    public override ushort VanillaFallbackTileAndMerge => TileID.HardenedSand;
    public override SoundStyle TileMineSound => SoundID.Dig;
    public override Color MapColor => new(38, 41, 72);
    public override bool MergesWithItself => true;
    public override bool NameShowsOnMapHover => false;

    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();
        TileID.Sets.Conversion.HardenedSand[Type] = true;
        TileID.Sets.SandBiome[Type] = 1;
        TileID.Sets.ForAdvancedCollision.ForSandshark[Type] = true;
        TileID.Sets.isDesertBiomeSand[Type] = true;
        Main.tileMerge[ModContent.TileType<AssecsandBlockTile>()][Type] = true;
        Main.tileMerge[Type][ModContent.TileType<AssecsandBlockTile>()] = true;
        Main.tileMerge[ModContent.TileType<AssecsandstoneBlockTile>()][Type] = true;
        Main.tileMerge[Type][ModContent.TileType<AssecsandstoneBlockTile>()] = true;
        TileID.Sets.CanBeClearedDuringGeneration[Type] = false;
    }
}