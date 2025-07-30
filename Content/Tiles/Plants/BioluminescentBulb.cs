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
using TerrariaParadox.Content.Items.Weapons.Magic;
using TerrariaParadox.Content.Items.Weapons.Melee;
using TerrariaParadox.Content.Items.Weapons.Summon.Minions;

namespace TerrariaParadox.Content.Tiles.Plants;

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

			AnimationFrameHeight = 36;
		}

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b) 
		{
			r = 0.93f;
			g = 0.11f;
			b = 0.12f;
		}

		public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset) 
		{
			// Tweak the frame drawn by x position so tiles next to each other are off-sync and look much more interesting
			int uniqueAnimationFrame = Main.tileFrame[Type] + i;

			if (i % 2 == 0)
			{
				uniqueAnimationFrame += 3;
			}
			if (i % 3 == 0)
			{
				uniqueAnimationFrame += 3;
				
			}

			if (i % 4 == 0)
			{
				uniqueAnimationFrame += 3;
			}
			
			uniqueAnimationFrame %= Frames;
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

		public override IEnumerable<Item> GetItemDrops(int i, int j)
		{
			int Random = Main.rand.Next(1, 7);
			Dictionary<int, int> DropsList = new Dictionary<int, int>()
			{
				{1, ModContent.ItemType<Leecharang>()},
				{2, ItemID.Musket},
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

		public int FrameTime = 12;
		public int Frames = 5;
		public override void AnimateTile(ref int frame, ref int frameCounter) 
		{
			frameCounter++;
			if (frameCounter >= FrameTime) 
			{
				frameCounter = 0;
				if (++frame >= Frames) 
				{
					frame = 0;
				}
			}
		}
}