using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria;
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
    public override ushort VanillaFallbackTile => TileID.Ebonsand;
    public override SoundStyle TileMineSound => SoundID.Dig;
    public override Color MapColor => new Color(12, 12, 18);
    public override int WaterfallStyleID => WaterStyleID.Corrupt;
    public override int SandballProjectileType => ModContent.ProjectileType<AssecsandballProjectile>();
    public override void CustomSetStaticDefaults()
    {
        MineResist *= 2f;
    }
}