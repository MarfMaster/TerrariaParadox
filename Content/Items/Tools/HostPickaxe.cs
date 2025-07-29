using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaParadox.Content.Items.Tools;

public class HostPickaxe : ModdedBasicItem
{
    public override int Damage => 13;
    public override int UseTime => 13;
    public override int UseAnimation => 18;
    public override float Knockback => 4;
    public override DamageClass DamageType => DamageClass.Melee;
    public override int UseStyle => ItemUseStyleID.Swing;
    public override bool DealsContactDamage => true;
    public override int Width => 32;
    public override int Height => 32;
    public override int Rarity => ItemRarityID.Blue;
    public override int Value => PriceByRarity.fromItem(Item);
    public override SoundStyle UseSound => SoundID.Item1;
    public override bool UseTurn => true;

    public override void CustomSetDefaults()
    {
        Item.pick = 85;
        Item.attackSpeedOnlyAffectsWeaponAnimation = true;
    }
}