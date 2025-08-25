using Microsoft.Xna.Framework;
using MLib.Common.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Dusts.Tiles.Blocks;

namespace TerrariaParadox.Content.Tiles.Walls;

public class MurkyIceWallTileUnsafe : ModdedWallTile
{
    public override bool PlayerPlaced => false;
    public override int OnMineDustType => ModContent.DustType<MurkyIceDust>();
    public override ushort VanillaFallbackTile => WallID.IceUnsafe;
    public override Color MapColor => new(41, 41, 77);

    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();
        WallID.Sets.Conversion.Ice[Type] = true;
        Main.wallBlend[Type] = WallID.IceUnsafe;
        WallID.Sets.CannotBeReplacedByWallSpread[Type] = true;
    }
}