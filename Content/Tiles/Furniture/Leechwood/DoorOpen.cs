using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using TerrariaParadox.Content.Dusts.Tiles.Blocks;
using TerrariaParadox.Content.Items.Tiles.Furniture;

namespace TerrariaParadox.Content.Tiles.Furniture.Leechwood;

public class DoorOpen : ModdedDoorOpen
{
    public override int ClosedDoorType => ModContent.TileType<DoorClosed>();
    public override int DoorItemType => ModContent.ItemType<LeechwoodDoor>();
    public override int OnMineDustType => ModContent.DustType<LeechwoodDust>();
    public override Color MapColor => new(37, 37, 50);
}