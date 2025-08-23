using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using TerrariaParadox.Content.Biomes.TheFlipside;
using TerrariaParadox.Content.Debuffs;
using TerrariaParadox.Content.Tiles.Blocks;
using TerrariaParadox.Content.Tiles.Plants;

namespace TerrariaParadox;

public partial class ParadoxPlayer : ModPlayer
{
    public override void PostHurt(Player.HurtInfo info)
    {
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        if ((hit.DamageType == DamageClass.Melee || hit.DamageType == DamageClass.SummonMeleeSpeed) &&
            WeaponImbueStickling) target.AddBuff(ModContent.BuffType<Stickled>(), Main.rand.Next(10, 20) * 60);
    }

    public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
        if (DebugNoDamageSpread) modifiers.DamageVariationScale *= 0;
    }

    public override void PreUpdate()
    {
    }

    public override void PreUpdateBuffs()
    {
        if (HitByThorns)
        {
            Player.AddBuff(ModContent.BuffType<Flipped>(), (int)(1f * 60f));
            HitByThorns = false;
        }
        if (Player.InModBiome(ModContent.GetInstance<FBiomeUnderground>()))
            Player.AddBuff(ModContent.BuffType<Flipped>(), 2 * 60);
    }

    public override void PostUpdateBuffs()
    {
        //if (FlippedGravity) Player.forcedGravity = 2;
        if (Player.forcedGravity == 1)
        {
            Player.fallStart = (int)(Player.position.Y / 16f);
        }
        if (Stickled) Player.GetDamage(DamageClass.Generic) *= 1f - Content.Debuffs.Stickled.DamageReduction / 100f;
    }

    public override void PostUpdateEquips()
    {
    }

    public override void PostUpdateMiscEffects()
    {
    }

    public override void PreUpdateMovement()
    {
    }

    public override void PostUpdateRunSpeeds()
    {
    }

    public override void PostUpdate()
    {
    }
}