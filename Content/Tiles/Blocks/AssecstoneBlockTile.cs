using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Dusts.Tiles.Blocks;

namespace TerrariaParadox.Content.Tiles.Blocks;

public class AssecstoneBlockTile : ModdedBlockTile
{
    public override bool SolidBlock => true;
    public override bool MergesWithDirt => true;
    public override int OnMineDustType => ModContent.DustType<AssecstoneDust>();
    public override ushort VanillaFallbackTileAndMerge => TileID.Stone;
    public override SoundStyle TileMineSound => SoundID.Tink;
    public override Color MapColor => new Color(69, 79, 101);
    public override int WaterfallStyleID => WaterStyleID.Corrupt;
    public override bool MergesWithItself => true;
    public override bool NameShowsOnMapHover => false;
    public override void CustomSetStaticDefaults()
    {
        MinPick = 65;
        MineResist = 2f;
        TileID.Sets.Conversion.Stone[Type] = true;
        TileID.Sets.Stone[Type] = true;
        TileID.Sets.CanBeClearedDuringOreRunner[Type] = true;
        TileID.Sets.CanBeClearedDuringGeneration[Type] = true;
    }
}