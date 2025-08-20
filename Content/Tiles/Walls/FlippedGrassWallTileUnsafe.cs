using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Dusts.Tiles.Blocks;

namespace TerrariaParadox.Content.Tiles.Walls;

public class FlippedGrassWallTileUnsafe : ModdedWallTile
{
    public override bool PlayerPlaced => false;
    public override int OnMineDustType => ModContent.DustType<AssecstoneDust>();
    public override ushort VanillaFallbackTile => WallID.CorruptGrassUnsafe;
    public override Color MapColor => new(44, 58, 57);

    public override void CustomSetStaticDefaults()
    {
        WallID.Sets.Conversion.Grass[Type] = true;
        Main.wallBlend[Type] = WallID.GrassUnsafe;
        WallID.Sets.CannotBeReplacedByWallSpread[Type] = true;
    }
}