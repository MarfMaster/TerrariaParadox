using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Items.Tiles.Blocks;

namespace TerrariaParadox.Content.Items.Weapons.Melee;

public class LeechwoodSword : ModdedBasicItem
{
    public override string LocalizationCategory => "Items.Weapons.Melee";
    public override int Damage => 12;
    public override int UseTime => 18;
    public override int UseAnimation => 18;
    public override float Knockback => 6.5f;
    public override DamageClass DamageType => DamageClass.Melee;
    public override int UseStyle => ItemUseStyleID.Swing;
    public override bool DealsContactDamage => true;
    public override int Width => 32;
    public override int Height => 32;
    public override int Rarity => ItemRarityID.White;
    public override int Value => PriceByRarity.fromItem(Item);
    public override SoundStyle UseSound => SoundID.Item1;
    public override bool UseTurn => true;
    public override void AddRecipes()
    {
        CreateRecipe().
            AddIngredient(ModContent.ItemType<Leechwood>(), 7).
            AddTile(TileID.WorkBenches).
            Register();
    }
}