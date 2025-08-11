using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaParadox.Content.Projectiles.Weapons;
/// <summary>
/// A basic projectile that homes in on NPCs. This projectile gets updated twice as often as normal projectiles, to allow for more accurate homing, so keep that in mind. 
/// </summary>
public abstract class ModdedFriendlyHomingProjectile : ModdedFriendlyProjectile
{
    /// <summary>
    /// The speed the projectile homes in at. Try to set this to a value that'll keep the speed the same as before it starts homing into an enemy, should be fine at around half the Item's shootSpeed.
    /// </summary>
    public abstract float ProjectileSpeed { get; }
    public abstract int MaxDetectRadius { get; }
    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();
        ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
    }

    public override void AI()
    {
        Player player = Main.player[Projectile.owner];
        NPC closestNPC = FindClosestNPC(MaxDetectRadius);

        if (closestNPC != null)
        {
            Projectile.velocity = (closestNPC.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * ProjectileSpeed;
        }
        base.AI();
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