using MLib.Common.Items;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Projectiles.Tiles.Sandball;
using TerrariaParadox.Content.Tiles.Blocks;

namespace TerrariaParadox.Content.Items.Tiles.Blocks;

public class AssecsandBlock : ModdedBlockItem
{
    public override int TileType => ModContent.TileType<AssecsandBlockTile>();

    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();
        ItemID.Sets.SandgunAmmoProjectileData[Type] =
            new ItemID.Sets.SandgunAmmoInfo(ModContent.ProjectileType<AssecsandballGunProj>(), 10);
    }

    public override void SetDefaults()
    {
        base.SetDefaults();
        Item.ammo = AmmoID.Sand;
        Item.notAmmo = true;
    }
}