using Terraria.ModLoader;
using TerrariaParadox.Content.Tiles.Blocks;
using TerrariaParadox.Content.Tiles.Walls;

namespace TerrariaParadox.Content.Items.Tiles.Walls;

public class AssecstoneBrickWall : ModdedWallItem
{
    public override string Texture => "TerrariaParadox/Content/Items/Tiles/Blocks/AssecstoneBlock";
    public override int WallType => ModContent.WallType<AssecstoneBrickWallTile>();
}