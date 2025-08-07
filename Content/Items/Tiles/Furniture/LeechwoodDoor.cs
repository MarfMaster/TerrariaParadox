using Terraria.ModLoader;
using TerrariaParadox.Content.Items.Tiles.Blocks;
using TerrariaParadox.Content.Tiles.Furniture;
using TerrariaParadox.Content.Tiles.Furniture.Leechwood;

namespace TerrariaParadox.Content.Items.Tiles.Furniture;
public class LeechwoodDoor : ModdedFurnitureItem
{
    public override int TileType => ModContent.TileType<Content.Tiles.Furniture.Leechwood.DoorClosed>();
    public override bool Craftable => true;
    public override int MaterialType => ModContent.ItemType<Leechwood>();
    public override int MaterialAmount => ModdedDoorClosed.MaterialAmount;
}