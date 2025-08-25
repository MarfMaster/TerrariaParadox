using Microsoft.Xna.Framework;
using MLib.Common.Items;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Dusts.Tiles.Misc;
using TerrariaParadox.Content.Items.Tiles.Blocks;
using TerrariaParadox.Content.Tiles.Furniture;

namespace TerrariaParadox.Content.Items.Tiles.Furniture;

public class FlippedTorch : ModdedTorchItem
{
    public override int TorchTileType => ModContent.TileType<FlippedTorchTile>();
    public override Color LightColor => new Color(0.0028f, 0.0198f, 0.0135f) * 0.5f;
    public override int TorchCraftAmount => 5;
    public override int MaterialItemType => ModContent.ItemType<AssecstoneBlock>();
    public override int SparkleDustType => ModContent.DustType<BioluminescentBulbDust>();
    public override bool CanFunctionInWater => false;
    public override bool CanFunctionInLava => false;
    public override int Rarity => ItemRarityID.White;
    public override int Value => 15;
}