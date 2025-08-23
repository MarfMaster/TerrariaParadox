using MLib.Common.Items;
using Terraria.ModLoader;
using TerrariaParadox.Content.Tiles;
using TerrariaParadox.Content.Tiles.Banners;
using TerrariaParadox.Content.Tiles.Furniture;

namespace TerrariaParadox.Content.Items.Tiles.Banners;

public class WalkingHiveBanner : ModdedBannerItem
{
    public override int StyleId => (int)FlipsideBanners.StyleId.WalkingHive;
    public override int ModBannerTileType => ModContent.TileType<FlipsideBanners>();
}