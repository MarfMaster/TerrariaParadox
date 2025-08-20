using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Projectiles.Weapons.Melee.Boomerangs;

namespace TerrariaParadox.Content.Items.Weapons.Melee;

public class Leecharang : ModdedBasicItem
{
    public const float ShootSpeed = 12f / 2f;
    public const int DebuffDotPerSecond = 2;
    public const int DebuffMaxStacks = 5;
    public const int DebuffDuration = 10 * 60;
    public override string LocalizationCategory => "Items.Weapons.Melee";
    public override int Damage => 25;
    public override int UseTime => 18;
    public override int ItemUseAnimation => 18;
    public override float Knockback => 9;
    public override DamageClass DamageType => DamageClass.MeleeNoSpeed;
    public override int ItemUseStyle => ItemUseStyleID.Swing;
    public override bool DealsContactDamage => false;
    public override int Width => 18;
    public override int Height => 32;
    public override int Rarity => ItemRarityID.Blue;
    public override int Value => PriceByRarity.fromItem(Item);
    public override SoundStyle UseSound => SoundID.Item7;
    public override bool UseTurn => false;

    public override void CustomSetDefaults()
    {
        Item.noUseGraphic = true;
        Item.shootSpeed = ShootSpeed;
        Item.shoot = ModContent.ProjectileType<LeecharangProjectile>();
    }

    public override bool CanUseItem(Player player)
    {
        // Ensures no more than one boomerang can be thrown out
        return player.ownedProjectileCounts[Item.shoot] < 1;
    }
}