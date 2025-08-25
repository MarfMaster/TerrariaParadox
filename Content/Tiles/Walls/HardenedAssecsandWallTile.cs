using Microsoft.Xna.Framework;
using MLib.Common.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Dusts.Tiles.Blocks;

namespace TerrariaParadox.Content.Tiles.Walls;

public class HardenedAssecsandWallTile : ModdedWallTile
{
    public override bool PlayerPlaced => true;
    public override int OnMineDustType => ModContent.DustType<HardenedAssecsandDust>();
    public override ushort VanillaFallbackTile => WallID.CorruptHardenedSandEcho;
    public override Color MapColor => new(55, 62, 71);

    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();
        WallID.Sets.Conversion.Grass[Type] = true;
        Main.wallBlend[Type] = WallID.GrassUnsafe;
        WallID.Sets.CannotBeReplacedByWallSpread[Type] = true;
    }
}