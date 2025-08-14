using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
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
                Provoked(target);
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
        if ((Main.GameUpdateCount % 60 == 0 ) && Main.netMode != NetmodeID.MultiplayerClient)
        {
            Projectile.NewProjectile(Projectile.InheritSource(this.Entity), NPC.Center,
                NPC.Center.DirectionTo(target.Center) * 10f, ModContent.ProjectileType<WalkingHivePuke>(), 10, 5f);
        }
    }

    private void Wandering(Player target)
    {
        AiTimer++;
        NPC.TargetClosest(false);

        if ((AiTimer % 60 == 0 || AiTimer == 1) && Main.netMode != NetmodeID.MultiplayerClient)
        {
            NPC.velocity = new Vector2(5, 0);//Main.rand.NextFloat(-2f, 2f), 0);
            
            NPC.netUpdate = true;
        }
        
        /*if (NPC.HasValidTarget && target.Distance(NPC.Center) < 800f)
        {
            AiState = (float)ActionState.Provoked;
            AiTimer = 0;
        }*/
    }
    private void Provoked(Player target)
    {
        
    }
    private void Hunting(Player target)
    {
        
    }
    private void Preparing(Player target)
    {
        
    }
    private void Puking(Player target)
    {
        
    }
    private void Tired(Player target)
    {
        
    }
    private void Fleeing(Player target)
    {
        
    }
    private void Exploding(Player target)
    {
        
    }
    
}