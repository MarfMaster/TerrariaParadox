using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using TerrariaParadox.Content.Dusts.Tiles.Blocks;
using TerrariaParadox.Content.Items.Tiles.Furniture;

namespace TerrariaParadox.Content.Tiles.Furniture.Leechwood;

public class ChairTile : ModdedChairTile
{
    public override int OnMineDustType => ModContent.DustType<LeechwoodDust>();
    public override int ItemType => ModContent.ItemType<LeechwoodChair>();
    public override Color MapColor => new(37, 37, 50);
}