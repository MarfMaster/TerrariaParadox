using AltLibrary.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Dusts.Items.Consumables;
using TerrariaParadox.Content.Dusts.Tiles.Blocks;

namespace TerrariaParadox.Content.Projectiles.Consumables;

public class ParasiticPowderProjectile : ModProjectile
{
	public static int ConversionType;
	
	public override void SetStaticDefaults() 
	{
		// Cache the conversion type here instead of repeately fetching it every frame
		ConversionType = ModContent.GetInstance<Biomes.TheFlipside.Flipping>().Type;
	}
    public override void SetDefaults()
    {
        Projectile.width = 48;
        Projectile.height = 48;
        Projectile.aiStyle = 0;
        Projectile.friendly = true;
        Projectile.tileCollide = false;
        Projectile.penetrate = -1;
        Projectile.alpha = 255;
        Projectile.ignoreWater = true;
    }

    public override bool? CanCutTiles()
    {
	    return false;
    }

    public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
	    bool crimson = Projectile.type == 463;
	    for (int num41 = 0; num41 < 200; num41++)
	    {
		    if (Main.npc[num41].active)
		    {
			    Rectangle value2 = new Rectangle((int)Main.npc[num41].position.X, (int)Main.npc[num41].position.Y, Main.npc[num41].width, Main.npc[num41].height);
			    if (projHitbox.Intersects(value2))
			    {
				    Main.npc[num41].AttemptToConvertNPCToEvil(crimson); //replace Projectile with a hook that allows for flipside conversion
			    }
		    }
	    }
	    return Projectile.Colliding(projHitbox, targetHitbox);
    }

    public override void AI()
    {
				Projectile.velocity *= 0.95f;
				Projectile.ai[0] += 1f;
				if (Projectile.ai[0] == 180f)
				{
					Projectile.Kill();
				}
				if (Projectile.ai[1] == 0f)
				{
					Projectile.ai[1] = 1f;
					int num966 = 30;
					Color dustColor = new Color(36, 36, 83);
					for (int num977 = 0; num977 < num966; num977++)
					{
						Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<ParasiticPowderDust>(), Projectile.velocity.X, Projectile.velocity.Y, 50, dustColor);
					}
				}
				bool playerIsOwner = Main.myPlayer == Projectile.owner;
				if (playerIsOwner)
				{
					int num988 = (int)(Projectile.position.X / 16f) - 1;
					int num999 = (int)((Projectile.position.X + (float)Projectile.width) / 16f) + 2;
					int num1010 = (int)(Projectile.position.Y / 16f) - 1;
					int num1021 = (int)((Projectile.position.Y + (float)Projectile.height) / 16f) + 2;
					if (num988 < 0)
					{
						num988 = 0;
					}
					if (num999 > Main.maxTilesX)
					{
						num999 = Main.maxTilesX;
					}
					if (num1010 < 0)
					{
						num1010 = 0;
					}
					if (num1021 > Main.maxTilesY)
					{
						num1021 = Main.maxTilesY;
					}
					Vector2 vector57 = default(Vector2);
					
					for (int num1032 = num988; num1032 < num999; num1032++)
					{
						for (int num1043 = num1010; num1043 < num1021; num1043++)
						{
							vector57.X = num1032 * 16;
							vector57.Y = num1043 * 16;
							if (!(Projectile.position.X + (float)Projectile.width > vector57.X) || !(Projectile.position.X < vector57.X + 16f) || !(Projectile.position.Y + (float)Projectile.height > vector57.Y) || !(Projectile.position.Y < vector57.Y + 16f) || !Main.tile[num1032, num1043].HasTile)
							{
								continue;
							}
							ALConvert.Convert<Biomes.TheFlipside.AltBiomeMain>(num1032, num1043, 1);
							goto IL_510c;
							IL_510c:
							continue;
							Tile tile = Main.tile[num1032, num1043];
							if (tile.TileType >= 0 && tile.TileType < TileID.Count && TileID.Sets.CommonSapling[tile.TileType])
							{
								if (Main.remixWorld && num1043 >= (int)Main.worldSurface - 1 && num1043 < Main.maxTilesY - 20)
								{
									WorldGen.AttemptToGrowTreeFromSapling(num1032, num1043, underground: false);
								}
								WorldGen.AttemptToGrowTreeFromSapling(num1032, num1043, num1043 > (int)Main.worldSurface - 1);
							}
						}
					}
				}
    }
}