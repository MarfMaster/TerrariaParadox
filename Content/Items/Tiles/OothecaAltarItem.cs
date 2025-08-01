using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Tiles;
using TerrariaParadox.Content.Tiles.Plants;

namespace TerrariaParadox.Content.Items.Tiles;

public class OothecaAltarItem : ModdedBlockItem
{
    public override string Texture => "TerrariaParadox/Content/Items/Tiles/Blocks/AssecstoneBlock";
    public override int TileType => ModContent.TileType<OothecaAltar>();
}