using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Biomes.TheFlipside;
using TerrariaParadox.Content.Debuffs;

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


    public override void CatchFish(FishingAttempt attempt, ref int itemDrop, ref int npcSpawn,
        ref AdvancedPopupRequest sonar, ref Vector2 sonarPosition)
    {
        var inWater = !attempt.inLava && !attempt.inHoney;
        var inLava = attempt.inLava;
        var inHoney = attempt.inHoney;
        int FlipsideQuestFish = ItemID.Ichorfish; //for now
        var inFlipside = Player.InModBiome<FBiomeMainSurface>();
        if (inWater && inFlipside)
        {
            if (attempt.questFish == FlipsideQuestFish && attempt.uncommon)
            {
                itemDrop = FlipsideQuestFish;
                return;
            }

            if (attempt.common)
            {
                itemDrop = ItemID.Ebonkoi; //for now
                return;
            }

            if (attempt.rare && attempt.crate) itemDrop = ItemID.CrimsonFishingCrateHard; //for now
        }
    }

    public override bool? CanConsumeBait(Item bait)
    {
        // during CanConsumeBait, Player.GetFishingConditions() == attempt.playerFishingConditions from CatchFish.
        var conditions = Player.GetFishingConditions();

        if (conditions.Pole.type == ItemID.HotlineFishingHook) return false;

        return null; // Let the default logic run
    }

    // If fishing with ladybug, we will receive multiple "fish" per bobber. Does not apply to quest fish
    /*
    public override void ModifyCaughtFish(Item fish)
    {
        // In this example, we make sure that we got a Ladybug as bait, and later on use that to determine what we catch
        if (Player.GetFishingConditions().BaitItemType == ItemID.LadyBug && fish.rare != ItemRarityID.Quest) {
            fish.stack += Main.rand.Next(1, 4);
        }
    }*/

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
        if (Player.forcedGravity == 1) Player.fallStart = (int)(Player.position.Y / 16f);
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