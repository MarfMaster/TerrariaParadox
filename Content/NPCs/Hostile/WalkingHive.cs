using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Debuffs.DoT;
using TerrariaParadox.Content.Items.Tiles.Banners;
using TerrariaParadox.Content.Projectiles.Hostile;

namespace TerrariaParadox.Content.NPCs.Hostile;
[Autoload(false)]
public class WalkingHive : ModNPC
{
    public const int Width = 48;
    public const int Height = 64;
    public const int BaseDmg = 15;
    public const int Defense = 0;
    public const int DefenseFromBack = 20;
    public const int BaseHP = 1000;
    public const int Value = 100000;
    public const int FrameDuration = 5;
    public const int Frames = 4;

    public ref float AiState => ref NPC.ai[0];
    public ref float AiTimer => ref NPC.ai[1];
    public ref float DashTimer => ref NPC.ai[2];
    private enum ActionState
    {
        Wandering,
        Aggroed,
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
        Aggroed1,
        Aggroed2,
        Aggroed3,
        Aggroed4,
        Aggroed5,
        Aggroed6,
        Aggroed7,
        Aggroed8,
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
                NPC.frame.Y = 0;
            }
        }
    }

    public override void AI()
    {/*
        Player target = Main.player[NPC.target];
        switch (AiState)
        {
            case (float)ActionState.Wandering:
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
        if ((Main.GameUpdateCount % 60 == 0 ) && Main.netMode != NetmodeID.MultiplayerClient)
        {
            Projectile.NewProjectile(Projectile.InheritSource(this.Entity), NPC.Center,
                NPC.Center.DirectionTo(target.Center) * 10f, ModContent.ProjectileType<WalkingHivePuke>(), 10, 5f);
        }*/
    }
}