using Terraria;
using Terraria.ModLoader;

namespace TerrariaParadox;

public partial class ParadoxNPC : GlobalNPC
{
    public override bool InstancePerEntity => true;

    public bool Stickled;
    
    public bool LeecharangBleed;
    
    public override void ResetEffects(NPC npc)
    {
        Stickled = false;
        
        LeecharangBleed = false;
    }
}