using Terraria.ModLoader;
using TerrariaParadox.Content.Projectiles.Weapons.Summon.Minions;

namespace TerrariaParadox.Content.Buffs.Minions;

public class Suspicious8BallBuff : ModdedMinionBuff
{
    public override int MinionProjectileType => ModContent.ProjectileType<Suspicious8BallProjectile>();
}