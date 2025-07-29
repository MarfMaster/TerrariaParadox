using Terraria;
using Terraria.ModLoader;
using TerrariaParadox.Content.NPCs.Global;

namespace TerrariaParadox.Content.Debuffs.DoT;

public class LeecharangBleed : ModBuff
{
    public override void Update(NPC npc, ref int buffIndex)
    {
        npc.GetGlobalNPC<NpcDoTSystem>().LeecharangBleed = true;
    }
}
