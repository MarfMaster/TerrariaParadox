using Microsoft.Xna.Framework;
using MLib.Common.Tiles;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Dusts.Tiles.Blocks;

namespace TerrariaParadox.Content.Tiles.Walls;

public class AssecstoneWallTile : ModdedWallTile
{
    public override bool PlayerPlaced => true;
    public override int OnMineDustType => ModContent.DustType<AssecstoneDust>();
    public override ushort VanillaFallbackTile => WallID.EbonstoneEcho;
    public override Color MapColor => new(31, 36, 48);

    public override void CustomSetStaticDefaults()
    {
        WallID.Sets.Conversion.Stone[Type] = true;
    }
}