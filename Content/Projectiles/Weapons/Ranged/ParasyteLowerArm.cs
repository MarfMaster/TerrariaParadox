using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Items.Weapons.Ranged;

namespace TerrariaParadox.Content.Projectiles.Weapons.Ranged;

public class ParasyteLowerArm : ModdedFriendlyProjectile
{
    public override int Frames => 1;
    public override int AnimationDuration => 0;
    public override int Width => 20;
    public override int Height => 120;
    public override DamageClass DamageType => DamageClass.Ranged;
    public override int ProjectileLifeSpan => 600;
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
    public override void OnSpawn(IEntitySource source)
    {
        Player player = Main.player[Projectile.owner];
        BulletVelocity = Projectile.velocity;
        Projectile.velocity = Vector2.Zero;
        Main.NewText(AITimer1);
        if (AITimer1 != ProjectileID.Bullet && AITimer1 != ProjectileID.SilverBullet)
        {
            BulletType = (int)AITimer1;
        }
    }
    public Vector2 BulletVelocity;
    public int BulletType = ProjectileID.BulletHighVelocity;
    public bool HasShot = false;
    public int TImer = 0;
    public override void CustomAI()
    {
        TImer++;
        Player player = Main.player[Projectile.owner];
        float swingTime = player.itemAnimationMax * Projectile.MaxUpdates;
        
        if (AITimer2 >= swingTime || player.itemAnimation <= 0)
        {
            Projectile.Kill();
            return;
        }
        if (AITimer2 <= swingTime / 4)
        {                
            AITimer2++;
        }
        else if (AITimer3 <= swingTime / 10)
        {
            AITimer3++;
        }
        else if (AITimer3 >= swingTime / 10 && !HasShot)
        {
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, BulletVelocity, 
                BulletType, Projectile.damage, Projectile.knockBack, Projectile.owner);
            HasShot = true;
        }
        
        player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, player.direction * -1.2f); //setting custom arm frame
        
        Projectile.rotation = ((MathHelper.PiOver4 - 0.4f) - (AITimer2 * 0.05f) + (AITimer3 * 0.2f)) * player.direction;
        Vector2 VelocityToPlayerHand = new Vector2(player.Center.X + (18 * player.direction), player.Center.Y + 10);
        Vector2 offsetFromPlayer = new Vector2(player.direction).RotatedBy(Projectile.rotation * 2f * (TImer / 60f) * player.direction);
        Vector2 Line = player.MountedCenter.DirectionTo(VelocityToPlayerHand) * player.MountedCenter.Distance(player.Center + new Vector2(10, 10));
        Vector2 armorig = Line.RotatedBy(0, player.MountedCenter);
        Dust.NewDust(player.Center + (offsetFromPlayer * 20), 1, 1, DustID.MartianSaucerSpark);
        Projectile.rotation = ((MathHelper.PiOver4 - 0.4f) - (AITimer2 * 0.05f) + (AITimer3 * 0.2f)) * player.direction;
        Projectile.Center = VelocityToPlayerHand;
        Projectile.direction = player.direction;
        Projectile.spriteDirection = Projectile.direction;
    }


    public override bool? CanDamage()
    {
        return false;
    }
    public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers,
        List<int> overWiresUI)
    {
        overPlayers.Add(index);
    }
}