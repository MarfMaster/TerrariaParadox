using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Dusts.Tiles.Blocks;

namespace TerrariaParadox.Content.Tiles.Walls;

public class AssecstoneWallTileUnsafe : ModdedWallTile
{
    public override bool PlayerPlaced => false;
    public override int OnMineDustType => ModContent.DustType<AssecstoneDust>();
    public override ushort VanillaFallbackTile => WallID.EbonstoneUnsafe;
    public override Color MapColor => new Color(31, 36, 48);
    public override void CustomSetStaticDefaults()
    {
        WallID.Sets.Conversion.Stone[Type] = true;
        Main.wallBlend[Type] = WallID.Stone;
        TerrariaParadox.WallHammerRequirement[Type] = 70;
        WallID.Sets.CannotBeReplacedByWallSpread[Type] = true;
    }
}