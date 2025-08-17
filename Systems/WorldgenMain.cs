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
    public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight) //only need to gen flipped pots at worldgen, no need for any conversion logic, pots do not get converted at any point(this si why hallowed pots don't exist)
    {
        int PotsIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Pots"));

        if (PotsIndex != -1) 
        {
            tasks.Insert(PotsIndex + 1, new FlippedPotsPass("Flipped Pots", 100f));
        }
    }

    public static void TunnelAToB(Point pA, Point pB, int outlineWidth, int outlineHeight, int tunnelSize,
        ushort blockType, ushort wallType, int overshoot)
    {
        GenShape outline = new Shapes.Circle(outlineWidth, outlineHeight);
        //GenShape tunnel = new Shapes.Circle(tunnelWidth, tunnelHeight);
        GenAction placeTiles = new Actions.SetTile(blockType);
        GenAction placeWalls = new Actions.PlaceWall(wallType);
        GenAction clearTiles = new Actions.ClearTile(false);

        //WorldUtils.Gen(pA, outline, placeTiles);
        //WorldUtils.Gen(pA, outline, placeWalls);

        //WorldUtils.Gen(pB, outline, placeTiles);
        //WorldUtils.Gen(pB, outline, placeWalls);

        Vector2 directionFromAToB = pA.ToWorldCoordinates().DirectionTo(pB.ToWorldCoordinates());
        double xDirToB = directionFromAToB.X;
        double yDirToB = directionFromAToB.Y;
        float distanceF = pA.ToWorldCoordinates().Distance(pB.ToWorldCoordinates()) / 16f;
        float distanceI = (int)distanceF;
        float circleProximityDivisor = 10.5f;

        for (int i = 0; i < (distanceF / 10f) - (distanceF / 40); i++)
        {
            Point pA2 = new Point(
                (int)(pA.X + (xDirToB * (distanceF / circleProximityDivisor) * (3 + i - (tunnelSize / 4f)))),
                (int)(pA.Y + (yDirToB * (distanceF / circleProximityDivisor) * (3 + i - (tunnelSize / 4f)))));

            WorldUtils.Gen(pA2, outline, placeTiles);
            WorldUtils.Gen(pA2, outline, placeWalls);
        }

        for (int i = 0; i < (distanceF / 10f) + overshoot; i++)
        {
            Point pA2 = new Point(
                (int)(pA.X + (xDirToB * (distanceF / circleProximityDivisor) * (i - (tunnelSize / 4f)))),
                (int)(pA.Y + (yDirToB * (distanceF / circleProximityDivisor) * (i - (tunnelSize / 4f)))));

            WorldGen.digTunnel(pA2.X, pA2.Y, xDirToB, yDirToB, 10, tunnelSize);
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
        for (int k = 0; k < 15; k++) 
        {
            bool success = false;
            int attempts = 0;

            while (!success) 
            {
                attempts++;
                if (attempts > 1000) 
                {
                    break;
                }
                int x = WorldGen.genRand.Next(Main.maxTilesX / 2 - 40, Main.maxTilesX / 2 + 40);
                int y = WorldGen.genRand.Next((int)GenVars.worldSurfaceLow, (int)GenVars.worldSurfaceHigh);
                int tileType = WorldGen.genRand.Next(tileTypes);
                int placeStyle = WorldGen.genRand.Next(9); // Each of these tiles have 6 place styles
                if (Main.tile[x, y].TileType == tileType) 
                {
                    continue;
                }

                WorldGen.PlaceTile(x, y, tileType, mute: true, style: placeStyle);
                success = Main.tile[x, y].TileType == tileType;
            }
        }
    }
}