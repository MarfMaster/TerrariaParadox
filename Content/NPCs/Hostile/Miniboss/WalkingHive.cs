using System;
using System.IO;
using Microsoft.Xna.Framework;
using MLib.Common.NPCs;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using TerrariaParadox.Content.Biomes.TheFlipside;
using TerrariaParadox.Content.Debuffs.DoT;
using TerrariaParadox.Content.Gores.NPCs.Hostile.Miniboss;
using TerrariaParadox.Content.Items.Materials;
using TerrariaParadox.Content.Items.Tiles.Banners;
using TerrariaParadox.Content.Projectiles.Hostile;

namespace TerrariaParadox.Content.NPCs.Hostile.Miniboss;

public class WalkingHive : ModdedHostileNPC
{
    public const int DefenseFromBack = 20;
    private const float AggroRange = 450f;
    private const float PukeRange = 400f;
    private const float ChaseRange = 1200f;

    private const float MaxWanderSpeed = 2.5f;
    private const float ChaseSpeed = 5f;
    private const float ChaseSpeedDesperate = 8f;
    private const float JumpSpeed = 8f;
    private const float JumpSpeedDesperate = 11f;

    private const int MaxBackShots = 10;

    private const int TotalWanderingFrames = 5;

    private const float TotalProvokedFrames = 8;
    private const float ProvokedDuration = 120;

    private const float TotalHuntingFrames = 5;
    private const float IsExplodingStartTime = 20;
    private const float ExplodeHuntDuration = 120;
    private const float DeAggroDuration = 120;

    private const float TotalPrepFrames = 8;
    private const float PrepDuration = 120;
    private const float TotalPukeFrames = 13;
    private const float PukeDuration = 120;
    private const float PukeDivisor = 2;
    private const float TotalTiredFrames = 8;
    private const float TiredDuration = 120;
    private const float FleeDuration = 120;

    private const float TotalExplosionFrames = 5;
    private const float ExplodingDuration = 60;
    private bool Hurt;
    private bool IsExploding;
    public override int TotalAnimationFrames => 52;
    public override int Width => 48;
    public override int Height => 64;
    public override int BaseLifePoints => 1000;
    public override int BaseDefense => 0;
    public override float BaseKnockbackReceivedMultiplier => 0.2f;
    public override int BaseContactDamage => 25;
    public const float ExplosionContactDamageMultiplier = 2f;
    public const int BasePukeDamage = 30;
    public override SoundStyle OnHitSound => SoundID.NPCDeath9;
    public override SoundStyle OnDeathSound => SoundID.NPCDeath11;
    public override int Value => 100000;
    public override int BannerItemType => ModContent.ItemType<WalkingHiveBanner>();

    private ref float BackShots => ref AiTimer2;

    public override void CustomSetStaticDefaults()
    {
        ParadoxSystem.FlippedBlockSpawnChance[Type] = 0.35f;
        NPCID.Sets.SpecificDebuffImmunity[Type][ModContent.BuffType<LeecharangBleed>()] = true;
    }

    public override void CustomSetDefaults()
    {
        ItemID.Sets.KillsToBanner[BannerItem] = 10;
        NPC.rarity = 5;
    }
    public override void FindFrame(int frameHeight)
    {
        FrameDuration = 10;
        NPC.spriteDirection = NPC.direction;

        switch (AiState)
        {
            case (float)ActionState.Wandering:
            {
                if (AiTimer == 1) NPC.frame.Y = (int)FrameState.Wandering1 * frameHeight;
                FrameDuration = 10;
                if (NPC.velocity.X != 0)
                {
                    NPC.frameCounter++;
                    if (NPC.frameCounter >= FrameDuration)
                    {
                        NPC.frame.Y += frameHeight;
                        NPC.frameCounter = 0;

                        if (NPC.frame.Y >= ((int)FrameState.Wandering5 + 1) * frameHeight)
                            NPC.frame.Y = (int)FrameState.Wandering1 * frameHeight;
                    }
                }
                else
                {
                    NPC.frame.Y = (int)FrameState.Wandering1 * frameHeight;
                }

                break;
            }
            case (float)ActionState.Provoked:
            {
                var dividend = ProvokedDuration / TotalProvokedFrames;
                switch (AiTimer)
                {
                    case float one when AiTimer < dividend:
                    {
                        NPC.frame.Y = (int)FrameState.Provoked1 * frameHeight;
                        break;
                    }
                    case float two when AiTimer > dividend && AiTimer < dividend * 2f:
                    {
                        NPC.frame.Y = (int)FrameState.Provoked2 * frameHeight;
                        break;
                    }
                    case float three when AiTimer > dividend * 2f && AiTimer < dividend * 3f:
                    {
                        NPC.frame.Y = (int)FrameState.Provoked3 * frameHeight;
                        break;
                    }
                    case float four when AiTimer > dividend * 3f && AiTimer < dividend * 4f:
                    {
                        NPC.frame.Y = (int)FrameState.Provoked4 * frameHeight;
                        break;
                    }
                    case float five when AiTimer > dividend * 4f && AiTimer < dividend * 5f:
                    {
                        NPC.frame.Y = (int)FrameState.Provoked5 * frameHeight;
                        break;
                    }
                    case float six when AiTimer > dividend * 5f && AiTimer < dividend * 6f:
                    {
                        NPC.frame.Y = (int)FrameState.Provoked6 * frameHeight;
                        break;
                    }
                    case float seven when AiTimer > dividend * 6f && AiTimer < dividend * 7f:
                    {
                        NPC.frame.Y = (int)FrameState.Provoked7 * frameHeight;
                        break;
                    }
                    case float eight when AiTimer > dividend * 7f && AiTimer < dividend * 8f:
                    {
                        NPC.frame.Y = (int)FrameState.Provoked8 * frameHeight;
                        break;
                    }
                }

                break;
            }
            case (float)ActionState.Hunting:
            {
                if (AiTimer == 1)
                {
                    NPC.frame.Y = (int)FrameState.Hunting1 * frameHeight;
                    AiTimer++;
                }

                FrameDuration = 5;
                if (NPC.velocity.X != 0)
                {
                    NPC.frameCounter++;
                    if (NPC.frameCounter >= FrameDuration)
                    {
                        NPC.frame.Y += frameHeight;
                        NPC.frameCounter = 0;

                        if (NPC.frame.Y >= ((int)FrameState.Hunting5 + 1) * frameHeight)
                            NPC.frame.Y = (int)FrameState.Hunting1 * frameHeight;
                    }
                }
                else
                {
                    NPC.frame.Y = (int)FrameState.Hunting1 * frameHeight;
                }

                break;
            }
            case (float)ActionState.Preparing:
            {
                var dividend = PrepDuration / TotalPrepFrames;
                switch (AiTimer)
                {
                    case float one when AiTimer < dividend:
                    {
                        NPC.frame.Y = (int)FrameState.Preparing1 * frameHeight;
                        break;
                    }
                    case float two when AiTimer > dividend && AiTimer < dividend * 2f:
                    {
                        NPC.frame.Y = (int)FrameState.Preparing2 * frameHeight;
                        break;
                    }
                    case float three when AiTimer > dividend * 2f && AiTimer < dividend * 3f:
                    {
                        NPC.frame.Y = (int)FrameState.Preparing3 * frameHeight;
                        break;
                    }
                    case float four when AiTimer > dividend * 3f && AiTimer < dividend * 4f:
                    {
                        NPC.frame.Y = (int)FrameState.Preparing4 * frameHeight;
                        break;
                    }
                    case float five when AiTimer > dividend * 4f && AiTimer < dividend * 5f:
                    {
                        NPC.frame.Y = (int)FrameState.Preparing5 * frameHeight;
                        break;
                    }
                    case float six when AiTimer > dividend * 5f && AiTimer < dividend * 6f:
                    {
                        NPC.frame.Y = (int)FrameState.Preparing6 * frameHeight;
                        break;
                    }
                    case float seven when AiTimer > dividend * 6f && AiTimer < dividend * 7f:
                    {
                        NPC.frame.Y = (int)FrameState.Preparing7 * frameHeight;
                        break;
                    }
                    case float eight when AiTimer > dividend * 7f && AiTimer < dividend * 8f:
                    {
                        NPC.frame.Y = (int)FrameState.Preparing8 * frameHeight;
                        break;
                    }
                }

                break;
            }
            case (float)ActionState.Puking:
            {
                var dividend = PukeDuration / TotalPukeFrames;
                switch (AiTimer)
                {
                    case float one when AiTimer < dividend:
                    {
                        NPC.frame.Y = (int)FrameState.Puking1 * frameHeight;
                        break;
                    }
                    case float two when AiTimer > dividend && AiTimer < dividend * 2f:
                    {
                        NPC.frame.Y = (int)FrameState.Puking2 * frameHeight;
                        break;
                    }
                    case float three when AiTimer > dividend * 2f && AiTimer < dividend * 3f:
                    {
                        NPC.frame.Y = (int)FrameState.Puking3 * frameHeight;
                        break;
                    }
                    case float four when AiTimer > dividend * 3f && AiTimer < dividend * 4f:
                    {
                        NPC.frame.Y = (int)FrameState.Puking4 * frameHeight;
                        break;
                    }
                    case float five when AiTimer > dividend * 4f && AiTimer < dividend * 5f:
                    {
                        NPC.frame.Y = (int)FrameState.Puking5 * frameHeight;
                        break;
                    }
                    case float six when AiTimer > dividend * 5f && AiTimer < dividend * 6f:
                    {
                        NPC.frame.Y = (int)FrameState.Puking6 * frameHeight;
                        break;
                    }
                    case float seven when AiTimer > dividend * 6f && AiTimer < dividend * 7f:
                    {
                        NPC.frame.Y = (int)FrameState.Puking7 * frameHeight;
                        break;
                    }
                    case float eight when AiTimer > dividend * 7f && AiTimer < dividend * 8f:
                    {
                        NPC.frame.Y = (int)FrameState.Puking8 * frameHeight;
                        break;
                    }
                    case float nine when AiTimer > dividend * 8f && AiTimer < dividend * 9f:
                    {
                        NPC.frame.Y = (int)FrameState.Puking9 * frameHeight;
                        break;
                    }
                    case float ten when AiTimer > dividend * 9f && AiTimer < dividend * 10f:
                    {
                        NPC.frame.Y = (int)FrameState.Puking10 * frameHeight;
                        break;
                    }
                    case float eleven when AiTimer > dividend * 10f && AiTimer < dividend * 11f:
                    {
                        NPC.frame.Y = (int)FrameState.Puking11 * frameHeight;
                        break;
                    }
                    case float twelve when AiTimer > dividend * 11f && AiTimer < dividend * 12f:
                    {
                        NPC.frame.Y = (int)FrameState.Puking12 * frameHeight;
                        break;
                    }
                    case float thirteen when AiTimer > dividend * 12f && AiTimer < dividend * 13f:
                    {
                        NPC.frame.Y = (int)FrameState.Puking13 * frameHeight;
                        break;
                    }
                }

                break;
            }
            case (float)ActionState.Tired:
            {
                var dividend = TiredDuration / TotalTiredFrames;
                switch (AiTimer)
                {
                    case float one when AiTimer < dividend:
                    {
                        NPC.frame.Y = (int)FrameState.Tired1 * frameHeight;
                        break;
                    }
                    case float two when AiTimer > dividend && AiTimer < dividend * 2f:
                    {
                        NPC.frame.Y = (int)FrameState.Tired2 * frameHeight;
                        break;
                    }
                    case float three when AiTimer > dividend * 2f && AiTimer < dividend * 3f:
                    {
                        NPC.frame.Y = (int)FrameState.Tired3 * frameHeight;
                        break;
                    }
                    case float four when AiTimer > dividend * 3f && AiTimer < dividend * 4f:
                    {
                        NPC.frame.Y = (int)FrameState.Tired4 * frameHeight;
                        break;
                    }
                    case float five when AiTimer > dividend * 4f && AiTimer < dividend * 5f:
                    {
                        NPC.frame.Y = (int)FrameState.Tired5 * frameHeight;
                        break;
                    }
                    case float six when AiTimer > dividend * 5f && AiTimer < dividend * 6f:
                    {
                        NPC.frame.Y = (int)FrameState.Tired6 * frameHeight;
                        break;
                    }
                    case float seven when AiTimer > dividend * 6f && AiTimer < dividend * 7f:
                    {
                        NPC.frame.Y = (int)FrameState.Tired7 * frameHeight;
                        break;
                    }
                    case float eight when AiTimer > dividend * 7f && AiTimer < dividend * 8f:
                    {
                        NPC.frame.Y = (int)FrameState.Tired8 * frameHeight;
                        break;
                    }
                }

                break;
            }
            case (float)ActionState.Fleeing:
            {
                if (AiTimer == 1) NPC.frame.Y = (int)FrameState.Hunting1 * frameHeight;
                FrameDuration = 7;
                NPC.frameCounter++;
                if (NPC.frameCounter >= FrameDuration)
                {
                    NPC.frame.Y += frameHeight;
                    NPC.frameCounter = 0;

                    if (NPC.frame.Y >= ((int)FrameState.Hunting5 + 1) * frameHeight)
                        NPC.frame.Y = (int)FrameState.Hunting1 * frameHeight;
                }

                break;
            }
            case (float)ActionState.Exploding:
            {
                var dividend = ExplodingDuration / TotalExplosionFrames;
                switch (AiTimer)
                {
                    case float one when AiTimer < dividend:
                    {
                        NPC.frame.Y = (int)FrameState.Explode1 * frameHeight;
                        break;
                    }
                    case float two when AiTimer > dividend && AiTimer < dividend * 2f:
                    {
                        NPC.frame.Y = (int)FrameState.Explode2 * frameHeight;
                        break;
                    }
                    case float three when AiTimer > dividend * 2f && AiTimer < dividend * 3f:
                    {
                        NPC.frame.Y = (int)FrameState.Explode3 * frameHeight;
                        break;
                    }
                    case float four when AiTimer > dividend * 3f && AiTimer < dividend * 4f:
                    {
                        NPC.frame.Y = (int)FrameState.Explode4 * frameHeight;
                        break;
                    }
                    case float five when AiTimer > dividend * 4f && AiTimer < dividend * 5f:
                    {
                        NPC.frame.Y = (int)FrameState.Explode5 * frameHeight;
                        break;
                    }
                }

                break;
            }
        }
    }

    public override void SendExtraAI(BinaryWriter writer)
    {
        writer.WriteFlags(Hurt, IsExploding);
    }

    public override void ReceiveExtraAI(BinaryReader reader)
    {
        reader.ReadFlags(out Hurt, out IsExploding);
    }

    public override void AI()
    {
        var target = Main.player[NPC.target];
        switch (AiState)
        {
            case (float)ActionState.Wandering:
            {
                Wandering(target);
                break;
            }
            case (float)ActionState.Provoked:
            {
                Provoked(target, Hurt);
                break;
            }
            case (float)ActionState.Hunting:
            {
                Hunting(target, IsExploding);
                break;
            }
            case (float)ActionState.Preparing:
            {
                Preparing(target);
                break;
            }
            case (float)ActionState.Puking:
            {
                Puking(target);
                break;
            }
            case (float)ActionState.Tired:
            {
                Tired(target);
                break;
            }
            case (float)ActionState.Fleeing:
            {
                Fleeing(target);
                break;
            }
            case (float)ActionState.Exploding:
            {
                Exploding(target);
                break;
            }
        }
    }

    private bool Backshot(int hitDirection)
    {
        return hitDirection == NPC.direction;
    }

    public override void OnHitByProjectile(Projectile projectile, NPC.HitInfo hit, int damageDone)
    {
        var player = Main.player[NPC.target];
        projectile.TryGetOwner(out player);

        if (AiState == (float)ActionState.Wandering && player != null)
        {
            NPC.target = player.whoAmI;
            Hurt = true;
            AiState = (float)ActionState.Provoked;
            AiTimer = 0;
            NPC.netUpdate = true;
        }
        else if (AiState == (float)ActionState.Wandering && player == null)
        {
            NPC.TargetClosest();
            Hurt = true;
            AiState = (float)ActionState.Provoked;
            AiTimer = 0;
            NPC.netUpdate = true;
        }

        if (projectile.CanBeReflected() && Backshot(hit.HitDirection))
        {
            var Reflected = Projectile.NewProjectileDirect(projectile.GetSource_FromThis(), projectile.position,
                projectile.velocity,
                projectile.type, projectile.damage, projectile.knockBack);
            NPC.ReflectProjectile(Reflected);
            projectile.Kill();
        }
    }

    public override void OnHitByItem(Player player, Item item, NPC.HitInfo hit, int damageDone)
    {
        if (AiState == (float)ActionState.Wandering && player != null)
        {
            NPC.target = player.whoAmI;
            Hurt = true;
            AiState = (float)ActionState.Provoked;
            NPC.netUpdate = true;
        }
        else if (AiState == (float)ActionState.Wandering && player == null)
        {
            NPC.TargetClosest();
            Hurt = true;
            AiState = (float)ActionState.Provoked;
            NPC.netUpdate = true;
        }
    }

    public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
    {
        if (Backshot(modifiers.HitDirection))
        {
            modifiers.Knockback *= 0;
            modifiers.Defense.Base += DefenseFromBack;
        }
    }

    public override void HitEffect(NPC.HitInfo hit)
    {
        if (Backshot(hit.HitDirection) && BackShots < MaxBackShots) BackShots++;
        if (Backshot(hit.HitDirection) && BackShots == MaxBackShots && !IsExploding)
        {
            IsExploding = true;
            AiState = (float)ActionState.Hunting;
            AiTimer = 0;
            NPC.netUpdate = true;
        }

        if (!Main.dedServ && NPC.life <= 0)
        {
            var velocity1 = new Vector2(Main.rand.Next(-30, 31) * 0.2f, Main.rand.Next(-30, 31) * 0.2f);
            var velocity2 = new Vector2(Main.rand.Next(-30, 31) * 0.2f, Main.rand.Next(-30, 31) * 0.2f);
            var velocity3 = new Vector2(Main.rand.Next(-30, 31) * 0.2f, Main.rand.Next(-30, 31) * 0.2f);
            var velocity4 = new Vector2(Main.rand.Next(-30, 31) * 0.2f, Main.rand.Next(-30, 31) * 0.2f);
            var velocity5 = new Vector2(Main.rand.Next(-30, 31) * 0.2f, Main.rand.Next(-30, 31) * 0.2f);

            if (AiState == (float)ActionState.Exploding)
            {
                Gore.NewGoreDirect(NPC.GetSource_Death(), NPC.Center, velocity1 * 2f,
                    ModContent.GoreType<WalkingHiveGore1>());
                Gore.NewGoreDirect(NPC.GetSource_Death(), NPC.Center, velocity2 * 2f,
                    ModContent.GoreType<WalkingHiveGore2>());
                Gore.NewGoreDirect(NPC.GetSource_Death(), NPC.Center, velocity3 * 2f,
                    ModContent.GoreType<WalkingHiveGore3>());
                Gore.NewGoreDirect(NPC.GetSource_Death(), NPC.Center, velocity4 * 2f,
                    ModContent.GoreType<WalkingHiveGore4>());
                Gore.NewGoreDirect(NPC.GetSource_Death(), NPC.Center, velocity5 * 2f,
                    ModContent.GoreType<WalkingHiveGore5>());
            }
            else
            {
                Gore.NewGoreDirect(NPC.GetSource_Death(), NPC.Center, velocity1,
                    ModContent.GoreType<WalkingHiveGore1>());
                Gore.NewGoreDirect(NPC.GetSource_Death(), NPC.Center, velocity2,
                    ModContent.GoreType<WalkingHiveGore2>());
                Gore.NewGoreDirect(NPC.GetSource_Death(), NPC.Center, velocity3,
                    ModContent.GoreType<WalkingHiveGore3>());
                Gore.NewGoreDirect(NPC.GetSource_Death(), NPC.Center, velocity4,
                    ModContent.GoreType<WalkingHiveGore4>());
                Gore.NewGoreDirect(NPC.GetSource_Death(), NPC.Center, velocity5,
                    ModContent.GoreType<WalkingHiveGore5>());
            }
        }
    }

    private void Wandering(Player target)
    {
        AiTimer++;
        NPC.TargetClosest(false);

        if (AiTimer == 120 && Main.rand.NextBool(5))
        {
            NPC.velocity.X = 0;
            NPC.netUpdate = true;
        }

        if (AiTimer >= 180 && Main.netMode != NetmodeID.MultiplayerClient)
        {
            NPC.velocity.X = Main.rand.NextFloat(-MaxWanderSpeed, MaxWanderSpeed);
            SetDirection();
            NPC.netUpdate = true;
            AiTimer = 0;
        }

        Collision.StepUp(ref NPC.position, ref NPC.velocity, NPC.width, NPC.height, ref NPC.stepSpeed, ref NPC.gfxOffY,
            (int)target.gravDir);

        if (NPC.collideX)
        {
            NPC.velocity.X *= -1f; //turn back
            SetDirection();
        }

        if (NPC.HasValidTarget && target.Distance(NPC.Center) < AggroRange)
        {
            AiState = (float)ActionState.Provoked;
            AiTimer = 0;
        }
    }

    private void Provoked(Player target, in bool Hurt)
    {
        if (Hurt)
        {
            AiTimer += 3;

            NPC.velocity.X = 0;
            if (AiTimer >= ProvokedDuration)
            {
                AiState = (float)ActionState.Hunting;
                AiTimer = 0;
            }
        }
        else
        {
            NPC.velocity.X = 0;

            if (target.Distance(NPC.Center) < AggroRange)
            {
                AiTimer++;


                if (AiTimer >= ProvokedDuration)
                {
                    AiState = (float)ActionState.Hunting;
                    AiTimer = 0;
                }
            }
            else
            {
                NPC.TargetClosest();
                AiTimer--;

                if (AiTimer <= 0)
                {
                    AiState = (float)ActionState.Wandering;
                    AiTimer = 0;
                }
            }
        }
    }

    private void Hunting(Player target, bool IsExploding)
    {
        if (IsExploding)
        {
            AiTimer++;
            switch (AiTimer)
            {
                case 1:
                {
                    SoundEngine.PlaySound(SoundID.DD2_KoboldFlyerChargeScream, NPC.Center);
                    AiTimer++;
                    break;
                }
                case <= 20:
                {
                    NPC.velocity.X = 0;
                    break;
                }
                case float start when AiTimer > IsExplodingStartTime:
                {
                    NPC.velocity.X = NPC.Center.DirectionTo(target.Center).X * ChaseSpeedDesperate;
                    SetDirection();

                    Collision.StepUp(ref NPC.position, ref NPC.velocity, NPC.width, NPC.height, ref NPC.stepSpeed,
                        ref NPC.gfxOffY,
                        (int)target.gravDir);

                    if (NPC.collideX && NPC.velocity.Y >= 0) NPC.velocity.Y += -JumpSpeedDesperate;

                    break;
                }
            }

            if (AiTimer >= ExplodeHuntDuration && NPC.velocity.Y == 0)
            {
                AiState = (float)ActionState.Exploding;
                AiTimer = 0;
            }
        }
        else
        {
            if (AiTimer == 0) AiTimer = 1;
            NPC.velocity.X = NPC.Center.DirectionTo(target.Center).X * ChaseSpeed;
            SetDirection();

            Collision.StepUp(ref NPC.position, ref NPC.velocity, NPC.width, NPC.height, ref NPC.stepSpeed,
                ref NPC.gfxOffY,
                (int)target.gravDir);

            if (NPC.collideX && NPC.velocity.Y >= 0) NPC.velocity.Y += -8f;

            if (target.Distance(NPC.Center) < PukeRange && NPC.HasValidTarget)
            {
                AiState = (float)ActionState.Preparing;
                AiTimer = 0;
            }

            if (target.Distance(NPC.Center) > ChaseRange || !NPC.HasValidTarget)
            {
                AiTimer++;

                NPC.TargetClosest();

                if (AiTimer >= DeAggroDuration)
                {
                    AiState = (float)ActionState
                        .Provoked; //go back to just provoked and have it automatically calm down if you're far enough away
                    AiTimer = 100;
                }
            }
        }
    }

    private void Preparing(Player target)
    {
        AiTimer++;

        NPC.velocity.X = 0;

        if (AiTimer >= PrepDuration)
        {
            AiState = (float)ActionState.Puking;
            AiTimer = 0;
        }
    }

    private void Puking(Player target)
    {
        AiTimer++;

        NPC.velocity.X = 0;

        if (AiTimer % PukeDivisor == 0 && Main.netMode != NetmodeID.MultiplayerClient)
        {
            Vector2 MouthCenter = NPC.Center + new Vector2(NPC.direction * 22, -14);
            Vector2 ProjVelocity = new Vector2((float)NPC.direction * Main.rand.NextFloat(8f, 12f), -1f * Main.rand.NextFloat(3f, 15f));
            Projectile.NewProjectile(Projectile.InheritSource(Entity), MouthCenter,
                ProjVelocity, ModContent.ProjectileType<WalkingHivePuke>(), BasePukeDamage / 2, 1f);
        }

        if (AiTimer >= PukeDuration)
        {
            AiState = (float)ActionState.Tired;
            AiTimer = 0;
        }
    }

    private void Tired(Player target)
    {
        AiTimer++;

        NPC.velocity.X = 0;

        if (AiTimer >= TiredDuration)
        {
            if (target.Distance(NPC.Center) < ChaseRange && NPC.HasValidTarget)
            {
                AiState = (float)ActionState.Fleeing;
                AiTimer = 0;
            }
            else
            {
                AiState = (float)ActionState.Wandering;
                AiTimer = 0;
            }
        }
    }

    private void Fleeing(Player target)
    {
        AiTimer++;

        NPC.velocity.X = NPC.Center.DirectionTo(target.Center).X * -ChaseSpeed;
        SetDirection();

        Collision.StepUp(ref NPC.position, ref NPC.velocity, NPC.width, NPC.height, ref NPC.stepSpeed, ref NPC.gfxOffY,
            (int)target.gravDir);

        if (NPC.collideX && NPC.velocity.Y >= 0) NPC.velocity.Y += -JumpSpeed;

        if (AiTimer >= FleeDuration)
        {
            if (target.Distance(NPC.Center) < ChaseRange)
            {
                AiState = (float)ActionState.Hunting;
                AiTimer = 0;
            }
            else
            {
                AiState = (float)ActionState.Wandering;
                AiTimer = 0;
            }
        }
    }

    private void Exploding(Player target)
    {
        AiTimer++;

        NPC.velocity.X = 0;
        


        if (AiTimer >= ExplodingDuration && Main.netMode != NetmodeID.MultiplayerClient) NPC.StrikeInstantKill();
    }
    public override void OnKill()
    {
        if (IsExploding)
        {
            NPC.value = 0;
            Projectile.NewProjectileDirect(NPC.GetSource_Death(), NPC.Center, Vector2.Zero,
                ModContent.ProjectileType<WalkingHiveExplosion>(), (int)((float)BaseContactDamage * ExplosionContactDamageMultiplier), 1f);
            SoundEngine.PlaySound(SoundID.DD2_KoboldExplosion, NPC.Center);
        }
    }

    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        IItemDropRule notBlownUp = new LeadingConditionRule(new WalkingHiveExplosionDropCondition());
        notBlownUp.OnSuccess(ItemDropRule.Common(ModContent.ItemType<EggCluster>(), 1, 2, 3));
        npcLoot.Add(notBlownUp);
    }

    private void SetDirection()
    {
        NPC.direction = NPC.velocity.X > 0f ? 1 : -1;
    }

    public enum ActionState
    {
        Wandering,
        Provoked,
        Hunting,
        Preparing,
        Puking,
        Tired,
        Fleeing, //uses Hunting frames
        Exploding
    }

    /// <summary>
    ///     Notes:
    ///     Wanders about in a random direction, until he notices the player.
    ///     Once he notices the player, he will first turn towards them for half a second and then turn his back to the player
    ///     for about 2 secs and prepare to puke.
    ///     Once the prep is done, he will face the player to puke at them.
    ///     After having puked, he will keep facing that direction for a few secs and be tired.
    ///     If the player can't finish it off while it's tired, it'll then turn it's back to the player and try to run away for
    ///     a few secs.
    ///     Then, it'll go back to preparing to puke if a nearby player is still alive. Else it'll go back to wandering.
    ///     Every time this enemy is hit in the back, it'll gain a stack and have increased defense against that hit, plus
    ///     reflecting basic projectiles back at the player.
    ///     If it reaches a certain number of stacks, it'll explode in a big radius around it, dealing high aoe damage and
    ///     dying in the process.
    ///     On death, no matter if it exploded or not, spawns 2 Swarms that are flung a decent distance to the right and left
    ///     of it.
    /// </summary>
    private enum FrameState
    {
        Wandering1,
        Wandering2,
        Wandering3,
        Wandering4,
        Wandering5,
        Provoked1,
        Provoked2,
        Provoked3,
        Provoked4,
        Provoked5,
        Provoked6,
        Provoked7,
        Provoked8,
        Hunting1,
        Hunting2,
        Hunting3,
        Hunting4,
        Hunting5,
        Preparing1,
        Preparing2,
        Preparing3,
        Preparing4,
        Preparing5,
        Preparing6,
        Preparing7,
        Preparing8,
        Puking1,
        Puking2,
        Puking3,
        Puking4,
        Puking5,
        Puking6,
        Puking7,
        Puking8,
        Puking9,
        Puking10,
        Puking11,
        Puking12,
        Puking13,
        Tired1,
        Tired2,
        Tired3,
        Tired4,
        Tired5,
        Tired6,
        Tired7,
        Tired8,
        Explode1,
        Explode2,
        Explode3,
        Explode4,
        Explode5
    }
}

public class WalkingHiveExplosionDropCondition : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info)
    {
        NPC npc = info.npc;
        return npc.ai[0] != (float)WalkingHive.ActionState.Exploding;
    }

    public bool CanShowItemDropInUI()
    {
        return false;
    }

    public string GetConditionDescription()
    {
        return String.Empty;
    }
}