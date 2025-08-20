using Microsoft.Xna.Framework;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.ObjectData;
using TerrariaParadox.Content.Dusts.Tiles.Blocks;
using TerrariaParadox.Content.Items.Tiles.Blocks;

namespace TerrariaParadox.Content.Tiles.Misc;

public class AssecstoneRubble1x1GroundedNatural : ModdedRubbleNatural
{
    public const int GrowChance = 100;
    public override string TexturePath => "TerrariaParadox/Content/Tiles/Misc/AssecstoneRubble1x1Grounded";
    public override int OnMineDustType => ModContent.DustType<AssecstoneDust>();
    public override TileObjectData TileStyle => TileObjectData.Style1x1;
    public override bool Grounded => true;
    public override bool Hanging => false;
    public override Color MapColor => new(59, 79, 101);
}

public class AssecstoneRubble1x1GroundedFake : ModdedRubbleFake
{
    public override string TexturePath => "TerrariaParadox/Content/Tiles/Misc/AssecstoneRubble1x1Grounded";
    public override int OnMineDustType => ModContent.DustType<AssecstoneDust>();
    public override TileObjectData TileStyle => TileObjectData.Style1x1;
    public override bool Grounded => true;
    public override bool Hanging => false;
    public override Color MapColor => new(59, 79, 101);
    public override int MaterialItemType => ModContent.ItemType<AssecstoneBlock>();

    public override void RubblePlacementLine()
    {
        FlexibleTileWand.RubblePlacementSmall.AddVariations(MaterialItemType, Type, 0, 1, 2, 3, 4, 5);
    }
}