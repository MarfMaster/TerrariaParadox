using Terraria.ModLoader;

namespace TerrariaParadox.Systems
{
    public class RecipeSystem : ModSystem
    {
        #region Recipes
        public override void AddRecipeGroups() => RecipeUtil.AddRecipeGroups();
        #endregion
    }
}