using Microsoft.Xna.Framework;
using MLib.Common.Items;
using MLib.Common.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Projectiles.Weapons.Ranged;

namespace TerrariaParadox.Content.Items.Weapons.Ranged;

public class Parasyte : ModdedBasicItem
{
    public override string LocalizationCategory => "Items.Weapons.Ranged";
    public override int Damage => 50;
    public override int UseTime => 60;
    public override int ItemUseAnimation => UseTime;
    public override float Knockback => 11.25f;
    public override DamageClass DamageType => DamageClass.Ranged;
    public override int ItemUseStyle => ItemUseStyleID.Shoot;
    public override bool DealsContactDamage => false;
    public override int Width => 80;
    public override int Height => 30;
    public override int Rarity => ItemRarityID.Blue;
    public override int Value => PriceByRarity.fromItem(Item);
    public override SoundStyle UseSound => SoundID.NPCHit8.WithVolumeScale(0.5f);
    public override bool UseTurn => false;

    public override void SetStaticDefaults()
    {
    }

    public override void CustomSetDefaults()
    {
        Item.crit = 30;
        Item.shoot = ProjectileID.PurificationPowder;
        Item.shootSpeed = 10f;
        Item.useAmmo = AmmoID.Bullet;
        Item.noUseGraphic = true;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity,
        int type,
        int damage, float knockback)
    {
        var projectile = type;
        var finalVelocity = velocity;
        player.PickAmmo(Item, out var shoot, out var speed, out var ammoDamage, out var ammoKnockback,
            out var ammoType);
        switch (ammoType)
        {
            case int normal when (ammoType == ItemID.MusketBall) | (ammoType == ItemID.SilverBullet) |
                                 (ammoType == ItemID.TungstenBullet):
            {
                projectile = ProjectileID.BulletHighVelocity;
                finalVelocity = velocity / 3;
                break;
            }
        }

        Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<ParasyteHeldProjectile>(),
            damage, knockback, player.whoAmI, 0, projectile);
        return false;
    }

    public override void HoldItem(Player player)
    {
    }
}