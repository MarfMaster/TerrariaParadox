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
using TerrariaParadox.Content.Tiles.Misc;
using TerrariaParadox.Content.Tiles.Walls;

namespace TerrariaParadox;

public class FlipsideGenerationPass : EvilBiomeGenerationPass
{
	public override string ProgressMessage => "Unleashing parasitic lifeforms upon this world";
	public override bool CanGenerateNearDungeonOcean => true;

        private GenAction placeStone;
        private GenAction placeStoneWall;
        private GenAction clearTiles;
        
        private ushort modStone;
        private ushort modStoneWall;
        private ushort modGrassBlock;
        private ushort modJGrassBlock;
        private ushort bulbTile;
		public override void GenerateEvil(int evilBiomePosition, int evilBiomePositionWestBound, int evilBiomePositionEastBound)
		{
			modStone = (ushort)ModContent.TileType<AssecstoneBlockTile>();
			modStoneWall = (ushort)ModContent.WallType<AssecstoneWallTileUnsafe>();
			modGrassBlock = (ushort)ModContent.TileType<FlippedGrassBlock>();
			modJGrassBlock = (ushort)ModContent.TileType<FlippedJungleGrassBlock>();
			bulbTile = (ushort)ModContent.TileType<BioluminescentBulb>();
			
			placeStone = new Actions.SetTile(modStone);
			placeStoneWall = new Actions.PlaceWall(modStoneWall);
			clearTiles = new Actions.ClearTile(false);
	    
	    float worldSizeMultiplier = 1f;
	    switch (WorldGen.GetWorldSize())
	    {
		    case 0:
		    {
			    worldSizeMultiplier = WorldGen.genRand.NextFloat(0.8f, 1.1f);
			    break;
		    }
		    case 1:
		    {
			    worldSizeMultiplier = WorldGen.genRand.NextFloat(1.3f, 1.7f);
			    break;
		    }
		    case 2:
		    {
			    worldSizeMultiplier = WorldGen.genRand.NextFloat(2f, 2.4f);
			    break;
		    }
	    }
	    


	    int width = (evilBiomePositionEastBound - evilBiomePositionWestBound) / 2;
        int evilBiomeCenter = evilBiomePositionWestBound + width;
        
        Point bugCenterPoint = new Point(evilBiomeCenter, (int)GenVars.rockLayerHigh);
        Point bulbOffset = new Point((int)(50 * worldSizeMultiplier * 1.35f), (int)(50 * worldSizeMultiplier));;
        Point bulbOffset2 = new Point((int)(50 * worldSizeMultiplier * 1.65f), (int)(50 * worldSizeMultiplier * 1.25f));
        
        Vector2 evilBiomeCenterVector = new Vector2((float)evilBiomeCenter, bugCenterPoint.Y);
        
        float bugBodyLength = 100f * worldSizeMultiplier;
        float bugBodyHollowWidth = bugBodyLength * 0.4f;
        float bugBodyHollowHeight = bugBodyLength * 0.85f;
        float bulbContainerSize = bugBodyLength * 0.035f;
        
        Point bugTopLeftPoint = new Point(bugCenterPoint.X - (int)(bugBodyLength), bugCenterPoint.Y - (int)(bugBodyLength));
        
        Rectangle bugRectangle = new Rectangle(bugTopLeftPoint.X, bugTopLeftPoint.Y, (int)bugBodyLength * 2,
	        (int)bugBodyLength * 2);
        GenVars.structures.AddProtectedStructure(bugRectangle);
        
        Flipping(evilBiomePosition, evilBiomePositionWestBound, evilBiomePositionEastBound, bugRectangle.Bottom);
        
        //int x = WorldGen.genRand.Next(0, Main.maxTilesX);
        bool foundSurface = false;
        int surfaceHeightY = 1;
        while (surfaceHeightY < Main.worldSurface) 
        {
	        if (TerrariaParadox.InfestedBlocks.Contains(Main.tile[evilBiomeCenter, surfaceHeightY].TileType)) 
	        {
		        foundSurface = true;
		        break;
	        }
	        surfaceHeightY++;
        }
        GenShape greatBug = new Shapes.Circle((int)bugBodyLength, (int)bugBodyLength);
        GenShape hollowBugBody = new Shapes.Circle((int)(bugBodyHollowWidth), (int)bugBodyHollowHeight);
        
        WorldUtils.Gen(bugCenterPoint, greatBug, placeStone);
        WorldUtils.Gen(bugCenterPoint, greatBug, placeStoneWall);
        
        GrowAntennae(bugCenterPoint, bugTopLeftPoint, bugBodyHollowWidth, bugBodyLength, surfaceHeightY);
        
        PlaceBulbs(bugCenterPoint, bulbOffset, bulbOffset2, bulbContainerSize, worldSizeMultiplier);
        
        WorldUtils.Gen(bugCenterPoint, hollowBugBody, clearTiles);

		}
    private void Flipping(int evilBiomePosition, int evilBiomePositionWestBound, int evilBiomePositionEastBound, int bugBottom)
    {
			double indexBottomBounds = bugBottom + 40.0;
			for (int indexBounds = evilBiomePositionWestBound; indexBounds < evilBiomePositionEastBound; indexBounds++) 
			{
				indexBottomBounds += WorldGen.genRand.Next(-2, 3);
				if (indexBottomBounds < bugBottom + 30.0) 
				{
					indexBottomBounds = bugBottom + 30.0;
				}
				if (indexBottomBounds > bugBottom + 50.0) 
				{
					indexBottomBounds = bugBottom + 50.0;
				}
				bool flag7 = false;
				int indexUpperBounds = (int)GenVars.worldSurfaceLow;
				while (indexUpperBounds < indexBottomBounds) 
				{
					if (Main.tile[indexBounds, indexUpperBounds].HasTile) 
					{
						if (Main.tile[indexBounds, indexUpperBounds].TileType == TileID.Sand && indexBounds >= evilBiomePositionWestBound + WorldGen.genRand.Next(5) && indexBounds <= evilBiomePositionEastBound - WorldGen.genRand.Next(5)) 
						{
							Main.tile[indexBounds, indexUpperBounds].TileType = (ushort)ModContent.TileType<AssecsandBlockTile>();
						}
						if (indexUpperBounds < GenVars.worldSurfaceHigh - 1.0 && !flag7) 
						{
							if (Main.tile[indexBounds, indexUpperBounds].TileType == TileID.Dirt) 
							{
								WorldGen.grassSpread = 0;
								WorldGen.SpreadGrass(indexBounds, indexUpperBounds, TileID.Dirt, ModContent.TileType<FlippedGrassBlock>(), true);
							} else if (Main.tile[indexBounds, indexUpperBounds].TileType == TileID.Mud) 
							{
								WorldGen.grassSpread = 0;
								WorldGen.SpreadGrass(indexBounds, indexUpperBounds, TileID.Mud, ModContent.TileType<FlippedJungleGrassBlock>());
							}
						}
						flag7 = true;
						if (Main.tile[indexBounds, indexUpperBounds].TileType == TileID.Stone && indexBounds >= evilBiomePositionWestBound + WorldGen.genRand.Next(5) && indexBounds <= evilBiomePositionEastBound - WorldGen.genRand.Next(5)) 
						{
							Main.tile[indexBounds, indexUpperBounds].TileType = (ushort)ModContent.TileType<AssecstoneBlockTile>();
						}
						if (Main.tile[indexBounds, indexUpperBounds].WallType == WallID.HardenedSand) 
						{
							Main.tile[indexBounds, indexUpperBounds].WallType = (ushort)ModContent.WallType<HardenedAssecsandWallTileUnsafe>();
						} else if (Main.tile[indexBounds, indexUpperBounds].WallType == WallID.Sandstone) 
						{
							Main.tile[indexBounds, indexUpperBounds].WallType = (ushort)ModContent.WallType<AssecsandstoneWallTileUnsafe>();
						}
						if (Main.tile[indexBounds, indexUpperBounds].TileType == TileID.Grass) 
						{
							Main.tile[indexBounds, indexUpperBounds].TileType = (ushort)ModContent.TileType<FlippedGrassBlock>();
						}
						if (Main.tile[indexBounds, indexUpperBounds].TileType == TileID.IceBlock) 
						{
							Main.tile[indexBounds, indexUpperBounds].TileType = (ushort)ModContent.TileType<MurkyIceBlockTile>();
						} else if (Main.tile[indexBounds, indexUpperBounds].TileType == TileID.Sandstone) 
						{
							Main.tile[indexBounds, indexUpperBounds].TileType = (ushort)ModContent.TileType<AssecsandstoneBlockTile>();
						} else if (Main.tile[indexBounds, indexUpperBounds].TileType == TileID.HardenedSand) 
						{
							Main.tile[indexBounds, indexUpperBounds].TileType = (ushort)ModContent.TileType<HardenedAssecsandBlockTile>();
						}
					}
					indexUpperBounds++;
				}
			}
    }

		private void GrowAntennae(Point bugCenterPoint, Point bugTopLeftPoint, float bugBodyHollowWidth, float bugBodyLength, int surfaceHeightY)
		{
			Point leftAntennaeStart = new Point(bugCenterPoint.X - (int)(bugBodyHollowWidth / 2f), bugTopLeftPoint.Y + (int)(bugBodyLength / 2f));
			Point leftAntennae1 = new Point(leftAntennaeStart.X, leftAntennaeStart.Y - ((leftAntennaeStart.Y - surfaceHeightY) / 2));
			Point leftAntennae2 = new Point(leftAntennaeStart.X - 35, surfaceHeightY - 30);
			Point leftAntennae3 = new Point(leftAntennae2.X - 90, leftAntennae2.Y - 30);
        
			Point rightAntennaeStart = new Point(bugCenterPoint.X + (int)(bugBodyHollowWidth / 2f), bugTopLeftPoint.Y + (int)(bugBodyLength / 2f));
			Point rightAntennae1 = new Point(rightAntennaeStart.X, rightAntennaeStart.Y - ((rightAntennaeStart.Y - surfaceHeightY) / 2));
			Point rightAntennae2 = new Point(rightAntennaeStart.X + 35, surfaceHeightY - 30);
			Point rightAntennae3 = new Point(rightAntennae2.X + 90, rightAntennae2.Y - 30);

			int antennaeOutlineSize = 16;
			int antennaeTunnelSize = 8;
        
        WorldgenMain.TunnelAToB(leftAntennaeStart, leftAntennae1, antennaeOutlineSize, antennaeOutlineSize, antennaeTunnelSize, modStone, modStoneWall, 2, 0);
        WorldgenMain.TunnelAToB(leftAntennae1, leftAntennae2, antennaeOutlineSize, antennaeOutlineSize, antennaeTunnelSize, modStone, modStoneWall, 2, 0);
        WorldgenMain.TunnelAToB(leftAntennae3, leftAntennae2, antennaeOutlineSize, antennaeOutlineSize, antennaeTunnelSize, modStone, modStoneWall, 3, 0);
        WorldGen.digTunnel(leftAntennae1.X, leftAntennae1.Y, 0, 2, 5, antennaeTunnelSize);
        WorldGen.digTunnel(leftAntennae2.X - 5, leftAntennae2.Y - 5, 0, 2, 5, antennaeTunnelSize);
        WorldGen.digTunnel(leftAntennae2.X - 8, leftAntennae2.Y - 7, 0, 2, 5, antennaeTunnelSize);
        WorldGen.digTunnel(leftAntennae2.X - 11, leftAntennae2.Y - 8, 0, 2, 5, antennaeTunnelSize);
        WorldGen.digTunnel(leftAntennae2.X - 13, leftAntennae2.Y - 9, 0, 2, 5, antennaeTunnelSize);
        WorldGen.digTunnel(leftAntennae2.X, leftAntennae2.Y, 0, 2, 5, antennaeTunnelSize);
        WorldGen.digTunnel(leftAntennae2.X + 5, leftAntennae2.Y + 5, 0, 2, 5, antennaeTunnelSize);
        WorldGen.digTunnel(leftAntennae2.X + 3, leftAntennae2.Y + 7, 0, 2, 5, antennaeTunnelSize);
        
        WorldgenMain.TunnelAToB(rightAntennaeStart, rightAntennae1, antennaeOutlineSize, antennaeOutlineSize, antennaeTunnelSize, modStone, modStoneWall, 2, 0);
        WorldgenMain.TunnelAToB(rightAntennae1, rightAntennae2, antennaeOutlineSize, antennaeOutlineSize, antennaeTunnelSize, modStone, modStoneWall, 2, 0);
        WorldgenMain.TunnelAToB(rightAntennae3, rightAntennae2, antennaeOutlineSize, antennaeOutlineSize, antennaeTunnelSize, modStone, modStoneWall, 3, 0);
        WorldGen.digTunnel(rightAntennae1.X, rightAntennae1.Y, 0, 2, 5, antennaeTunnelSize);
        WorldGen.digTunnel(rightAntennae2.X + 5, rightAntennae2.Y - 5, 0, 2, 5, antennaeTunnelSize);
        WorldGen.digTunnel(rightAntennae2.X + 8, rightAntennae2.Y - 7, 0, 2, 5, antennaeTunnelSize);
        WorldGen.digTunnel(rightAntennae2.X + 11, rightAntennae2.Y - 8, 0, 2, 5, antennaeTunnelSize);
        WorldGen.digTunnel(rightAntennae2.X + 13, rightAntennae2.Y - 9, 0, 2, 5, antennaeTunnelSize);
        WorldGen.digTunnel(rightAntennae2.X, rightAntennae2.Y, 0, 2, 5, antennaeTunnelSize);
        WorldGen.digTunnel(rightAntennae2.X - 5, rightAntennae2.Y + 5, 0, 2, 5, antennaeTunnelSize);
        WorldGen.digTunnel(rightAntennae2.X - 3, rightAntennae2.Y + 7, 0, 2, 5, antennaeTunnelSize);
		}
		private void PlaceBulbs(Point bugCenterPoint, Point bulbOffset1, Point bulbOffset2, float bulbContainerSize, float worldSizeMultiplier)
		{
			Point currentBulbPoint;
			Point bulbCenterPoint1 = bugCenterPoint + bulbOffset1;
			Point bulbCenterPoint2 = bugCenterPoint - bulbOffset1;
			Point bulbCenterPoint3 = bugCenterPoint + new Point(-bulbOffset1.X, bulbOffset1.Y);
			Point bulbCenterPoint4 = bugCenterPoint + new Point(bulbOffset1.X, -bulbOffset1.Y);
        
			Point bulbCenterPoint5 = bugCenterPoint + new Point(bulbOffset2.X, bulbOffset1.Y / 3);
			Point bulbCenterPoint6 = bugCenterPoint + new Point(-bulbOffset2.X, bulbOffset1.Y / -3);
			Point bulbCenterPoint7 = bugCenterPoint + new Point(bulbOffset2.X, bulbOffset1.Y / -3);
			Point bulbCenterPoint8 = bugCenterPoint + new Point(-bulbOffset2.X, bulbOffset1.Y / 3);
			
			GenShape outerBulbContainer = new Shapes.Circle((int)(bulbContainerSize * 1.75f));
			GenShape bulbContainer = new Shapes.Circle((int)bulbContainerSize);

			int outlineWidth = 16;
			int outlineHeight = 16;
			int tunnelSize = 0;
			int overshoot = 0;
			switch (worldSizeMultiplier)
			{
				case var xs when xs > 0.79f & xs < 1.01f:
				{
					overshoot = +2;
					tunnelSize = 7;
					break;
				}
				case var s when s > 1f & s < 1.11f:
				{
					overshoot = +2;
					tunnelSize = 7;
					break;
				}
				case var ms when ms > 1.29f & ms < 1.51f:
				{
					overshoot = -1;
					tunnelSize = 8;
					break;
				}
				case var m when m > 1.5f & m < 1.71f:
				{
					overshoot = -1;
					tunnelSize = 8;
					break;
				}
				case var l when l > 1.99f & l < 2.21f:
				{
					overshoot = -1;
					tunnelSize = 14;
					break;
				}
				case var xl when xl > 2.2f & xl < 2.41f:
				{
					overshoot = -1;
					tunnelSize = 14;
					break;
				}
			}

			currentBulbPoint = bulbCenterPoint1;
			WorldgenMain.TunnelAToB(bugCenterPoint, currentBulbPoint, outlineWidth, outlineHeight, tunnelSize, modStone, modStoneWall, overshoot);
			WorldUtils.Gen(currentBulbPoint, outerBulbContainer, placeStone);
			WorldUtils.Gen(currentBulbPoint, outerBulbContainer, placeStoneWall);
			WorldUtils.Gen(currentBulbPoint, bulbContainer, clearTiles);
			WorldGen.PlaceSign(currentBulbPoint.X, currentBulbPoint.Y, bulbTile);
			
			currentBulbPoint = bulbCenterPoint2;
			WorldgenMain.TunnelAToB(bugCenterPoint, currentBulbPoint, outlineWidth, outlineHeight, tunnelSize, modStone, modStoneWall, overshoot);
			WorldUtils.Gen(currentBulbPoint, outerBulbContainer, placeStone);
			WorldUtils.Gen(currentBulbPoint, outerBulbContainer, placeStoneWall);
			WorldUtils.Gen(currentBulbPoint, bulbContainer, clearTiles);
			WorldGen.PlaceSign(currentBulbPoint.X, currentBulbPoint.Y, bulbTile);
			
			currentBulbPoint = bulbCenterPoint3;
			WorldgenMain.TunnelAToB(bugCenterPoint, currentBulbPoint, outlineWidth, outlineHeight, tunnelSize, modStone, modStoneWall, overshoot);
			WorldUtils.Gen(currentBulbPoint, outerBulbContainer, placeStone);
			WorldUtils.Gen(currentBulbPoint, outerBulbContainer, placeStoneWall);
			WorldUtils.Gen(currentBulbPoint, bulbContainer, clearTiles);
			WorldGen.PlaceSign(currentBulbPoint.X, currentBulbPoint.Y, bulbTile);
			
			currentBulbPoint = bulbCenterPoint4;
			WorldgenMain.TunnelAToB(bugCenterPoint, currentBulbPoint, outlineWidth, outlineHeight, tunnelSize, modStone, modStoneWall, overshoot);
			WorldUtils.Gen(currentBulbPoint, outerBulbContainer, placeStone);
			WorldUtils.Gen(currentBulbPoint, outerBulbContainer, placeStoneWall);
			WorldUtils.Gen(currentBulbPoint, bulbContainer, clearTiles);
			WorldGen.PlaceSign(currentBulbPoint.X, currentBulbPoint.Y, bulbTile);
			
			currentBulbPoint = bulbCenterPoint5;
			WorldgenMain.TunnelAToB(bugCenterPoint, currentBulbPoint, outlineWidth, outlineHeight, tunnelSize, modStone, modStoneWall, overshoot);
			WorldUtils.Gen(currentBulbPoint, outerBulbContainer, placeStone);
			WorldUtils.Gen(currentBulbPoint, outerBulbContainer, placeStoneWall);
			WorldUtils.Gen(currentBulbPoint, bulbContainer, clearTiles);
			WorldGen.PlaceSign(currentBulbPoint.X, currentBulbPoint.Y, bulbTile);
			
			currentBulbPoint = bulbCenterPoint6;
			WorldgenMain.TunnelAToB(bugCenterPoint, currentBulbPoint, outlineWidth, outlineHeight, tunnelSize, modStone, modStoneWall, overshoot);
			WorldUtils.Gen(currentBulbPoint, outerBulbContainer, placeStone);
			WorldUtils.Gen(currentBulbPoint, outerBulbContainer, placeStoneWall);
			WorldUtils.Gen(currentBulbPoint, bulbContainer, clearTiles);
			WorldGen.PlaceSign(currentBulbPoint.X, currentBulbPoint.Y, bulbTile);
			
			currentBulbPoint = bulbCenterPoint7;
			WorldgenMain.TunnelAToB(bugCenterPoint, currentBulbPoint, outlineWidth, outlineHeight, tunnelSize, modStone, modStoneWall, overshoot);
			WorldUtils.Gen(currentBulbPoint, outerBulbContainer, placeStone);
			WorldUtils.Gen(currentBulbPoint, outerBulbContainer, placeStoneWall);
			WorldUtils.Gen(currentBulbPoint, bulbContainer, clearTiles);
			WorldGen.PlaceSign(currentBulbPoint.X, currentBulbPoint.Y, bulbTile);
			
			currentBulbPoint = bulbCenterPoint8;
			WorldgenMain.TunnelAToB(bugCenterPoint, currentBulbPoint, outlineWidth, outlineHeight, tunnelSize, modStone, modStoneWall, overshoot);
			WorldUtils.Gen(currentBulbPoint, outerBulbContainer, placeStone);
			WorldUtils.Gen(currentBulbPoint, outerBulbContainer, placeStoneWall);
			WorldUtils.Gen(currentBulbPoint, bulbContainer, clearTiles);
			WorldGen.PlaceSign(currentBulbPoint.X, currentBulbPoint.Y, bulbTile);
		}


    public override void PostGenerateEvil()
    {
    }

}