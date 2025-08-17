using AltLibrary.Core.Generation;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Input;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation;
using Terraria.WorldBuilding;
using TerrariaParadox.Content.Tiles.Blocks;
using TerrariaParadox.Content.Tiles.Misc;
using TerrariaParadox.Content.Tiles.Walls;

namespace TerrariaParadox
{
    public class WorldGenTutorialWorld : ModSystem
    {
        public static bool JustPressed(Keys key) 
        {
            return Main.keyState.IsKeyDown(key) && !Main.oldKeyState.IsKeyDown(key);
        }

        public override void PostUpdateWorld() 
        {
            if (JustPressed(Keys.NumPad9) && !JustPressed(Keys.NumPad0))
            {
	            TestMethod((int)Main.MouseWorld.X / 16, (int)Main.MouseWorld.Y / 16);
            }
                
                
                //TestMethod((int)Main.MouseWorld.X / 16, (int)Main.MouseWorld.Y / 16);
        }

        private void TestMethod(int x, int y) 
        {
            Dust.QuickBox(new Vector2(x, y) * 16, new Vector2(x + 1, y + 1) * 16, 2, Color.YellowGreen, null);

            // Code to test placed here:
            //WorldGen.TileRunner(x - 1, y, WorldGen.genRand.Next(3, 8), WorldGen.genRand.Next(2, 8), TileID.CobaltBrick);
            Vector2 direction = Main.LocalPlayer.Center.DirectionTo(Main.MouseWorld);
            Point playerCenter = Main.LocalPlayer.Center.ToTileCoordinates();
            Point cursorCenter =  Main.MouseWorld.ToTileCoordinates();
            //WorldGen.digTunnel(playerCenter.X, playerCenter.Y, direction.X, direction.Y, 30, 6);
            
            GenerateEvil(x, x, (x + 100));
            //TunnelAToB(playerCenter, cursorCenter, 20, 20, 10, (ushort)ModContent.TileType<AssecstoneBlockTile>(), (ushort)ModContent.WallType<AssecstoneWallTileUnsafe>());
        }

        private GenAction placeStone;
        private GenAction placeStoneWall;
        private GenAction clearTiles;
        
        private ushort modStone;
        private ushort modStoneWall;
        private ushort bulbTile;
		private void GenerateEvil(int evilBiomePosition, int evilBiomePositionWestBound, int evilBiomePositionEastBound)
		{
			modStone = (ushort)ModContent.TileType<AssecstoneBlockTile>();
			modStoneWall = (ushort)ModContent.WallType<AssecstoneWallTileUnsafe>();
			bulbTile = (ushort)ModContent.TileType<BioluminescentBulb>();
	    //Main.rand mit WorldGen.Rand ersetzen sobald fertig
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
        
        
        Point evilBiomeCenterPoint = new Point(evilBiomeCenter, (int)Main.MouseWorld.Y / 16);
        Point bulbOffset = new Point((int)(width * worldSizeMultiplier * 1.35f), (int)(width * worldSizeMultiplier));;
        Point bulbOffset2 = new Point((int)(width * worldSizeMultiplier * 1.65f), (int)(width * worldSizeMultiplier * 1.25f));
        
        
        
        Vector2 evilBiomeCenterVector = new Vector2((float)evilBiomeCenter, (float)GenVars.rockLayerHigh);
        
        
        float bugBodyLength = 100f * worldSizeMultiplier;
        float bugBodyHollowWidth = bugBodyLength * 0.4f;
        float bugBodyHollowHeight = bugBodyLength * 0.85f;
        float bulbContainerSize = bugBodyLength * 0.035f;
        
        GenShape greatBug = new Shapes.Circle((int)bugBodyLength, (int)bugBodyLength);
        GenShape hollowBugBody = new Shapes.Circle((int)(bugBodyHollowWidth), (int)bugBodyHollowHeight);
        GenShape bugLeg = new ShapeRoot(MathHelper.Pi, 100, 10, 20);
        
        placeStone = new Actions.SetTile(modStone);
        placeStoneWall = new Actions.PlaceWall(modStoneWall);
        clearTiles = new Actions.ClearTile(false);
        
        //Main.NewText("Cursor: " + Main.MouseWorld + ", westbound: " + evilBiomePositionWestBound + ", eastbound: " + evilBiomePositionEastBound);
        WorldUtils.Gen(evilBiomeCenterPoint, greatBug, placeStone);
        WorldUtils.Gen(evilBiomeCenterPoint, greatBug, placeStoneWall);
        
        PlaceBulbs(evilBiomeCenterPoint, bulbOffset, bulbOffset2, bulbContainerSize);
        //WorldUtils.Gen(evilBiomeCenterPoint, bugLeg, clearTiles);

        //Vector2 direction = evilBiomeCenterPoint.ToVector2().DirectionTo(bulbCenterPoint1.ToVector2());
        //WorldGen.digTunnel(evilBiomeCenterPoint.X, evilBiomeCenterPoint.X, direction.X, direction.Y, 30, 4);
        
        WorldUtils.Gen(evilBiomeCenterPoint, hollowBugBody, clearTiles);
        //GenVars.structures.AddProtectedStructure(Utils.CenteredRectangle(evilBiomeCenterVector.ToWorldCoordinates(), new Vector2(bugBodyLength, bugBodyLength)));
        
        //WorldGen.TileRunner(evilBiomePosition, (int)GenVars.worldSurface, 50, WorldGen.genRand.Next(2, 8), TileID.PinkSlimeBlock);
        //Flipping(evilBiomePosition, evilBiomePositionWestBound, evilBiomePositionEastBound);
		}

		private void PlaceBulbs(Point evilBiomeCenter, Point bulbOffset1, Point bulbOffset2, float bulbContainerSize)
		{
			Point currentBulbPoint;
			Point bulbCenterPoint1 = evilBiomeCenter + bulbOffset1;
			Point bulbCenterPoint2 = evilBiomeCenter - bulbOffset1;
			Point bulbCenterPoint3 = evilBiomeCenter + new Point(-bulbOffset1.X, bulbOffset1.Y);
			Point bulbCenterPoint4 = evilBiomeCenter + new Point(bulbOffset1.X, -bulbOffset1.Y);
        
			Point bulbCenterPoint5 = evilBiomeCenter + new Point(bulbOffset2.X, bulbOffset1.Y / 3);
			Point bulbCenterPoint6 = evilBiomeCenter + new Point(-bulbOffset2.X, bulbOffset1.Y / -3);
			Point bulbCenterPoint7 = evilBiomeCenter + new Point(bulbOffset2.X, bulbOffset1.Y / -3);
			Point bulbCenterPoint8 = evilBiomeCenter + new Point(-bulbOffset2.X, bulbOffset1.Y / 3);
			
			GenShape outerBulbContainer = new Shapes.Circle((int)(bulbContainerSize * 1.75f));
			GenShape bulbContainer = new Shapes.Circle((int)bulbContainerSize);

			int outlineWidth = 16;
			int outlineHeight = 16;
			int tunnelSize = 10;
			int overshoot = -3;

			currentBulbPoint = bulbCenterPoint1;
			WorldgenMain.TunnelAToB(evilBiomeCenter, currentBulbPoint, outlineWidth, outlineHeight, tunnelSize, modStone, modStoneWall, overshoot);
			WorldUtils.Gen(currentBulbPoint, outerBulbContainer, placeStone);
			WorldUtils.Gen(currentBulbPoint, outerBulbContainer, placeStoneWall);
			WorldUtils.Gen(currentBulbPoint, bulbContainer, clearTiles);
			WorldGen.PlaceSign(currentBulbPoint.X, currentBulbPoint.Y, bulbTile);
			
			currentBulbPoint = bulbCenterPoint2;
			WorldgenMain.TunnelAToB(evilBiomeCenter, currentBulbPoint, outlineWidth, outlineHeight, tunnelSize, modStone, modStoneWall, overshoot);
			WorldUtils.Gen(currentBulbPoint, outerBulbContainer, placeStone);
			WorldUtils.Gen(currentBulbPoint, outerBulbContainer, placeStoneWall);
			WorldUtils.Gen(currentBulbPoint, bulbContainer, clearTiles);
			WorldGen.PlaceSign(currentBulbPoint.X, currentBulbPoint.Y, bulbTile);
			
			currentBulbPoint = bulbCenterPoint3;
			WorldgenMain.TunnelAToB(evilBiomeCenter, currentBulbPoint, outlineWidth, outlineHeight, tunnelSize, modStone, modStoneWall, overshoot);
			WorldUtils.Gen(currentBulbPoint, outerBulbContainer, placeStone);
			WorldUtils.Gen(currentBulbPoint, outerBulbContainer, placeStoneWall);
			WorldUtils.Gen(currentBulbPoint, bulbContainer, clearTiles);
			WorldGen.PlaceSign(currentBulbPoint.X, currentBulbPoint.Y, bulbTile);
			
			currentBulbPoint = bulbCenterPoint4;
			WorldgenMain.TunnelAToB(evilBiomeCenter, currentBulbPoint, outlineWidth, outlineHeight, tunnelSize, modStone, modStoneWall, overshoot);
			WorldUtils.Gen(currentBulbPoint, outerBulbContainer, placeStone);
			WorldUtils.Gen(currentBulbPoint, outerBulbContainer, placeStoneWall);
			WorldUtils.Gen(currentBulbPoint, bulbContainer, clearTiles);
			WorldGen.PlaceSign(currentBulbPoint.X, currentBulbPoint.Y, bulbTile);
			
			currentBulbPoint = bulbCenterPoint5;
			WorldgenMain.TunnelAToB(evilBiomeCenter, currentBulbPoint, outlineWidth, outlineHeight, tunnelSize, modStone, modStoneWall, overshoot);
			WorldUtils.Gen(currentBulbPoint, outerBulbContainer, placeStone);
			WorldUtils.Gen(currentBulbPoint, outerBulbContainer, placeStoneWall);
			WorldUtils.Gen(currentBulbPoint, bulbContainer, clearTiles);
			WorldGen.PlaceSign(currentBulbPoint.X, currentBulbPoint.Y, bulbTile);
			
			currentBulbPoint = bulbCenterPoint6;
			WorldgenMain.TunnelAToB(evilBiomeCenter, currentBulbPoint, outlineWidth, outlineHeight, tunnelSize, modStone, modStoneWall, overshoot);
			WorldUtils.Gen(currentBulbPoint, outerBulbContainer, placeStone);
			WorldUtils.Gen(currentBulbPoint, outerBulbContainer, placeStoneWall);
			WorldUtils.Gen(currentBulbPoint, bulbContainer, clearTiles);
			WorldGen.PlaceSign(currentBulbPoint.X, currentBulbPoint.Y, bulbTile);
			
			currentBulbPoint = bulbCenterPoint7;
			WorldgenMain.TunnelAToB(evilBiomeCenter, currentBulbPoint, outlineWidth, outlineHeight, tunnelSize, modStone, modStoneWall, overshoot);
			WorldUtils.Gen(currentBulbPoint, outerBulbContainer, placeStone);
			WorldUtils.Gen(currentBulbPoint, outerBulbContainer, placeStoneWall);
			WorldUtils.Gen(currentBulbPoint, bulbContainer, clearTiles);
			WorldGen.PlaceSign(currentBulbPoint.X, currentBulbPoint.Y, bulbTile);
			
			currentBulbPoint = bulbCenterPoint8;
			WorldgenMain.TunnelAToB(evilBiomeCenter, currentBulbPoint, outlineWidth, outlineHeight, tunnelSize, modStone, modStoneWall, overshoot);
			WorldUtils.Gen(currentBulbPoint, outerBulbContainer, placeStone);
			WorldUtils.Gen(currentBulbPoint, outerBulbContainer, placeStoneWall);
			WorldUtils.Gen(currentBulbPoint, bulbContainer, clearTiles);
			WorldGen.PlaceSign(currentBulbPoint.X, currentBulbPoint.Y, bulbTile);
		}

			
			//WorldGen.digTunnel(pA.X, pA.Y, xDirToB, yDirToB, 10, 5);
			//WorldUtils.Gen(pA, tunnel, clearTiles);
			//WorldUtils.Gen(pB, tunnel, clearTiles);
		}
    }