using MLib.Common.Items;
using Terraria.ModLoader;
using TerrariaParadox.Content.Tiles.Banners;

namespace TerrariaParadox.Content.Items.Tiles.Banners;

public class SwarmBanner : ModdedBannerItem
{
    public override int StyleId => (int)FlipsideBanners.StyleId.Swarm;
    public override int ModBannerTileType => ModContent.TileType<FlipsideBanners>();
}