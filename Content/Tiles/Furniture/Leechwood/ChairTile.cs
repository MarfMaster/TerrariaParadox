using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using TerrariaParadox.Content.Dusts.Tiles.Blocks;
using TerrariaParadox.Content.Items.Tiles.Furniture;

namespace TerrariaParadox.Content.Tiles.Furniture.Leechwood;

public class ChairTile : ModdedChairTile
{
    public override int OnMineDustType => ModContent.DustType<LeechwoodDust>();
    public override int ItemType => ModContent.ItemType<LeechwoodChair>();
}