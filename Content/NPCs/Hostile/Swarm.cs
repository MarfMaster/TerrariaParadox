using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Debuffs.DoT;
using TerrariaParadox.Content.Items.Tiles.Banners;
using TerrariaParadox.Content.Projectiles.Hostile;

namespace TerrariaParadox.Content.NPCs.Hostile;

public class Swarm : ModNPC
{
    public const int Width = 52;
    public const int Height = 52;
    public const int BaseDmg = 15;
    public const int Defense = 0;
    public const int BaseHP = 70;
    public const int Value = 100;
    
    public const int FrameDuration = 5;
    public const int Frames = 4;
    public ref float AiState => ref NPC.ai[0];
    public ref float AiTimer => ref NPC.ai[1];
    public ref float DashTimer => ref NPC.ai[2];
    private Rectangle DashRangeBox;
    private float DmgMultiplierWhileDashing = 2f;
    private enum ActionState
    {
        Wander,
        Notice,
        Position,
        Dash
    }

    private enum Frame
    {
        First,
        Second,
        Third,
        Fourth
    }

    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[Type] = Frames;
        
        NPCID.Sets.SpecificDebuffImmunity[Type][ModContent.BuffType<LeecharangBleed>()] = true;
    }

    public override void SetDefaults()
    {
        NPC.width = Width;
        NPC.height = Height;
        NPC.aiStyle = -1;
        NPC.damage = BaseDmg;
        NPC.defense = Defense;
        NPC.lifeMax = BaseHP;
        NPC.HitSound = SoundID.NPCDeath9;
        NPC.DeathSound = SoundID.NPCDeath11;
        NPC.value = Value;
        NPC.noGravity  = true;
        NPC.knockBackResist = 0f; //knockback immunity

        Banner = Type;
        BannerItem = ModContent.ItemType<SwarmBanner>();
    }

    public override float SpawnChance(NPCSpawnInfo spawnInfo)
    {
        float chance = 0f;
        if (spawnInfo.Player.InModBiome(ModContent.GetInstance<Biomes.TheFlipside.BiomeMainSurface>()))// && !NPC.AnyNPCs(Type))so it can spawn one at a time
        {
            chance = 0.1f;
        }
        return chance;
    }

    public override void AI()
    {
        Player target = Main.player[NPC.target];
        switch (AiState)
        {
            case (float)ActionState.Wander:
            {
                Wandering(target);
                break;
            }
            case (float)ActionState.Notice:
            {
                Noticing(target);
                break;
            }
            case (float)ActionState.Position:
            {
                Positioning(target);
                break;
            }
            case (float)ActionState.Dash:
            {
                Dashing(target);
                break;
            }
        }
    }

    public override void FindFrame(int frameHeight)
    {
        NPC.spriteDirection = NPC.direction;
        
        NPC.frameCounter++;
        if (NPC.frameCounter >= FrameDuration)
        {
            NPC.frame.Y += frameHeight;
            NPC.frameCounter = 0;

            if (NPC.frame.Y >= Main.npcFrameCount[Type] * frameHeight)
            {
                NPC.frame.Y = (int)Frame.First;
            }
        }
    }

    private void Wandering(Player target)
    {
        AiTimer++;
        DashTimer--;
        NPC.TargetClosest(false);

        if ((AiTimer % 60 == 0 || AiTimer == 1) && Main.netMode != NetmodeID.MultiplayerClient)
        {
            NPC.velocity = new Vector2(Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f));
            
            NPC.netUpdate = true;
        }
        
        if (NPC.HasValidTarget && target.Distance(NPC.Center) < 800f && DashTimer <= 0)
        {
            AiState = (float)ActionState.Notice;
            AiTimer = 0;
        }
    }

    private void Noticing(Player target)
    {
        if (target.Distance(NPC.Center) < 600f)
        {
            AiTimer++;
            Vector2 Rotation = new Vector2(1).RotatedBy((float)Math.PI * 2f * (AiTimer / 60f));
            
            NPC.velocity = NPC.Center.DirectionTo(NPC.Center + Rotation * 10f) * 2f;
            
            if (AiTimer >= 120)
            {
                AiState = (float)ActionState.Position;
                AiTimer = 0;
            }
        }
        else
        {
            NPC.TargetClosest();

            if (!NPC.HasValidTarget || target.Distance(NPC.Center) > 800f)
            {
                AiState = (float)ActionState.Wander;
                AiTimer = 0;
            }
        }
    }

    private void Positioning(Player target)
    {
        Vector2 CenterHeightOffset = new Vector2(0, -175);
        Vector2 BoxSize = new Vector2(250f, Height);
        DashRangeBox = Utils.CenteredRectangle(target.Center + CenterHeightOffset, BoxSize);

        AiTimer++;
        NPC.velocity = NPC.Center.DirectionTo(DashRangeBox.Center()) * MathF.Min(8f, AiTimer / 30f);
        
        if (NPC.Hitbox.Intersects(DashRangeBox))
        {
            DashTimer++;
            if (DashTimer >= 30)
            {
                AiState = (float)ActionState.Dash;
                AiTimer = 0;
                DashTimer = 0;
            }
        }
    }

    private void Dashing(Player target)
    {
        AiTimer++;
        Vector2 prediction = new Vector2(35, 0) * target.velocity;
        
        switch (AiTimer)
        {
            case 10:
            {
                NPC.velocity = NPC.Center.DirectionTo(target.Center + prediction) * 10f;
                break;
            }
            case >= 120:
            {
                AiState = (float)ActionState.Wander;
                AiTimer = 0;
                DashTimer = 180;
                Main.NewText(2);
                break;
            }
        }
        
        Vector2 PlayerOffsetLowerCenter = new Vector2(target.Center.X, target.Center.Y + (Height / 2f));
        
        if (AiTimer < 120 && NPC.Center.Y > PlayerOffsetLowerCenter.Y && AiTimer != 0)
        {
            NPC.velocity *= 0.98f;
        }
    }

    public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
    {
        if (AiState == (float)ActionState.Dash)
        {
            modifiers.SourceDamage *= DmgMultiplierWhileDashing;
        }
    }

    public override bool? CanFallThroughPlatforms()
    {
        return true;
    }
}