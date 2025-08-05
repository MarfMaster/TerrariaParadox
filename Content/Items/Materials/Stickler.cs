using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaParadox.Content.Items.Materials;

public class Stickler : ModItem
{
    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Orange;
        Item.value = PriceByRarity.fromItem(Item) / 35;
    }
}