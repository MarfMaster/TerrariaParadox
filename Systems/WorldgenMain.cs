using System;
using System.Collections.Generic;
using AltLibrary.Common.AltBiomes;
using AltLibrary.Common.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using TerrariaParadox.Content.Biomes.TheFlipside;
using TerrariaParadox.Content.Tiles.Blocks;
using TerrariaParadox.Content.Tiles.Misc;

namespace TerrariaParadox;

public class WorldgenMain : ModSystem
{
    public int FlippedBlockCount;

    public override void TileCountsAvailable(ReadOnlySpan<int> tileCounts)
    {
        FlippedBlockCount = 0;
        foreach (var i in ParadoxSystem.AssimilatedBlocks.Values) FlippedBlockCount += tileCounts[i];
    }

    public static void TunnelAToB(Point pA, Point pB, int outlineWidth, int outlineHeight, int tunnelSize,
        ushort blockType, ushort wallType, int dugOvershoot, int tileOvershoot = 0)
    {
        GenShape outline = new Shapes.Circle(outlineWidth, outlineHeight);
        //GenShape tunnel = new Shapes.Circle(tunnelWidth, tunnelHeight);
        GenAction placeTiles = new Actions.SetTile(blockType);
        GenAction placeWalls = new Actions.PlaceWall(wallType);

        var directionFromAToB = pA.ToWorldCoordinates().DirectionTo(pB.ToWorldCoordinates());
        double xDirToB = directionFromAToB.X;
        double yDirToB = directionFromAToB.Y;
        var distanceF = pA.ToWorldCoordinates().Distance(pB.ToWorldCoordinates()) / 16f;
        var distanceSQ = pA.ToWorldCoordinates().DistanceSQ(pB.ToWorldCoordinates());
        float distanceI = (int)distanceF;
        var circleProximityDivisor = 15f;

        for (var i = 0; i < distanceF / 5f + tileOvershoot; i++)
        {
            var pA2 = new Point(
                (int)(pA.X + xDirToB * (distanceF / circleProximityDivisor) * (3 + i - tunnelSize / 4f)),
                (int)(pA.Y + yDirToB * (distanceF / circleProximityDivisor) * (3 + i - tunnelSize / 4f)));

            if (distanceSQ < pA.ToWorldCoordinates().DistanceSQ(pA2.ToWorldCoordinates())) break;

            WorldUtils.Gen(pA2, outline, placeTiles);
            WorldUtils.Gen(pA2, outline, placeWalls);
        }

        var tunnelProximityDivisor = 27f;
        for (var i = 0; i < distanceF / 5f + dugOvershoot; i++)
        {
            var pA2 = new Point(
                (int)(pA.X + xDirToB * (distanceF / tunnelProximityDivisor) * (i - tunnelSize / 4f)),
                (int)(pA.Y + yDirToB * (distanceF / tunnelProximityDivisor) * (i - tunnelSize / 4f)));

            if (distanceSQ < pA.ToWorldCoordinates().DistanceSQ(pA2.ToWorldCoordinates())) break;

            WorldGen.digTunnel(pA2.X, pA2.Y, xDirToB, yDirToB, 5, tunnelSize);
        }
    }
}