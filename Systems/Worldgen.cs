using System.Collections.Generic;
using Terraria;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using TerrariaParadox.Content.Tiles.Misc;

namespace TerrariaParadox;

public class Worldgen : ModSystem
{
    public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight) //only need to gen flipped pots at worldgen, no need for any conversion logic, pots do not get converted at any point(this si why hallowed pots don't exist)
    {
        int PotsIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Pots"));

        if (PotsIndex != -1) 
        {
            tasks.Insert(PotsIndex + 1, new FlippedPotsPass("Flipped Pots", 100f));
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