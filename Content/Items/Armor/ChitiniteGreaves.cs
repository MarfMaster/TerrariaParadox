using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaParadox.Content.Items.Armor;

[AutoloadEquip(EquipType.Legs)]
public class ChitiniteGreaves : ModdedGreaves
{
    public override int Defense => 7;
    public override int Rarity => ItemRarityID.Blue;
    public override int Value => PriceByRarity.fromItem(Item);
}