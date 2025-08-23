using Microsoft.Xna.Framework;
using MLib.Common.Tiles;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Dusts.Tiles.Blocks;

namespace TerrariaParadox.Content.Tiles.Blocks;

public class AssecstoneBrickTile : ModdedBlockTile
{
    public override bool SolidBlock => true;
    public override bool MergesWithDirt => true;
    public override int OnMineDustType => ModContent.DustType<AssecstoneDust>();
    public override ushort VanillaFallbackTileAndMerge => TileID.GrayBrick;
    public override SoundStyle TileMineSound => SoundID.Tink;
    public override Color MapColor => new(69, 79, 101);
    public override int WaterfallStyleID => WaterStyleID.Corrupt;
    public override bool MergesWithItself => false;
    public override bool NameShowsOnMapHover => false;

    public override void CustomSetStaticDefaults()
    {
        Main.tileBrick[Type] = true;
    }
}