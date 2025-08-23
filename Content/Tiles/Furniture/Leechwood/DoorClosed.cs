using Microsoft.Xna.Framework;
using MLib.Common.Tiles.Furniture;
using Terraria.ModLoader;
using TerrariaParadox.Content.Dusts.Tiles.Blocks;
using TerrariaParadox.Content.Items.Tiles.Furniture;

namespace TerrariaParadox.Content.Tiles.Furniture.Leechwood;

public class DoorClosed : ModdedDoorClosed
{
    public override int OpenDoorType => ModContent.TileType<DoorOpen>();
    public override int DoorItemType => ModContent.ItemType<LeechwoodDoor>();
    public override int OnMineDustType => ModContent.DustType<LeechwoodDust>();
    public override Color MapColor => new(37, 37, 50);
}