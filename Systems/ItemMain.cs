using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Items;

namespace TerrariaParadox;

public partial class ParadoxItem : GlobalItem
{
    public override bool? UseItem(Item item, Player player)
    {
        /*if (item.buffType != 0 && BuffID.Sets.IsAFlaskBuff[item.buffType]) //tried this before I realized there's a buff bool that takes care of it
        {
            foreach (int buffType in player.buffType)
            {
                if (BuffID.Sets.IsAFlaskBuff[buffType] && buffType != item.buffType)
                {
                    player.ClearBuff(buffType);
                    return true;
                }
            }
        }*/
        return base.UseItem(item, player);
    }
}