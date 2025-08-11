using Microsoft.Xna.Framework;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using TerrariaParadox.Content.Debuffs;
using TerrariaParadox.Content.Debuffs.DoT;
using TerrariaParadox.Content.Items.Materials;
using TerrariaParadox.Content.Items.Tiles.Banners;
using TerrariaParadox.Content.NPCs;
using TerrariaParadox.Content.Tiles.Plants.Cactus;

namespace TerrariaParadox.Content.NPCs.Hostile.Worms
{
	// These three class showcase usage of the WormHead, WormBody and WormTail classes from Worm.cs
	internal class Flatworm : WormHead
	{
		public override int BodyType => ModContent.NPCType<FlatwormBody>();
		public override int TailType => ModContent.NPCType<FlatwormTail>();
		public const int Health = 125;
		public const int Value = 190; //in copper coins = 1 silver is 100 here
		public const int HeadDmg = 35;
		public const int HeadDef = 4;
		public const int BodyDmg = 20;
		public const int BodyDef = 12;
		public const int TailDmg = 25;
		public const int TailDef = 6;
		public const float MovementSpeed = 10f;
		public const float Acceleration = 0.2f;
		public const int MinimumSegments = 9;
		public const int MaximumSegments = 15;

		public override void SetStaticDefaults() 
		{
			
			var drawModifier = new NPCID.Sets.NPCBestiaryDrawModifiers() 
			{ // Influences how the NPC looks in the Bestiary
				CustomTexturePath = "TerrariaParadox/Content/NPCs/Hostile/Worms/FlatwormBestiary", // If the NPC is multiple parts like a worm, a custom texture for the Bestiary is encouraged.
				Position = new Vector2(40f, 24f),
				PortraitPositionXOverride = 0f,
				PortraitPositionYOverride = 12f
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, drawModifier);
		}

		public override void SetDefaults()
		{
			NPC.width = 14;
			NPC.height = 18;
			NPC.aiStyle = -1;
			NPC.netAlways = true;
			NPC.damage = HeadDmg;
			NPC.defense = HeadDef;
			NPC.lifeMax = Health;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.knockBackResist = 0f;
			NPC.behindTiles = true;
			NPC.value = Value;

			Banner = Type;
			// These lines are only needed in the main body part.
			BannerItem = ModContent.ItemType<FlatwormBanner>();
			//ItemID.Sets.KillsToBanner[BannerItem] = 25; default 50
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			float chance = 0f;
			if (spawnInfo.Player.InModBiome(ModContent.GetInstance<Biomes.TheFlipside.BiomeMainSurface>()))// && !NPC.AnyNPCs(Type))so it can spawn one at a time
			{
				chance = 0.33f;
			}
			return chance;
		}
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(ItemDropRule.Common(ItemID.WormTooth, 1, 5, 10));
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<EggCluster>(), 2, 2, 4));
		}
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) 
		{
			// We can use AddRange instead of calling Add multiple times in order to add multiple items at once
			bestiaryEntry.Info.AddRange([
				// Sets the spawning conditions of this NPC that is listed in the bestiary.
				ModContent.GetInstance<Biomes.TheFlipside.BiomeMainSurface>().ModBiomeBestiaryInfoElement,
				ModContent.GetInstance<Biomes.TheFlipside.BiomeUnderground>().ModBiomeBestiaryInfoElement,

				// Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement( "Mods.TerrariaParadox." + LocalizationCategory + "." + Name + ".Bestiary")
			]);
		}



		public override void Init() 
		{
			// Set the segment variance
			// If you want the segment length to be constant, set these two properties to the same value
			MinSegmentLength = MinimumSegments;
			MaxSegmentLength = MaximumSegments;

			CommonWormInit(this);
		}

		internal static void CommonWormInit(ExampleModWorm worm) 
		{
			// These two properties handle the movement of the worm
			worm.MoveSpeed = MovementSpeed;
			worm.Acceleration = Acceleration;
		}

		private int attackCounter;
		public override void SendExtraAI(BinaryWriter writer) 
		{
			writer.Write(attackCounter);
		}

		public override void ReceiveExtraAI(BinaryReader reader) 
		{
			attackCounter = reader.ReadInt32();
		}

		/*public override void AI() 
		{
			if (Main.netMode != NetmodeID.MultiplayerClient) 
			{
				if (attackCounter > 0) 
				{
					attackCounter--; // tick down the attack counter.
				}

				Player target = Main.player[NPC.target];
				// If the attack counter is 0, this NPC is less than 12.5 tiles away from its target, and has a path to the target unobstructed by blocks, summon a projectile.
				if (attackCounter <= 0 && Vector2.Distance(NPC.Center, target.Center) < 200 && Collision.CanHit(NPC.Center, 1, 1, target.Center, 1, 1)) 
				{
					Vector2 direction = (target.Center - NPC.Center).SafeNormalize(Vector2.UnitX);
					direction = direction.RotatedByRandom(MathHelper.ToRadians(10));

					int projectile = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, direction * 1, ProjectileID.ShadowBeamHostile, 5, 0, Main.myPlayer);
					Main.projectile[projectile].timeLeft = 300;
					attackCounter = 500;
					NPC.netUpdate = true;
				}
			}
		}*/
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
		}
	}

	internal class FlatwormBody : WormBody
	{

		public override void SetStaticDefaults() 
		{
			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers() {
				Hide = true // Hides this NPC from the Bestiary, useful for multi-part NPCs whom you only want one entry.
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
			NPCID.Sets.RespawnEnemyID[NPC.type] = ModContent.NPCType<Flatworm>();
		}

		public override void CustomSetDefaults() 
		{
			NPC.width = 14;
			NPC.height = 22;
			NPC.aiStyle = -1;
			NPC.netAlways = true;
			NPC.damage = Flatworm.BodyDmg;
			NPC.defense = Flatworm.BodyDef;
			NPC.lifeMax = Flatworm.Health;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.knockBackResist = 0f;
			NPC.behindTiles = true;
			//NPC.scale = 0.9f;
			NPC.value = Flatworm.Value;
			NPC.dontCountMe = true;

			// Extra body parts should use the same Banner value as the main ModNPC.
			Banner = ModContent.NPCType<Flatworm>();
		}

		public override void Init() 
		{
			Flatworm.CommonWormInit(this);
		}
	}

	internal class FlatwormTail : WormTail
	{
		public override void SetStaticDefaults() 
		{
			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers() 
			{
				Hide = true // Hides this NPC from the Bestiary, useful for multi-part NPCs whom you only want one entry.
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
			NPCID.Sets.RespawnEnemyID[NPC.type] = ModContent.NPCType<Flatworm>();
		}

		public override void CustomSetDefaults() 
		{
			NPC.width = 14;
			NPC.height = 32;
			NPC.aiStyle = -1;
			NPC.netAlways = true;
			NPC.damage = Flatworm.TailDmg;
			NPC.defense = Flatworm.TailDef;
			NPC.lifeMax = Flatworm.Health;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.knockBackResist = 0f;
			NPC.behindTiles = true;
			//NPC.scale = 0.9f;
			NPC.value = Flatworm.Value;
			NPC.dontCountMe = true;

			// Extra body parts should use the same Banner value as the main ModNPC.
			Banner = ModContent.NPCType<Flatworm>();
		}

		public override void Init() 
		{
			Flatworm.CommonWormInit(this);
		}
	}
}
