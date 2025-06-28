using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaParadox.Items.Weapons.Summon
{
    public class Suspicious8Ball:ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Summon";

        public override void SetDefaults()
        {
            Item.width = Item.height = 32;
            Item.height = 32;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;

            Item.DamageType = DamageClass.Summon;
            Item.mana = 17;
            Item.damage = 20;
            Item.knockBack = 4f;
            Item.useTime = 36;
        }

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient(ItemID.SoulofNight, 5).
                AddIngredient(ItemID.JojaCola).
                AddRecipeGroup("AnyCobaltBar", 5).
                AddTile(TileID.Anvils).
                Register();
        }
    }
}