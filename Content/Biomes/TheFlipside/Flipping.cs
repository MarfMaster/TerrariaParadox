using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Tiles.Blocks;

namespace TerrariaParadox.Content.Biomes.TheFlipside;

public class Flipping : ModBiomeConversion
{
    public static int Assecstone;

    public override void PostSetupContent()
    {
        // Cache the conversion types.
        Assecstone = ModContent.TileType<AssecstoneBlockTile>();


        // Go over every tile and add a conversion to it for our conversion type if they're part of the list of usual conversion tiles
        for (var i = 0; i < TileLoader.TileCount; i++)
        {
            switch (i)
            {
                case int stone when TileID.Sets.Conversion.Stone[i]:
                {
                    TileLoader.RegisterConversion(i, Type, ConvertStone);
                    break;
                }
            }

            if (ParadoxSystem.AssimilatedBlocks.ContainsKey((ushort)i) &&
                (TileID.Sets.Conversion.Grass[i] ||
                 TileID.Sets.Conversion.JungleGrass[i] ||
                 TileID.Sets.Conversion.Sand[i] ||
                 TileID.Sets.Conversion.Sandstone[i] ||
                 TileID.Sets.Conversion.HardenedSand[i] ||
                 TileID.Sets.Conversion.Ice[i] ||
                 TileID.Sets.Conversion.Thorn[i]
                ))
                TileLoader.RegisterConversion(i, Type, ConvertTiles);
        }

        // Register conversions for every natural wall
        for (var i = 0; i < WallLoader.WallCount; i++)
            if (WallID.Sets.Conversion.Grass[i] ||
                WallID.Sets.Conversion.Stone[i] ||
                WallID.Sets.Conversion.Sandstone[i] ||
                WallID.Sets.Conversion.HardenedSand[i] ||
                WallID.Sets.Conversion.Ice[i] ||
                WallID.Sets.Conversion.NewWall1[i] || // NewWalls are the underground wall variants 
                WallID.Sets.Conversion.NewWall2[i] ||
                WallID.Sets.Conversion.NewWall3[i] ||
                WallID.Sets.Conversion.NewWall4[i]
               )
                WallLoader.RegisterConversion(i, Type, ConvertWalls);
    }

    public bool ConvertTiles(int i, int j, int type, int conversionType)
    {
        WorldGen.ConvertTile(i, j, ParadoxSystem.AssimilatedBlocks[(ushort)type], true);
        return false;
    }

    public bool ConvertStone(int i, int j, int type, int conversionType)
    {
        //Main.NewText(1);
        /*int tileTypeAbove = -1;
        if (j > 1 && Main.tile[i, j - 1].HasTile)
            tileTypeAbove = Main.tile[i, j - 1].TileType;

        // Convert gem trees above stone into exampletrees
        // If your tree is a ModTree and is meant to replace generic surface trees, we wouldn't really need this method, since ModTrees share an ID with the vanilla tree types.
        // ModTrees automatically convert based on the tile type they're planted on, and we're converting the ground tile.
        // In this case, we're converting stone, so we need this code to transform gem trees into regular vanilla tree types.
        // ...Admittedly, you'd still need this method since vanity trees (Sakura and Willow) aren't part of the vanilla tree types, so they would break on conversion if not accounted for
        FindAndConvertTree(i, j, tileTypeAbove);*/

        WorldGen.ConvertTile(i, j, Assecstone, true);
        return false;
    }

    public void FindAndConvertTree(int i, int j, int tileTypeAbove)
    {
        if (tileTypeAbove == -1)
            return;

        if (!TileID.Sets.CountsAsGemTree[tileTypeAbove])
            return;

        var treeBottom = j;
        var treeTop = treeBottom - 1;
        var treeCenterX = i;

        // Check for if the tile is the tree's "trunk" or just the root tiles on the side
        // We do this by checking for the specific tile frame of the tree tile.
        // Necessary because the "CountsAsGemTree" ID set doesn't care about the tile's frame and returns true even if the tile isnt the tree's "trunk"
        var treeFrameX = Main.tile[treeCenterX, treeTop].TileFrameX / 22;
        var treeFrameY = Main.tile[treeCenterX, treeTop].TileFrameY / 22;
        var isTreeTrunk = (treeFrameX != 1 && treeFrameX != 2) || treeFrameY < 6;

        // Niche edgecase check: If a stone block was placed under a tree's branch, it shouldnt be converted at all, as it is not actually attached to the stone tile below
        var isTreeBranch = (treeFrameX == 3 && treeFrameY < 3) ||
                           (treeFrameX == 4 && treeFrameY >= 3 && treeFrameY < 6);
        if (isTreeBranch)
            return;

        // If the tile above wasn't a tree trunk but instead a root tile on the side, check the adjacent two tiles to find it
        if (!isTreeTrunk)
            for (var x = treeCenterX - 1; x < treeCenterX + 2; x += 2)
            {
                var topTile = Main.tile[x, treeTop];
                if (!topTile.HasTile || !TileID.Sets.CountsAsGemTree[topTile.TileType])
                    continue;

                // Check for tree trunk framing
                treeFrameX = topTile.TileFrameX / 22;
                treeFrameY = topTile.TileFrameY / 22;
                isTreeTrunk = (treeFrameX != 1 && treeFrameX != 2) || treeFrameY < 6;

                // We found our tree trunk center
                if (isTreeTrunk)
                {
                    treeCenterX = x;
                    break;
                }
            }

        // Find the top of the tree by repeatedly going up until we don't find any more tree tiles
        while (treeTop >= 0 && Main.tile[treeCenterX, treeTop].HasTile &&
               TileID.Sets.CountsAsGemTree[Main.tile[treeCenterX, treeTop].TileType])
            treeTop--;

        // Turn all the tiles around it into example trees
        // Because ExampleTree is a ModTree, it shares its tileID with the rest of the vanilla trees, and will automatically convert based on the floor type
        for (var x = treeCenterX - 1; x < treeCenterX + 2; x++)
        for (var y = treeTop; y < treeBottom; y++)
        {
            var t = Main.tile[x, y];
            if (t.HasTile && TileID.Sets.CountsAsGemTree[t.TileType])
                t.TileType = TileID.Trees;
        }

        // Turn the floor into example tiles (We have to convert the adjacent tiles, otherwise the side root tiles may get broken)
        // The framing will happen naturally when the floor tile below gets converted and frames the other adjacent tiles, so we don't need to use WorldGen.Convert here
        for (var x = treeCenterX - 1; x < treeCenterX + 2; x++)
        {
            var t = Main.tile[x, treeBottom];
            if (t.HasTile && t.TileType == TileID.Stone)
                t.TileType = (ushort)Assecstone;
        }
    }

    public bool ConvertWalls(int i, int j, int type, int conversionType)
    {
        WorldGen.ConvertWall(i, j, ParadoxSystem.AssimilatedWalls[(ushort)type]);
        /*var wallType = ModContent.WallType<AssecstoneWallTileUnsafe>();
        switch (type)
        {
            case int stone when WallID.Sets.Conversion.Stone[type]:
            {
                wallType = Main.wallHouse[type] ? AssecstoneWall : AssecstoneWallUnsafe;
                break;
            }
            case int grass when WallID.Sets.Conversion.Grass[type]:
            {
                wallType = Main.wallHouse[type] ? FlippedGrassWall : FlippedGrassWallUnsafe;
                break;
            }
            case int sandstone when WallID.Sets.Conversion.Sandstone[type]:
            {
                wallType = Main.wallHouse[type] ? AssecSandstoneWall : AssecSandstoneWallUnsafe;
                break;
            }
            case int hardenedsand when WallID.Sets.Conversion.HardenedSand[type]:
            {
                wallType = Main.wallHouse[type] ? HardenedAssecsandWall : HardenedAssecsandWallUnsafe;
                break;
            }
        }

        WorldGen.ConvertWall(i, j, wallType);*/
        return false;
    }
}