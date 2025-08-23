using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Tiles.Blocks;
using TerrariaParadox.Content.Tiles.Plants;

namespace TerrariaParadox;

public partial class ParadoxTile : GlobalTile
{
	private int _flippedVine;
	private List<int> _flippedVineBlocksList = new List<int>();
	public override void SetStaticDefaults()
	{
		_flippedVine = ModContent.TileType<FlippedVine>();
		_flippedVineBlocksList = new List<int>()
		{
			ModContent.TileType<FlippedGrassBlock>(),
			ModContent.TileType<FlippedJungleGrassBlock>()
		};
	}
	}