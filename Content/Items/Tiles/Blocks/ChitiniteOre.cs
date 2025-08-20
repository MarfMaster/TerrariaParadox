using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Tiles.Blocks;

namespace TerrariaParadox.Content.Items.Tiles.Blocks;

public class ChitiniteOre : ModdedBlockItem
{
    public override int TileType => ModContent.TileType<ChitiniteOreTile>();

    public override void CustomSetDefaults()
    {
        Item.rare = ItemRarityID.Blue;
        Item.value = PriceByRarity.fromItem(Item) / 45;
    }
}