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
        if (WorldBiomeManager.WorldEvilBiome.Type == ModContent.GetInstance<FAltBiomeMain>().Type)
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
                    var firstbelowTile = Main.tile[indexXCoords, indexYCoordsMax + 1];
                    List<ushort> validTiles = new List<ushort>()
                    {
                        (ushort)ModContent.TileType<FlippedGrassBlock>(),
                        (ushort)ModContent.TileType<FlippedJungleGrassBlock>()
                    };
                    if (validTiles.Contains(tile.TileType) && WorldGen.genRand.NextBool(FlippedVine.GrowChance / 11) && !firstbelowTile.HasTile)
                    {
                        for (int j = 0; j < WorldGen.genRand.Next(1, 11); j++)
                        {
                            var belowTile = Main.tile[indexXCoords, indexYCoordsMax + j];
                            if (!belowTile.HasTile)
                            {
                                belowTile.ResetToType((ushort)ModContent.TileType<FlippedVine>());
                            }
                        }
                    }

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
        progress.Message = "Flipping Sand Blocks";
        foreach (var p in FlipsideEvilPass.SandToBeFlipped)
        {
            Tile tile = Main.tile[p.X, p.Y];
            if (tile.TileType == TileID.Sand)
            {
                tile.TileType = (ushort)ModContent.TileType<AssecsandBlockTile>();
            }
        }
    }
}
public class FlipStalacPass : GenPass  //this just doesn't work despite me doing everything the same as in randomupdate, where it works(they do grow naturally yes)
{
    public FlipStalacPass(string name, float loadWeight) : base(name, loadWeight)
    {
    }

    protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
    {
        progress.Message = "Flipping Stalactites and Stalagmites";
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
                    if (indexYCoordsMax > GenVars.worldSurfaceHigh - 1.0)
                    {
                        var tile = Framing.GetTileSafely(indexXCoords, indexYCoordsMax);
                        var oneAboveTile = Framing.GetTileSafely(indexXCoords, indexYCoordsMax - 1);
                        var twoAboveTile = Framing.GetTileSafely(indexXCoords, indexYCoordsMax - 2);
                        var oneBelowTile = Framing.GetTileSafely(indexXCoords, indexYCoordsMax + 1);
                        var twoBelowTile = Framing.GetTileSafely(indexXCoords, indexYCoordsMax + 2);
                        var stalactiS = (ushort)ModContent.TileType<AssecstoneStalactitesSmallNatural>();
                        var stalactiM = (ushort)ModContent.TileType<AssecstoneStalactitesNatural>();
                        var stalagmiS = (ushort)ModContent.TileType<AssecstoneStalagmitesSmallNatural>();
                        var stalagmiM = (ushort)ModContent.TileType<AssecstoneStalagmitesNatural>();
                        
                        var frameX = WorldGen.genRand.Next(0, 6); //generate a random tileframe for alternate styles
                        if (tile.TileType == (ushort)ModContent.TileType<AssecstoneBlockTile>() &&
                            tile.BlockType == BlockType.Solid)
                        {
                            if (!oneAboveTile.HasTile) //check for empty space and whether this block is solid
                            {
                                if (WorldGen.genRand.NextBool(2)) //1 in X chance
                                {
                                    WorldGen.KillTile(indexXCoords, indexYCoordsMax - 1);
                                    WorldGen.Place1x1(indexXCoords, indexYCoordsMax - 1, stalagmiS); //place tile
                                    oneAboveTile.TileFrameX = (short)(frameX * 18); //reframe it so it can show alternate styles
                                }
                                else if (WorldGen.genRand.NextBool(AssecstoneStalagmitesNatural.GrowChance / 20) &&
                                         !twoAboveTile.HasTile) //1 in X chance and whether there's enough space
                                {
                                    WorldGen.Place1x2(indexXCoords, indexYCoordsMax - 1, (ushort)ModContent.TileType<AssecstoneStalagmitesNatural>(), 0);
                                    oneAboveTile.TileFrameX = (short)(frameX * 18); //need to reframe both tiles to the same frame
                                    twoAboveTile.TileFrameX = (short)(frameX * 18);
                                }

                                WorldGen.TileFrame(indexXCoords, indexYCoordsMax - 1);
                                return;
                            }

                            //everything for hanging tiles it the same but adjusted for it hanging below instead of being grounded on top of this tile
                            if (!oneBelowTile.HasTile)
                            {
                                if (WorldGen.genRand.NextBool(AssecstoneStalactitesSmallNatural.GrowChance / 20))
                                {
                                    oneBelowTile.ResetToType(
                                        (ushort)ModContent
                                            .TileType<
                                                AssecstoneStalactitesSmallNatural>()); //using resettotype here because there is no worldgen method for hanging 1x1 rubble
                                    oneBelowTile.TileFrameX = (short)(frameX * 18);
                                }
                                else if (WorldGen.genRand.NextBool(AssecstoneStalactitesNatural.GrowChance) && !twoBelowTile.HasTile)
                                {
                                    WorldGen.Place1x2Top(indexXCoords, indexYCoordsMax + 1, (ushort)ModContent.TileType<AssecstoneStalactitesNatural>(), 0);
                                    oneBelowTile.TileFrameX = (short)(frameX * 18);
                                    twoBelowTile.TileFrameX = (short)(frameX * 18);
                                }

                                WorldGen.TileFrame(indexXCoords, indexYCoordsMax + 1);
                            }
                        }
                        else if (tile.TileType == (ushort)ModContent.TileType<MurkyIceBlockTile>() &&
                                 tile.BlockType == BlockType.Solid)
                        {
                            if (!oneBelowTile.HasTile)
                            {
                                if (WorldGen.genRand.NextBool(MurkyIcicles1x1Natural.GrowChance / 4))
                                {
                                    oneBelowTile.ResetToType((ushort)ModContent.TileType<MurkyIcicles1x1Natural>());
                                    oneBelowTile.TileFrameX = (short)(frameX * 18);
                                }
                                else if (WorldGen.genRand.NextBool(MurkyIcicles1x2Natural.GrowChance / 4) && !twoBelowTile.HasTile)
                                {
                                    WorldGen.Place1x2Top(indexXCoords, indexYCoordsMax + 1, (ushort)ModContent.TileType<MurkyIcicles1x2Natural>(), 0);
                                    oneBelowTile.TileFrameX = (short)(frameX * 18);
                                    twoBelowTile.TileFrameX = (short)(frameX * 18);
                                }

                                WorldGen.TileFrame(indexXCoords, indexYCoordsMax + 1);
                            }
                        }
                    }
                    
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
        FlipsideEvilPass.SandToBeFlipped.Clear();
    }
}