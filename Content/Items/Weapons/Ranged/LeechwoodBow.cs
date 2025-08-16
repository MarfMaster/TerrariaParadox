using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Items.Tiles.Blocks;

namespace TerrariaParadox.Content.Items.Weapons.Ranged;

public class LeechwoodBow : ModdedBasicItem
{
    public override string LocalizationCategory => "Items.Weapons.Ranged";
    public override int Damage => 9;
    public override int UseTime => 25;
    public override int ItemUseAnimation => 25;
    public override float Knockback => 0.25f;
    public override DamageClass DamageType => DamageClass.Ranged;
    public override int ItemUseStyle => ItemUseStyleID.Shoot;
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
        Item.useAmmo = AmmoID.Arrow;
    }
    public override void AddRecipes()
    {
        CreateRecipe().
            AddIngredient(ModContent.ItemType<Leechwood>(), 10).
            AddTile(TileID.WorkBenches).
            Register();
    }
}