using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace TerrariaParadox;

public partial class ParadoxNPC : GlobalNPC
{
    public static List<int> FlipsideEnemies;
    
    public bool LeecharangBleed;

    public int LeecharangBleedStacks = 0; //gets reset in the buff once it runs out

    public bool Stickled;
    public override bool InstancePerEntity => true;

    public override void ResetEffects(NPC npc)
    {
        Stickled = false;

        LeecharangBleed = false;
    }
}