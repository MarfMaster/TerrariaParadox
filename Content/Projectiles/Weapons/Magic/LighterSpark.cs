using MLib.Common.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Items.Weapons.Magic;

namespace TerrariaParadox.Content.Projectiles.Weapons.Magic;

public class LighterSpark : ModdedFriendlyProjectile
{
    public override int Frames => 1;
    public override int AnimationDuration => 0;
    public override int Width => 24;
    public override int Height => 24;
    public override DamageClass DamageType => DamageClass.Magic;
    public override int LifeSpan => 60;
    public override bool PassThroughBlocks => false;
    public override int Pierce => 2;
    public override float RotationHelper => 0;

    public override void CustomSetDefaults()
    {
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = -1;
    }

    public override void CustomAI()
    {
        AITimer1++;
        if (AITimer1 % 2 == 0)
        {
            var Spark1 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch);
            Spark1.noGravity = true;
            var Spark2 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch);
            Spark2.velocity *= 2f;
            Spark2.noGravity = true;
            var Spark3 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch);
            Spark3.scale *= 1.6f;
            Spark3.noGravity = true;
        }
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(BuffID.OnFire, Lighter.DebuffDuration);
    }
}