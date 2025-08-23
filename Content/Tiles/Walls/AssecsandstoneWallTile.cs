using Microsoft.Xna.Framework;
using MLib.Common.Tiles;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Dusts.Tiles.Blocks;

namespace TerrariaParadox.Content.Tiles.Walls;

public class AssecsandstoneWallTile : ModdedWallTile
{
    public override bool PlayerPlaced => true;
    public override int OnMineDustType => ModContent.DustType<AssecsandstoneDust>();
    public override ushort VanillaFallbackTile => WallID.CorruptSandstoneEcho;
    public override Color MapColor => new(25, 26, 40);

    public override void CustomSetStaticDefaults()
    {
        WallID.Sets.Conversion.Sandstone[Type] = true;
        WallID.Sets.CannotBeReplacedByWallSpread[Type] = true;
    }
}