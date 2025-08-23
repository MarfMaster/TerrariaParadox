using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Biomes.TheFlipside;
using TerrariaParadox.Content.Items.Weapons.Melee;
using TerrariaParadox.Content.Items.Weapons.Ranged;
using TerrariaParadox.Content.NPCs.Hostile;
using TerrariaParadox.Content.NPCs.Hostile.Miniboss;
using TerrariaParadox.Content.NPCs.Hostile.Worms;

namespace TerrariaParadox;

public partial class ParadoxNPC : GlobalNPC
{
    public override void ModifyHitPlayer(NPC npc, Player target, ref Player.HurtModifiers modifiers)
    {
        if (Stickled) modifiers.IncomingDamageMultiplier *= 10f - Content.Debuffs.Stickled.DamageReduction / 100f;
    }

    public override void UpdateLifeRegen(NPC npc, ref int damage)
    {
        if (LeecharangBleed)
        {
            var DoTPerS = Leecharang.DebuffDotPerSecond * LeecharangBleedStacks;
            npc.lifeRegen -= DoTPerS * 2;
            damage += DoTPerS + 1;
        }
    }
    public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
    {
        if (ParadoxSystem.AssimilatedBlocks.ContainsValue((ushort)spawnInfo.SpawnTileType)) 
            pool.Remove(0);
        
        foreach (var npcType in FlipsideEnemies)
        {
            if (ParadoxSystem.AssimilatedBlocks.ContainsValue((ushort)spawnInfo.SpawnTileType))
            {
                pool.Add(npcType, ParadoxSystem.FlippedBlockSpawnChance[npcType]);
            }
        }
        //if (NPC.AnyNPCs(ModContent.NPCType<WalkingHive>()))
        //    pool.Remove(ModContent.NPCType<WalkingHive>());
        //for stopping the game from spawning more than one walking hive at a time
    }

    public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
    {
        if (player.InModBiome<FBiomeMainSurface>())
        {
            spawnRate = (int)(spawnRate * 0.55f);//normal evil has a 0.65x mult, lower = more spawns
            maxSpawns = (int)(maxSpawns * 1.4f); //normal evil has a 1.3x mult, higher = more spawns
        }
    }
}