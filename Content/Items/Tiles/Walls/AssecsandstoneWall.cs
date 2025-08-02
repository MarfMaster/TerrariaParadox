using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Items.Tiles.Blocks;
using TerrariaParadox.Content.Tiles.Blocks;
using TerrariaParadox.Content.Tiles.Walls;

namespace TerrariaParadox.Content.Items.Tiles.Walls;

public class AssecsandstoneWall : ModdedWallItem
{
    public override int WallType => ModContent.WallType<AssecsandstoneWallTile>();
    public override void AddRecipes()
    {
        CreateRecipe(4).
            AddIngredient(ModContent.ItemType<AssecsandstoneBlock>(), 1).
            AddTile(TileID.WorkBenches).
            AddCondition(Condition.InGraveyard).
            Register();
    }
}