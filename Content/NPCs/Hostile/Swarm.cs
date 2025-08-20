using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Biomes.TheFlipside;
using TerrariaParadox.Content.Debuffs.DoT;
using TerrariaParadox.Content.Items.Tiles.Banners;

namespace TerrariaParadox.Content.NPCs.Hostile;

public class Swarm : ModdedHostileNPC
{
    private readonly float DmgMultiplierWhileDashing = 2f;
    private Rectangle DashRangeBox;
    public override int TotalAnimationFrames => 4;
    public override int Width => 52;
    public override int Height => 52;
    public override int BaseLifePoints => 70;
    public override int BaseDefense => 0;
    public override float BaseKnockbackReceivedMultiplier => 0f;
    public override int BaseContactDamage => 15;
    public override SoundStyle OnHitSound => SoundID.NPCDeath9;
    public override SoundStyle OnDeathSound => SoundID.NPCDeath11;
    public override int Value => 100;
    public override int BannerItemType => ModContent.ItemType<SwarmBanner>();

    private ref float DashTimer => ref AiTimer2;

    public override void CustomSetStaticDefaults()
    {
        NPCID.Sets.SpecificDebuffImmunity[Type][ModContent.BuffType<LeecharangBleed>()] = true;
    }

    public override void CustomSetDefaults()
    {
        NPC.noGravity = true;
    }

    public override float SpawnChance(NPCSpawnInfo spawnInfo)
    {
        var chance = 0f;
        if (spawnInfo.Player.InModBiome(ModContent
                .GetInstance<BiomeMainSurface>())) // && !NPC.AnyNPCs(Type))so it can spawn one at a time
            chance = 0.1f;
        return chance;
    }

    public override void AI()
    {
        var target = Main.player[NPC.target];
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
        FrameDuration = 5;
        NPC.spriteDirection = NPC.direction;

        NPC.frameCounter++;
        if (NPC.frameCounter >= FrameDuration)
        {
            NPC.frame.Y += frameHeight;
            NPC.frameCounter = 0;

            if (NPC.frame.Y >= Main.npcFrameCount[Type] * frameHeight) NPC.frame.Y = (int)Frame.First;
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
            var rotation = new Vector2(1).RotatedBy((float)Math.PI * 2f * (AiTimer / 60f));

            NPC.velocity = NPC.Center.DirectionTo(NPC.Center + rotation * 10f) * 2f;

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
        var centerHeightOffset = new Vector2(0, -175);
        var boxSize = new Vector2(250f, Height);
        DashRangeBox = Utils.CenteredRectangle(target.Center + centerHeightOffset, boxSize);

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
        var prediction = new Vector2(35, 0) * target.velocity;

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

        var playerOffsetLowerCenter = new Vector2(target.Center.X, target.Center.Y + Height / 2f);

        if (AiTimer < 120 && NPC.Center.Y > playerOffsetLowerCenter.Y && AiTimer != 0) NPC.velocity *= 0.98f;
    }

    public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
    {
        if (AiState == (float)ActionState.Dash) modifiers.SourceDamage *= DmgMultiplierWhileDashing;
    }

    public override bool? CanFallThroughPlatforms()
    {
        return true;
    }

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
}