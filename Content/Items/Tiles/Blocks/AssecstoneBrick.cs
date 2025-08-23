using MLib.Common.Items;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Items.Tiles.Walls;
using TerrariaParadox.Content.Tiles.Blocks;

namespace TerrariaParadox.Content.Items.Tiles.Blocks;

public class AssecstoneBrick : ModdedBlockItem
{
    public override int TileType => ModContent.TileType<AssecstoneBrickTile>();

    public override void AddRecipes()
    {
        CreateRecipe().AddIngredient(ModContent.ItemType<AssecstoneBrickWall>(), 4).AddTile(TileID.WorkBenches)
            .Register();
    }
}