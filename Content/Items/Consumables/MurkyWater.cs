using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Items.Tiles.Blocks;
using TerrariaParadox.Content.Items.Tiles.Plants;
using TerrariaParadox.Content.Projectiles.Consumables;

namespace TerrariaParadox.Content.Items.Consumables;

public class MurkyWater : ModItem
{
    public override string LocalizationCategory => "Items.Consumables";

    public override void SetDefaults()
    {
        Item.useStyle = 1;
        Item.shootSpeed = 9f;
        Item.rare = 3;
        Item.damage = 20;
        Item.shoot = ModContent.ProjectileType<MurkyWaterProjectile>();
        Item.width = 18;
        Item.height = 20;
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true;
        Item.knockBack = 3f;
        Item.UseSound = SoundID.Item1;
        Item.useAnimation = 15;
        Item.useTime = 15;
        Item.noUseGraphic = true;
        Item.noMelee = true;
        Item.value = 100;
    }

    public override void AddRecipes()
    {
        CreateRecipe(10).AddIngredient(ItemID.Bottle, 10).AddIngredient(ModContent.ItemType<AssecsandBlock>())
            .AddIngredient(ModContent.ItemType<FlippedSeeds>()).Register();
    }
}