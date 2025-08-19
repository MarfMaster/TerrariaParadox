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
        }
		}
    }