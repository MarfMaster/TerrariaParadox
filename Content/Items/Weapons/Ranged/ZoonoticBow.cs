using MLib.Common.Items;
using MLib.Common.Utilities;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Items.Tiles.Bars;

namespace TerrariaParadox.Content.Items.Weapons.Ranged;

public class ZoonoticBow : ModdedBasicItem
{
    public override string LocalizationCategory => "Items.Weapons.Ranged";
    public override int Damage => 20;
    public override int UseTime => 24;
    public override int ItemUseAnimation => 24;
    public override float Knockback => 1.5f;
    public override DamageClass DamageType => DamageClass.Ranged;
    public override int ItemUseStyle => ItemUseStyleID.Shoot;
    public override bool DealsContactDamage => false;
    public override int Width => 18;
    public override int Height => 32;
    public override int Rarity => ItemRarityID.Blue;
    public override int Value => PriceByRarity.fromItem(Item);
    public override SoundStyle UseSound => SoundID.Item5;
    public override bool UseTurn => false;

    public override void SetDefaults()
    {
        base.SetDefaults();
        Item.shoot = ProjectileID.PurificationPowder;
        Item.shootSpeed = 7f;
        Item.useAmmo = AmmoID.Arrow;
    }

    public override void AddRecipes()
    {
        CreateRecipe().AddIngredient(ModContent.ItemType<ChitiniteBar>(), 8).AddTile(TileID.Anvils).Register();
    }
}