using MLib.Common.Items;
using MLib.Common.Utilities;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Tiles.Blocks;

namespace TerrariaParadox.Content.Items.Tiles.Blocks;

public class ChitiniteOre : ModdedBlockItem
{
    public override int TileType => ModContent.TileType<ChitiniteOreTile>();

    public override void SetDefaults()
    {
        base.SetDefaults();
        Item.rare = ItemRarityID.Blue;
        Item.value = PriceByRarity.fromItem(Item) / 45;
    }
}