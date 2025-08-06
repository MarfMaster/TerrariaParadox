using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Debuffs;

namespace TerrariaParadox;

public partial class ParadoxProjectile : GlobalProjectile
{
    public override void OnSpawn(Projectile projectile, IEntitySource source)
    {
        if (source is EntitySource_Parent parent && parent.Entity is NPC npc && npc.HasBuff(ModContent.BuffType<Stickled>()))
        {
            projectile.damage = (int)(projectile.damage * (1f - (Stickled.DamageReduction / 100f)));
        }
    }

    public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
    {
    }

    public override void ModifyHitNPC(Projectile projectile, NPC target, ref NPC.HitModifiers modifiers)
    {
    }
}