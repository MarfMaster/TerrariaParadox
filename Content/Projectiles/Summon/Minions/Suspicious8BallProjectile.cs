using Terraria.ModLoader;
using TerrariaParadox.Content.Buffs.Minions;
using TerrariaParadox.Content.Items.Weapons.Summon.Minions;

namespace TerrariaParadox.Content.Projectiles.Summon.Minions;

public class Suspicious8BallProjectile : ModdedMinionProjectile
{
    public override int ProjectileFrames => 1;
    public override int Width => 16;
    public override int Height => 16;
    public override float MinionSlotsRequired => Suspicious8Ball.MinionSlots;
    public override int ProjectileBuffType => ModContent.BuffType<Suspicious8BallBuff>();
    public override bool ContactDamage => true;
    public override DamageClass ProjectileDamageType => DamageClass.Summon;
    public override float SummonTagDamagePercentage => Suspicious8Ball.SummonTagMult;
    public override int ShotProjectileType => 0;
    public override void CustomSetDefaults()
    {
        Projectile.usesLocalNPCImmunity  = true;
        Projectile.localNPCHitCooldown = 10;
    }
}