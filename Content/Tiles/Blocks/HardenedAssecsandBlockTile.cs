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
    public override ushort VanillaFallbackTile => TileID.CorruptHardenedSand;
    public override SoundStyle TileMineSound => SoundID.Dig;
    public override Color MapColor => new Color(38, 41, 72);
    public override int WaterfallStyleID => WaterStyleID.Corrupt;
    public override void CustomSetStaticDefaults()
    {
        TileID.Sets.Conversion.HardenedSand[Type] = true;
    }
}