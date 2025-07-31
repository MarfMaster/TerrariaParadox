using Terraria.ModLoader;
using TerrariaParadox.Content.Tiles.Blocks;

namespace TerrariaParadox.Content.Items.Tiles.Blocks;

public class InfestedGrassBlockItem : ModdedBlockItem
{
    public override int TileType => ModContent.TileType<InfestedGrassBlock>();
}