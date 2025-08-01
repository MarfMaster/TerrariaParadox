using Terraria.ModLoader;
using TerrariaParadox.Content.Tiles.Blocks;
using TerrariaParadox.Content.Tiles.Walls;

namespace TerrariaParadox.Content.Items.Tiles.Walls;

public class AssecstoneWall : ModdedWallItem
{
    public override int WallType => ModContent.WallType<AssecstoneWallTile>();
}