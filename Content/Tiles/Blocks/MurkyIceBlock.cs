using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Dusts.Tiles.Blocks;

namespace TerrariaParadox.Content.Tiles.Blocks;
public class MurkyIceBlockTile : ModdedBlockTile
{
    public override bool SolidBlock => true;
    public override bool MergesWithDirt => true;
    public override int OnMineDustType => ModContent.DustType<MurkyIceDust>();
    public override ushort VanillaFallbackTileAndMerge => TileID.IceBlock;
    public override SoundStyle TileMineSound => SoundID.Item50; //Ice Block mine sound
    public override Color MapColor => new Color(42, 42, 79);
    public override int WaterfallStyleID => WaterStyleID.Corrupt;
    public override bool MergesWithItself => true;
    public override bool NameShowsOnMapHover => false;
    public override void CustomSetStaticDefaults()
    {
        TileID.Sets.Conversion.Ice[Type] = true;
        TileID.Sets.Ices[Type] = true;
        TileID.Sets.IceSkateSlippery[Type] = true;
        TileID.Sets.IcesSlush[Type] = true;
        TileID.Sets.IcesSnow[Type] = true;
        TileID.Sets.CanBeClearedDuringGeneration[Type] = true;
        Main.tileMerge[Type][TileID.SnowBlock] = true;
        Main.tileMerge[TileID.SnowBlock][Type] = true;
        Main.tileMerge[TileID.Slush][Type] = true;
        Main.tileMerge[Type][TileID.Slush] = true;
    }
}