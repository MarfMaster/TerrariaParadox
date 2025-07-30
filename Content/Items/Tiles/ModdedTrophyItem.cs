using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaParadox.Content.Items.Tiles;

public abstract class ModdedTrophyItem : ModdedBlockItem
{
    public override string LocalizationCategory => "Items.Tiles.Trophies";
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }
    public override void CustomSetDefaults()
    {
        Item.rare = ItemRarityID.Blue;
        Item.value = PriceByRarity.fromItem(Item);
    }
}