using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Items.Materials;
using TerrariaParadox.Content.Projectiles.Consumables;

namespace TerrariaParadox.Content.Items.Consumables;

public class ParasiticPowder : ModItem
{
    public override string LocalizationCategory => "Items.Consumables";

    public override void SetDefaults()
    {
        Item.damage = 0;
        Item.useStyle = 1;
        Item.shootSpeed = 4f;
        Item.shoot = ModContent.ProjectileType<ParasiticPowderProjectile>();
        Item.width = 16;
        Item.height = 24;
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true;
        Item.UseSound = SoundID.Item1;
        Item.useAnimation = 15;
        Item.useTime = 15;
        Item.noMelee = true;
        Item.value = 100;
    }

    public override void AddRecipes()
    {
        CreateRecipe(5).AddIngredient(ModContent.ItemType<ParasiticMushroom>()).AddTile(TileID.Bottles).Register();
    }
}