using Microsoft.Xna.Framework;
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
}