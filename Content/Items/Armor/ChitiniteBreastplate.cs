using MLib.Common.Items;
using MLib.Common.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TerrariaParadox.Content.Items.Armor;

[AutoloadEquip(EquipType.Body)]
public class ChitiniteBreastplate : ModdedBreastplate
{
    public const float AttackSpeed = 6f;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AttackSpeed);
    public override int Defense => 8;
    public override int Rarity => ItemRarityID.Blue;
    public override int Value => PriceByRarity.fromItem(Item);

    public override void EquipEffects(Player player)
    {
        player.GetAttackSpeed(DamageClass.Generic) += AttackSpeed / 100f;
    }
}