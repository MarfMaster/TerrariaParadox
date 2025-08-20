using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaParadox.Content.Items.Tiles;

public abstract class ModdedFurnitureItem : ModItem
{
    public override string LocalizationCategory => "Items.Tiles.Furniture";
    public abstract int TileType { get; }
    public abstract bool Craftable { get; }
    public abstract int MaterialType { get; }
    public abstract int MaterialAmount { get; }

    public virtual void CustomSetStaticDefaults()
    {
    }

    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
        CustomSetStaticDefaults();
    }

    public virtual void CustomSetDefaults()
    {
    }

    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(TileType);
        CustomSetDefaults();
    }

    /// <summary>
    ///     Add any extra ingredients to the recipe here
    /// </summary>
    public virtual void RecipeExtraIngredients(Recipe recipe)
    {
    }

    public override void AddRecipes()
    {
        if (Craftable)
        {
            var recipe = CreateRecipe();
            recipe.AddIngredient(MaterialType, MaterialAmount);
            RecipeExtraIngredients(recipe);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}