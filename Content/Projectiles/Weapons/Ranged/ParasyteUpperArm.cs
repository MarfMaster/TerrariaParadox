using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Items.Weapons.Ranged;

namespace TerrariaParadox.Content.Projectiles.Weapons.Ranged;

public class ParasyteUpperArm : ModdedFriendlyProjectile
{
    public override int Frames => 1;
    public override int AnimationDuration => 0;
    public override int Width => 48;
    public override int Height => 12;
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
        if (Main.myPlayer == player.whoAmI)
        {
            //Projectile.NewProjectile(source, Projectile.position, Projectile.velocity, ModContent.ProjectileType<ParasyteLowerArm>(), Projectile.damage, Projectile.knockBack, Projectile.owner, AITimer1);
        }
        Projectile.velocity = Vector2.Zero;
    }

    public override void CustomAI()
    {
        Player player = Main.player[Projectile.owner];
        AITimer2++;
        //DecideToKillArm(player);
        
        if (player.HeldItem.type == ModContent.ItemType<Parasyte>()) //keeping the projectile alive while it's item is being held
        {
            //Projectile.timeLeft = 600;
        }
        
        
        Projectile.rotation = AITimer2 * 0.01f * player.direction;
        player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, Projectile.rotation); //setting custom arm frame
        Vector2 offsetFromPlayer;
        if (player.direction == 1)
        {
            offsetFromPlayer = new Vector2(14, -4 * player.direction).RotatedBy(Projectile.rotation) * player.direction;
        }
        else
        {
            offsetFromPlayer = new Vector2(20, -4 * player.direction).RotatedBy(Projectile.rotation) * player.direction;
        }
        
        Vector2 VelocityToPlayerHand = new Vector2(player.Center.X + (14 * player.direction), player.Center.Y - 16);
        //Projectile.rotation = (MathHelper.PiOver4 - 0.4f) * player.direction;
        Projectile.Center = VelocityToPlayerHand;
        Projectile.direction = player.direction;
        Projectile.spriteDirection = player.direction;
        Vector2 ArmCenter = Projectile.Center + offsetFromPlayer;
        Dust.NewDust(ArmCenter, 1, 1, DustID.MartianSaucerSpark);
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
        overWiresUI.Add(index);
    }
}