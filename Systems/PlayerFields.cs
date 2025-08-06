using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.ModLoader;

namespace TerrariaParadox;

public partial class ParadoxPlayer : ModPlayer
{
    public bool Stickled;
    public bool WeaponImbueStickling;
    
    public bool DebugNoDamageSpread = false;

    public override void ResetEffects()
    {
        Stickled = false;
        WeaponImbueStickling = false;
    }
}