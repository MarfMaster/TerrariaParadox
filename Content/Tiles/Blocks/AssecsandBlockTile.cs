using Microsoft.Xna.Framework;
using MLib.Common.Tiles;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Dusts.Tiles.Blocks;
using TerrariaParadox.Content.Projectiles.Tiles.Sandball;

namespace TerrariaParadox.Content.Tiles.Blocks;

public class AssecsandBlockTile : ModdedSandBlockTile
{
    public override bool SolidBlock => true;
    public override bool MergesWithDirt => true;
    public override int OnMineDustType => ModContent.DustType<AssecsandDust>();
    public override ushort VanillaFallbackTileAndMerge => TileID.Sand;
    public override SoundStyle TileMineSound => SoundID.Dig;
    public override Color MapColor => new(12, 12, 18);
    public override int SandballProjectileType => ModContent.ProjectileType<AssecsandballProjectile>();
    public override bool MergesWithItself => true;
    public override bool NameShowsOnMapHover => false;

    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();
        MineResist *= 2f;
        Main.tileMerge[ModContent.TileType<AssecsandstoneBlockTile>()][Type] = true;
        Main.tileMerge[Type][ModContent.TileType<AssecsandstoneBlockTile>()] = true;
        Main.tileMerge[ModContent.TileType<HardenedAssecsandBlockTile>()][Type] = true;
        Main.tileMerge[Type][ModContent.TileType<HardenedAssecsandBlockTile>()] = true;
    }
}