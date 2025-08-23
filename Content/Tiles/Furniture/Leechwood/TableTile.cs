using Microsoft.Xna.Framework;
using MLib.Common.Tiles.Furniture;
using Terraria.ModLoader;
using TerrariaParadox.Content.Dusts.Tiles.Blocks;

namespace TerrariaParadox.Content.Tiles.Furniture.Leechwood;

public class TableTile : ModdedTableTile
{
    public override int OnMineDustType => ModContent.DustType<LeechwoodDust>();
    public override Color MapColor => new(37, 37, 50);
}