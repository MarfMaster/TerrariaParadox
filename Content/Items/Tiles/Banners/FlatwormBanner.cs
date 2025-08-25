using MLib.Common.Items;
using Terraria.ModLoader;
using TerrariaParadox.Content.Tiles.Banners;

namespace TerrariaParadox.Content.Items.Tiles.Banners;

public class FlatwormBanner : ModdedBannerItem
{
    public override int StyleId => (int)FlipsideBanners.StyleId.Flatworm;
    public override int ModBannerTileType => ModContent.TileType<FlipsideBanners>();
}