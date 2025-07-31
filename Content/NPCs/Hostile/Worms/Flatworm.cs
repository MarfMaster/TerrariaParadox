using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.NPCs;

namespace TerrariaParadox.Content.NPCs.Hostile.Worms
{
	// These three class showcase usage of the WormHead, WormBody and WormTail classes from Worm.cs
	internal class FlatwormHead : WormHead
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
				CustomTexturePath = "TerrariaParadox/Content/NPCs/Hostile/Worms/FlatwormHead", // If the NPC is multiple parts like a worm, a custom texture for the Bestiary is encouraged.
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
			//NPC.scale = 0.9f;
			NPC.value = Value;

			Banner = Type;
			// These lines are only needed in the main body part.
			BannerItem = ItemID.WormBanner;
			//ItemID.Sets.KillsToBanner[BannerItem] = 25; default 50
		}

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) 
		{
			// We can use AddRange instead of calling Add multiple times in order to add multiple items at once
			bestiaryEntry.Info.AddRange([
				// Sets the spawning conditions of this NPC that is listed in the bestiary.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns,

				// Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("Mods.TerrariaParadox.Bestiary.Flatworm")
			]);
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(ItemDropRule.Common(ItemID.WormTooth, 1, 5, 10));
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
	}

	internal class FlatwormBody : WormBody
	{
		public override void SetStaticDefaults() 
		{
			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers() {
				Hide = true // Hides this NPC from the Bestiary, useful for multi-part NPCs whom you only want one entry.
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
			NPCID.Sets.RespawnEnemyID[NPC.type] = ModContent.NPCType<FlatwormHead>();
		}

		public override void SetDefaults() 
		{
			NPC.width = 14;
			NPC.height = 22;
			NPC.aiStyle = -1;
			NPC.netAlways = true;
			NPC.damage = FlatwormHead.BodyDmg;
			NPC.defense = FlatwormHead.BodyDef;
			NPC.lifeMax = FlatwormHead.Health;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.knockBackResist = 0f;
			NPC.behindTiles = true;
			//NPC.scale = 0.9f;
			NPC.value = FlatwormHead.Value;
			NPC.dontCountMe = true;

			// Extra body parts should use the same Banner value as the main ModNPC.
			Banner = ModContent.NPCType<FlatwormHead>();
		}

		public override void Init() 
		{
			FlatwormHead.CommonWormInit(this);
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
			NPCID.Sets.RespawnEnemyID[NPC.type] = ModContent.NPCType<FlatwormHead>();
		}

		public override void SetDefaults() 
		{
			NPC.width = 14;
			NPC.height = 32;
			NPC.aiStyle = -1;
			NPC.netAlways = true;
			NPC.damage = FlatwormHead.TailDmg;
			NPC.defense = FlatwormHead.TailDef;
			NPC.lifeMax = FlatwormHead.Health;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.knockBackResist = 0f;
			NPC.behindTiles = true;
			//NPC.scale = 0.9f;
			NPC.value = FlatwormHead.Value;
			NPC.dontCountMe = true;

			// Extra body parts should use the same Banner value as the main ModNPC.
			Banner = ModContent.NPCType<FlatwormHead>();
		}

		public override void Init() 
		{
			FlatwormHead.CommonWormInit(this);
		}
	}
}
