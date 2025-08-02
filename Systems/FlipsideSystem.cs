using System;
using System.Buffers;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using TerrariaParadox.Content.Tiles.Blocks;

namespace TerrariaParadox.Systems;

public class FlipsideSystem : ModSystem
{
    public int InfestedBlockCount;

    public override void TileCountsAvailable(ReadOnlySpan<int> tileCounts)
    {
        InfestedBlockCount = 0;
        foreach (var i in TerrariaParadox.InfestedBlocks)
        {
            InfestedBlockCount += tileCounts[i];
        }
    }
    
    
}