using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using TerrariaParadox.Content.Dusts.Tiles.Blocks;

namespace TerrariaParadox.Content.Tiles.Plants;

public class FlippedThorns : ModdedBlockTile
{
    public override bool SolidBlock => false;
    public override bool MergesWithDirt => false;
    public override int OnMineDustType => ModContent.DustType<AssecstoneDust>();
    public override ushort VanillaFallbackTileAndMerge => TileID.CorruptThorns;
    public override SoundStyle TileMineSound => SoundID.Grass;
    public override Color MapColor => new Color(57, 63, 75);
    public override int WaterfallStyleID => WaterStyleID.Corrupt;
    public override bool MergesWithItself => false;
    public override bool NameShowsOnMapHover => true;
    public override void CustomSetStaticDefaults()
    {
        Main.tileCut[Type] = true;
        Main.tileLavaDeath[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileNoAttach[Type] = true;
        Main.tileWaterDeath[Type] = true;
        TileID.Sets.TouchDamageImmediate[Type] = 20;
        TileID.Sets.TouchDamageBleeding[Type] = true;
        TileID.Sets.TouchDamageDestroyTile[Type] = true;
    }

    public override bool IsTileDangerous(int i, int j, Player player)
    {
        return true;
    }
}