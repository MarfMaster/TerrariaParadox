using MLib.Common.Items;
using Terraria.ModLoader;
using TerrariaParadox.Content.Tiles;
using TerrariaParadox.Content.Tiles.Banners;
using TerrariaParadox.Content.Tiles.Furniture;

namespace TerrariaParadox.Content.Items.Tiles.Banners;

public class FlatwormBanner : ModdedBannerItem
{
    public override int StyleId => (int)FlipsideBanners.StyleId.Flatworm;
    public override int ModBannerTileType => ModContent.TileType<FlipsideBanners>();
}