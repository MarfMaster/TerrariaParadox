using System;
using AltLibrary.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Biomes.TheFlipside;

namespace TerrariaParadox.Content.Projectiles.Consumables;

public class MurkyWaterProjectile : ModProjectile
{
    public static int ConversionType;

    public override void SetStaticDefaults()
    {
        // Cache the conversion type here instead of repeately fetching it every frame
        ConversionType = ModContent.GetInstance<Flipping>().Type;
    }

    public override void SetDefaults()
    {
        Projectile.width = 14;
        Projectile.height = 14;
        Projectile.aiStyle = 0;
        Projectile.friendly = true;
        Projectile.penetrate = 1;
    }

    public override void AI()
    {
        if (Main.windPhysics) Projectile.velocity.X += Main.windSpeedCurrent * Main.windPhysicsStrength;
        Projectile.rotation += (Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y)) * 0.03f *
                               Projectile.direction;

        Projectile.ai[0] += 1f;
        if (Projectile.ai[0] >= 10f)
        {
            Projectile.velocity.Y += 0.25f;
            Projectile.velocity.X *= 0.99f;
        }

        if (Projectile.velocity.Y > 16f) Projectile.velocity.Y = 16f;
    }

    public override void OnKill(int timeLeft)
    {
        SoundEngine.PlaySound(SoundID.Shatter, Projectile.position);
        for (var num996 = 0; num996 < 5; num996++)
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Glass);
        var dustColor = new Color(36, 36, 83);
        for (var num997 = 0; num997 < 30; num997++)
        {
            var num999 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width,
                Projectile.height, DustID.UnholyWater, 0f, -2f, 0, dustColor, 1.1f);
            Main.dust[num999].alpha = 100;
            Main.dust[num999].velocity.X *= 1.5f;
            var dust97 = Main.dust[num999];
            var dust334 = dust97;
            dust334.velocity *= 3f;
        }

        var i2 = (int)(Projectile.position.X + Projectile.width / 2) / 16;
        var j2 = (int)(Projectile.position.Y + Projectile.height / 2) / 16;
        ALConvert.Convert<AltBiomeMain>(i2, j2);
    }
}