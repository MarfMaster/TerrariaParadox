using Terraria.ModLoader;

namespace TerrariaParadox;

public partial class ParadoxPlayer : ModPlayer
{
    //Buffs
    public bool ChitiniteFlip;

    //Other
    public bool DebugNoDamageSpread = false;

    //Debuffs
    public bool FlippedGravity;
    public bool HitByThorns;
    public bool Stickled;
    public bool WeaponImbueStickling;

    public override void ResetEffects()
    {
        FlippedGravity = false;
        Stickled = false;
        ChitiniteFlip = false;
        WeaponImbueStickling = false;
    }
}