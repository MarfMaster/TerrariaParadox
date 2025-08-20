using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using TerrariaParadox.Content.Dusts.Projectiles.Hostile;

namespace TerrariaParadox.Content.Projectiles.Hostile;

public class WalkingHivePuke : ModdedHostileProjectile
{
    public override int Frames => 1;
    public override int AnimationDuration => 0;
    public override int Width => 20;
    public override int Height => 20;
    public override int ProjectileLifeSpan => 180;
    public override bool PassThroughBlocks => false;
    public override int Pierce => -2;
    public override float RotationHelper => 0;

    public override void CustomAI()
    {
        Projectile.velocity.Y += 0.05f;
        Visuals();
    }

    public void Visuals()
    {
        for (var num1054 = 0; num1054 < 5; num1054++)
        {
            var num1065 = Projectile.velocity.X / 3f * num1054;
            var num3 = Projectile.velocity.Y / 3f * num1054;
            var num14 = 4;
            var num25 = Dust.NewDust(new Vector2(Projectile.position.X + num14, Projectile.position.Y + num14),
                Projectile.width - num14 * 2, Projectile.height - num14 * 2, ModContent.DustType<WalkingHivePukeDust>(),
                0f, 0f, 100, default, 1.2f);
            Main.dust[num25].noGravity = true;
            var dust51 = Main.dust[num25];
            var dust212 = dust51;
            dust212.velocity *= 0.1f;
            dust51 = Main.dust[num25];
            dust212 = dust51;
            dust212.velocity += Projectile.velocity * 0.1f;
            Main.dust[num25].position.X -= num1065;
            Main.dust[num25].position.Y -= num3;
        }

        if (Main.rand.NextBool(20))
        {
            var num36 = 4;
            var num47 = Dust.NewDust(new Vector2(Projectile.position.X + num36, Projectile.position.Y + num36),
                Projectile.width - num36 * 2, Projectile.height - num36 * 2, ModContent.DustType<WalkingHivePukeDust>(),
                0f, 0f, 100, default, 0.6f);
            var dust50 = Main.dust[num47];
            var dust212 = dust50;
            dust212.velocity *= 0.25f;
            dust50 = Main.dust[num47];
            dust212 = dust50;
            dust212.velocity += Projectile.velocity * 0.5f;
        }
    }
}