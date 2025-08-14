using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TerrariaParadox.Content.Items.Armor;

[AutoloadEquip(EquipType.Head)]
public class ChitiniteHelmet : ModdedHelmet
{
    public const float AttackSpeed = 6f;
    public const int SetBonusDefense = 10;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AttackSpeed);
    public override int Defense => 7;
    public override int Rarity => ItemRarityID.Blue;
    public override int Value => PriceByRarity.fromItem(Item);
    public override string HasArmorSetBonusName => "Chitinite";
    public override int BodyType => ModContent.ItemType<ChitiniteBreastplate>();
    public override int LegsType => ModContent.ItemType<ChitiniteGreaves>();
    public override float SetBonusStat0 => SetBonusDefense;
    public override float SetBonusStat1 { get; }
    public override float SetBonusStat2 { get; }
    public override float SetBonusStat3 { get; }

    public override void EquipEffects(Player player)
    {
        player.GetAttackSpeed(DamageClass.Generic) += AttackSpeed / 100f;
    }

    public override void ArmorSetBonus(Player player)
    {
        player.statDefense += SetBonusDefense;
        player.GetModPlayer<ParadoxPlayer>().ChitiniteFlip = true;
    }

    public override void ArmorSetShadows(Player player)
    {
        player.armorEffectDrawOutlinesForbidden = true;
        player.armorEffectDrawShadowLokis = true;
    }
}