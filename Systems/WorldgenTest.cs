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
            //WorldGen.digTunnel(playerCenter.X, playerCenter.Y, direction.X, direction.Y, 30, 6);
            
            //GenerateEvil(x, x, (x + 100));
        }
        
    private void GenerateEvil(int evilBiomePosition, int evilBiomePositionWestBound, int evilBiomePositionEastBound)
    {
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
        
        Point bulbCenterPoint1 = evilBiomeCenterPoint + bulbOffset;
        Point bulbCenterPoint2 = evilBiomeCenterPoint - bulbOffset;
        Point bulbCenterPoint3 = evilBiomeCenterPoint + new Point(-bulbOffset.X, bulbOffset.Y);
        Point bulbCenterPoint4 = evilBiomeCenterPoint + new Point(bulbOffset.X, -bulbOffset.Y);
        
        Point bulbCenterPoint5 = evilBiomeCenterPoint + new Point(bulbOffset2.X, bulbOffset.Y / 3);
        Point bulbCenterPoint6 = evilBiomeCenterPoint + new Point(-bulbOffset2.X, bulbOffset.Y / -3);
        Point bulbCenterPoint7 = evilBiomeCenterPoint + new Point(bulbOffset2.X, bulbOffset.Y / -3);
        Point bulbCenterPoint8 = evilBiomeCenterPoint + new Point(-bulbOffset2.X, bulbOffset.Y / 3);
        
        
        
        Vector2 evilBiomeCenterVector = new Vector2((float)evilBiomeCenter, (float)GenVars.rockLayerHigh);
        
        
        float bugBodyLength = 100f * worldSizeMultiplier;
        float bugBodyHollowWidth = bugBodyLength * 0.625f;
        float bulbContainerWidth = bugBodyLength * 0.035f;
        
        GenShape greatBug = new Shapes.Circle((int)bugBodyLength, (int)bugBodyLength);
        GenShape hollowBugBody = new Shapes.Circle((int)(bugBodyHollowWidth * 0.65f), (int)bugBodyHollowWidth);
        GenShape bulbContainer = new Shapes.Circle((int)bulbContainerWidth);
        GenShape bugLeg = new ShapeRoot(MathHelper.Pi, 100, 10, 20);
        
        GenAction placeStone = new Actions.SetTile((ushort)ModContent.TileType<AssecstoneBlockTile>());
        GenAction placeStoneWall = new Actions.PlaceWall((ushort)ModContent.WallType<AssecstoneWallTileUnsafe>());
        GenAction clearTiles = new Actions.ClearTile(false);
        
        Main.NewText("Cursor: " + Main.MouseWorld + ", westbound: " + evilBiomePositionWestBound + ", eastbound: " + evilBiomePositionEastBound);
        WorldUtils.Gen(evilBiomeCenterPoint, greatBug, placeStone);
        WorldUtils.Gen(evilBiomeCenterPoint, greatBug, placeStoneWall);

        WorldUtils.Gen(bulbCenterPoint1, bulbContainer, clearTiles);
        WorldGen.PlaceSign(bulbCenterPoint1.X, bulbCenterPoint1.Y, (ushort)ModContent.TileType<BioluminescentBulb>(), 0);
        WorldUtils.Gen(bulbCenterPoint2, bulbContainer, clearTiles);
        WorldGen.PlaceSign(bulbCenterPoint2.X, bulbCenterPoint2.Y, (ushort)ModContent.TileType<BioluminescentBulb>(), 0);
        WorldUtils.Gen(bulbCenterPoint3, bulbContainer, clearTiles);
        WorldGen.PlaceSign(bulbCenterPoint3.X, bulbCenterPoint3.Y, (ushort)ModContent.TileType<BioluminescentBulb>(), 0);
        WorldUtils.Gen(bulbCenterPoint4, bulbContainer, clearTiles);
        WorldGen.PlaceSign(bulbCenterPoint4.X, bulbCenterPoint4.Y, (ushort)ModContent.TileType<BioluminescentBulb>(), 0);
        WorldUtils.Gen(bulbCenterPoint5, bulbContainer, clearTiles);
        WorldGen.PlaceSign(bulbCenterPoint5.X, bulbCenterPoint5.Y, (ushort)ModContent.TileType<BioluminescentBulb>(), 0);
        WorldUtils.Gen(bulbCenterPoint6, bulbContainer, clearTiles);
        WorldGen.PlaceSign(bulbCenterPoint6.X, bulbCenterPoint6.Y, (ushort)ModContent.TileType<BioluminescentBulb>(), 0);
        WorldUtils.Gen(bulbCenterPoint7, bulbContainer, clearTiles);
        WorldGen.PlaceSign(bulbCenterPoint7.X, bulbCenterPoint7.Y, (ushort)ModContent.TileType<BioluminescentBulb>(), 0);
        WorldUtils.Gen(bulbCenterPoint8, bulbContainer, clearTiles);
        WorldGen.PlaceSign(bulbCenterPoint8.X, bulbCenterPoint8.Y, (ushort)ModContent.TileType<BioluminescentBulb>(), 0);
        //WorldUtils.Gen(evilBiomeCenterPoint, bugLeg, clearTiles);

        Vector2 direction = evilBiomeCenterPoint.ToVector2().DirectionTo(bulbCenterPoint1.ToVector2());
        WorldGen.digTunnel(evilBiomeCenterPoint.X, evilBiomeCenterPoint.X, direction.X, direction.Y, 30, 4);
        
        //WorldUtils.Gen(evilBiomeCenterPoint, hollowBugBody, clearTiles);
        //GenVars.structures.AddProtectedStructure(Utils.CenteredRectangle(evilBiomeCenterVector.ToWorldCoordinates(), new Vector2(bugBodyLength, bugBodyLength)));
        
        //WorldGen.TileRunner(evilBiomePosition, (int)GenVars.worldSurface, 50, WorldGen.genRand.Next(2, 8), TileID.PinkSlimeBlock);
        //Flipping(evilBiomePosition, evilBiomePositionWestBound, evilBiomePositionEastBound);
    }
    }
}