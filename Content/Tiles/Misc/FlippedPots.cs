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
}