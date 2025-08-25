using MLib.Common.Items;
using MLib.Common.Tiles.Furniture;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Items.Tiles.Blocks;
using TerrariaParadox.Content.Tiles.Furniture.Leechwood;

namespace TerrariaParadox.Content.Items.Tiles.Furniture;

public class LeechwoodChest : ModdedFurnitureItem
{
    public override int TileType => ModContent.TileType<ChestTile>();
    public override bool Craftable => true;
    public override int MaterialType => ModContent.ItemType<Leechwood>();
    public override int MaterialAmount => ModdedChestTile.MaterialAmount;

    public override void RecipeExtraIngredients(Recipe recipe)
    {
        recipe.AddRecipeGroup(RecipeGroupID.IronBar, 2);
    }
}