using AltLibrary.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Biomes.TheFlipside;
using TerrariaParadox.Content.Dusts.Tiles.Blocks;

namespace TerrariaParadox.Content.Projectiles.Consumables.Ammo;

	public class MurkySolutionProjectile : ModProjectile
	{
		public static int ConversionType;

		public ref float Progress => ref Projectile.ai[0];
		// Solutions shot by the terraformer get an increase in conversion area size, indicated by the second AI parameter being set to 1
		public bool ShotFromTerraformer => Projectile.ai[1] == 1f;

		public override void SetStaticDefaults() 
		{
			// Cache the conversion type here instead of repeately fetching it every frame
			ConversionType = ModContent.GetInstance<Biomes.TheFlipside.Flipping>().Type;
		}

		public override void SetDefaults() 
		{
			// This method quickly sets the projectile properties to match other sprays.
			Projectile.DefaultToSpray();
			Projectile.aiStyle = 0; // Here we set aiStyle back to 0 because we have custom AI code
		}

		public override bool? CanDamage() => false;

		public override void AI() 
		{
			if (Projectile.timeLeft > 133)
				Projectile.timeLeft = 133;

			if (Projectile.owner == Main.myPlayer) {
				int size = ShotFromTerraformer ? 3 : 2;
				Point tileCenter = Projectile.Center.ToTileCoordinates();
				ALConvert.Convert<Biomes.TheFlipside.AltBiomeMain>(tileCenter.X, tileCenter.Y, size);
			}

			int spawnDustTreshold = 7;
			if (ShotFromTerraformer)
				spawnDustTreshold = 3;

			if (Progress > (float)spawnDustTreshold) {
				float dustScale = 1f;
				int dustType = DustID.MushroomSpray;
				Color dustColor = new Color(-36f, -36f, -83f);

				if (Progress == spawnDustTreshold + 1)
					dustScale = 0.2f;
				else if (Progress == spawnDustTreshold + 2)
					dustScale = 0.4f;
				else if (Progress == spawnDustTreshold + 3)
					dustScale = 0.6f;
				else if (Progress == spawnDustTreshold + 4)
					dustScale = 0.8f;

				int dustArea = 0;
				if (ShotFromTerraformer) {
					dustScale *= 1.2f;
					dustArea = (int)(12f * dustScale);
				}

				Dust sprayDust = Dust.NewDustDirect(new Vector2(Projectile.position.X - dustArea, Projectile.position.Y - dustArea), Projectile.width + dustArea * 2, Projectile.height + dustArea * 2, dustType, Projectile.velocity.X * 0.4f, Projectile.velocity.Y * 0.4f, 100, dustColor);
				sprayDust.noGravity = true;
				sprayDust.scale *= 1.75f * dustScale;
			}

			Progress++;
			Projectile.rotation += 0.3f * Projectile.direction;
		}
	}