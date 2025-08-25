using MLib.Common.Items;
using MLib.Common.Tiles.Furniture;
using Terraria.ModLoader;
using TerrariaParadox.Content.Items.Tiles.Blocks;
using TerrariaParadox.Content.Tiles.Furniture.Leechwood;

namespace TerrariaParadox.Content.Items.Tiles.Furniture;

public class LeechwoodChair : ModdedFurnitureItem
{
    public override int TileType => ModContent.TileType<ChairTile>();
    public override bool Craftable => true;
    public override int MaterialType => ModContent.ItemType<Leechwood>();
    public override int MaterialAmount => ModdedChairTile.MaterialAmount;
}