using MLib.Common.Projectiles;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Items.Tiles.Blocks;
using TerrariaParadox.Content.Tiles.Blocks;

namespace TerrariaParadox.Content.Projectiles.Tiles.Sandball;

public class AssecsandballProjectile : ModdedSandballProjectile
{
    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();
        ProjectileID.Sets.FallingBlockTileItem[Type] =
            new ProjectileID.Sets.FallingBlockTileItemInfo(ModContent.TileType<AssecsandBlockTile>(),
                ModContent.ItemType<AssecsandBlock>());
    }

    public override void SetDefaults()
    {
        // The falling projectile when compared to the sandgun projectile is hostile.
        Projectile.CloneDefaults(ProjectileID.EbonsandBallFalling);
    }
}

public class AssecsandballGunProj : ModdedSandballProjectile
{
    public override string Texture => "TerrariaParadox/Content/Projectiles/Tiles/Sandball/AssecsandballProjectile";

    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();
        ProjectileID.Sets.FallingBlockTileItem[Type] =
            new ProjectileID.Sets.FallingBlockTileItemInfo(ModContent.TileType<AssecsandBlockTile>());
    }

    public override void SetDefaults()
    {
        // The sandgun projectile when compared to the falling projectile has a ranged damage type, isn't hostile, and has extraupdates = 1.
        // Note that EbonsandBallGun has infinite penetration, unlike SandBallGun
        Projectile.CloneDefaults(ProjectileID.EbonsandBallGun);
        AIType = ProjectileID.EbonsandBallGun; // This is needed for some logic in the ProjAIStyleID.FallingTile code.
    }
}