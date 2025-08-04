using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Dusts.Tiles.Blocks;

namespace TerrariaParadox.Content.Tiles.Walls;

public class AssecsandstoneWallTile : ModdedWallTile
{
    public override bool PlayerPlaced => true;
    public override int OnMineDustType => ModContent.DustType<AssecsandstoneDust>();
    public override ushort VanillaFallbackTile => WallID.CorruptSandstoneEcho;
    public override Color MapColor => new Color(25, 26, 40);
}