using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using TerrariaParadox.Content.Tiles.Blocks;

namespace TerrariaParadox
{
	// Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
	public class TerrariaParadox : Mod
	{
		public static List<int> InfestedBlocks;
		
		#region Load
        public override void Load()
        {
	        PopulateArrays();
	        MethodSwaps.ApplyMethodSwaps();
		}

		private void LoadClient()
        {
		}
		#endregion

		#region Unload
        public override void Unload()
        {
	        UnpopulateArrays();
		}
		#endregion

		private void PopulateArrays()
		{
			InfestedBlocks = new List<int>()
			{
				ModContent.TileType<InfestedGrassBlock>(),
				ModContent.TileType<InfestedJungleGrassBlock>(),
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
