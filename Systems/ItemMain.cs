using System.Collections.Generic;
using MLib.Common.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.NPCs.Hostile;

namespace TerrariaParadox;

public partial class ParadoxItem : GlobalItem
{
    public override void SetStaticDefaults()
    {
        ItemsWithExtraTooltips = new List<int>
        {
            ItemID.HotlineFishingHook
        };
        TooltipHelper.GlobalRegisterExtraTooltips(Mod, ItemsWithExtraTooltips, ExtraTooltipsWithKeys1, "One");
    }

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


    public override bool? CanHitNPC(Item item, Player player, NPC target)
    {
        if ((item.type == ItemID.BugNet || item.type == ItemID.FireproofBugNet || item.type == ItemID.GoldenBugNet) &&
            target.type == ModContent.NPCType<Swarm>()) return true;
        return base.CanHitNPC(item, player, target);
    }

    public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage)
    {
        if (item.type == ItemID.BugNet)
            base.ModifyWeaponDamage(item, player, ref damage);
    }


    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        foreach (var KeyAndValue in ExtraTooltipsWithKeys1)
            TooltipHelper.SimpleTooltip(KeyAndValue.Key, item, Mod, tooltips, ExtraTooltipsWithKeys1[KeyAndValue.Key]);
    }
}