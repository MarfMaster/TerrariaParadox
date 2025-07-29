using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Debuffs.DoT;
using TerrariaParadox.Content.Items.Weapons.Melee;

namespace TerrariaParadox.Content.Projectiles.Weapons.Melee.Boomerangs;

public class LeecharangProjectile : ModdedFriendlyProjectile
{
    public override int Frames => 1;
    public override int AnimationDuration => 0;
    public override int Width => 24;
    public override int Height => 24;
    public override DamageClass DamageType => DamageClass.MeleeNoSpeed;
    public override int ProjectileLifeSpan => 1200;
    public override bool PassThroughBlocks => false;
    public override int Pierce => 1; //so it can return after having hit an enemy
    public override float RotationHelper => 0;
    public bool Returning = false;
    public override void CustomAI()
    {
        Player player = Main.player[Projectile.owner];
        AITimer1++;
        switch (Returning)
        {
            case false:
            {
                Projectile.rotation += AITimer1 * 0.15f;
                if (AITimer1 >= 1 * 60)
                {
                    Return();
                }
                break;
            }
            case true:
            {
                Projectile.velocity = (player.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * Leecharang.ShootSpeed * 1.25f;
                Projectile.rotation -= AITimer1 * 0.15f;
                if (Projectile.Hitbox.Intersects(player.Hitbox))
                {
                    Projectile.Kill();
                }
                break;
            }
        }
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(ModContent.BuffType<LeecharangBleed>(), Leecharang.DebuffDuration);
        if (!Returning)
        {
            Return();
        }
    }

    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        if (!Returning)
        {
            Return();
        }
        return false;
    }

    private void Return()
    {
        Projectile.tileCollide = false;
        Projectile.penetrate = -1;
        Returning = true;
    }
}