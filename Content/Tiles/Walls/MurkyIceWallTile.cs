using Microsoft.Xna.Framework;
using MLib.Common.Tiles;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Dusts.Tiles.Blocks;

namespace TerrariaParadox.Content.Tiles.Walls;

public class MurkyIceWallTile : ModdedWallTile
{
    public override bool PlayerPlaced => true;
    public override int OnMineDustType => ModContent.DustType<MurkyIceDust>();
    public override ushort VanillaFallbackTile => WallID.IceEcho;
    public override Color MapColor => new(41, 41, 77);

    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();
        WallID.Sets.Conversion.Ice[Type] = true;
    }
}