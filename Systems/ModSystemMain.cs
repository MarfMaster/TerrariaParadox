using System;
using Terraria.ID;
using Terraria.ModLoader;

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