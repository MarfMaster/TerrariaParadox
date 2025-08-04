using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaParadox.Content.Items.Armor;

[AutoloadEquip(EquipType.Head)]
public class ChitiniteHelmet : ModdedHelmet
{
    public override int Defense => 7;
    public override int Rarity => ItemRarityID.Blue;
    public override int Value => PriceByRarity.fromItem(Item);
    public override string HasArmorSetBonusName => "Chitinite";
    public override int BodyType => ModContent.ItemType<ChitiniteBreastplate>();
    public override int LegsType => ModContent.ItemType<ChitiniteGreaves>();
    public override void ArmorSetBonus(Player player)
    {
        player.statDefense += 10;
    }
}