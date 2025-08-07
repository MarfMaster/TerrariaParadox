using Terraria.ModLoader;
using TerrariaParadox.Content.Items.Tiles.Blocks;
using TerrariaParadox.Content.Tiles.Furniture;
using TerrariaParadox.Content.Tiles.Furniture.Leechwood;

namespace TerrariaParadox.Content.Items.Tiles.Furniture;
[Autoload(false)]
public class LeechwoodDoor : ModdedFurnitureItem
{
    public override int TileType { get; }
    public override bool Craftable { get; }
    public override int MaterialType { get; }
    public override int MaterialAmount { get; }
}