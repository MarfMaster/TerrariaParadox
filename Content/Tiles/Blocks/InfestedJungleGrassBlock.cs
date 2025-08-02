using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Dusts.Tiles.Blocks;

namespace TerrariaParadox.Content.Tiles.Blocks;

public class InfestedJungleGrassBlock : ModdedBlockTile
{
    public override bool SolidBlock => true;
    public override bool MergesWithDirt => true;
    public override int OnMineDustType => DustID.Dirt;
    public override ushort VanillaFallbackTile => TileID.CorruptJungleGrass;
    public override SoundStyle TileMineSound => SoundID.Dig;
    public override Color MapColor => new Color(69, 79, 101);
    public override int WaterfallStyleID => WaterStyleID.Corrupt;
    public override void CustomSetStaticDefaults()
    {
        TileID.Sets.CanBeDugByShovel[Type] = true;
        TileID.Sets.Mud[Type] = true;
        TileID.Sets.Conversion.JungleGrass[Type] = true;
    }
}