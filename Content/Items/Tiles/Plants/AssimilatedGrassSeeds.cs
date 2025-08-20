using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Tiles.Plants;

namespace TerrariaParadox.Content.Items.Tiles.Plants;

public class AssimilatedGrassSeeds : ModItem
{
    public override string LocalizationCategory => "Items.Tiles.Plants";

    public override void SetStaticDefaults()
    {
        ItemID.Sets.DisableAutomaticPlaceableDrop[Type] = true;
        Item.ResearchUnlockCount = 25;
    }

    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(ModContent.TileType<AssimilatedGrassTile>());
        Item.value = 20;
    }
}