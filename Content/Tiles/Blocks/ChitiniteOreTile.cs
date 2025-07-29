using Microsoft.Xna.Framework;
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
    public override ushort VanillaFallbackTile => TileID.Demonite;
    public override SoundStyle TileMineSound => SoundID.Tink;
    public override Color MapColor => new Color(46, 126, 113);
    public override int WaterfallStyleID => WaterStyleID.Corrupt;
}