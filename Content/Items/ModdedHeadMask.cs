using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaParadox.Content.Items;
public abstract class ModdedHeadMask : ModItem
{
    public override void SetDefaults()
    {
        Item.vanity = true;
        Item.rare = ItemRarityID.Blue;
        Item.value = PriceByRarity.fromItem(Item);
    }
}