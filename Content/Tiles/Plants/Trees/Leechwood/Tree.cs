using System.Reflection.Metadata;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Tiles.Blocks;

namespace TerrariaParadox.Content.Tiles.Plants.Trees.Leechwood;

public class Tree : ModTree
{
    private Asset<Texture2D> WoodTexture;
    private Asset<Texture2D> BranchTexture;
    private Asset<Texture2D> TopTexture;
    
    public override void SetStaticDefaults()
    {
        WoodTexture = ModContent.Request<Texture2D>("TerrariaParadox/Content/Tiles/Plants/Trees/Leechwood/TreeWood");
        BranchTexture = ModContent.Request<Texture2D>("TerrariaParadox/Content/Tiles/Plants/Trees/Leechwood/TreeBranches");
        TopTexture = ModContent.Request<Texture2D>( "TerrariaParadox/Content/Tiles/Plants/Trees/Leechwood/TreeTops");

        GrowsOnTileId = [ModContent.TileType<InfestedGrassBlock>()];
    }

    public override Asset<Texture2D> GetTexture() => WoodTexture;
    
    public override Asset<Texture2D> GetBranchTextures() => BranchTexture;
    
    public override Asset<Texture2D> GetTopTextures() => TopTexture;

    public override int TreeLeaf()
    {
        return ModContent.GoreType<Leechwood.Leaf>();
    }

    public override int SaplingGrowthType(ref int style)
    {
        style = 0;
        return ModContent.TileType<Leechwood.Sapling>();
    }

    public override int DropWood()
    {
        return ItemID.Wood;
    }
    
    public override bool Shake(int x, int y, ref bool createLeaves) 
    {
        Item.NewItem(WorldGen.GetItemSource_FromTreeShake(x, y), new Vector2(x, y) * 16, ItemID.Spaghetti);
        return true;
    }

    public override void SetTreeFoliageSettings(Tile tile, ref int xoffset, ref int treeFrame, ref int floorY, ref int topTextureFrameWidth,
        ref int topTextureFrameHeight)
    {
        //nothing so far
    }

    public override TreePaintingSettings TreeShaderSettings => new TreePaintingSettings 
    {
        UseSpecialGroups = true,
        SpecialGroupMinimalHueValue = 11f / 72f,
        SpecialGroupMaximumHueValue = 0.25f,
        SpecialGroupMinimumSaturationValue = 0.88f,
        SpecialGroupMaximumSaturationValue = 1f
    };
}