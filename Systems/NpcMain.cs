using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Items.Weapons.Melee;

namespace TerrariaParadox;

public partial class ParadoxNPC : GlobalNPC
{
    public override void ModifyHitPlayer(NPC npc, Player target, ref Player.HurtModifiers modifiers)
    {
        if (Stickled || npc.type == NPCID.SkeletonArcher)
        {
            modifiers.IncomingDamageMultiplier *= 10f - (Content.Debuffs.Stickled.DamageReduction / 100f);
        }
    }
    public override void UpdateLifeRegen(NPC npc, ref int damage)
    {
        if (LeecharangBleed)
        {
            int DoTPerS = Leecharang.DebuffDotPerSecond * LeecharangBleedStacks;
            npc.lifeRegen -= DoTPerS * 2;
            damage += DoTPerS + 1;
        }
    }

    public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
    {
        if (spawnInfo.Player.InModBiome<Content.Biomes.TheFlipside.BiomeMainSurface>())
        {
            pool.Remove(0);
        }
    }
}