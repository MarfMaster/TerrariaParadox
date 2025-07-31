using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Debuffs.DoT;
using TerrariaParadox.Content.Dusts.Tiles.Blocks;
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
    public Vector2 InitialProjVelocity;
    public override void CustomAI()
    {
        Player player = Main.player[Projectile.owner];
        
        AITimer1++;

        Projectile.rotation += AITimer1 * 0.15f;
        
        if (AITimer1 % 2 == 0)
        {
            Dust Boomer = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<AssecstoneDust>());
            Boomer.noGravity = true;
        }
        if (!Returning)
        {
            switch (AITimer1)
            {
                case 1:
                {
                    InitialProjVelocity = Projectile.velocity;
                    break;
                }
                case float Slowdown when (Slowdown >= 55 && Slowdown < 85): //make it slow down
                {
                    Projectile.velocity -= InitialProjVelocity / 30f;
                    break;
                }
                case 85:
                {
                    Return(false);
                    break;
                }
            }
        }
        else
        {
            //Projectile.rotation -= AITimer1 * 0.15f;
            switch (AITimer1)
            {
                case float Speedup when (Speedup > 85 && Speedup <= 110): //make it speed up while going back towards the player
                {
                    AITimer2++;
                    Projectile.velocity = ((player.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * Leecharang.ShootSpeed * 1.25f) * (AITimer2 * 0.04f);
                    break;
                }
                case float accelerated when (accelerated > 110):
                {
                    Projectile.velocity = (player.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * Leecharang.ShootSpeed * 1.25f;
                    break;
                }
            }
            if (Projectile.Hitbox.Intersects(player.Hitbox))
            {
                Projectile.Kill();
            }
        }
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(ModContent.BuffType<LeecharangBleed>(), Leecharang.DebuffDuration);
        if (!Returning)
        {
            Return(true);
        }
    }

    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        if (!Returning)
        {
            Return(true);
        }
        return false;
    }
    /// <summary>
    /// Puts the Boomerang into a returning state.
    /// </summary>
    /// <param name="HitCollision">
    /// Whether Return was called by hitting a npc or tile. Set to true to make it return at full speed instantly.
    /// </param>
    private void Return(bool HitCollision)
    {
        if (HitCollision)
        {
            AITimer1 = 111;
        }
        Projectile.tileCollide = false;
        Projectile.penetrate = -1;
        Returning = true;
    }
}