using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaParadox.Content.Projectiles.Weapons;
/// <summary>
/// A basic homing projectile. This projectile gets updated twice as often as normal projectiles, to allow for more accurate homing, so keep that in mind. 
/// </summary>
public abstract class HomingProjectile : ModProjectile
{
    public abstract int Frames { get; }
    public abstract int Width { get; }
    public abstract int Height { get; }
    public abstract DamageClass DamageType { get; }
    /// <summary>
    /// This determines how long the projectile lasts. Terraria runs at 60 ticks per second, so 60 would normally be 1 second, but this projectile updates twice as often, so 120 is 1 second. 
    /// </summary>
    public abstract int ProjectileLifeSpan { get; }
    public abstract float ProjectileSpeed { get; }
    public abstract bool PassThroughBlocks { get; }
    /// <summary>
    /// Determines how often the projectile pierces. Default is 0 for no pierce. For infinite pierce, set this to -2.
    /// </summary>
    public abstract int Pierce { get; }
    public abstract int MaxDetectRadius { get; }
    public abstract float RotationHelper { get; }
    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
        Main.projFrames[Projectile.type] = Frames;
    }
    public virtual void CustomSetDefaults() {}
    public override void SetDefaults()
    {
        Projectile.width = Width;
        Projectile.height = Height;
        Projectile.DamageType = DamageType;
        Projectile.timeLeft = ProjectileLifeSpan;
        Projectile.extraUpdates = 1;
        Projectile.friendly = true;
        Projectile.tileCollide = !PassThroughBlocks;
        Projectile.penetrate = 1 + Pierce;
        CustomSetDefaults();
    }

    public override void AI()
    {
        Player player = Main.player[Projectile.owner];
        NPC closestNPC = FindClosestNPC(MaxDetectRadius);

        if (closestNPC != null)
        {
            Projectile.velocity = (closestNPC.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * ProjectileSpeed;
        }

        Projectile.rotation = Projectile.velocity.ToRotation() + RotationHelper;

        int frameSpeed = 10;

        Projectile.frameCounter++;

        if (Projectile.frameCounter >= frameSpeed)
        {
            Projectile.frameCounter = 0;
            Projectile.frame++;

            if (Projectile.frame >= Main.projFrames[Projectile.type])
            {
                Projectile.frame = 0;
            }
        }
    }
    
    public NPC FindClosestNPC(float maxDetectDistance)
    {
        NPC closestNPC = null;

        float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

        foreach (NPC target in Main.ActiveNPCs)
        {
            if (!target.CanBeChasedBy())
                continue;

            float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Projectile.Center);
            
            if (sqrDistanceToTarget < sqrMaxDetectDistance)
            {
                sqrMaxDetectDistance = sqrDistanceToTarget; // Set the closest found distance to this NPC
                closestNPC = target;
            }
        }
        return closestNPC;
    }
}