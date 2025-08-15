using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using TerrariaParadox.Content.Debuffs.DoT;
using TerrariaParadox.Content.Items.Tiles.Banners;
using TerrariaParadox.Content.Projectiles.Hostile;

namespace TerrariaParadox.Content.NPCs.Hostile.Miniboss;
public class WalkingHive : ModdedHostileNPC
{
    public override int TotalAnimationFrames => 52;
    public override int Width => 48;
    public override int Height => 64;
    public override int BaseLifePoints => 1000;
    public override int BaseDefense => 0;
    public override float BaseKnockbackReceivedMultiplier => 0.2f;
    public override int BaseContactDamage => 15;
    public override SoundStyle OnHitSound => SoundID.NPCDeath9;
    public override SoundStyle OnDeathSound => SoundID.NPCDeath11;
    public override int Value => 100000;
    public override int BannerItemType => ModContent.ItemType<WalkingHiveBanner>();

    private ref float BackShots => ref AiTimer2;
    public int DefenseFromBack = 20;
    private enum ActionState
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
    /// Notes:
    /// Wanders about in a random direction, until he notices the player.
    /// Once he notices the player, he will first turn towards them for half a second and then turn his back to the player for about 2 secs and prepare to puke.
    /// Once the prep is done, he will face the player to puke at them.
    /// After having puked, he will keep facing that direction for a few secs and be tired.
    /// If the player can't finish it off while it's tired, it'll then turn it's back to the player and try to run away for a few secs.
    /// Then, it'll go back to preparing to puke if a nearby player is still alive. Else it'll go back to wandering.
    /// Every time this enemy is hit in the back, it'll gain a stack and have increased defense against that hit, plus reflecting basic projectiles back at the player.
    /// If it reaches a certain number of stacks, it'll explode in a big radius around it, dealing high aoe damage and dying in the process.
    /// On death, no matter if it exploded or not, spawns 2 Swarms that are flung a decent distance to the right and left of it.
    /// </summary>
    private enum Frame
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
    public override void CustomSetStaticDefaults()
    {
        NPCID.Sets.SpecificDebuffImmunity[Type][ModContent.BuffType<LeecharangBleed>()] = true;
    }

    public override void CustomSetDefaults()
    {
        ItemID.Sets.KillsToBanner[BannerItem] = 10;
    }

    public override float SpawnChance(NPCSpawnInfo spawnInfo)
    {
        float chance = 0f;
        if (spawnInfo.Player.InModBiome(ModContent.GetInstance<Biomes.TheFlipside.BiomeMainSurface>()))// && !NPC.AnyNPCs(Type))so it can spawn one at a time
        {
            chance = 0.001f;
        }
        return chance;
    }

    public override void FindFrame(int frameHeight)
    {
        FrameDuration = 10;
        NPC.spriteDirection = NPC.direction;
        
        NPC.frameCounter++;
        if (NPC.frameCounter >= FrameDuration)
        {
            NPC.frame.Y += frameHeight;
            NPC.frameCounter = 0;

            if (NPC.frame.Y >= Main.npcFrameCount[Type] * frameHeight)
            {
                NPC.frame.Y = 0;
            }
        }

        NPC.frame.Y = 0;
    }
    private float AggroDistance = 400f;
    private float PukeRange = 200f;
    private float ChaseRange = 800f;
    private bool Hurt = false;
    public override void SendExtraAI(BinaryWriter writer)
    {
        writer.WriteFlags(Hurt);
    }

    public override void ReceiveExtraAI(BinaryReader reader)
    {
        reader.ReadFlags(out Hurt);
    }

    public override void AI()
    {
        Player target = Main.player[NPC.target];
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
                Hunting(target);
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

        NPC.reflectsProjectiles = false;
        if (Main.GameUpdateCount % 30 == 0)
        {
            //Main.NewText(AiState);
        }
    }

    private bool Backshot(int hitDirection)
    {
        return hitDirection == NPC.direction;
    }
    public override void OnHitByProjectile(Projectile projectile, NPC.HitInfo hit, int damageDone)
    {
        Player player = Main.player[NPC.target];
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
            NPC.TargetClosest(true);
            Hurt = true;
            AiState = (float)ActionState.Provoked;
            AiTimer = 0;
            NPC.netUpdate = true;
        }
        
        if (projectile.CanBeReflected() && Backshot(hit.HitDirection))
        {
            Projectile Reflected = Projectile.NewProjectileDirect(projectile.GetSource_FromThis(), projectile.position, projectile.velocity,
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
            NPC.TargetClosest(true);
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

    private int MaxBackShots = 10;
    public override void HitEffect(NPC.HitInfo hit)
    {
        if (Backshot(hit.HitDirection) && BackShots < MaxBackShots)
        {
            BackShots++;
        }
        if (Backshot(hit.HitDirection) && BackShots == MaxBackShots)
        {
            AiState = (float)ActionState.Exploding;
            AiTimer = 0;
            NPC.netUpdate = true;
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
            NPC.velocity.X = Main.rand.NextFloat(-2f, 2f);
            SetDirection();
            NPC.netUpdate = true;
            AiTimer = 0;
        }
        
        Collision.StepUp(ref NPC.position, ref NPC.velocity, NPC.width, NPC.height, ref NPC.stepSpeed, ref NPC.gfxOffY, (int)target.gravDir, default);

        if (NPC.collideX)
        {
            NPC.velocity.X *= -1f;
            SetDirection();
        }
        
        if (NPC.HasValidTarget && target.Distance(NPC.Center) < AggroDistance)
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
            
            if (AiTimer >= 120)
            {
                AiState = (float)ActionState.Hunting;
                AiTimer = 0;
            }
        }
        else
        {
            if (target.Distance(NPC.Center) < AggroDistance)
            {
                AiTimer++;
            
                NPC.velocity.X = 0;
            
                if (AiTimer >= 120)
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
    private void Hunting(Player target)
    {
        NPC.velocity.X = NPC.Center.DirectionTo(target.Center).X * 4f;
        SetDirection();
        
        Collision.StepUp(ref NPC.position, ref NPC.velocity, NPC.width, NPC.height, ref NPC.stepSpeed, ref NPC.gfxOffY, (int)target.gravDir, default);
        
        if (NPC.collideX && NPC.velocity.Y >= 0)
        {
            NPC.velocity.Y += -8f;
        }
            
        if (target.Distance(NPC.Center) < AggroDistance * 0.5f && NPC.HasValidTarget)
        {
            AiState = (float)ActionState.Preparing;
            AiTimer = 0;
        }
        
        if (target.Distance(NPC.Center) > ChaseRange || !NPC.HasValidTarget)
        {
            AiTimer++;
            
            NPC.TargetClosest();

            if (AiTimer >= 120)
            {
                AiState = (float)ActionState.Provoked; //go back to just provoked and have it automatically calm down if you're far enough away
                AiTimer = 100;
            }
        }
    }
    private void Preparing(Player target)
    {
        AiTimer++;
            
        NPC.velocity.X = 0;

        if (AiTimer >= 120)
        {
            AiState = (float)ActionState.Puking;
            AiTimer = 0;
        }
    }
    private void Puking(Player target)
    {
        AiTimer++;
            
        NPC.velocity.X = 0;
        
        if ((AiTimer % 5 == 0 ) && Main.netMode != NetmodeID.MultiplayerClient)
        {
            Projectile.NewProjectile(Projectile.InheritSource(this.Entity), NPC.Center,
                NPC.Center.DirectionTo(target.Center) * 10f, ModContent.ProjectileType<WalkingHivePuke>(), 10, 5f);
        }

        if (AiTimer >= 120)
        {
            AiState = (float)ActionState.Tired;
            AiTimer = 0;
        }
    }
    private void Tired(Player target)
    {
        AiTimer++;
            
        NPC.velocity.X = 0;

        if (AiTimer >= 120)
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
        
        NPC.velocity.X = NPC.Center.DirectionTo(target.Center).X * -3f;
        SetDirection();
        
        Collision.StepUp(ref NPC.position, ref NPC.velocity, NPC.width, NPC.height, ref NPC.stepSpeed, ref NPC.gfxOffY, (int)target.gravDir, default);
        
        if (NPC.collideX && NPC.velocity.Y >= 0)
        {
            NPC.velocity.Y += -8f;
        }

        if (AiTimer >= 120)
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

        if (AiTimer >= 120 && Main.netMode != NetmodeID.MultiplayerClient)
        {
            NPC.StrikeInstantKill();
        }
    }

    public override void OnKill()
    {
        if (AiState == (float)ActionState.Exploding)
        {
            Main.NewText("boom");
            //call some explosion projectile
        }
    }

    private void SetDirection()
    {
        NPC.direction = NPC.velocity.X > 0f ? 1 : -1;
    }
    
}