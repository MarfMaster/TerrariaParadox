using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaParadox.Content.Tiles.Blocks;

[Autoload(false)]
public class MurkyIceBlockTile : ModdedBlockTile
{
    public override bool SolidBlock => true;
    public override bool MergesWithDirt => false;
    public override int OnMineDustType => DustID.Ebonwood;
    public override ushort VanillaFallbackTile => TileID.CorruptIce;
    public override SoundStyle TileMineSound => SoundID.Item50; //Ice Block mine sound
    public override Color MapColor => new Color(255, 255, 255);
    public override int WaterfallStyleID => WaterStyleID.Corrupt;
}