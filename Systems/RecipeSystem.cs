using Terraria.ModLoader;

namespace TerrariaParadox
{
    public class RecipeSystem : ModSystem
    {
        #region Recipes
        public override void AddRecipeGroups() => RecipeUtil.AddRecipeGroups();
        #endregion
    }
}