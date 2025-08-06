using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.ModLoader;
using TerrariaParadox.Content.Debuffs;

namespace TerrariaParadox;

public partial class ParadoxPlayer : ModPlayer
{
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        if ((hit.DamageType == DamageClass.Melee || hit.DamageType == DamageClass.SummonMeleeSpeed) && WeaponImbueStickling)
        {
            target.AddBuff(ModContent.BuffType<Stickled>(), Main.rand.Next(10, 20) * 60);
        }
    }

    public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
        if (DebugNoDamageSpread)
        {
            modifiers.DamageVariationScale *= 0;
        }
    }
    public override void PostUpdateBuffs()
    {
        if (Stickled)
        {
            Player.GetDamage(DamageClass.Generic) *= 1f - (Content.Debuffs.Stickled.DamageReduction / 100f);
        }
    }
}