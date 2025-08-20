using System;
using Terraria.ModLoader;

namespace TerrariaParadox;

public class BiomeBlockCounter : ModSystem
{
    public int InfestedBlockCount;

    public override void TileCountsAvailable(ReadOnlySpan<int> tileCounts)
    {
        InfestedBlockCount = 0;
        foreach (var i in TerrariaParadox.InfestedBlocks) InfestedBlockCount += tileCounts[i];
    }
}