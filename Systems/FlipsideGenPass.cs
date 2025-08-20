using AltLibrary.Core.Generation;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using TerrariaParadox.Content.Tiles.Blocks;
using TerrariaParadox.Content.Tiles.Misc;
using TerrariaParadox.Content.Tiles.Walls;

namespace TerrariaParadox;

public class FlipsideGenerationPass : EvilBiomeGenerationPass
{
    private ushort _bulbTile;

    private GenAction _clearTiles;
    private ushort _modStone;
    private ushort _modStoneWall;
    private GenAction _placeStone;
    private GenAction _placeStoneWall;
    public override string ProgressMessage => LangUtils.GetTextValue("Biomes.TheFlipside.AltBiomeMain.GenPassName");
    public override bool CanGenerateNearDungeonOcean => true;

    public override void GenerateEvil(int evilBiomePosition, int evilBiomePositionWestBound,
        int evilBiomePositionEastBound)
    {
        _modStone = (ushort)ModContent.TileType<AssecstoneBlockTile>();
        _modStoneWall = (ushort)ModContent.WallType<AssecstoneWallTileUnsafe>();
        _bulbTile = (ushort)ModContent.TileType<BioluminescentBulb>();

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

        PlaceBulbs(bugCenterPoint, bulbOffset, bulbOffset2, bulbContainerSize, worldSizeMultiplier);

        WorldUtils.Gen(bugCenterPoint, hollowBugBody, _clearTiles);
    }

    private void FirstFlipping(int evilBiomePosition, int evilBiomePositionWestBound, int evilBiomePositionEastBound,
        int bugBottom)
    {
        var indexYCoordsMin = bugBottom + 40.0;
        for (var indexXCoords = evilBiomePositionWestBound; indexXCoords < evilBiomePositionEastBound; indexXCoords++)
        {
            indexYCoordsMin += WorldGen.genRand.Next(-2, 3);
            if (indexYCoordsMin < bugBottom + 30.0) indexYCoordsMin = bugBottom + 30.0;
            if (indexYCoordsMin > bugBottom + 50.0) indexYCoordsMin = bugBottom + 50.0;
            //var flag7 = false;
            var indexYCoordsMax = (int)GenVars.worldSurfaceLow;
            while (indexYCoordsMax < indexYCoordsMin)
            {
                var tile = Main.tile[indexXCoords, indexYCoordsMax];
                if (tile.TileType == TileID.Sand &&
                    indexXCoords >= evilBiomePositionWestBound + WorldGen.genRand.Next(5) &&
                    indexXCoords <= evilBiomePositionEastBound - WorldGen.genRand.Next(5))
                    tile.TileType =
                        (ushort)ModContent.TileType<AssecsandBlockTile>();

                if (indexYCoordsMax < GenVars.worldSurfaceHigh - 1.0) //&& !flag7)
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
                    //flag7 = true;
                    if (ParadoxSystem.AssimilatedBlocks.TryGetValue(tile.TileType, out var tileType))
                        tile.TileType = tileType;

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
        WorldGen.digTunnel(rightAntennae2.X + 5, rightAntennae2.Y - 5, 0, 2, 5, antennaeTunnelSize);
        WorldGen.digTunnel(rightAntennae2.X + 8, rightAntennae2.Y - 7, 0, 2, 5, antennaeTunnelSize);
        WorldGen.digTunnel(rightAntennae2.X + 11, rightAntennae2.Y - 8, 0, 2, 5, antennaeTunnelSize);
        WorldGen.digTunnel(rightAntennae2.X + 13, rightAntennae2.Y - 9, 0, 2, 5, antennaeTunnelSize);
        WorldGen.digTunnel(rightAntennae2.X, rightAntennae2.Y, 0, 2, 5, antennaeTunnelSize);
        WorldGen.digTunnel(rightAntennae2.X - 5, rightAntennae2.Y + 5, 0, 2, 5, antennaeTunnelSize);
        WorldGen.digTunnel(rightAntennae2.X - 3, rightAntennae2.Y + 7, 0, 2, 5, antennaeTunnelSize);
    }

    private void PlaceBulbs(Point bugCenterPoint, Point bulbOffset1, Point bulbOffset2, float bulbContainerSize,
        float worldSizeMultiplier)
    {
        Point currentBulbPoint;
        var bulbCenterPoint1 = bugCenterPoint + bulbOffset1;
        var bulbCenterPoint2 = bugCenterPoint - bulbOffset1;
        var bulbCenterPoint3 = bugCenterPoint + new Point(-bulbOffset1.X, bulbOffset1.Y);
        var bulbCenterPoint4 = bugCenterPoint + new Point(bulbOffset1.X, -bulbOffset1.Y);

        var bulbCenterPoint5 = bugCenterPoint + new Point(bulbOffset2.X, bulbOffset1.Y / 3);
        var bulbCenterPoint6 = bugCenterPoint + new Point(-bulbOffset2.X, bulbOffset1.Y / -3);
        var bulbCenterPoint7 = bugCenterPoint + new Point(bulbOffset2.X, bulbOffset1.Y / -3);
        var bulbCenterPoint8 = bugCenterPoint + new Point(-bulbOffset2.X, bulbOffset1.Y / 3);

        GenShape outerBulbContainer = new Shapes.Circle((int)(bulbContainerSize * 1.75f));
        GenShape bulbContainer = new Shapes.Circle((int)bulbContainerSize);

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

        currentBulbPoint = bulbCenterPoint1;
        WorldgenMain.TunnelAToB(bugCenterPoint, currentBulbPoint, outlineWidth, outlineHeight, tunnelSize, _modStone,
            _modStoneWall, overshoot);
        WorldUtils.Gen(currentBulbPoint, outerBulbContainer, _placeStone);
        WorldUtils.Gen(currentBulbPoint, outerBulbContainer, _placeStoneWall);
        WorldUtils.Gen(currentBulbPoint, bulbContainer, _clearTiles);
        WorldGen.PlaceSign(currentBulbPoint.X, currentBulbPoint.Y, _bulbTile);

        currentBulbPoint = bulbCenterPoint2;
        WorldgenMain.TunnelAToB(bugCenterPoint, currentBulbPoint, outlineWidth, outlineHeight, tunnelSize, _modStone,
            _modStoneWall, overshoot);
        WorldUtils.Gen(currentBulbPoint, outerBulbContainer, _placeStone);
        WorldUtils.Gen(currentBulbPoint, outerBulbContainer, _placeStoneWall);
        WorldUtils.Gen(currentBulbPoint, bulbContainer, _clearTiles);
        WorldGen.PlaceSign(currentBulbPoint.X, currentBulbPoint.Y, _bulbTile);

        currentBulbPoint = bulbCenterPoint3;
        WorldgenMain.TunnelAToB(bugCenterPoint, currentBulbPoint, outlineWidth, outlineHeight, tunnelSize, _modStone,
            _modStoneWall, overshoot);
        WorldUtils.Gen(currentBulbPoint, outerBulbContainer, _placeStone);
        WorldUtils.Gen(currentBulbPoint, outerBulbContainer, _placeStoneWall);
        WorldUtils.Gen(currentBulbPoint, bulbContainer, _clearTiles);
        WorldGen.PlaceSign(currentBulbPoint.X, currentBulbPoint.Y, _bulbTile);

        currentBulbPoint = bulbCenterPoint4;
        WorldgenMain.TunnelAToB(bugCenterPoint, currentBulbPoint, outlineWidth, outlineHeight, tunnelSize, _modStone,
            _modStoneWall, overshoot);
        WorldUtils.Gen(currentBulbPoint, outerBulbContainer, _placeStone);
        WorldUtils.Gen(currentBulbPoint, outerBulbContainer, _placeStoneWall);
        WorldUtils.Gen(currentBulbPoint, bulbContainer, _clearTiles);
        WorldGen.PlaceSign(currentBulbPoint.X, currentBulbPoint.Y, _bulbTile);

        currentBulbPoint = bulbCenterPoint5;
        WorldgenMain.TunnelAToB(bugCenterPoint, currentBulbPoint, outlineWidth, outlineHeight, tunnelSize, _modStone,
            _modStoneWall, overshoot);
        WorldUtils.Gen(currentBulbPoint, outerBulbContainer, _placeStone);
        WorldUtils.Gen(currentBulbPoint, outerBulbContainer, _placeStoneWall);
        WorldUtils.Gen(currentBulbPoint, bulbContainer, _clearTiles);
        WorldGen.PlaceSign(currentBulbPoint.X, currentBulbPoint.Y, _bulbTile);

        currentBulbPoint = bulbCenterPoint6;
        WorldgenMain.TunnelAToB(bugCenterPoint, currentBulbPoint, outlineWidth, outlineHeight, tunnelSize, _modStone,
            _modStoneWall, overshoot);
        WorldUtils.Gen(currentBulbPoint, outerBulbContainer, _placeStone);
        WorldUtils.Gen(currentBulbPoint, outerBulbContainer, _placeStoneWall);
        WorldUtils.Gen(currentBulbPoint, bulbContainer, _clearTiles);
        WorldGen.PlaceSign(currentBulbPoint.X, currentBulbPoint.Y, _bulbTile);

        currentBulbPoint = bulbCenterPoint7;
        WorldgenMain.TunnelAToB(bugCenterPoint, currentBulbPoint, outlineWidth, outlineHeight, tunnelSize, _modStone,
            _modStoneWall, overshoot);
        WorldUtils.Gen(currentBulbPoint, outerBulbContainer, _placeStone);
        WorldUtils.Gen(currentBulbPoint, outerBulbContainer, _placeStoneWall);
        WorldUtils.Gen(currentBulbPoint, bulbContainer, _clearTiles);
        WorldGen.PlaceSign(currentBulbPoint.X, currentBulbPoint.Y, _bulbTile);

        currentBulbPoint = bulbCenterPoint8;
        WorldgenMain.TunnelAToB(bugCenterPoint, currentBulbPoint, outlineWidth, outlineHeight, tunnelSize, _modStone,
            _modStoneWall, overshoot);
        WorldUtils.Gen(currentBulbPoint, outerBulbContainer, _placeStone);
        WorldUtils.Gen(currentBulbPoint, outerBulbContainer, _placeStoneWall);
        WorldUtils.Gen(currentBulbPoint, bulbContainer, _clearTiles);
        WorldGen.PlaceSign(currentBulbPoint.X, currentBulbPoint.Y, _bulbTile);
    }


    public override void PostGenerateEvil()
    {
    }
}