using Microsoft.Xna.Framework;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.ObjectData;
using TerrariaParadox.Content.Dusts.Tiles.Blocks;
using TerrariaParadox.Content.Items.Tiles.Blocks;

namespace TerrariaParadox.Content.Tiles.Misc;

public class AssecstoneStalagmitesNatural : ModdedRubbleNatural
{
    public const int GrowChance = 160;
    public override string TexturePath => "TerrariaParadox/Content/Tiles/Misc/AssecstoneStalagmites";
    public override int OnMineDustType => ModContent.DustType<AssecstoneDust>();
    public override TileObjectData TileStyle => TileObjectData.Style1x2;
    public override bool Grounded => true;
    public override bool Hanging => false;
    public override Color MapColor => new(59, 79, 101);
}

public class AssecstoneStalagmitesFake : ModdedRubbleFake
{
    public override string TexturePath => "TerrariaParadox/Content/Tiles/Misc/AssecstoneStalagmites";
    public override int OnMineDustType => ModContent.DustType<AssecstoneDust>();
    public override TileObjectData TileStyle => TileObjectData.Style1x2;
    public override bool Grounded => true;
    public override bool Hanging => false;
    public override Color MapColor => new(59, 79, 101);
    public override int MaterialItemType => ModContent.ItemType<AssecstoneBlock>();

    public override void RubblePlacementLine()
    {
        FlexibleTileWand.RubblePlacementMedium.AddVariations(MaterialItemType, Type, 0, 1, 2, 3, 4, 5);
    }
}