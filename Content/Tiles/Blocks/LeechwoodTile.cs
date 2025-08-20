using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Dusts.Tiles.Blocks;

namespace TerrariaParadox.Content.Tiles.Blocks;

public class LeechwoodTile : ModdedBlockTile
{
    public override bool SolidBlock => true;
    public override bool MergesWithDirt => true;
    public override int OnMineDustType => ModContent.DustType<LeechwoodDust>();
    public override ushort VanillaFallbackTileAndMerge => TileID.WoodBlock;
    public override SoundStyle TileMineSound => SoundID.Dig;
    public override Color MapColor => new(37, 37, 50);
    public override int WaterfallStyleID => WaterStyleID.Corrupt;
    public override bool MergesWithItself => false;
    public override bool NameShowsOnMapHover => false;
}