using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaParadox.Content.Items.TreasureBags;

public class NyxarBag : ModdedBossBag
{
    public override bool PreHardmodeBossBag => true;

    public override void ModifyItemLoot(ItemLoot itemLoot)
    {
        itemLoot.Add(ItemDropRule.Common(ItemID.DemoniteBar, 1, 10, 20));
    }
}