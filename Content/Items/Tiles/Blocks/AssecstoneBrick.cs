using Terraria.ModLoader;
using TerrariaParadox.Content.Tiles.Blocks;

namespace TerrariaParadox.Content.Items.Tiles.Blocks;

public class AssecstoneBrick : ModdedBlockItem
{
    public override int TileType => ModContent.TileType<AssecstoneBrickTile>();
}