using MLib.Common.Items;
using Terraria.ModLoader;
using TerrariaParadox.Content.Tiles.Furniture.Trophies;

namespace TerrariaParadox.Content.Items.Tiles.Furniture.Trophies;

public class NyxarTrophy : ModdedTrophyItem
{
    public override int TileType => ModContent.TileType<NyxarTrophyTile>();
}