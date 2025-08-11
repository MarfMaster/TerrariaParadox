using Terraria;
using Terraria.ModLoader;

namespace TerrariaParadox.Content.Debuffs.DoT;

public class LeecharangBleed : ModBuff
{
    public override string LocalizationCategory => "Debuffs";
    public override void Update(NPC npc, ref int buffIndex)
    {
        npc.GetGlobalNPC<ParadoxNPC>().LeecharangBleed = true;
        if (npc.buffTime[buffIndex] == 1) //buff is about to run out
        {
            npc.GetGlobalNPC<ParadoxNPC>().LeecharangBleedStacks = 0;
        }
    }
}
