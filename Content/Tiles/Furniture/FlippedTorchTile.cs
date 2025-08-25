using Microsoft.Xna.Framework;
using MLib.Common.Tiles.Furniture;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Biomes.TheFlipside;
using TerrariaParadox.Content.Dusts.Tiles.Misc;

namespace TerrariaParadox.Content.Tiles.Furniture;

public class FlippedTorchTile : ModdedTorchTile
{
    public override int SparkleDustType => ModContent.DustType<BioluminescentBulbDust>();
    public override Color LightColor => new Color(0.0028f, 0.0198f, 0.0135f) * 0.5f;
    public override Color MapColor => Color.LimeGreen;
    public override bool CanFunctionInWater => false;
    public override bool CanFunctionInLava => false;
    public override int VanillaFallbackTile => TileID.Torches;
    public override bool BelongsToAModdedBiome => true;
    public override ModBiome ModdedBiomeForLuck => ModContent.GetInstance<FBiomeMainSurface>();
}