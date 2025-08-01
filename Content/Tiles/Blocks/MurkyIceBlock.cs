using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaParadox.Content.Tiles.Blocks;
public class MurkyIceBlockTile : ModdedBlockTile
{
    public override bool SolidBlock => true;
    public override bool MergesWithDirt => true;
    public override int OnMineDustType => DustID.Ice;
    public override ushort VanillaFallbackTile => TileID.CorruptIce;
    public override SoundStyle TileMineSound => SoundID.Item50; //Ice Block mine sound
    public override Color MapColor => new Color(42, 42, 79);
    public override int WaterfallStyleID => WaterStyleID.Corrupt;
}