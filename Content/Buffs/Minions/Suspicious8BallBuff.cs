using Terraria;
using Terraria.ModLoader;
using TerrariaParadox.Content.Projectiles.Summon.Minions;

namespace TerrariaParadox.Content.Buffs.Minions
{
    public class Suspicious8BallBuff : ModdedMinionBuff
    {
        public override int MinionProjectileType => ModContent.ProjectileType<Suspicious8BallProjectile>();
    }
}