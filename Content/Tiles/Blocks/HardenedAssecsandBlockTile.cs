using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria;
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
    public override Color MapColor => new Color(38, 41, 72);
    public override int WaterfallStyleID => WaterStyleID.Corrupt;
    public override bool MergesWithItself => true;
    public override bool NameShowsOnMapHover => false;
    public override void CustomSetStaticDefaults()
    {
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