using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Projectiles.Weapons.Magic;

namespace TerrariaParadox.Content.Items.Weapons.Magic;

public class Lighter : ModdedBasicItem
{
    public override string LocalizationCategory => "Items.Weapons.Magic";
    public override int Damage => 10;
    public override int UseTime => 24;
    public override int UseAnimation => 24;
    public override float Knockback => 0.5f;
    public override DamageClass DamageType => DamageClass.Magic;
    public override int UseStyle => ItemUseStyleID.Shoot;
    public override bool DealsContactDamage => false;
    public override int Width => 10;
    public override int Height => 24;
    public override int Rarity => ItemRarityID.Blue;
    public override int Value => PriceByRarity.fromItem(Item);
    public override SoundStyle UseSound => SoundID.Item5;
    public override bool UseTurn => false;
    public const int DebuffDuration = 5 * 60;
    public override void CustomSetDefaults()
    {
        Main.RegisterItemAnimation(Type, new DrawAnimationVertical(10, 3));
        Item.mana = 5;
        Item.shoot = ModContent.ProjectileType<LighterSpark>();
        Item.shootSpeed = 7f;
        Item.holdStyle = ItemHoldStyleID.HoldFront;
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        float numberProjectiles = 3;
        float rotation = MathHelper.ToRadians(12);

        //position += Vector2.Normalize(velocity) * 2f;
        for (int i = 0; i < numberProjectiles; i++) 
        {
            Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))); // Watch out for dividing by 0 if there is only 1 projectile.
            Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
        }

        return false; // return false to stop vanilla from calling Projectile.NewProjectile.
    }

    public override void HoldStyle(Player player, Rectangle heldItemFrame)
    {
        if (player.direction == 1)
        {
            player.itemLocation += new Vector2(-9, -14);
        }
        else
        {
            player.itemLocation += new Vector2(+9, -14);
        }
    }

    public override void HoldItem(Player player)
    {
        Lighting.AddLight(player.Center, new Vector3(1.98f, 1.19f, 0.31f) * 0.33f);
    }
}