using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaParadox.Content.Projectiles.Tiles;

public abstract class ModdedSandballProjectile : ModProjectile
{
    public virtual void CustomSetStaticDefaults(){}
    public override void SetStaticDefaults() 
    {
        ProjectileID.Sets.FallingBlockDoesNotFallThroughPlatforms[Type] = true;
        ProjectileID.Sets.ForcePlateDetection[Type] = true;
        CustomSetStaticDefaults();
    }
}