using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using TerrariaParadox.Content.Gores.Tiles.Plants.Trees;
using TerrariaParadox.Content.Items.Consumables.Buffs;
using TerrariaParadox.Content.Tiles.Blocks;

namespace TerrariaParadox.Content.Tiles.Plants.Trees.Leechwood.Palm;

public class Tree : ModPalmTree
{
    private Asset<Texture2D> BeachTopsTexture;
    private Asset<Texture2D> OasisTopsTexture;
    private Asset<Texture2D> WoodTexture;

    // This is a blind copy-paste from Vanilla's PurityPalmTree settings.
    // TODO: This needs some explanations
    public override TreePaintingSettings TreeShaderSettings => new()
    {
        UseSpecialGroups = true,
        SpecialGroupMinimalHueValue = 11f / 72f,
        SpecialGroupMaximumHueValue = 0.25f,
        SpecialGroupMinimumSaturationValue = 0.88f,
        SpecialGroupMaximumSaturationValue = 1f
    };

    public override void SetStaticDefaults()
    {
        WoodTexture =
            ModContent.Request<Texture2D>("TerrariaParadox/Content/Tiles/Plants/Trees/Leechwood/Palm/TreeWood");
        OasisTopsTexture =
            ModContent.Request<Texture2D>("TerrariaParadox/Content/Tiles/Plants/Trees/Leechwood/Palm/OasisTreeTops");
        BeachTopsTexture =
            ModContent.Request<Texture2D>("TerrariaParadox/Content/Tiles/Plants/Trees/Leechwood/Palm/BeachTreeTops");

        GrowsOnTileId = [ModContent.TileType<AssecsandBlockTile>()];
    }

    public override Asset<Texture2D> GetTexture()
    {
        return WoodTexture;
    }

    public override Asset<Texture2D> GetOasisTopTextures()
    {
        return OasisTopsTexture;
    }

    public override Asset<Texture2D> GetTopTextures()
    {
        return BeachTopsTexture;
    }

    public override int TreeLeaf()
    {
        return ModContent.GoreType<LeechwoodLeaf>();
    }

    public override int SaplingGrowthType(ref int style)
    {
        style = 1;
        return ModContent.TileType<Sapling>();
    }

    public override int DropWood()
    {
        return ModContent.ItemType<Items.Tiles.Blocks.Leechwood>();
    }

    public override bool Shake(int x, int y, ref bool createLeaves)
    {
        if (Main.rand.NextBool(6))
        {
            if (Main.rand.NextBool(2))
                Item.NewItem(WorldGen.GetItemSource_FromTreeShake(x, y), new Vector2(x, y) * 16,
                    ModContent.ItemType<Moonana>());
            else
                Item.NewItem(WorldGen.GetItemSource_FromTreeShake(x, y), new Vector2(x, y) * 16,
                    ModContent.ItemType<Akebi>());
        }

        if (Main.rand.NextBool(3)) createLeaves = true;
        return true;
    }
}