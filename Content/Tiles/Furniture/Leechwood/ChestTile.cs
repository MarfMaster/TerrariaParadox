using Microsoft.Xna.Framework;
using MLib.Common.Tiles.Furniture;
using Terraria.ModLoader;
using TerrariaParadox.Content.Dusts.Tiles.Blocks;
using TerrariaParadox.Content.Items.Tiles.Furniture;

namespace TerrariaParadox.Content.Tiles.Furniture.Leechwood;

public class ChestTile : ModdedChestTile
{
    public override int OnMineDustType => ModContent.DustType<LeechwoodDust>();
    public override int ChestItemType => ModContent.ItemType<LeechwoodChest>();
    public override Color MapColor => new(37, 37, 50);
    public override bool CanBeLocked => false;
    public override int KeyItemType { get; }

    public override bool CanLockUnlockedChest()
    {
        return base.CanLockUnlockedChest();
    }

    public override bool CanUnlockLockedChest()
    {
        return base.CanUnlockLockedChest();
    }
}