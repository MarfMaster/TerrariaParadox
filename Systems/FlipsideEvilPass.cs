using System.Collections.Generic;
using AltLibrary.Core.Generation;
using Microsoft.Xna.Framework;
using MLib.Common.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using TerrariaParadox.Content.Tiles.Blocks;
using TerrariaParadox.Content.Tiles.Misc;
using TerrariaParadox.Content.Tiles.Walls;

namespace TerrariaParadox;

public class FlipsideEvilPass : EvilBiomeGenerationPass
{
    private ushort _bulbTile;
    private ushort _modAltar;

    private GenAction _clearTiles;
    private ushort _modStone;
    private ushort _modStoneWall;
    private GenAction _placeStone;
    private GenAction _placeStoneWall;
    public override string ProgressMessage => Language.GetTextValue("Mods.TerrariaParadox.Biomes.TheFlipside.FAltBiomeMain.GenPassName");
    public override bool CanGenerateNearDungeonOcean => true;

    private static Dictionary<Point, float> _bulbPoints = new Dictionary<Point, float>();
    
    public static List<int> FlipsideWestEdges = new List<int>();
    public static List<int> FlipsideEastEdges = new List<int>();
    public static List<int> FlipsideBottomEdges = new List<int>();
    public override void GenerateEvil(int evilBiomePosition, int evilBiomePositionWestBound,
        int evilBiomePositionEastBound)
    {
        _modStone = (ushort)ModContent.TileType<AssecstoneBlockTile>();
        _modStoneWall = (ushort)ModContent.WallType<AssecstoneWallTileUnsafe>();
        _bulbTile = (ushort)ModContent.TileType<BioluminescentBulb>();
        _modAltar = (ushort)ModContent.TileType<OothecaAltar>();

        _placeStone = new Actions.SetTile(ParadoxSystem.AssimilatedBlocks[TileID.Stone]);
        _placeStoneWall = new Actions.PlaceWall(ParadoxSystem.AssimilatedWalls[WallID.Stone]);
        _clearTiles = new Actions.ClearTile();

        var worldSizeMultiplier = 1f;
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


        var width = (evilBiomePositionEastBound - evilBiomePositionWestBound) / 2;
        var evilBiomeCenter = evilBiomePositionWestBound + width;

        var bugCenterPoint = new Point(evilBiomeCenter, (int)GenVars.rockLayerHigh);
        var bulbOffset = new Point((int)(50 * worldSizeMultiplier * 1.35f), (int)(50 * worldSizeMultiplier));
        ;
        var bulbOffset2 = new Point((int)(50 * worldSizeMultiplier * 1.65f), (int)(50 * worldSizeMultiplier * 1.25f));

        var evilBiomeCenterVector = new Vector2(evilBiomeCenter, bugCenterPoint.Y);

        var bugBodyLength = 100f * worldSizeMultiplier;
        var bugBodyHollowWidth = bugBodyLength * 0.4f;
        var bugBodyHollowHeight = bugBodyLength * 0.85f;
        var bulbContainerSize = bugBodyLength * 0.035f;

        var bugTopLeftPoint = new Point(bugCenterPoint.X - (int)bugBodyLength, bugCenterPoint.Y - (int)bugBodyLength);

        var bugRectangle = new Rectangle(bugTopLeftPoint.X, bugTopLeftPoint.Y, (int)bugBodyLength * 2,
            (int)bugBodyLength * 2);
        GenVars.structures.AddProtectedStructure(bugRectangle);

        if (bugRectangle.Right > evilBiomePositionEastBound)
        {
            evilBiomePositionEastBound = bugRectangle.Right;
        }
        if (bugRectangle.Left < evilBiomePositionWestBound)
        {
            evilBiomePositionWestBound = bugRectangle.Left;
        }


        FirstFlipping(evilBiomePosition, evilBiomePositionWestBound, evilBiomePositionEastBound, bugRectangle.Bottom);

        //int x = WorldGen.genRand.Next(0, Main.maxTilesX);
        //var foundSurface = false;
        var surfaceHeightY = 1;
        while (surfaceHeightY < Main.worldSurface)
        {
            if (ParadoxSystem.AssimilatedBlocks.ContainsValue(Main.tile[evilBiomeCenter, surfaceHeightY].TileType))
                //foundSurface = true;
                break;

            surfaceHeightY++;
        }

        GenShape greatBug = new Shapes.Circle((int)bugBodyLength, (int)bugBodyLength);
        GenShape hollowBugBody = new Shapes.Circle((int)bugBodyHollowWidth, (int)bugBodyHollowHeight);

        WorldUtils.Gen(bugCenterPoint, greatBug, _placeStone);
        WorldUtils.Gen(bugCenterPoint, greatBug, _placeStoneWall);

        GrowAntennae(bugCenterPoint, bugTopLeftPoint, bugBodyHollowWidth, bugBodyLength, surfaceHeightY);

        PrepareBulbs(bugCenterPoint, bulbOffset, bulbOffset2, bulbContainerSize, worldSizeMultiplier);

        WorldUtils.Gen(bugCenterPoint, hollowBugBody, _clearTiles);

        GenShape bugSpots = new Shapes.Mound((int)(12f * worldSizeMultiplier), (int)(6f * worldSizeMultiplier));
        for (int i = 0; i < 5; i++)
        {
            Point bugSpotPoint = new Point(bugCenterPoint.X + WorldGen.genRand.Next(-8, 8 + 1), bugCenterPoint.Y + (int)((bugBodyHollowHeight / 3f) * (2 - i)));
            WorldUtils.Gen(bugSpotPoint, bugSpots, _placeStone);
            for (int j = 0; j < 100; j++)
            {
                Point bugSpotTopCenterPoint = new Point(bugSpotPoint.X, bugSpotPoint.Y - j);
                Tile bugSpotCenterTopTile = Main.tile[bugSpotTopCenterPoint.X, bugSpotTopCenterPoint.Y];
                if (!bugSpotCenterTopTile.HasTile)
                {
                    WorldGen.Place3x2(bugSpotTopCenterPoint.X, bugSpotTopCenterPoint.Y, _modAltar);
                    break;
                }
            }
        }
        
        FlipsideWestEdges.Add(evilBiomePositionWestBound);
        FlipsideEastEdges.Add(evilBiomePositionEastBound);
        FlipsideBottomEdges.Add(bugRectangle.Bottom);
    }
    public static List<Point> SandToBeFlipped = new List<Point>();
    private void FirstFlipping(int evilBiomePosition, int evilBiomePositionWestBound, int evilBiomePositionEastBound,
        int bugBottom)
    {
        var indexYCoordsMin = bugBottom + 40.0;
        for (var indexXCoords = evilBiomePositionWestBound; indexXCoords < evilBiomePositionEastBound; indexXCoords++)
        {
            indexYCoordsMin += WorldGen.genRand.Next(-2, 3);
            if (indexYCoordsMin < bugBottom + 30.0) indexYCoordsMin = bugBottom + 30.0;
            if (indexYCoordsMin > bugBottom + 50.0) indexYCoordsMin = bugBottom + 50.0;
            var indexYCoordsMax = (int)GenVars.worldSurfaceLow;
            while (indexYCoordsMax < indexYCoordsMin)
            {
                var tile = Main.tile[indexXCoords, indexYCoordsMax];
                if (tile.TileType == TileID.Sand &&
                    indexXCoords >= evilBiomePositionWestBound + WorldGen.genRand.Next(5) &&
                    indexXCoords <= evilBiomePositionEastBound - WorldGen.genRand.Next(5))
                {
                    Tile above =  Main.tile[indexXCoords, indexYCoordsMax - 1];
                    if (!above.HasTile && indexYCoordsMax < GenVars.worldSurfaceHigh - 1.0)
                    {
                        SandToBeFlipped.Add(new Point(indexXCoords, indexYCoordsMax));
                    }
                    else
                    {
                        tile.TileType = (ushort)ModContent.TileType<AssecsandBlockTile>();
                    }
                }

                if (indexYCoordsMax < GenVars.worldSurfaceHigh - 1.0)
                {
                    if (tile.TileType == TileID.Dirt)
                    {
                        WorldGen.grassSpread = 0;
                        WorldGen.SpreadGrass(indexXCoords, indexYCoordsMax, TileID.Dirt,
                            ModContent.TileType<FlippedGrassBlock>());
                    }
                    else if (tile.TileType == TileID.Mud)
                    {
                        WorldGen.grassSpread = 0;
                        WorldGen.SpreadGrass(indexXCoords, indexYCoordsMax, TileID.Mud,
                            ModContent.TileType<FlippedJungleGrassBlock>());
                    }
                }

                if (tile.HasTile)
                {
                    if (tile.TileType != TileID.Sand && ParadoxSystem.AssimilatedBlocks.TryGetValue(tile.TileType, out var tileType))
                    {
                        tile.TileType = tileType;
                    }
                }

                if (tile.WallType != WallID.None)
                    if (ParadoxSystem.AssimilatedWalls.TryGetValue(tile.WallType, out var wallType))
                        tile.WallType = wallType;

                indexYCoordsMax++;
            }
        }
    }

    private void GrowAntennae(Point bugCenterPoint, Point bugTopLeftPoint, float bugBodyHollowWidth,
        float bugBodyLength, int surfaceHeightY)
    {
        var leftAntennaeStart = new Point(bugCenterPoint.X - (int)(bugBodyHollowWidth / 2f),
            bugTopLeftPoint.Y + (int)(bugBodyLength / 2f));
        var leftAntennae1 = new Point(leftAntennaeStart.X,
            leftAntennaeStart.Y - (leftAntennaeStart.Y - surfaceHeightY) / 2);
        var leftAntennae2 = new Point(leftAntennaeStart.X - 35, surfaceHeightY - 30);
        var leftAntennae3 = new Point(leftAntennae2.X - 90, leftAntennae2.Y - 30);

        var rightAntennaeStart = new Point(bugCenterPoint.X + (int)(bugBodyHollowWidth / 2f),
            bugTopLeftPoint.Y + (int)(bugBodyLength / 2f));
        var rightAntennae1 = new Point(rightAntennaeStart.X,
            rightAntennaeStart.Y - (rightAntennaeStart.Y - surfaceHeightY) / 2);
        var rightAntennae2 = new Point(rightAntennaeStart.X + 35, surfaceHeightY - 30);
        var rightAntennae3 = new Point(rightAntennae2.X + 90, rightAntennae2.Y - 30);

        var antennaeOutlineSize = 16;
        var antennaeTunnelSize = 8;

        WorldgenMain.TunnelAToB(leftAntennaeStart, leftAntennae1, antennaeOutlineSize, antennaeOutlineSize,
            antennaeTunnelSize, _modStone, _modStoneWall, 2);
        WorldgenMain.TunnelAToB(leftAntennae1, leftAntennae2, antennaeOutlineSize, antennaeOutlineSize,
            antennaeTunnelSize, _modStone, _modStoneWall, 2);
        WorldgenMain.TunnelAToB(leftAntennae3, leftAntennae2, antennaeOutlineSize, antennaeOutlineSize,
            antennaeTunnelSize, _modStone, _modStoneWall, 3);
        WorldGen.digTunnel(leftAntennae1.X, leftAntennae1.Y, 0, 2, 5, antennaeTunnelSize);
        WorldGen.digTunnel(leftAntennae1.X, leftAntennae1.Y - 5, 0, 2, 5, antennaeTunnelSize);
        WorldGen.digTunnel(leftAntennae1.X, leftAntennae1.Y + 5, 0, 2, 5, antennaeTunnelSize);
        WorldGen.digTunnel(leftAntennae1.X, leftAntennae1.Y + 10, 0, 2, 5, antennaeTunnelSize);
        WorldGen.digTunnel(leftAntennae1.X, leftAntennae1.Y + 15, 0, 2, 5, antennaeTunnelSize);
        WorldGen.digTunnel(leftAntennae2.X - 5, leftAntennae2.Y - 5, 0, 2, 5, antennaeTunnelSize);
        WorldGen.digTunnel(leftAntennae2.X - 8, leftAntennae2.Y - 7, 0, 2, 5, antennaeTunnelSize);
        WorldGen.digTunnel(leftAntennae2.X - 11, leftAntennae2.Y - 8, 0, 2, 5, antennaeTunnelSize);
        WorldGen.digTunnel(leftAntennae2.X - 13, leftAntennae2.Y - 9, 0, 2, 5, antennaeTunnelSize);
        WorldGen.digTunnel(leftAntennae2.X, leftAntennae2.Y, 0, 2, 5, antennaeTunnelSize);
        WorldGen.digTunnel(leftAntennae2.X + 5, leftAntennae2.Y + 5, 0, 2, 5, antennaeTunnelSize);
        WorldGen.digTunnel(leftAntennae2.X + 3, leftAntennae2.Y + 7, 0, 2, 5, antennaeTunnelSize);

        WorldgenMain.TunnelAToB(rightAntennaeStart, rightAntennae1, antennaeOutlineSize, antennaeOutlineSize,
            antennaeTunnelSize, _modStone, _modStoneWall, 2);
        WorldgenMain.TunnelAToB(rightAntennae1, rightAntennae2, antennaeOutlineSize, antennaeOutlineSize,
            antennaeTunnelSize, _modStone, _modStoneWall, 2);
        WorldgenMain.TunnelAToB(rightAntennae3, rightAntennae2, antennaeOutlineSize, antennaeOutlineSize,
            antennaeTunnelSize, _modStone, _modStoneWall, 3);
        WorldGen.digTunnel(rightAntennae1.X, rightAntennae1.Y, 0, 2, 5, antennaeTunnelSize);
        WorldGen.digTunnel(rightAntennae1.X, rightAntennae1.Y - 5, 0, 2, 5, antennaeTunnelSize);
        WorldGen.digTunnel(rightAntennae1.X, rightAntennae1.Y + 5, 0, 2, 5, antennaeTunnelSize);
        WorldGen.digTunnel(rightAntennae1.X, rightAntennae1.Y + 10, 0, 2, 5, antennaeTunnelSize);
        WorldGen.digTunnel(rightAntennae1.X, rightAntennae1.Y + 15, 0, 2, 5, antennaeTunnelSize);
        WorldGen.digTunnel(rightAntennae2.X + 5, rightAntennae2.Y - 5, 0, 2, 5, antennaeTunnelSize);
        WorldGen.digTunnel(rightAntennae2.X + 8, rightAntennae2.Y - 7, 0, 2, 5, antennaeTunnelSize);
        WorldGen.digTunnel(rightAntennae2.X + 11, rightAntennae2.Y - 8, 0, 2, 5, antennaeTunnelSize);
        WorldGen.digTunnel(rightAntennae2.X + 13, rightAntennae2.Y - 9, 0, 2, 5, antennaeTunnelSize);
        WorldGen.digTunnel(rightAntennae2.X, rightAntennae2.Y, 0, 2, 5, antennaeTunnelSize);
        WorldGen.digTunnel(rightAntennae2.X - 5, rightAntennae2.Y + 5, 0, 2, 5, antennaeTunnelSize);
        WorldGen.digTunnel(rightAntennae2.X - 3, rightAntennae2.Y + 7, 0, 2, 5, antennaeTunnelSize);
    }

    private void PrepareBulbs(Point bugCenterPoint, Point bulbOffset1, Point bulbOffset2, float bulbContainerSize,
        float worldSizeMultiplier)
    {
        List<Point> bulbPointList = new List<Point>();
        
        var bulbCenterPoint1 = bugCenterPoint + bulbOffset1;
        bulbPointList.Add(bulbCenterPoint1);
        
        var bulbCenterPoint2 = bugCenterPoint - bulbOffset1;
        bulbPointList.Add(bulbCenterPoint2);
        
        var bulbCenterPoint3 = bugCenterPoint + new Point(-bulbOffset1.X, bulbOffset1.Y);
        bulbPointList.Add(bulbCenterPoint3);
        
        var bulbCenterPoint4 = bugCenterPoint + new Point(bulbOffset1.X, -bulbOffset1.Y);
        bulbPointList.Add(bulbCenterPoint4);

        var bulbCenterPoint5 = bugCenterPoint + new Point(bulbOffset2.X, bulbOffset1.Y / 3);
        bulbPointList.Add(bulbCenterPoint5);
        
        var bulbCenterPoint6 = bugCenterPoint + new Point(-bulbOffset2.X, bulbOffset1.Y / -3);
        bulbPointList.Add(bulbCenterPoint6);
        
        var bulbCenterPoint7 = bugCenterPoint + new Point(bulbOffset2.X, bulbOffset1.Y / -3);
        bulbPointList.Add(bulbCenterPoint7);
        
        var bulbCenterPoint8 = bugCenterPoint + new Point(-bulbOffset2.X, bulbOffset1.Y / 3);
        bulbPointList.Add(bulbCenterPoint8);

        var outlineWidth = 16;
        var outlineHeight = 16;
        var tunnelSize = 0;
        var overshoot = 0;
        switch (worldSizeMultiplier)
        {
            case var xs when (xs > 0.79f) & (xs < 1.01f):
            {
                overshoot = +2;
                tunnelSize = 7;
                break;
            }
            case var s when (s > 1f) & (s < 1.11f):
            {
                overshoot = +2;
                tunnelSize = 7;
                break;
            }
            case var ms when (ms > 1.29f) & (ms < 1.51f):
            {
                overshoot = -1;
                tunnelSize = 8;
                break;
            }
            case var m when (m > 1.5f) & (m < 1.71f):
            {
                overshoot = -1;
                tunnelSize = 8;
                break;
            }
            case var l when (l > 1.99f) & (l < 2.21f):
            {
                overshoot = -1;
                tunnelSize = 14;
                break;
            }
            case var xl when (xl > 2.2f) & (xl < 2.41f):
            {
                overshoot = -1;
                tunnelSize = 14;
                break;
            }
        }

        foreach (var p in bulbPointList)
        {
            WorldgenMain.TunnelAToB(bugCenterPoint, p, outlineWidth, outlineHeight, tunnelSize, _modStone,
                _modStoneWall, overshoot);
            _bulbPoints.Add(p, bulbContainerSize);
        }
    }
    private void PlaceBulbs()
    {
        foreach (var p in _bulbPoints.Keys)
        {
            GenShape outerBulbContainer = new Shapes.Circle((int)(_bulbPoints[p] * 1.75f));
            GenShape bulbContainer = new Shapes.Circle((int)_bulbPoints[p]);
            
            WorldUtils.Gen(p, outerBulbContainer, _placeStone);
            WorldUtils.Gen(p, outerBulbContainer, _placeStoneWall);
            WorldUtils.Gen(p, bulbContainer, _clearTiles);
            WorldGen.PlaceSign(p.X, p.Y, _bulbTile);
        }
    }
    public override void PostGenerateEvil()
    {
        PlaceBulbs();
        _bulbPoints.Clear();
    }
}