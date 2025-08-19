using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using TerrariaParadox.Content.Dusts.Tiles;
using TerrariaParadox.Content.Dusts.Tiles.Blocks;
using TerrariaParadox.Content.Dusts.Tiles.Misc;
using TerrariaParadox.Content.Items.Weapons.Magic;
using TerrariaParadox.Content.Items.Weapons.Melee;
using TerrariaParadox.Content.Items.Weapons.Summon.Minions;

namespace TerrariaParadox.Content.Tiles.Misc;

public class FlippedPots : ModTile
{

	public override void SetStaticDefaults() 
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileCut[Type] = true;
			//Main.tileNoFail[Type] = true;
			Main.tileObsidianKill[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(Type);

			AddMapEntry(new Color(36, 40, 47), Language.GetText("MapObject.Pot"));

			DustType = ModContent.DustType<AssecstoneDust>();
		}

		// This method allows you to change the sound a tile makes when hit
		public override bool KillSound(int i, int j, bool fail) 
		{
			// Play the glass shattering sound instead of the normal digging sound if the tile is destroyed on this hit
			if (!fail) 
			{
				SoundEngine.PlaySound(SoundID.Shatter, new Vector2(i, j).ToWorldCoordinates());
				return false;
			}
			return base.KillSound(i, j, fail);
		}
		/*public override IEnumerable<Item> GetItemDrops(int i, int j)
		{
			//chance for coin portal that drops 5-15 gold, based on luck
			//ftw seed lit bomb projectile drop 1/6 chance
			//don't dig up seed fallen star drop 1/5 chance, half the time as item, half the time inside of a slime, always drops the item in the underworld
			//If the pot is within the "safe area" of the underworld (here up to 20 blocks above the underworld and within the center 26% of the world), if a star fails to drop, the pot will always drop 20–40 RopeRope.
			//1/45 chance to drop a potion, varying with depth: 
			//Surface: 1/10 Ironskin, 1/10 Shine, 1/10 Night Owl, 1/10 Swiftness, 1/10 Mining, 1/10 Calming, 1/10 Builder, 3/10 Recall
			//Underground: 1/11 Regeneration, 1/11 Shine, 1/11 Night Owl, 1/11 Swiftness, 1/11 Archery, 1/11 Gills, 1/11 Hunter, 1/11 Mining, 1/11 Dangersense, 2/11 Recall
			//Cavern: 1/15 Spelunker, 1/15 Featherfall, 1/15 Night Owl, 1/15 Water Walking, 1/15 Archery, 1/15 Gravity, 1/15 Hunter, 1/15 Invisibility, 1/15 Thorns, 1/15 Mining, 1/15 Heartreach, 1/15 Flipper, 1/15 Dangersense, 1/15 Recall
			//Underworld: 1/14 Spelunker, 1/14 Featherfall, 1/14 Mana Regen, 1/14 Obsidian Skin, 1/14 Magic Power, 1/14 Invisibility, 1/14 Hunter, 1/14 Gravity, 1/14 Thorns, 1/14 Water Walking, 1/14 Battle, 1/14 Heartreach, 1/14 Titan, 1/5 Return(this can drop with any of the above underworld pots)
			//in multiplayer: 1/30 Wormhole
			//gen rand 0 to 6(inclusive): if s 0, if closest player to pot not full hp drop 1 heart, 50% chance to drop another heart, in expert 2 additional 50% chance to drop 1 heart each
			//else if closest player full hp, but has less than 20 of any torch, drop torches, else drop money
			//if s 1: drop 4-12 flipped torches, 5-18 in expert
			//if s 2: drop 10-20 ammo, default wooden arrow, in surface or underground it's 50% chance to be shuriken in pre-hardmode, grenade in hardmode, in underworld it'll be hellfire arrows
			// in hardmode it'll be replaced by unholy arrow or silver/tungsten bullets, both 50% chance each
			//if s 3: drop 1 lesser heal pot, or normal hel pot if in underworld or hardmode, 33% chance for 1 extra in expert
			//if s 4: if desert pot, drop 1-4 scarab bombs, 1-7 in expert, else if above underworld drop 1-4 normal bombs, 1-7 in expert, else drop ropes
			//if s 5: if not in underworld and hardmode, drop 20-40 ropes, else drop money
			//if all else fails, drop money: 80 copper - 3.60 silver base, multiplied by 50% if on surface, 75% if above cavern layer, 125% if pot in bottom 250 tiles in world,
			//random 25% chance for 105-110% mult, 12.5% chance for 110-120% mult, 8.33% chance for 120-140% mult, 6.25% chance for 140-180%, 5% chance for 150-200% mult,
			//250% mult in expert, 25% chance in expert for extra 125% mult, 33% chance in expert for extra 150% mult, 25% chance in expert for extra 175% mult,
			//110% mult for most bosses if defeated
			//160% mult for corrupt pots
		}
		/*private static void SpawnThingsFromPot(int i, int j, int x2, int y2, int style)
		{
			bool flag = (double)j < Main.rockLayer;
			bool flag2 = j < Main.UnderworldLayer;
			if (Main.remixWorld)
			{
				flag = (double)j > Main.rockLayer && j < Main.UnderworldLayer;
				flag2 = (double)j > Main.worldSurface && (double)j < Main.rockLayer;
			}
			float num = 1f;
			bool flag3 = style >= 34 && style <= 36;
			switch (style)
			{
			case 4:
			case 5:
			case 6:
				num = 1.25f;
				break;
			case 7:
			case 8:
			case 9:
				num = 1.75f;
				break;
			default:
				if (style >= 10 && style <= 12)
				{
					num = 1.9f;
				}
				else if (style >= 13 && style <= 15)
				{
					num = 2.1f;
				}
				else if (style >= 16 && style <= 18)
				{
					num = 1.6f;
				}
				else if (style >= 19 && style <= 21)
				{
					num = 3.5f;
				}
				else if (style >= 22 && style <= 24)
				{
					num = 1.6f;
				}
				else if (style >= 25 && style <= 27)
				{
					num = 10f;
				}
				else if (style >= 28 && style <= 30)
				{
					if (Main.hardMode)
					{
						num = 4f;
					}
				}
				else if (style >= 31 && style <= 33)
				{
					num = 2f;
				}
				else if (style >= 34 && style <= 36)
				{
					num = 1.25f;
				}
				break;
			case 0:
			case 1:
			case 2:
			case 3:
				break;
			}
			num = (num * 2f + 1f) / 3f;
			int range = (int)(500f / ((num + 1f) / 2f));
			if (WorldGen.gen)
			{
				return;
			}
			if (Player.GetClosestRollLuck(i, j, range) == 0f)
			{
				if (Main.netMode != 1)
				{
					Projectile.NewProjectile(WorldGen.GetProjectileSource_TileBreak(i, j), i * 16 + 16, j * 16 + 16, 0f, -12f, 518, 0, 0f, Main.myPlayer);
				}
				return;
			}
			if (WorldGen.genRand.Next(35) == 0 && Main.wallDungeon[Main.tile[i, j].wall] && (double)j > Main.worldSurface)
			{
				Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 327);
				return;
			}
			if (Main.getGoodWorld && WorldGen.genRand.Next(6) == 0)
			{
				Projectile.NewProjectile(WorldGen.GetProjectileSource_TileBreak(i, j), i * 16 + 16, j * 16 + 8, (float)Main.rand.Next(-100, 101) * 0.002f, 0f, 28, 0, 0f, Main.myPlayer, 16f, 16f);
				return;
			}
			if (Main.remixWorld && Main.netMode != 1 && WorldGen.genRand.Next(5) == 0)
			{
				Player player = Main.player[Player.FindClosest(new Vector2(i * 16, j * 16), 16, 16)];
				if (Main.rand.Next(2) == 0)
				{
					Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 75);
				}
				else if (player.ZoneJungle)
				{
					int num12 = -1;
					num12 = NPC.NewNPC(NPC.GetSpawnSourceForNaturalSpawn(), x2 * 16 + 16, y2 * 16 + 32, -10);
					if (num12 > -1)
					{
						Main.npc[num12].ai[1] = 75f;
						Main.npc[num12].netUpdate = true;
					}
				}
				else if ((double)j > Main.rockLayer && j < Main.maxTilesY - 350)
				{
					int num13 = -1;
					num13 = ((Main.rand.Next(9) == 0) ? NPC.NewNPC(NPC.GetSpawnSourceForNaturalSpawn(), x2 * 16 + 16, y2 * 16 + 32, -7) : ((Main.rand.Next(7) == 0) ? NPC.NewNPC(NPC.GetSpawnSourceForNaturalSpawn(), x2 * 16 + 16, y2 * 16 + 32, -8) : ((Main.rand.Next(6) == 0) ? NPC.NewNPC(NPC.GetSpawnSourceForNaturalSpawn(), x2 * 16 + 16, y2 * 16 + 32, -9) : ((Main.rand.Next(3) != 0) ? NPC.NewNPC(NPC.GetSpawnSourceForNaturalSpawn(), x2 * 16 + 16, y2 * 16 + 32, 1) : NPC.NewNPC(NPC.GetSpawnSourceForNaturalSpawn(), x2 * 16 + 16, y2 * 16 + 32, -3)))));
					if (num13 > -1)
					{
						Main.npc[num13].ai[1] = 75f;
						Main.npc[num13].netUpdate = true;
					}
				}
				else if ((double)j > Main.worldSurface && (double)j <= Main.rockLayer)
				{
					int num14 = -1;
					num14 = NPC.NewNPC(NPC.GetSpawnSourceForNaturalSpawn(), x2 * 16 + 16, y2 * 16 + 32, -6);
					if (num14 > -1)
					{
						Main.npc[num14].ai[1] = 75f;
						Main.npc[num14].netUpdate = true;
					}
				}
				else
				{
					Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 75);
				}
				return;
			}
			if (Main.remixWorld && (double)i > (double)Main.maxTilesX * 0.37 && (double)i < (double)Main.maxTilesX * 0.63 && j > Main.maxTilesY - 220)
			{
				int stack = Main.rand.Next(20, 41);
				Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 965, stack);
				return;
			}
			if (WorldGen.genRand.Next(45) == 0 || (Main.rand.Next(45) == 0 && Main.expertMode))
			{
				if ((double)j < Main.worldSurface)
				{
					int num16 = WorldGen.genRand.Next(10);
					if (num16 == 0)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 292);
					}
					if (num16 == 1)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 298);
					}
					if (num16 == 2)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 299);
					}
					if (num16 == 3)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 290);
					}
					if (num16 == 4)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 2322);
					}
					if (num16 == 5)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 2324);
					}
					if (num16 == 6)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 2325);
					}
					if (num16 >= 7)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 2350, WorldGen.genRand.Next(1, 3));
					}
				}
				else if (flag)
				{
					int num17 = WorldGen.genRand.Next(11);
					if (num17 == 0)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 289);
					}
					if (num17 == 1)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 298);
					}
					if (num17 == 2)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 299);
					}
					if (num17 == 3)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 290);
					}
					if (num17 == 4)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 303);
					}
					if (num17 == 5)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 291);
					}
					if (num17 == 6)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 304);
					}
					if (num17 == 7)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 2322);
					}
					if (num17 == 8)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 2329);
					}
					if (num17 >= 7)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 2350, WorldGen.genRand.Next(1, 3));
					}
				}
				else if (flag2)
				{
					int num18 = WorldGen.genRand.Next(15);
					if (num18 == 0)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 296);
					}
					if (num18 == 1)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 295);
					}
					if (num18 == 2)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 299);
					}
					if (num18 == 3)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 302);
					}
					if (num18 == 4)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 303);
					}
					if (num18 == 5)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 305);
					}
					if (num18 == 6)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 301);
					}
					if (num18 == 7)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 302);
					}
					if (num18 == 8)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 297);
					}
					if (num18 == 9)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 304);
					}
					if (num18 == 10)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 2322);
					}
					if (num18 == 11)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 2323);
					}
					if (num18 == 12)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 2327);
					}
					if (num18 == 13)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 2329);
					}
					if (num18 >= 7)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 2350, WorldGen.genRand.Next(1, 3));
					}
				}
				else
				{
					int num19 = WorldGen.genRand.Next(14);
					if (num19 == 0)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 296);
					}
					if (num19 == 1)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 295);
					}
					if (num19 == 2)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 293);
					}
					if (num19 == 3)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 288);
					}
					if (num19 == 4)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 294);
					}
					if (num19 == 5)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 297);
					}
					if (num19 == 6)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 304);
					}
					if (num19 == 7)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 305);
					}
					if (num19 == 8)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 301);
					}
					if (num19 == 9)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 302);
					}
					if (num19 == 10)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 288);
					}
					if (num19 == 11)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 300);
					}
					if (num19 == 12)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 2323);
					}
					if (num19 == 13)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 2326);
					}
					if (WorldGen.genRand.Next(5) == 0)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 4870);
					}
				}
				return;
			}
			if (Main.netMode == 2 && Main.rand.Next(30) == 0)
			{
				Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 2997);
				return;
			}
			int num15 = Main.rand.Next(7);
			if (Main.expertMode)
			{
				num15--;
			}
			Player player2 = Main.player[Player.FindClosest(new Vector2(i * 16, j * 16), 16, 16)];
			int num2 = 0;
			int num3 = 20;
			for (int k = 0; k < 50; k++)
			{
				Item item = player2.inventory[k];
				if (!item.IsAir && item.createTile == 4)
				{
					num2 += item.stack;
					if (num2 >= num3)
					{
						break;
					}
				}
			}
			bool flag4 = num2 < num3;
			if (num15 == 0 && player2.statLife < player2.statLifeMax2)
			{
				Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 58);
				if (Main.rand.Next(2) == 0)
				{
					Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 58);
				}
				if (Main.expertMode)
				{
					if (Main.rand.Next(2) == 0)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 58);
					}
					if (Main.rand.Next(2) == 0)
					{
						Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 58);
					}
				}
				return;
			}
			if (num15 == 1 || (num15 == 0 && flag4))
			{
				int num4 = Main.rand.Next(2, 7);
				if (Main.expertMode)
				{
					num4 += Main.rand.Next(1, 7);
				}
				int type = 8;
				int type2 = 282;
				if (player2.ZoneHallow)
				{
					num4 += Main.rand.Next(2, 7);
					type = 4387;
				}
				else if ((style >= 22 && style <= 24) || player2.ZoneCrimson)
				{
					num4 += Main.rand.Next(2, 7);
					type = 4386;
				}
				else if ((style >= 16 && style <= 18) || player2.ZoneCorrupt)
				{
					num4 += Main.rand.Next(2, 7);
					type = 4385;
				}
				else if (style >= 7 && style <= 9)
				{
					num4 += Main.rand.Next(2, 7);
					num4 = (int)((float)num4 * 1.5f);
					type = 4388;
				}
				else if (style >= 4 && style <= 6)
				{
					type = 974;
					type2 = 286;
				}
				else if (style >= 34 && style <= 36)
				{
					num4 += Main.rand.Next(2, 7);
					type = 4383;
				}
				else if (player2.ZoneGlowshroom)
				{
					num4 += Main.rand.Next(2, 7);
					type = 5293;
				}
				if (Main.tile[i, j].liquid > 0)
				{
					Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, type2, num4);
				}
				else
				{
					Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, type, num4);
				}
				return;
			}
			switch (num15)
			{
			case 2:
			{
				int stack2 = Main.rand.Next(10, 21);
				int type4 = 40;
				if (flag && WorldGen.genRand.Next(2) == 0)
				{
					type4 = ((!Main.hardMode) ? 42 : 168);
				}
				if (j > Main.UnderworldLayer)
				{
					type4 = 265;
				}
				else if (Main.hardMode)
				{
					type4 = ((Main.rand.Next(2) != 0) ? 47 : ((SavedOreTiers.Silver != 168) ? 278 : 4915));
				}
				Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, type4, stack2);
				return;
			}
			case 3:
			{
				int type5 = 28;
				if (j > Main.UnderworldLayer || Main.hardMode)
				{
					type5 = 188;
				}
				int num6 = 1;
				if (Main.expertMode && Main.rand.Next(3) != 0)
				{
					num6++;
				}
				Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, type5, num6);
				return;
			}
			case 4:
				if (flag3 || flag2)
				{
					int type3 = 166;
					if (flag3)
					{
						type3 = 4423;
					}
					int num5 = Main.rand.Next(4) + 1;
					if (Main.expertMode)
					{
						num5 += Main.rand.Next(4);
					}
					Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, type3, num5);
					return;
				}
				break;
			}
			if ((num15 == 4 || num15 == 5) && j < Main.UnderworldLayer && !Main.hardMode)
			{
				int stack3 = Main.rand.Next(20, 41);
				Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 965, stack3);
				return;
			}
			float num7 = 200 + WorldGen.genRand.Next(-100, 101);
			if ((double)j < Main.worldSurface)
			{
				num7 *= 0.5f;
			}
			else if (flag)
			{
				num7 *= 0.75f;
			}
			else if (j > Main.maxTilesY - 250)
			{
				num7 *= 1.25f;
			}
			num7 *= 1f + (float)Main.rand.Next(-20, 21) * 0.01f;
			if (Main.rand.Next(4) == 0)
			{
				num7 *= 1f + (float)Main.rand.Next(5, 11) * 0.01f;
			}
			if (Main.rand.Next(8) == 0)
			{
				num7 *= 1f + (float)Main.rand.Next(10, 21) * 0.01f;
			}
			if (Main.rand.Next(12) == 0)
			{
				num7 *= 1f + (float)Main.rand.Next(20, 41) * 0.01f;
			}
			if (Main.rand.Next(16) == 0)
			{
				num7 *= 1f + (float)Main.rand.Next(40, 81) * 0.01f;
			}
			if (Main.rand.Next(20) == 0)
			{
				num7 *= 1f + (float)Main.rand.Next(50, 101) * 0.01f;
			}
			if (Main.expertMode)
			{
				num7 *= 2.5f;
			}
			if (Main.expertMode && Main.rand.Next(2) == 0)
			{
				num7 *= 1.25f;
			}
			if (Main.expertMode && Main.rand.Next(3) == 0)
			{
				num7 *= 1.5f;
			}
			if (Main.expertMode && Main.rand.Next(4) == 0)
			{
				num7 *= 1.75f;
			}
			num7 *= num;
			if (NPC.downedBoss1)
			{
				num7 *= 1.1f;
			}
			if (NPC.downedBoss2)
			{
				num7 *= 1.1f;
			}
			if (NPC.downedBoss3)
			{
				num7 *= 1.1f;
			}
			if (NPC.downedMechBoss1)
			{
				num7 *= 1.1f;
			}
			if (NPC.downedMechBoss2)
			{
				num7 *= 1.1f;
			}
			if (NPC.downedMechBoss3)
			{
				num7 *= 1.1f;
			}
			if (NPC.downedPlantBoss)
			{
				num7 *= 1.1f;
			}
			if (NPC.downedQueenBee)
			{
				num7 *= 1.1f;
			}
			if (NPC.downedGolemBoss)
			{
				num7 *= 1.1f;
			}
			if (NPC.downedPirates)
			{
				num7 *= 1.1f;
			}
			if (NPC.downedGoblins)
			{
				num7 *= 1.1f;
			}
			if (NPC.downedFrost)
			{
				num7 *= 1.1f;
			}
			while ((int)num7 > 0)
			{
				if (num7 > 1000000f)
				{
					int num8 = (int)(num7 / 1000000f);
					if (num8 > 50 && Main.rand.Next(2) == 0)
					{
						num8 /= Main.rand.Next(3) + 1;
					}
					if (Main.rand.Next(2) == 0)
					{
						num8 /= Main.rand.Next(3) + 1;
					}
					num7 -= (float)(1000000 * num8);
					Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 74, num8);
					continue;
				}
				if (num7 > 10000f)
				{
					int num9 = (int)(num7 / 10000f);
					if (num9 > 50 && Main.rand.Next(2) == 0)
					{
						num9 /= Main.rand.Next(3) + 1;
					}
					if (Main.rand.Next(2) == 0)
					{
						num9 /= Main.rand.Next(3) + 1;
					}
					num7 -= (float)(10000 * num9);
					Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 73, num9);
					continue;
				}
				if (num7 > 100f)
				{
					int num10 = (int)(num7 / 100f);
					if (num10 > 50 && Main.rand.Next(2) == 0)
					{
						num10 /= Main.rand.Next(3) + 1;
					}
					if (Main.rand.Next(2) == 0)
					{
						num10 /= Main.rand.Next(3) + 1;
					}
					num7 -= (float)(100 * num10);
					Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 72, num10);
					continue;
				}
				int num11 = (int)num7;
				if (num11 > 50 && Main.rand.Next(2) == 0)
				{
					num11 /= Main.rand.Next(3) + 1;
				}
				if (Main.rand.Next(2) == 0)
				{
					num11 /= Main.rand.Next(4) + 1;
				}
				if (num11 < 1)
				{
					num11 = 1;
				}
				num7 -= (float)num11;
				Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, 71, num11);
			}
		}*/
}