using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace TerrariaParadox.Content.Tiles;

public abstract class ModdedBlockTile : ModTile
{
    public abstract bool SolidBlock { get; }
    public abstract bool MergesWithDirt { get; }
    public abstract int OnMineDustType { get; }

    /// <summary>
    ///     Determines which vanilla tile to revert to if the mod is unloaded. Defaults to 0 for Dirt.
    ///     Also makes this block merge correctly with that tile.
    /// </summary>
    public abstract ushort VanillaFallbackTileAndMerge { get; }

    public abstract SoundStyle TileMineSound { get; }
    public abstract Color MapColor { get; }

    /// <summary>
    ///     Changes the style of any waterfall produced by this block. Defaults to 0 for normal water.
    /// </summary>
    public abstract int WaterfallStyleID { get; }

    public abstract bool MergesWithItself { get; }
    public abstract bool NameShowsOnMapHover { get; }

    public virtual void CustomSetStaticDefaults()
    {
    }

    public override void SetStaticDefaults()
    {
        Main.tileSolid[Type] = SolidBlock;
        Main.tileMergeDirt[Type] = MergesWithDirt;
        Main.tileBlockLight[Type] = SolidBlock;

        DustType = OnMineDustType;
        VanillaFallbackOnModDeletion = VanillaFallbackTileAndMerge;
        HitSound = TileMineSound;

        if (NameShowsOnMapHover)
        {
            var name = CreateMapEntryName();
            AddMapEntry(MapColor, name);
        }
        else
        {
            AddMapEntry(MapColor);
        }

        if (MergesWithItself)
        {
            Main.tileMerge[Type] = Main.tileMerge[VanillaFallbackTileAndMerge];
            Main.tileMerge[Type][VanillaFallbackTileAndMerge] = true;
            Main.tileMerge[VanillaFallbackTileAndMerge][Type] = true;
        }

        CustomSetStaticDefaults();
    }

    public override void ChangeWaterfallStyle(ref int style)
    {
        style = WaterfallStyleID;
    }
}