using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace TerrariaParadox.Content.Projectiles.Weapons.Melee;

public class TarsalSaberGnat : HomingProjectile
{
    public override int Frames => 2;
    public override int Width => 10;
    public override int Height => 10;
    public override DamageClass DamageType => DamageClass.Melee;
    public override int ProjectileLifeSpan => 1200;
    public override float ProjectileSpeed => 0.5f;
    public override bool PassThroughBlocks => false;
    public override int MaxDetectRadius => 300;
    public override float RotationHelper => -MathHelper.PiOver2;
    public override int Pierce => 3;
    public override void OnSpawn(IEntitySource source)
    {
        Projectile.CritChance = (int)Projectile.ai[0];
        Projectile.ai[0] = 0;
    }
}