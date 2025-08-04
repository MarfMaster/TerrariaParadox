using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaParadox.Content.Items.Armor;

[AutoloadEquip(EquipType.Body)]
public class ChitiniteBreastplate : ModdedBreastplate
{
    public override int Defense => 8;
    public override int Rarity => ItemRarityID.Blue;
    public override int Value => PriceByRarity.fromItem(Item);
}