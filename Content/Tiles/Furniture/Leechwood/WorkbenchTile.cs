using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using TerrariaParadox.Content.Dusts.Tiles.Blocks;

namespace TerrariaParadox.Content.Tiles.Furniture.Leechwood;

public class WorkbenchTile : ModdedWorkbenchTile
{
    public override int OnMineDustType => ModContent.DustType<LeechwoodDust>();
    public override Color MapColor => new Color(37, 37, 50);
}