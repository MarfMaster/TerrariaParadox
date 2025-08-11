using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Items.Tiles.Blocks;
using TerrariaParadox.Content.Projectiles.Weapons.Ranged;

namespace TerrariaParadox.Content.Items.Weapons.Ranged;
[Autoload(false)]
public class Parasyte : ModdedBasicItem
{
    public override string LocalizationCategory => "Items.Weapons.Ranged";
    public override int Damage => 50;
    public override int UseTime => 60;
    public override int UseAnimation => 60;
    public override float Knockback => 7.25f;
    public override DamageClass DamageType => DamageClass.Ranged;
    public override int UseStyle => ItemUseStyleID.Shoot;
    public override bool DealsContactDamage => false;
    public override int Width => 16;
    public override int Height => 32;
    public override int Rarity => ItemRarityID.White;
    public override int Value => PriceByRarity.fromItem(Item);
    public override SoundStyle UseSound => SoundID.Item5;
    public override bool UseTurn => false;
    public override void CustomSetDefaults()
    {
        Item.shoot = ProjectileID.PurificationPowder;
        Item.shootSpeed = 6.7f;
        Item.useAmmo = AmmoID.Bullet;
        Item.holdStyle = ItemHoldStyleID.HoldHeavy;
        Item.noUseGraphic = true;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type,
        int damage, float knockback)
    {
        Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<ParasyteUpperArm>(), damage, knockback, player.whoAmI, type);
        return false;
    }


    public override void HoldItem(Player player)
    {
        if (player.ownedProjectileCounts[ModContent.ProjectileType<ParasyteUpperArm>()] == 0 
            && player.ownedProjectileCounts[ModContent.ProjectileType<ParasyteArmHeld>()] == 0 && !player.dead && player.whoAmI == Main.myPlayer)
        {
            Projectile.NewProjectile(Projectile.InheritSource(player), player.Center, Vector2.Zero, ModContent.ProjectileType<ParasyteArmHeld>(), 0, 0, player.whoAmI);
        }
    }
}