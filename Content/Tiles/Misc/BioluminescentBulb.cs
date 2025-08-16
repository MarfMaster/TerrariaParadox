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
using TerrariaParadox.Content.Dusts.Tiles.Misc;
using TerrariaParadox.Content.Items.Weapons.Magic;
using TerrariaParadox.Content.Items.Weapons.Melee;
using TerrariaParadox.Content.Items.Weapons.Ranged;
using TerrariaParadox.Content.Items.Weapons.Summon.Minions;

namespace TerrariaParadox.Content.Tiles.Misc;

public class BioluminescentBulb : ModTile
{

	public override void SetStaticDefaults() 
		{
			Main.tileLighted[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = false;
			Main.tileNoAttach[Type] = true;
			Main.tileHammer[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.AnchorBottom = AnchorData.Empty;
			TileObjectData.newTile.AnchorWall = true;
			TileObjectData.addTile(Type);

			LocalizedText name = CreateMapEntryName();
			AddMapEntry(new Color(64, 129, 119), name);

			DustType = ModContent.DustType<BioluminescentBulbDust>();
				
			AnimationFrameHeight = 36;
		}

		private const float Red = 0.15f;
		private const float Green = 0.85f;
		private const float Blue = 0.58f;
		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			//Main.NewText(CurrentFrameCounter);
			float lightLevel = (0.003f * CurrentFrameCounter);
			r = Red * lightLevel;
			g = Green * lightLevel;
			b = Blue * lightLevel;
		}

		public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset) 
		{
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

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			if (!WorldGen.shadowOrbSmashed)
			{
				WorldGen.shadowOrbSmashed = true;
			}

			WorldGen.shadowOrbCount++;
		}

		public override IEnumerable<Item> GetItemDrops(int i, int j)
		{
			int Random = !Condition.SmashedShadowOrb.IsMet() ? 2 : Main.rand.Next(1, 7);
			Dictionary<int, int> DropsList = new Dictionary<int, int>()
			{
				{1, ModContent.ItemType<Leecharang>()},
				{2, ModContent.ItemType<Parasyte>()},
				{3, ModContent.ItemType<Lighter>()},
				{4, ModContent.ItemType<Suspicious8Ball>()},
				{5, ItemID.ShadowOrb},
				{6, ItemID.BandofStarpower},
			};
			Item Drop = new Item(DropsList[Random]);
			yield return Drop;
			if (Random == 2)
			{
				yield return new Item(ItemID.MusketBall, 100);
			}
		}

		public int FrameTime = 50;
		public int Frames = 5;
		public bool FrameOrder = false;
		public float CurrentFrameCounter = 1;
		public override void AnimateTile(ref int frame, ref int frameCounter)
		{
			if (!FrameOrder)
			{
				frameCounter++;
				if (frameCounter >= FrameTime) 
				{
					frameCounter = 1;
					if (++frame == Frames - 1)
					{
						FrameOrder = true;
					}
				}
			}
			else
			{
				frameCounter--;
				if (frameCounter <= 1) 
				{
					frameCounter = FrameTime - 1;
					if (--frame == 0)
					{
						FrameOrder = false;
					}
				}
			}
			CurrentFrameCounter = (float)(frameCounter + (frame * FrameTime));
		}
}