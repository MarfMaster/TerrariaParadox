using MLib.Common.Items;
using MLib.Common.Utilities;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Items.Tiles.Blocks;
using TerrariaParadox.Content.Tiles.Bars;

namespace TerrariaParadox.Content.Items.Tiles.Bars;

public class ChitiniteBar : ModdedBarItem
{
    public override int TileType => ModContent.TileType<ChitiniteBarTile>();

    public override void CustomSetDefaults()
    {
        Item.rare = ItemRarityID.Blue;
        Item.value = PriceByRarity.fromItem(Item) / 15;
    }

    public override void AddRecipes()
    {
        CreateRecipe().AddIngredient(ModContent.ItemType<ChitiniteOre>(), 3).AddTile(TileID.Furnaces).Register();
    }
}