using Terraria;
using Terraria.ModLoader;

namespace TerrariaParadox.Content.Debuffs.DoT;

public class LeecharangBleed : ModBuff
{
    public override string LocalizationCategory => "Debuffs";
    public override void Update(NPC npc, ref int buffIndex)
    {
        npc.GetGlobalNPC<ParadoxNPC>().LeecharangBleed = true;
    }
}
