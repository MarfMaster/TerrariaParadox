using Terraria.ModLoader;
using TerrariaParadox.Content.Tiles.Blocks;

namespace TerrariaParadox.Content.Items.Tiles.Blocks;

public class AssecsandstoneBlock : ModdedBlockItem
{
    public override string Texture => "TerrariaParadox/Content/Items/Tiles/Blocks/AssecstoneBlock";
    public override int TileType => ModContent.TileType<AssecsandstoneBlockTile>();
}