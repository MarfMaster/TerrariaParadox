using Microsoft.Xna.Framework;
using MLib.Common.Tiles;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Dusts.Tiles.Blocks;

namespace TerrariaParadox.Content.Tiles.Walls;

public class LeechwoodWallTile : ModdedWallTile
{
    public override bool PlayerPlaced => true;
    public override int OnMineDustType => ModContent.DustType<LeechwoodDust>();
    public override ushort VanillaFallbackTile => WallID.Ebonwood;
    public override Color MapColor => new(55, 62, 71);
}