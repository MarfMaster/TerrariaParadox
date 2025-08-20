using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Items.Tiles.Blocks;
using TerrariaParadox.Content.Tiles.Walls;

namespace TerrariaParadox.Content.Items.Tiles.Walls;

public class AssecstoneWall : ModdedWallItem
{
    public override int WallType => ModContent.WallType<AssecstoneWallTile>();

    public override void AddRecipes()
    {
        CreateRecipe(4).AddIngredient(ModContent.ItemType<AssecstoneBlock>()).AddTile(TileID.WorkBenches)
            .AddCondition(Condition.InGraveyard).Register();
    }
}