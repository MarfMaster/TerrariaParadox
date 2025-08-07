using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using TerrariaParadox.Content.Dusts.Tiles;
using TerrariaParadox.Content.Dusts.Tiles.Blocks;
using TerrariaParadox.Content.Dusts.Tiles.Misc;
using TerrariaParadox.Content.Items.Tiles.Blocks;
using TerrariaParadox.Content.Items.Weapons.Magic;
using TerrariaParadox.Content.Items.Weapons.Melee;
using TerrariaParadox.Content.Items.Weapons.Summon.Minions;

namespace TerrariaParadox.Content.Tiles.Misc;

public class AssecstoneRubble1x1HangingNatural : ModdedRubbleNatural
{
	public const int GrowChance = 50;
	public override string TexturePath => "TerrariaParadox/Content/Tiles/Misc/AssecstoneRubble1x1Hanging";
	public override int OnMineDustType => ModContent.DustType<AssecstoneDust>();
	public override TileObjectData TileStyle => TileObjectData.Style1x1;
	public override bool Grounded => false;
	public override bool Hanging => true;
	public override Color MapColor => new Color(59, 79, 101);
}

public class AssecstoneRubble1x1HangingFake : ModdedRubbleFake
{
	public override string TexturePath => "TerrariaParadox/Content/Tiles/Misc/AssecstoneRubble1x1Hanging";
	public override int OnMineDustType => ModContent.DustType<AssecstoneDust>();
	public override TileObjectData TileStyle => TileObjectData.Style1x1;
	public override bool Grounded => false;
	public override bool Hanging => true;
	public override Color MapColor => new Color(59, 79, 101);
	public override int MaterialItemType => ModContent.ItemType<AssecstoneBlock>();
	public override void RubblePlacementLine()
	{
		FlexibleTileWand.RubblePlacementSmall.AddVariations(MaterialItemType, Type, 0, 1, 2, 3, 4, 5);
	}
}