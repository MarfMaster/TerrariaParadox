using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Dusts.Tiles.Blocks;
using TerrariaParadox.Content.Items.Weapons.Ranged;

namespace TerrariaParadox.Content.Projectiles.Weapons.Ranged;

public class ParasyteHeldProjectile : ModdedFriendlyProjectile
{
    public override int Frames => 5;
    public override int AnimationDuration => 10;
    public override int Width => 80;
    public override int Height => 30;
    public override DamageClass DamageType => DamageClass.Ranged;
    public override int LifeSpan => 600;
    public override bool PassThroughBlocks => true;
    public override int Pierce => -2;
    public override float RotationHelper => 0;
    public override void CustomSetStaticDefaults()
    {
        ProjectileID.Sets.HeldProjDoesNotUsePlayerGfxOffY[Type] = true;
    }

    public override void CustomSetDefaults()
    {
    }

    public override void OnSpawn(IEntitySource source)
    {
        Player player = Main.player[Projectile.owner];
        Vector2 mountedCenter = player.MountedCenter;
        Vector2 unitVectorTowardsMouse = mountedCenter.DirectionTo(Main.MouseWorld).SafeNormalize(Vector2.UnitX * player.direction);
    }

    private bool Throwing = false;
    public override void AI()
    {
			Player player = Main.player[Projectile.owner];
			
			Vector2 playerCenter = player.RotatedRelativePoint(player.MountedCenter);
			if (Main.myPlayer == Projectile.owner) 
			{
				float holdoutDistance = 32f * Projectile.scale;
				// Calculate a normalized vector from player to mouse and multiply by holdoutDistance to determine resulting holdoutOffset
				Vector2 holdoutOffset = holdoutDistance * Vector2.Normalize(Main.MouseWorld - playerCenter);
				if (holdoutOffset.X != Projectile.velocity.X || holdoutOffset.Y != Projectile.velocity.Y) 
				{
					// This will sync the projectile, most importantly, the velocity.
					Projectile.netUpdate = true;
				}

				// Projectile.velocity acts as a holdoutOffset for held projectiles.
				Projectile.velocity = holdoutOffset;
				// This code must only be ran on the client of the projectile 
			}
			
			AITimer1++;

			float swingTime = player.itemAnimationMax * Projectile.MaxUpdates;
			
			float dividend = swingTime / ((float)Frames * 2f);
			switch (AITimer1)
			{
				case float one when AITimer1 < dividend * 1.76f:
				{
					Projectile.frame = 4;
					break;
				}
				case float two when AITimer1 > dividend * 1.76f && AITimer1 < dividend * 3.52f:
				{
					Projectile.frame = 3;
					break;
				}
				case float three when AITimer1 > dividend * 3.52f && AITimer1 < dividend * 5.28f:
				{
					Projectile.frame = 2;
					break;
				}
				case float four when AITimer1 > dividend * 5.28f && AITimer1 < dividend * 7.04f:
				{
					Projectile.frame = 0;
					break;
				}
				case float five when AITimer1 > dividend * 7.04f && AITimer1 < dividend * 8.8f:
				{
					Projectile.frame = 1;
					break;
				}
				case float six when AITimer1 > dividend * 8.8f && AITimer1 < dividend * 9.1f:
				{
					Projectile.frame = 0;
					break;
				}
				case float seven when AITimer1 > dividend * 9.1f && AITimer1 < dividend * 9.4f:
				{
					Projectile.frame = 2;
					break;
				}
				case float eight when AITimer1 > dividend * 9.4f && AITimer1 < dividend * 9.7f:
				{
					Projectile.frame = 4;
					break;
				}
				case float nine when AITimer1 > dividend * 9.7f && AITimer1 < dividend * 10f:
				{
					Projectile.frame = 3;
					break;
				}
			}

			if (AITimer1 >= dividend * 8.75f && !Throwing)
			{
				SoundEngine.PlaySound(SoundID.Item42, Projectile.Center);
				if (player.whoAmI == Main.myPlayer)
				{
					Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), player.Center, Projectile.velocity, (int)AITimer2, Projectile.damage, Projectile.knockBack, Projectile.owner);
				}
				Throwing = true;
			}

			if (Projectile.velocity.X > 0f) 
			{
				player.ChangeDir(1);
			}
			else if (Projectile.velocity.X < 0f) 
			{
				player.ChangeDir(-1);
			}

			player.ChangeDir(Projectile.direction); // Change the player's direction based on the projectile's own
			Projectile.spriteDirection = player.direction;
			player.heldProj = Projectile.whoAmI; // We tell the player that the drill is the held projectile, so it will draw in their hand
			Projectile.Center = playerCenter + new Vector2(-0, -0); // Centers the projectile on the player. Projectile.velocity will be added to this in later Terraria code causing the projectile to be held away from the player at a set distance.
			Projectile.rotation = Projectile.velocity.ToRotation() + (Projectile.velocity.X < 0f ? -MathHelper.Pi : 0);
			player.itemRotation = (Projectile.velocity * Projectile.direction).ToRotation();
			
			
			if (AITimer1 >= swingTime || player.itemAnimation <= 0) 
			{
				Projectile.Kill();
				return;
			}
			

			// Gives the drill a slight jiggle
			//Projectile.velocity.X *= 1f + Main.rand.Next(-3, 4) * 0.01f;

			// Spawning dust
			if (Main.rand.NextBool(10)) 
			{
				Dust dust = Dust.NewDustDirect(Projectile.position + Projectile.velocity * Main.rand.Next(6, 10) * 0.15f, Projectile.width, Projectile.height, ModContent.DustType<AssecstoneDust>(), 0f, 0f, 80, Color.White, 1f);
				dust.position.X -= 4f;
				dust.noGravity = true;
				dust.velocity.X *= 0.5f;
				dust.velocity.Y = -Main.rand.Next(3, 8) * 0.1f;
			}
    }

    public override bool? CanDamage()
    {
        return false;
    }
    public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
    {
        overPlayers.Add(index);
    }
}