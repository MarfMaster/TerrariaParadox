using Microsoft.Xna.Framework;
using MLib.Common.Projectiles;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Dusts.Tiles.Blocks;

namespace TerrariaParadox.Content.Projectiles.Weapons.Melee.Drill;

[Autoload(false)]
public class CustomDrillProj : ModdedFriendlyProjectile
{
    public override int Frames => 1;
    public override int AnimationDuration => 0;
    public override int Width => 22;
    public override int Height => 22;
    public override DamageClass DamageType => DamageClass.Ranged;
    public override int LifeSpan => 60;
    public override bool PassThroughBlocks => true;
    public override int Pierce => -2;
    public override float RotationHelper => 0;

    public override void CustomSetStaticDefaults()
    {
        ProjectileID.Sets.HeldProjDoesNotUsePlayerGfxOffY[Type] = true;
    }

    public override void CustomSetDefaults()
    {
        Projectile.ownerHitCheck = true;
        Projectile.hide =
            true; // Hides the projectile, so it will draw in the player's hand when we set the player's heldProj to this one.
    }

    public override void CustomAI()
    {
        var player = Main.player[Projectile.owner];

        Projectile.timeLeft = 60;

        // Animation code could go here if the projectile was animated. 

        // Plays a sound every 20 ticks. In aiStyle 20, soundDelay is set to 30 ticks.
        if (Projectile.soundDelay <= 0)
        {
            SoundEngine.PlaySound(SoundID.Item22, Projectile.Center);
            Projectile.soundDelay = 20;
        }

        var playerCenter = player.RotatedRelativePoint(player.MountedCenter);
        if (Main.myPlayer == Projectile.owner)
        {
            // This code must only be ran on the client of the projectile owner
            if (player.channel)
            {
                var holdoutDistance = player.HeldItem.shootSpeed * Projectile.scale;
                // Calculate a normalized vector from player to mouse and multiply by holdoutDistance to determine resulting holdoutOffset
                var holdoutOffset = holdoutDistance * Vector2.Normalize(Main.MouseWorld - playerCenter);
                if (holdoutOffset.X != Projectile.velocity.X || holdoutOffset.Y != Projectile.velocity.Y)
                    // This will sync the projectile, most importantly, the velocity.
                    Projectile.netUpdate = true;

                // Projectile.velocity acts as a holdoutOffset for held projectiles.
                Projectile.velocity = holdoutOffset;
            }
            else
            {
                Projectile.Kill();
            }
        }

        if (Projectile.velocity.X > 0f)
            player.ChangeDir(1);
        else if (Projectile.velocity.X < 0f) player.ChangeDir(-1);

        Projectile.spriteDirection = Projectile.direction;
        player.ChangeDir(Projectile.direction); // Change the player's direction based on the projectile's own
        player.heldProj =
            Projectile.whoAmI; // We tell the player that the drill is the held projectile, so it will draw in their hand
        player.SetDummyItemTime(2); // Make sure the player's item time does not change while the projectile is out
        Projectile.Center =
            playerCenter; // Centers the projectile on the player. Projectile.velocity will be added to this in later Terraria code causing the projectile to be held away from the player at a set distance.
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        player.itemRotation = (Projectile.velocity * Projectile.direction).ToRotation();

        // Gives the drill a slight jiggle
        Projectile.velocity.X *= 1f + Main.rand.Next(-3, 4) * 0.01f;

        // Spawning dust
        if (Main.rand.NextBool(10))
        {
            var dust = Dust.NewDustDirect(Projectile.position + Projectile.velocity * Main.rand.Next(6, 10) * 0.15f,
                Projectile.width, Projectile.height, ModContent.DustType<AssecstoneDust>(), 0f, 0f, 80, Color.White);
            dust.position.X -= 4f;
            dust.noGravity = true;
            dust.velocity.X *= 0.5f;
            dust.velocity.Y = -Main.rand.Next(3, 8) * 0.1f;
        }
    }
}