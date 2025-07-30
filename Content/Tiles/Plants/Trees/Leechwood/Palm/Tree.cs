using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaParadox.Content.Tiles.Plants.Trees.Leechwood.Palm
{
	[Autoload(false)]
	public class Tree : ModPalmTree
	{
		private Asset<Texture2D> WoodTexture;
		private Asset<Texture2D> OasisTopsTexture;
		private Asset<Texture2D> BeachTopsTexture;

		public override void SetStaticDefaults() 
		{
			WoodTexture = ModContent.Request<Texture2D>("TerrariaParadox/Content/Tiles/Plants/Trees/Leechwood/Palm/TreeWood");
			OasisTopsTexture = ModContent.Request<Texture2D>("TerrariaParadox/Content/Tiles/Plants/Trees/Leechwood/Palm/OasisTreeTops");
			BeachTopsTexture = ModContent.Request<Texture2D>("TerrariaParadox/Content/Tiles/Plants/Trees/Leechwood/Palm/BeachTreeTops");
			
			GrowsOnTileId = [TileID.Gold]; //would be assec sand
		}
		public override Asset<Texture2D> GetTexture() => WoodTexture;
		
		public override Asset<Texture2D> GetOasisTopTextures() => OasisTopsTexture;
		
		public override Asset<Texture2D> GetTopTextures() => BeachTopsTexture;
		
		public override int TreeLeaf()
		{
			return ModContent.GoreType<Leechwood.Leaf>();
		}

		public override int SaplingGrowthType(ref int style) 
		{
			style = 1;
			return ModContent.TileType<Leechwood.Sapling>();
		}
		public override int DropWood()
		{
			return ItemID.Wood;
		}
    
		public override bool Shake(int x, int y, ref bool createLeaves) 
		{
			Item.NewItem(WorldGen.GetItemSource_FromTreeShake(x, y), new Vector2(x, y) * 16, ItemID.Spaghetti);
			return true;
		}
		
		// This is a blind copy-paste from Vanilla's PurityPalmTree settings.
		// TODO: This needs some explanations
		public override TreePaintingSettings TreeShaderSettings => new TreePaintingSettings 
		{
			UseSpecialGroups = true,
			SpecialGroupMinimalHueValue = 11f / 72f,
			SpecialGroupMaximumHueValue = 0.25f,
			SpecialGroupMinimumSaturationValue = 0.88f,
			SpecialGroupMaximumSaturationValue = 1f
		};
	}
}