using System;
using System.Collections.Generic;
using AltLibrary.Core.Generation;
using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using TerrariaParadox.Content.Tiles.Blocks;
using TerrariaParadox.Content.Tiles.Walls;

namespace TerrariaParadox;

public class FlipsideGenerationPass : EvilBiomeGenerationPass
{
    public override void GenerateEvil(int evilBiomePosition, int evilBiomePositionWestBound, int evilBiomePositionEastBound)
    {
	    float worldSizeMultiplier = 1f;
	    switch (WorldGen.GetWorldSize())
	    {
		    case 0:
		    {
			    worldSizeMultiplier = Main.rand.NextFloat(0.9f, 1.3f);
			    break;
		    }
		    case 1:
		    {
			    worldSizeMultiplier = Main.rand.NextFloat(1.5f, 1.9f);
			    break;
		    }
		    case 2:
		    {
			    worldSizeMultiplier = Main.rand.NextFloat(2.4f, 2.9f);
			    break;
		    }
		    break;
	    }
        int width = (evilBiomePositionEastBound - evilBiomePositionWestBound) / 2;
        int evilBiomeCenter = evilBiomePositionWestBound + (width);
        
        Point evilBiomeCenterPoint = new Point(evilBiomeCenter, (int)GenVars.rockLayerHigh);
        Vector2 evilBiomeCenterVector = new Vector2((float)evilBiomeCenter, (float)GenVars.rockLayerHigh);
        float bugBodyLength = 100f * worldSizeMultiplier;
        float bugBodyHollowWidth = bugBodyLength * 0.625f;
        
        GenShape greatBug = new Shapes.Circle((int)bugBodyLength, (int)bugBodyLength);
        GenShape hollowBugBody = new Shapes.Circle((int)(bugBodyHollowWidth * 0.65f), (int)bugBodyHollowWidth);
        GenShape bugLeg = new ShapeBranch(0, width);
        
        GenAction placeStone = new Actions.SetTile((ushort)ModContent.TileType<AssecstoneBlockTile>());
        GenAction placeStoneWall = new Actions.PlaceWall((ushort)ModContent.WallType<AssecstoneWallTileUnsafe>());
        GenAction clearTiles = new Actions.ClearTile(false);
        
        WorldUtils.Gen(evilBiomeCenterPoint, greatBug, placeStone);
        WorldUtils.Gen(evilBiomeCenterPoint, greatBug, placeStoneWall);

        WorldUtils.Gen(evilBiomeCenterPoint, bugLeg, clearTiles);
        
        
        WorldUtils.Gen(new Point((int)evilBiomeCenterVector.X, (int)evilBiomeCenterVector.Y), hollowBugBody, clearTiles);
        GenVars.structures.AddProtectedStructure(Utils.CenteredRectangle(evilBiomeCenterVector.ToWorldCoordinates(), new Vector2(bugBodyLength, bugBodyLength)));
        
        //WorldGen.TileRunner(evilBiomePosition, (int)GenVars.worldSurface, 50, WorldGen.genRand.Next(2, 8), TileID.PinkSlimeBlock);
        Flipping(evilBiomePosition, evilBiomePositionWestBound, evilBiomePositionEastBound);
    }

    public override void PostGenerateEvil()
    {
    }

    public void Flipping(int evilBiomePosition, int evilBiomePositionWestBound, int evilBiomePositionEastBound)
    {
			double num43 = Main.worldSurface + 40.0;
			for (int num44 = evilBiomePositionWestBound; num44 < evilBiomePositionEastBound; num44++) 
			{
				num43 += WorldGen.genRand.Next(-2, 3);
				if (num43 < Main.worldSurface + 30.0) 
				{
					num43 = Main.worldSurface + 30.0;
				}
				if (num43 > Main.worldSurface + 50.0) 
				{
					num43 = Main.worldSurface + 50.0;
				}
				int i2 = num44;
				bool flag7 = false;
				int num45 = (int)GenVars.worldSurfaceLow;
				while (num45 < num43) {
					if (Main.tile[i2, num45].HasTile) 
					{
						if (Main.tile[i2, num45].TileType == TileID.Sand && i2 >= evilBiomePositionWestBound + WorldGen.genRand.Next(5) && i2 <= evilBiomePositionEastBound - WorldGen.genRand.Next(5)) 
						{
							Main.tile[i2, num45].TileType = (ushort)ModContent.TileType<AssecsandBlockTile>();
						}
						if (num45 < Main.worldSurface - 1.0 && !flag7) 
						{
							if (Main.tile[i2, num45].TileType == TileID.Dirt) 
							{
								WorldGen.grassSpread = 0;
								WorldGen.SpreadGrass(i2, num45, TileID.Dirt, ModContent.TileType<FlippedGrassBlock>(), true);
							} else if (Main.tile[i2, num45].TileType == TileID.Mud) 
							{
								WorldGen.grassSpread = 0;
								WorldGen.SpreadGrass(i2, num45, TileID.Mud, ModContent.TileType<FlippedJungleGrassBlock>());
							}
						}
						flag7 = true;
						if (Main.tile[i2, num45].TileType == TileID.Stone && i2 >= evilBiomePositionWestBound + WorldGen.genRand.Next(5) && i2 <= evilBiomePositionEastBound - WorldGen.genRand.Next(5)) 
						{
							Main.tile[i2, num45].TileType = (ushort)ModContent.TileType<AssecstoneBlockTile>();
						}
						if (Main.tile[i2, num45].WallType == WallID.HardenedSand) 
						{
							Main.tile[i2, num45].WallType = (ushort)ModContent.WallType<HardenedAssecsandWallTileUnsafe>();
						} else if (Main.tile[i2, num45].WallType == WallID.Sandstone) 
						{
							Main.tile[i2, num45].WallType = (ushort)ModContent.WallType<AssecsandstoneWallTileUnsafe>();
						}
						if (Main.tile[i2, num45].TileType == TileID.Grass) 
						{
							Main.tile[i2, num45].TileType = (ushort)ModContent.TileType<FlippedGrassBlock>();
						}
						if (Main.tile[i2, num45].TileType == TileID.IceBlock) 
						{
							Main.tile[i2, num45].TileType = (ushort)ModContent.TileType<MurkyIceBlockTile>();
						} else if (Main.tile[i2, num45].TileType == TileID.Sandstone) 
						{
							Main.tile[i2, num45].TileType = (ushort)ModContent.TileType<AssecsandstoneBlockTile>();
						} else if (Main.tile[i2, num45].TileType == TileID.HardenedSand) 
						{
							Main.tile[i2, num45].TileType = (ushort)ModContent.TileType<HardenedAssecsandBlockTile>();
						}
					}
					num45++;
				}
			}
    }
}