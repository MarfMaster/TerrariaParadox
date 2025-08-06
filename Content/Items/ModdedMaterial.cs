using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaParadox.Content.Items;

public abstract class ModdedMaterial : ModItem
{
    public override string LocalizationCategory => "Items.Materials";
    public abstract int Rarity { get; }
    public abstract int Value { get; }
    public override void SetDefaults()
    {
        Item.rare = Rarity;
        Item.value = Value;
    }
}