using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Tiles.Blocks;
using TerrariaParadox.Content.Tiles.Misc;
using TerrariaParadox.Content.Tiles.Plants;
using TerrariaParadox.Content.Tiles.Walls;

namespace TerrariaParadox;

public partial class ParadoxSystem : ModSystem
{
    public override void ResizeArrays()
    {
        Array.Resize(ref _minHammer, WallLoader.WallCount);
        FlippedBlockSpawnChance = NPCID.Sets.Factory.CreateFloatSet(0f);
        TileTransformsOnKill = TileID.Sets.Factory.CreateBoolSet(false);
    }
}