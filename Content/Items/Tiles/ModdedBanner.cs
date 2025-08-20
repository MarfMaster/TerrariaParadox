using Terraria.ModLoader;
using TerrariaParadox.Content.Tiles;

namespace TerrariaParadox.Content.Items.Tiles;

public abstract class ModdedBannerItem : ModItem
{
    public override string LocalizationCategory => "Items.Tiles.Banners";
    public abstract int BannerStyle { get; }

    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(ModContent.TileType<ModdedBannerTile>(), BannerStyle);
        Item.width = 10;
        Item.height = 24;
    }
}