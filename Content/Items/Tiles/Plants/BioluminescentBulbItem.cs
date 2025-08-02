using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Tiles.Misc;
using TerrariaParadox.Content.Tiles.Plants;

namespace TerrariaParadox.Content.Items.Tiles.Plants;

public class BioluminescentBulbItem : ModdedBlockItem
{
    public override int TileType => ModContent.TileType<BioluminescentBulb>();
}