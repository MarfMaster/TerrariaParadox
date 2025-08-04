using Terraria;
using Terraria.ModLoader;

namespace TerrariaParadox.Content.Items;

public abstract class ModdedBreastplate : ModItem
{
    public abstract int Defense { get; }
    public abstract int Rarity { get; }
    public abstract int Value { get; }
    public override void SetDefaults()
    {
        Item.defense = Defense;
        Item.rare = Rarity;
        Item.value = Value;
    }
    public override string LocalizationCategory => "Items.Armor";
}