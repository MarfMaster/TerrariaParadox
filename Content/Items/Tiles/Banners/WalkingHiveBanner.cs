using MLib.Common.Items;
using Terraria.ModLoader;
using TerrariaParadox.Content.Tiles.Banners;

namespace TerrariaParadox.Content.Items.Tiles.Banners;

public class WalkingHiveBanner : ModdedBannerItem
{
    public override int StyleId => (int)FlipsideBanners.StyleId.WalkingHive;
    public override int ModBannerTileType => ModContent.TileType<FlipsideBanners>();
}