using MLib.Common.Items;
using Terraria.ModLoader;
using TerrariaParadox.Content.Tiles.Blocks;

namespace TerrariaParadox.Content.Items.Tiles.Blocks;

public class MurkyIceBlock : ModdedBlockItem
{
    public override int TileType => ModContent.TileType<MurkyIceBlockTile>();
}