using Microsoft.Xna.Framework;
using MLib.Common.Tiles;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Dusts.Tiles.Blocks;

namespace TerrariaParadox.Content.Tiles.Blocks;

public class ChitiniteOreTile : ModdedBlockTile
{
    public override bool SolidBlock => true;
    public override bool MergesWithDirt => true;
    public override int OnMineDustType => ModContent.DustType<ChitiniteOreDust>();
    public override ushort VanillaFallbackTileAndMerge => TileID.Ebonstone;
    public override SoundStyle TileMineSound => SoundID.Tink;
    public override Color MapColor => new(46, 126, 113);
    public override bool MergesWithItself => true;
    public override bool NameShowsOnMapHover => true;

    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();
        Main.tileLighted[Type] = true;
        Main.tileMerge[ModContent.TileType<AssecstoneBlockTile>()][Type] = true;
        Main.tileMerge[Type][ModContent.TileType<AssecstoneBlockTile>()] = true;
        Main.tileMerge[TileID.Mud][Type] = true;
        Main.tileMerge[Type][TileID.Mud] = true;
    }

    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        r = 0.1f;
        g = 0.3f;
        b = 0.25f;
    }
}