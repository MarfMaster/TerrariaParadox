using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace TerrariaParadox.Content.Projectiles.Weapons.Melee;

public class TarsalSaberGnat : ModdedFriendlyHomingProjectile
{
    public override int Frames => 2;
    public override int AnimationDuration => 10;
    public override int Width => 10;
    public override int Height => 10;
    public override DamageClass DamageType => DamageClass.Melee;
    public override int LifeSpan => 1200;
    public override float ProjectileSpeed => 5f;
    public override bool PassThroughBlocks => false;
    public override int MaxDetectRadius => 300;
    public override float RotationHelper => -MathHelper.PiOver2;
    public override int Pierce => 5;

    public override void OnSpawn(IEntitySource source)
    {
        Projectile.CritChance = (int)Projectile.ai[0];
        Projectile.ai[0] = 0;
    }

    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        Projectile.velocity = -oldVelocity;
        return false;
    }
}