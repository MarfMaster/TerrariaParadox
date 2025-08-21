using System.Collections.Generic;
using AltLibrary.Common.Systems;
using PegasusLib;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using TerrariaParadox.Content.Biomes.TheFlipside;
using TerrariaParadox.Content.Tiles.Blocks;
using TerrariaParadox.Content.Tiles.Misc;
using TerrariaParadox.Content.Tiles.Plants;
using TerrariaParadox.Content.Tiles.Plants.Cactus;

namespace TerrariaParadox;
public class FlippingGenPasses : ModSystem
{
    public const int ExtraBoundary = 50;
    public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight) 
    {
        if (WorldBiomeManager.WorldEvilBiome.Type == ModContent.GetInstance<AltBiomeMain>().Type)
        {
            var potsIndex = tasks.FindIndex(potsIndex => potsIndex.Name.Equals("Pots"));
            
            var spreadingGrassIndex = tasks.FindIndex(spreadingGrassIndex => spreadingGrassIndex.Name.Equals("Spreading Grass"));
            
            var grassWallIndex = tasks.FindIndex(grassWallIndex => grassWallIndex.Name.Equals("Grass Wall"));
            
            var herbsIndex = tasks.FindIndex(herbsIndex => herbsIndex.Name.Equals("Herbs"));
            
            var weedsIndex = tasks.FindIndex(weedsIndex => weedsIndex.Name.Equals("Weeds"));
            
            var vinesIndex = tasks.FindIndex(vinesIndex => vinesIndex.Name.Equals("Vines"));
            
            var cactusPalmIndex = tasks.FindIndex(cactusPalmIndex => cactusPalmIndex.Name.Equals("Cactus, Palm Trees, & Coral"));
            
            var stalacIndex = tasks.FindIndex(stalacIndex => stalacIndex.Name.Equals("Stalac"));
            
            var finalCleanupIndex = tasks.FindIndex(finalCleanupIndex => finalCleanupIndex.Name.Equals("Final Cleanup"));

            var indexFixer = 1;
            
            if (potsIndex != -1)
            {
                tasks.Insert(potsIndex + indexFixer, new FlippingPotsPass("Flipping Pots", 100f));
                indexFixer++;
            }
            
            if (spreadingGrassIndex != -1)
            {
                tasks.Insert(spreadingGrassIndex + indexFixer, new FlipGrassPass("Flipping Grass", 100f));
                indexFixer++;
            }
            
            if (grassWallIndex != -1)
            {
                tasks.Insert(grassWallIndex + indexFixer, new FlipGrassWallPass("Flipping Grass Walls", 100f));
                indexFixer++;
            }
            
            if (herbsIndex != -1)
            {
                tasks.Insert(herbsIndex + indexFixer, new AssimilateGrassPass("Assimilating Grass", 100f));
                indexFixer++;
            }
            
            if (weedsIndex != -1)
            {
                tasks.Insert(weedsIndex + indexFixer, new FlipWeedsPass("Flipping Weeds", 100f));
                indexFixer++;
            }
            
            if (vinesIndex != -1)
            {
                tasks.Insert(vinesIndex + indexFixer, new FlipVinesPass("Flipping Vines", 100f));
                indexFixer++;
            }
            
            if (cactusPalmIndex != -1)
            {
                tasks.Insert(cactusPalmIndex + indexFixer, new FlipCactusPalmPass("Flipping Cactus & Palm Trees", 100f));
                indexFixer++;
            }
            
            if (stalacIndex != -1)
            {
                tasks.Insert(stalacIndex + indexFixer, new FlipStalacPass("Flipping Stalactites & Stalagmites", 100f));
                indexFixer++;
            }
            
            if (finalCleanupIndex != -1)
            {
                tasks.Insert(finalCleanupIndex + indexFixer, new FlippingCleanupPass("Flipping Cleanup", 100f));
            }
        }
    }
}

public class FlippingPotsPass : GenPass
{
    public FlippingPotsPass(string name, float loadWeight) : base(name, loadWeight)
    {
    }

    protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
    {
        progress.Message = "Flipping Pots";

        ushort tileType = (ushort)ModContent.TileType<FlippedPots>();
        
        //+X ist nach rechts

        for (int i = 0; i < FlipsideEvilPass.FlipsideBottomEdges.Count; i++)
        {
            var indexYCoordsMin = FlipsideEvilPass.FlipsideBottomEdges[i] + 40.0 + FlippingGenPasses.ExtraBoundary;
            for (var indexXCoords = FlipsideEvilPass.FlipsideWestEdges[i] - FlippingGenPasses.ExtraBoundary; indexXCoords < FlipsideEvilPass.FlipsideEastEdges[i]  + FlippingGenPasses.ExtraBoundary; indexXCoords++)
            {
                indexYCoordsMin += WorldGen.genRand.Next(-2, 3);
                if (indexYCoordsMin < FlipsideEvilPass.FlipsideBottomEdges[i] + 30.0) indexYCoordsMin = FlipsideEvilPass.FlipsideBottomEdges[i] + 30.0;
                if (indexYCoordsMin > FlipsideEvilPass.FlipsideBottomEdges[i] + 50.0) indexYCoordsMin = FlipsideEvilPass.FlipsideBottomEdges[i] + 50.0;
                var indexYCoordsMax = (int)GenVars.worldSurfaceLow;
                while (indexYCoordsMax < indexYCoordsMin)
                {
                    var tile = Main.tile[indexXCoords, indexYCoordsMax];
                    var leftBottomTile = Main.tile[indexXCoords, indexYCoordsMax + 2];
                    var rightBottomTile = Main.tile[indexXCoords + 1, indexYCoordsMax + 2];

                    if (tile.HasTile && tile.TileType == TileID.Pots && 
                        (ParadoxSystem.AssimilatedBlocks.ContainsValue(rightBottomTile.TileType) ||
                         ParadoxSystem.AssimilatedBlocks.ContainsValue(leftBottomTile.TileType)))
                    {
                        WorldGen.KillTile(indexXCoords, indexYCoordsMax);
                        WorldGen.Place2x2(indexXCoords + 1, indexYCoordsMax + 1, tileType, WorldGen.genRand.Next(0, 9));
                    }
                    
                    indexYCoordsMax++;
                }
            }
        }
    }
}

public class FlipGrassPass : GenPass
{
    public FlipGrassPass(string name, float loadWeight) : base(name, loadWeight)
    {
    }

    protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
    {
        progress.Message = "Flipping Grass";
        for (int i = 0; i < FlipsideEvilPass.FlipsideBottomEdges.Count; i++)
        {
            var indexYCoordsMin = FlipsideEvilPass.FlipsideBottomEdges[i] + 40.0;
            for (var indexXCoords = FlipsideEvilPass.FlipsideWestEdges[i]; indexXCoords < FlipsideEvilPass.FlipsideEastEdges[i]; indexXCoords++)
            {
                indexYCoordsMin += WorldGen.genRand.Next(-2, 3);
                if (indexYCoordsMin < FlipsideEvilPass.FlipsideBottomEdges[i] + 30.0) indexYCoordsMin = FlipsideEvilPass.FlipsideBottomEdges[i] + 30.0;
                if (indexYCoordsMin > FlipsideEvilPass.FlipsideBottomEdges[i] + 50.0) indexYCoordsMin = FlipsideEvilPass.FlipsideBottomEdges[i] + 50.0;
                var indexYCoordsMax = (int)GenVars.worldSurfaceLow;
                while (indexYCoordsMax < indexYCoordsMin)
                {
                    var tile = Main.tile[indexXCoords, indexYCoordsMax];
                    if (indexYCoordsMax < GenVars.worldSurfaceHigh - 1.0)
                    {
                        if (tile.HasTile && (tile.TileType == TileID.Grass || tile.TileType == TileID.JungleGrass))
                            if (ParadoxSystem.AssimilatedBlocks.TryGetValue(tile.TileType, out var tileType))
                                tile.TileType = tileType;
                    }
                    indexYCoordsMax++;
                }
            }
        }
    }
}
public class FlipGrassWallPass : GenPass
{
    public FlipGrassWallPass(string name, float loadWeight) : base(name, loadWeight)
    {
    }

    protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
    {
        progress.Message = "Flipping Grass Walls";
        for (int i = 0; i < FlipsideEvilPass.FlipsideBottomEdges.Count; i++)
        {
            var indexYCoordsMin = FlipsideEvilPass.FlipsideBottomEdges[i] + 40.0 + FlippingGenPasses.ExtraBoundary;
            for (var indexXCoords = FlipsideEvilPass.FlipsideWestEdges[i] - FlippingGenPasses.ExtraBoundary; indexXCoords < FlipsideEvilPass.FlipsideEastEdges[i] + FlippingGenPasses.ExtraBoundary; indexXCoords++)
            {
                indexYCoordsMin += WorldGen.genRand.Next(-2, 3);
                if (indexYCoordsMin < FlipsideEvilPass.FlipsideBottomEdges[i] + 30.0) indexYCoordsMin = FlipsideEvilPass.FlipsideBottomEdges[i] + 30.0;
                if (indexYCoordsMin > FlipsideEvilPass.FlipsideBottomEdges[i] + 50.0) indexYCoordsMin = FlipsideEvilPass.FlipsideBottomEdges[i] + 50.0;
                var indexYCoordsMax = (int)GenVars.worldSurfaceLow;
                while (indexYCoordsMax < indexYCoordsMin)
                {
                    var tile = Main.tile[indexXCoords, indexYCoordsMax];
                    
                    if (tile.WallType != WallID.None)
                        if (ParadoxSystem.AssimilatedWalls.TryGetValue(tile.WallType, out var wallType))
                            tile.WallType = wallType;

                    indexYCoordsMax++;
                }
            }
        }
    }
}
public class AssimilateGrassPass : GenPass
{
    public AssimilateGrassPass(string name, float loadWeight) : base(name, loadWeight)
    {
    }

    protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
    {
        progress.Message = "Assimilating Grass";
        for (int i = 0; i < FlipsideEvilPass.FlipsideBottomEdges.Count; i++)
        {
            var indexYCoordsMin = FlipsideEvilPass.FlipsideBottomEdges[i] + 40.0 + FlippingGenPasses.ExtraBoundary;
            for (var indexXCoords = FlipsideEvilPass.FlipsideWestEdges[i] - FlippingGenPasses.ExtraBoundary; indexXCoords < FlipsideEvilPass.FlipsideEastEdges[i] + FlippingGenPasses.ExtraBoundary; indexXCoords++)
            {
                indexYCoordsMin += WorldGen.genRand.Next(-2, 3);
                if (indexYCoordsMin < FlipsideEvilPass.FlipsideBottomEdges[i] + 30.0) indexYCoordsMin = FlipsideEvilPass.FlipsideBottomEdges[i] + 30.0;
                if (indexYCoordsMin > FlipsideEvilPass.FlipsideBottomEdges[i] + 50.0) indexYCoordsMin = FlipsideEvilPass.FlipsideBottomEdges[i] + 50.0;
                var indexYCoordsMax = (int)GenVars.worldSurfaceLow;
                while (indexYCoordsMax < indexYCoordsMin)
                {
                    var tile = Main.tile[indexXCoords, indexYCoordsMax];
                    var aboveTile = Main.tile[indexXCoords, indexYCoordsMax - 1];
                    List<ushort> validTiles = new List<ushort>()
                    {
                        (ushort)ModContent.TileType<FlippedGrassBlock>(),
                        (ushort)ModContent.TileType<FlippedJungleGrassBlock>(),
                        (ushort)ModContent.TileType<AssecstoneBlockTile>()
                    };

                    if (tile.HasTile && validTiles.Contains(tile.TileType) && !aboveTile.HasTile &&
                        WorldGen.genRand.NextBool(AssimilatedGrassTile.GrowChance / 4))
                        aboveTile.ResetToType((ushort)ModContent.TileType<AssimilatedGrassTile>());

                    indexYCoordsMax++;
                }
            }
        }
    }
}
public class FlipWeedsPass : GenPass
{
    public FlipWeedsPass(string name, float loadWeight) : base(name, loadWeight)
    {
    }

    protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
    {
        progress.Message = "Flipping Weeds";
        for (int i = 0; i < FlipsideEvilPass.FlipsideBottomEdges.Count; i++)
        {
            var indexYCoordsMin = FlipsideEvilPass.FlipsideBottomEdges[i] + 40.0 + FlippingGenPasses.ExtraBoundary;
            for (var indexXCoords = FlipsideEvilPass.FlipsideWestEdges[i] - FlippingGenPasses.ExtraBoundary; indexXCoords < FlipsideEvilPass.FlipsideEastEdges[i] + FlippingGenPasses.ExtraBoundary; indexXCoords++)
            {
                indexYCoordsMin += WorldGen.genRand.Next(-2, 3);
                if (indexYCoordsMin < FlipsideEvilPass.FlipsideBottomEdges[i] + 30.0) indexYCoordsMin = FlipsideEvilPass.FlipsideBottomEdges[i] + 30.0;
                if (indexYCoordsMin > FlipsideEvilPass.FlipsideBottomEdges[i] + 50.0) indexYCoordsMin = FlipsideEvilPass.FlipsideBottomEdges[i] + 50.0;
                var indexYCoordsMax = (int)GenVars.worldSurfaceLow;
                while (indexYCoordsMax < indexYCoordsMin)
                {
                    var tile = Main.tile[indexXCoords, indexYCoordsMax];
                    var aboveTile = Main.tile[indexXCoords, indexYCoordsMax - 1];
                    List<ushort> validTiles = new List<ushort>()
                    {
                        (ushort)ModContent.TileType<FlippedGrassBlock>(),
                        (ushort)ModContent.TileType<FlippedJungleGrassBlock>()
                    };

                    if (tile.HasTile && validTiles.Contains(tile.TileType) && !aboveTile.HasTile)
                    {
                        #region Thorns
                        if (WorldGen.genRand.NextBool(FlippedThorns.GrowChance * 3))
                        {
                            aboveTile.ResetToType((ushort)ModContent.TileType<FlippedThorns>());
                            int successX = 0;
                            int successY = 1;
                            if (WorldGen.genRand.NextBool(2))
                            {
                                for (int j = 0; j < 40; j++)
                                {
                                    Tile t = Main.tile[indexXCoords + successX, indexYCoordsMax - successY];
                                    if (!t.HasTile)
                                    {
                                        t.ResetToType((ushort)ModContent.TileType<FlippedThorns>());
                                        if (successX < successY)
                                        {
                                            successX++;
                                        }
                                        else
                                        {
                                            successY++;
                                        }
                                    }
                                    else
                                    {
                                        if (successX * -1 < successY)
                                        {
                                            successX--;
                                        }
                                        else
                                        {
                                            successY++;
                                        }
                                    }
                                    if (successY > 4)
                                    {
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                for (int j = 0; j < 40; j++)
                                {
                                    Tile t = Main.tile[indexXCoords - successX, indexYCoordsMax - successY];
                                    if (!t.HasTile)
                                    {
                                        t.ResetToType((ushort)ModContent.TileType<FlippedThorns>());
                                        if (successX < successY)
                                        {
                                            successX++;
                                        }
                                        else
                                        {
                                            successY++;
                                        }
                                    }
                                    else
                                    {
                                        if (successX * -1 < successY)
                                        {
                                            successX--;
                                        }
                                        else
                                        {
                                            successY++;
                                        }
                                    }
                                    if (successY > 4)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        #endregion
                        else if (WorldGen.genRand.NextBool(FlippedGrassPlants.GrowChance - 1))
                        {
                            aboveTile.ResetToType((ushort)ModContent.TileType<FlippedGrassPlants>());
                        }
                    }

                    indexYCoordsMax++;
                }
            }
        }
    }
}
public class FlipVinesPass : GenPass
{
    public FlipVinesPass(string name, float loadWeight) : base(name, loadWeight)
    {
    }

    protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
    {
        progress.Message = "Flipping Vines";
        for (int i = 0; i < FlipsideEvilPass.FlipsideBottomEdges.Count; i++)
        {
            var indexYCoordsMin = FlipsideEvilPass.FlipsideBottomEdges[i] + 40.0 + FlippingGenPasses.ExtraBoundary;
            for (var indexXCoords = FlipsideEvilPass.FlipsideWestEdges[i] - FlippingGenPasses.ExtraBoundary; indexXCoords < FlipsideEvilPass.FlipsideEastEdges[i] + FlippingGenPasses.ExtraBoundary; indexXCoords++)
            {
                indexYCoordsMin += WorldGen.genRand.Next(-2, 3);
                if (indexYCoordsMin < FlipsideEvilPass.FlipsideBottomEdges[i] + 30.0) indexYCoordsMin = FlipsideEvilPass.FlipsideBottomEdges[i] + 30.0;
                if (indexYCoordsMin > FlipsideEvilPass.FlipsideBottomEdges[i] + 50.0) indexYCoordsMin = FlipsideEvilPass.FlipsideBottomEdges[i] + 50.0;
                var indexYCoordsMax = (int)GenVars.worldSurfaceLow;
                while (indexYCoordsMax < indexYCoordsMin)
                {
                    var tile = Main.tile[indexXCoords, indexYCoordsMax];
                    var belowTile = Main.tile[indexXCoords, indexYCoordsMax + 1];
                    List<ushort> validTiles = new List<ushort>()
                    {
                        (ushort)ModContent.TileType<FlippedGrassBlock>(),
                        (ushort)ModContent.TileType<FlippedJungleGrassBlock>()
                    };

                    //if (tile.HasTile && validTiles.Contains(tile.TileType) && !aboveTile.HasTile && WorldGen.genRand.NextBool(FlippedGrassPlants.GrowChance / 2))
                        //belowTile.TileType = (ushort)ModContent.TileType<FlippedGrassPlants>();

                    indexYCoordsMax++;
                }
            }
        }
    }
}
public class FlipCactusPalmPass : GenPass
{
    public FlipCactusPalmPass(string name, float loadWeight) : base(name, loadWeight)
    {
    }

    protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
    {
        progress.Message = "Flipping Grass";
        for (int i = 0; i < FlipsideEvilPass.FlipsideBottomEdges.Count; i++)
        {
            var indexYCoordsMin = FlipsideEvilPass.FlipsideBottomEdges[i] + 40.0 + FlippingGenPasses.ExtraBoundary;
            for (var indexXCoords = FlipsideEvilPass.FlipsideWestEdges[i] - FlippingGenPasses.ExtraBoundary; indexXCoords < FlipsideEvilPass.FlipsideEastEdges[i] + FlippingGenPasses.ExtraBoundary; indexXCoords++)
            {
                indexYCoordsMin += WorldGen.genRand.Next(-2, 3);
                if (indexYCoordsMin < FlipsideEvilPass.FlipsideBottomEdges[i] + 30.0) indexYCoordsMin = FlipsideEvilPass.FlipsideBottomEdges[i] + 30.0;
                if (indexYCoordsMin > FlipsideEvilPass.FlipsideBottomEdges[i] + 50.0) indexYCoordsMin = FlipsideEvilPass.FlipsideBottomEdges[i] + 50.0;
                var indexYCoordsMax = (int)GenVars.worldSurfaceLow;
                while (indexYCoordsMax < indexYCoordsMin)
                {
                    var tile = Main.tile[indexXCoords, indexYCoordsMax];
                    var aboveTile = Main.tile[indexXCoords, indexYCoordsMax - 1];

                    if (tile.HasTile && tile.TileType == ModContent.TileType<AssecsandBlockTile>() && !aboveTile.HasTile && WorldGen.genRand.NextBool(FlipsideCactus.WorldGenChance))
                        WorldGen.GrowCactus(indexXCoords, indexYCoordsMax - 1);

                    indexYCoordsMax++;
                }
            }
        }
    }
}
public class FlipStalacPass : GenPass
{
    public FlipStalacPass(string name, float loadWeight) : base(name, loadWeight)
    {
    }

    protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
    {
        progress.Message = "Flipping Grass";
        for (int i = 0; i < FlipsideEvilPass.FlipsideBottomEdges.Count; i++)
        {
            var indexYCoordsMin = FlipsideEvilPass.FlipsideBottomEdges[i] + 40.0;
            for (var indexXCoords = FlipsideEvilPass.FlipsideWestEdges[i]; indexXCoords < FlipsideEvilPass.FlipsideEastEdges[i]; indexXCoords++)
            {
                indexYCoordsMin += WorldGen.genRand.Next(-2, 3);
                if (indexYCoordsMin < FlipsideEvilPass.FlipsideBottomEdges[i] + 30.0) indexYCoordsMin = FlipsideEvilPass.FlipsideBottomEdges[i] + 30.0;
                if (indexYCoordsMin > FlipsideEvilPass.FlipsideBottomEdges[i] + 50.0) indexYCoordsMin = FlipsideEvilPass.FlipsideBottomEdges[i] + 50.0;
                var indexYCoordsMax = (int)GenVars.worldSurfaceLow;
                while (indexYCoordsMax < indexYCoordsMin)
                {
                    var tile = Main.tile[indexXCoords, indexYCoordsMax];
                    if (tile.TileType == TileID.Sand &&
                        indexXCoords >= FlipsideEvilPass.FlipsideWestEdges[i] + WorldGen.genRand.Next(5) &&
                        indexXCoords <= FlipsideEvilPass.FlipsideEastEdges[i] - WorldGen.genRand.Next(5))
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
                        if (ParadoxSystem.AssimilatedBlocks.TryGetValue(tile.TileType, out var tileType))
                            tile.TileType = tileType;

                    if (tile.WallType != WallID.None)
                        if (ParadoxSystem.AssimilatedWalls.TryGetValue(tile.WallType, out var wallType))
                            tile.WallType = wallType;

                    indexYCoordsMax++;
                }
            }
        }
    }
}
public class FlippingCleanupPass : GenPass
{
    public FlippingCleanupPass(string name, float loadWeight) : base(name, loadWeight)
    {
    }

    protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
    {
        progress.Message = "Flipping Cleanup";
        FlipsideEvilPass.FlipsideBottomEdges.Clear();
        FlipsideEvilPass.FlipsideWestEdges.Clear();
        FlipsideEvilPass.FlipsideEastEdges.Clear();
    }
}