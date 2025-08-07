using Microsoft.Xna.Framework;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.ObjectData;
using TerrariaParadox.Content.Dusts.Tiles.Blocks;
using TerrariaParadox.Content.Items.Tiles.Blocks;

namespace TerrariaParadox.Content.Tiles.Misc;

public class MurkyIcicles1x2Natural : ModdedRubbleNatural
{
    public const int GrowChance = 44;
    public override string TexturePath => "TerrariaParadox/Content/Tiles/Misc/MurkyIcicles1x2";
    public override int OnMineDustType => ModContent.DustType<MurkyIceDust>();
    public override TileObjectData TileStyle => TileObjectData.Style1x2Top;
    public override bool Grounded => false;
    public override bool Hanging => true;
    public override Color MapColor => new Color(64, 64, 101);
}

public class MurkyIcicles1x2Fake : ModdedRubbleFake
{
    public override string TexturePath => "TerrariaParadox/Content/Tiles/Misc/MurkyIcicles1x2";
    public override int OnMineDustType => ModContent.DustType<MurkyIceDust>();
    public override TileObjectData TileStyle => TileObjectData.Style1x2Top;
    public override bool Grounded => false;
    public override bool Hanging => true;
    public override Color MapColor => new Color(64, 64, 101);
    public override int MaterialItemType => ModContent.ItemType<MurkyIceBlock>();
    public override void RubblePlacementLine()
    {
        FlexibleTileWand.RubblePlacementMedium.AddVariations(MaterialItemType, Type, 0, 1, 2);
    }
}