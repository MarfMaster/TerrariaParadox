using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Items.Tiles.Bars;
using TerrariaParadox.Content.Projectiles.Weapons.Melee;

namespace TerrariaParadox.Content.Items.Weapons.Melee;

public class TarsalSaber : ModdedBasicItem
{
    public override string LocalizationCategory => "Items.Weapons.Melee";
    public override int Damage => 23;
    public override int UseTime => 19;
    public override int UseAnimation => 19;
    public override float Knockback => 5.5f;
    public override DamageClass DamageType => DamageClass.Melee;
    public override int UseStyle => ItemUseStyleID.Swing;
    public override bool DealsContactDamage => true;
    public override int Width => 48;
    public override int Height => 48;
    public override int Rarity => ItemRarityID.Blue;
    public override int Value => PriceByRarity.fromItem(Item);
    public override bool UseTurn => true;
    public override SoundStyle UseSound => SoundID.Item1;
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
        Projectile.NewProjectile(Item.GetSource_FromThis(), target.Center, player.Center.DirectionTo(target.Center) * 0.05f, ModContent.ProjectileType<TarsalSaberGnat>(), damageDone / 3, Knockback, Main.myPlayer, player.GetTotalCritChance(DamageClass.Melee) + Item.crit);
    }
    public override void AddRecipes()
    {
        CreateRecipe().
            AddIngredient(ModContent.ItemType<ChitiniteBar>(), 10).
            AddTile(TileID.Anvils).
            Register();
    }
}