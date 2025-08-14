using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.ModLoader;

namespace TerrariaParadox;

public partial class ParadoxPlayer : ModPlayer
{
    //Debuffs
    public bool FlippedGravity;
    public bool Stickled;
    
    //Buffs
    public bool ChitiniteFlip;
    public bool WeaponImbueStickling;
    
    //Other
    public bool DebugNoDamageSpread = false;

    public override void ResetEffects()
    {
        FlippedGravity  = false;
        Stickled = false;
        ChitiniteFlip = false;
        WeaponImbueStickling = false;
    }
}