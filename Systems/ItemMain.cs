using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Items;
using TerrariaParadox.Content.NPCs.Hostile;

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


    public override bool? CanHitNPC(Item item, Player player, NPC target)
    {
        if ((item.type == ItemID.BugNet || item.type == ItemID.FireproofBugNet || item.type == ItemID.GoldenBugNet) && target.type == ModContent.NPCType<Swarm>())
        {
            return true;
        }
        return base.CanHitNPC(item, player, target);
    }

    public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage)
    {
        if (item.type == ItemID.BugNet)
        base.ModifyWeaponDamage(item, player, ref damage);
    }
}