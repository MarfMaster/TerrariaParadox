using Microsoft.Xna.Framework;
using MLib.Common.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Dusts.Tiles.Blocks;

namespace TerrariaParadox.Content.Tiles.Walls;

public class AssecstoneWallTileUnsafe : ModdedWallTile
{
    public override bool PlayerPlaced => false;
    public override int OnMineDustType => ModContent.DustType<AssecstoneDust>();
    public override ushort VanillaFallbackTile => WallID.EbonstoneUnsafe;
    public override Color MapColor => new(31, 36, 48);

    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();
        WallID.Sets.Conversion.Stone[Type] = true;
        Main.wallBlend[Type] = WallID.Stone;
        ParadoxSystem.MinHammer[Type] = 70;
        WallID.Sets.CannotBeReplacedByWallSpread[Type] = true;
    }
}