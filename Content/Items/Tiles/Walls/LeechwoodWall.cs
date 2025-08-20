using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Items.Tiles.Blocks;
using TerrariaParadox.Content.Tiles.Walls;

namespace TerrariaParadox.Content.Items.Tiles.Walls;

public class LeechwoodWall : ModdedWallItem
{
    public override int WallType => ModContent.WallType<LeechwoodWallTile>();

    public override void AddRecipes()
    {
        CreateRecipe(4).AddIngredient(ModContent.ItemType<Leechwood>()).AddTile(TileID.WorkBenches).Register();
    }
}