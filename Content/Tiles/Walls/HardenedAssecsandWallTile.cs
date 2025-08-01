using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Dusts.Tiles.Blocks;

namespace TerrariaParadox.Content.Tiles.Walls;

public class HardenedAssecsandWallTile : ModdedWallTile
{
    public override int OnMineDustType => ModContent.DustType<AssecstoneDust>();
    public override ushort VanillaFallbackTile => WallID.CorruptHardenedSand;
    public override Color MapColor => new Color(55, 62, 71);
}