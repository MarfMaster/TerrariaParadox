using Terraria.ModLoader;
using TerrariaParadox.Content.Tiles.Walls;

namespace TerrariaParadox.Content.Items.Tiles.Walls;

public class FlippedGrassWall : ModdedWallItem
{
    public override int WallType => ModContent.WallType<FlippedGrassWallTile>();
}