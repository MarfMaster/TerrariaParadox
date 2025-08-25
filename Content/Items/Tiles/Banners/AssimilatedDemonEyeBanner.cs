using MLib.Common.Items;
using Terraria.ModLoader;
using TerrariaParadox.Content.Tiles.Banners;

namespace TerrariaParadox.Content.Items.Tiles.Banners;

public class AssimilatedDemonEyeBanner : ModdedBannerItem
{
    public override int StyleId => (int)FlipsideBanners.StyleId.AssimilatedDemonEye;
    public override int ModBannerTileType => ModContent.TileType<FlipsideBanners>();
}