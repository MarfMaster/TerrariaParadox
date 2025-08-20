using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using TerrariaParadox.Content.Tiles.Misc;

namespace TerrariaParadox;

public class WorldgenMain : ModSystem
{
    public override void
        ModifyWorldGenTasks(List<GenPass> tasks,
            ref double totalWeight) //only need to gen flipped pots at worldgen, no need for any conversion logic, pots do not get converted at any point(this si why hallowed pots don't exist)
    {
        var PotsIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Pots"));

        if (PotsIndex != -1) tasks.Insert(PotsIndex + 1, new FlippedPotsPass("Flipped Pots", 100f));
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

public class FlippedPotsPass : GenPass
{
    public FlippedPotsPass(string name, float loadWeight) : base(name, loadWeight)
    {
    }

    protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
    {
        progress.Message = "Flipped Pots";

        int[] tileTypes = [ModContent.TileType<FlippedPots>()];

        // To not be annoying, we'll only spawn 15 Example Rubble near the spawn point.
        // This example uses the Try Until Success approach: https://github.com/tModLoader/tModLoader/wiki/World-Generation#try-until-success
        for (var k = 0; k < 15; k++)
        {
            var success = false;
            var attempts = 0;

            while (!success)
            {
                attempts++;
                if (attempts > 1000) break;
                var x = WorldGen.genRand.Next(Main.maxTilesX / 2 - 40, Main.maxTilesX / 2 + 40);
                var y = WorldGen.genRand.Next((int)GenVars.worldSurfaceLow, (int)GenVars.worldSurfaceHigh);
                var tileType = WorldGen.genRand.Next(tileTypes);
                var placeStyle = WorldGen.genRand.Next(9); // Each of these tiles have 6 place styles
                if (Main.tile[x, y].TileType == tileType) continue;

                WorldGen.PlaceTile(x, y, tileType, true, style: placeStyle);
                success = Main.tile[x, y].TileType == tileType;
            }
        }
    }
}