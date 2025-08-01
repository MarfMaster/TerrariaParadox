using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Tiles.Blocks;

namespace TerrariaParadox.Content.Items.Tiles.Blocks;

public class Leechwood : ModdedBlockItem
{
    public override int TileType => ModContent.TileType<LeechwoodTile>();
}