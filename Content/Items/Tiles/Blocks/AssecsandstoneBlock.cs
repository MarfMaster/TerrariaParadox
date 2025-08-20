using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Items.Tiles.Walls;
using TerrariaParadox.Content.Tiles.Blocks;

namespace TerrariaParadox.Content.Items.Tiles.Blocks;

public class AssecsandstoneBlock : ModdedBlockItem
{
    public override int TileType => ModContent.TileType<AssecsandstoneBlockTile>();

    public override void AddRecipes()
    {
        CreateRecipe().AddIngredient(ModContent.ItemType<AssecsandstoneWall>(), 4).AddTile(TileID.WorkBenches)
            .Register();
    }
}