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
    public override Color LightColor => new Color(0.28f, 1.98f, 1.135f);
    public override Color MapColor => Color.LimeGreen;
    public override bool CanFunctionInLiquids => false;
    public override int VanillaFallbackTile => TileID.Torches;
    public override bool BelongsToAModdedBiome => true;
    public override ModBiome ModdedBiomeForLuck => ModContent.GetInstance<FBiomeMainSurface>();
}