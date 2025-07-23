using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaParadox.Content.Items.Weapons.Melee;

public class Leecharang : ModItem
{
    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Blue;
        Item.damage = 19;
        Item.width = 18;
        Item.height = 32;
        Item.knockBack = 6f;
        Item.DamageType = DamageClass.MeleeNoSpeed;
        Item.useAnimation = 14;
        Item.useTime = 14;
        Item.UseSound = SoundID.Item1;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = PriceByRarity.fromItem(Item);
        Item.noUseGraphic = true;
        Item.noMelee = true;
        Item.shootSpeed = 18;
        Item.shoot = ProjectileID.WoodenBoomerang;
    }
    public override bool CanUseItem(Player player)
    {
        // Ensures no more than one boomerang can be thrown out
        return player.ownedProjectileCounts[Item.shoot] < 1;
    }
}