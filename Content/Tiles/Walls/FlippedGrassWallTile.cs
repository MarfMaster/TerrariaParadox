using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Dusts.Tiles.Blocks;

namespace TerrariaParadox.Content.Tiles.Walls;

public class FlippedGrassWallTile : ModdedWallTile
{
    public override bool PlayerPlaced => true;
    public override int OnMineDustType => ModContent.DustType<AssecstoneDust>();
    public override ushort VanillaFallbackTile => WallID.CorruptGrassEcho;
    public override Color MapColor => new(44, 58, 57);

    public override void CustomSetStaticDefaults()
    {
        WallID.Sets.Conversion.Grass[Type] = true;
    }
}