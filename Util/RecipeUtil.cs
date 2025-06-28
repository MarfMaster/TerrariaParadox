using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace TerrariaParadox
{
    internal class RecipeUtil
    {
        #region Recipe Group Definitions
        public static int AnyCobaltBar;

        public static void AddRecipeGroups()
        {
            AddOreAndBarRecipeGroups();
        }

        private static void AddOreAndBarRecipeGroups()
        {
            // Cobalt and Palladium
            RecipeGroup group = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.CobaltBar)}", new int[]
            {
                ItemID.CobaltBar,
                ItemID.PalladiumBar
            });
            AnyCobaltBar = RecipeGroup.RegisterGroup("AnyCobaltBar", group);
        }
        #endregion
    }
}