using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Items.Weapons.Ranged;

namespace TerrariaParadox.Content.Projectiles.Weapons.Ranged;

public class ParasyteArmHeld : ModdedFriendlyProjectile
{
    public override int Frames => 1;
    public override int AnimationDuration => 0;
    public override int Width => 48;
    public override int Height => 76;
    public override DamageClass DamageType => DamageClass.Ranged;
    public override int ProjectileLifeSpan => 60;
    public override bool PassThroughBlocks => true;
    public override int Pierce => -2;
    public override float RotationHelper => 0;
    public override void CustomSetStaticDefaults()
    {
        ProjectileID.Sets.HeldProjDoesNotUsePlayerGfxOffY[Type] = true;
    }

    public override void CustomSetDefaults()
    {
        Projectile.netImportant = true; // This ensures that the projectile is synced when other players join the world.
        Projectile.aiStyle = -1;
        Projectile.DamageType = DamageClass.Ranged;
    }

    public override void CustomAI()
    {
        Player player = Main.player[Projectile.owner];
        DecideToKillArm(player);
        
        if (player.HeldItem.type == ModContent.ItemType<Parasyte>()) //keeping the projectile alive while it's item is being held
        {
            Projectile.timeLeft = 2;
        }
        
        player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, player.direction * -1.2f); //setting custom arm frame
        
        Vector2 VelocityToPlayerHand = new Vector2(player.Center.X + (14 * player.direction), player.Center.Y - 16);
        Projectile.rotation = (MathHelper.PiOver4 - 0.4f) * player.direction;
        Projectile.Center = VelocityToPlayerHand;
        Projectile.direction = player.direction;
        Projectile.spriteDirection = Projectile.direction;
    }
    public virtual void DecideToKillArm(Player player)
    {
        if (player.ownedProjectileCounts[ModContent.ProjectileType<ParasyteUpperArm>()] > 0 || player.dead)
        {
            Projectile.Kill();
        }
    }

    public override bool? CanDamage()
    {
        return false;
    }
    public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
    {
        overPlayers.Add(index);
    }
}