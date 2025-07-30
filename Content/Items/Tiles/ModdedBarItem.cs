using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaParadox.Content.Items.Tiles;

public abstract class ModdedBarItem : ModdedBlockItem
{
    public override string LocalizationCategory => "Items.Tiles.Bars";
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 25;
        CustomSetStaticDefaults();
    }
}