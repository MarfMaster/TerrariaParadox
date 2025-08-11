using Terraria;
using Terraria.ModLoader;

namespace TerrariaParadox;

public partial class ParadoxNPC : GlobalNPC
{
    public override bool InstancePerEntity => true;

    public bool Stickled;
    
    public bool LeecharangBleed;

    public int LeecharangBleedStacks = 0; //gets reset in the buff once it runs out
    
    public override void ResetEffects(NPC npc)
    {
        Stickled = false;
        
        LeecharangBleed = false;
    }
}