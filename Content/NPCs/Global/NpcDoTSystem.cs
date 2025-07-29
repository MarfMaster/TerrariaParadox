using Terraria;
using Terraria.ModLoader;
using TerrariaParadox.Content.Items.Weapons.Melee;

namespace TerrariaParadox.Content.NPCs.Global;

public class NpcDoTSystem : GlobalNPC
{
    public override bool InstancePerEntity => true;
    public bool LeecharangBleed;
    public override void ResetEffects(NPC npc)
    {
        LeecharangBleed = false;
    }

    public override void UpdateLifeRegen(NPC npc, ref int damage)
    {
        if (LeecharangBleed)
        {
            int DoTPerS = Leecharang.DebuffDotPerSecond;
            npc.lifeRegen -= DoTPerS * 2;
            damage += DoTPerS;
        }
    }
}