using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;
using TerrariaParadox.Content.Tiles.Blocks;

namespace TerrariaParadox.Content.Tiles.Plants.Cactus
{
	public class FlipsideCactus : ModCactus
	{
		private Asset<Texture2D> Texture;
		private Asset<Texture2D> FruitTexture;

		public override void SetStaticDefaults() 
		{
			// Makes Example Cactus grow on ExampleSand. You will need to use ExampleSolution to convert regular sand since ExampleCactus will not grow naturally yet.
			GrowsOnTileId = [ModContent.TileType<AssecsandBlockTile>()];
			Texture = ModContent.Request<Texture2D>("TerrariaParadox/Content/Tiles/Plants/Cactus/FlipsideCactus");
			FruitTexture = ModContent.Request<Texture2D>("TerrariaParadox/Content/Tiles/Plants/Cactus/FlipsideCactus_Fruit");
		}

		public override Asset<Texture2D> GetTexture() => Texture;

		// This would be where the Cactus Fruit Texture would go, if we had one.
		public override Asset<Texture2D> GetFruitTexture() => FruitTexture;
	}
}