using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Tiles.Blocks;

namespace TerrariaParadox
{
	// Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
	public partial class TerrariaParadox : Mod
	{
		public static TerrariaParadox instance;

		static int[] wallHammerRequirement;
		public static int[] WallHammerRequirement
		{
			get => wallHammerRequirement;
		}
		static bool[] tileTransformsOnKill;
		public static bool[] TileTransformsOnKill
		{
			get => tileTransformsOnKill;
		}
		public TerrariaParadox()
		{
			instance = this;
		}
			public static List<int> InfestedBlocks;
		
			#region Load
			public override void Load()
			{
				ApplyMethodSwaps();
			}

			private void LoadClient()
			{
			}
			#endregion

			#region Unload
			public override void Unload()
			{
				instance = null;
				UnpopulateArrays();
			}
			#endregion

			internal static void PopulateArrays()
			{
				Array.Resize(ref wallHammerRequirement, WallLoader.WallCount);
				tileTransformsOnKill = TileID.Sets.Factory.CreateBoolSet(false);
				InfestedBlocks = new List<int>()
				{
					ModContent.TileType<FlippedGrassBlock>(),
					ModContent.TileType<FlippedJungleGrassBlock>(),
					ModContent.TileType<AssecstoneBlockTile>(),
					ModContent.TileType<MurkyIceBlockTile>(),
					ModContent.TileType<AssecsandBlockTile>(),
					ModContent.TileType<HardenedAssecsandBlockTile>(),
					ModContent.TileType<AssecsandstoneBlockTile>()
				};
			}
			private void UnpopulateArrays()
			{
				InfestedBlocks = null;
			}
	}

}