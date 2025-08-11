using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Dusts.Tiles.Blocks;

namespace TerrariaParadox.Content.Tiles.Walls;

public class AssecsandstoneWallTileUnsafe : ModdedWallTile
{
    public override bool PlayerPlaced => false;
    public override int OnMineDustType => ModContent.DustType<AssecsandstoneDust>();
    public override ushort VanillaFallbackTile => WallID.CorruptSandstone;
    public override Color MapColor => new Color(25, 26, 40);    
    public override void CustomSetStaticDefaults()
    {
        WallID.Sets.Conversion.Sandstone[Type] = true;
        WallID.Sets.CannotBeReplacedByWallSpread[Type] = true;
    }
}