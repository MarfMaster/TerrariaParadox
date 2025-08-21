using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Tiles.Plants;

namespace TerrariaParadox.Content.Tiles.Blocks;

public class FlippedGrassBlock : ModdedBlockTile
{
    public override bool SolidBlock => true;
    public override bool MergesWithDirt => true;
    public override int OnMineDustType => DustID.Dirt;
    public override ushort VanillaFallbackTileAndMerge => TileID.Dirt;
    public override SoundStyle TileMineSound => SoundID.Dig;
    public override Color MapColor => new(69, 79, 101);
    public override int WaterfallStyleID => WaterStyleID.Corrupt;
    public override bool MergesWithItself => false;
    public override bool NameShowsOnMapHover => false;

    public override void CustomSetStaticDefaults()
    {
        Main.tileBrick[Type] = true;
        TileID.Sets.CanBeDugByShovel[Type] = true;
        TileID.Sets.CanBeClearedDuringGeneration[Type] = true;
        TileID.Sets.Grass[Type] = true;
        TileID.Sets.Conversion.Grass[Type] = true;
        TileID.Sets.NeedsGrassFraming[Type] = true;
        TileID.Sets.NeedsGrassFramingDirt[Type] = 1;
        TileID.Sets.ChecksForMerge[Type] = true;
        Main.tileMerge[Type] = Main.tileMerge[TileID.Grass];
        Main.tileMerge[Type][TileID.Dirt] = true;
        Main.tileMerge[TileID.Dirt][Type] = true;
        Main.tileMerge[Type][TileID.Mud] = true;
        Main.tileMerge[TileID.Mud][Type] = true;
        for (var i = 0; i < TileLoader.TileCount; i++)
            if (TileID.Sets.Grass[i] || TileID.Sets.GrassSpecial[i])
            {
                Main.tileMerge[Type][i] = true;
                Main.tileMerge[i][Type] = true;
            }

        ParadoxSystem.TileTransformsOnKill[Type] = true;
        TileID.Sets.ForcedDirtMerging[Type] = true;
        TileID.Sets.Conversion.MergesWithDirtInASpecialWay[Type] = true;
    }

    public override bool CanReplace(int i, int j, int tileTypeBeingPlaced)
    {
        if (tileTypeBeingPlaced == TileID.Dirt) return false;
        return base.CanReplace(i, j, tileTypeBeingPlaced);
    }

    public override void RandomUpdate(int i, int j)
    {
        var above = Framing.GetTileSafely(i, j - 1);
        if (!above.HasTile && Main.tile[i, j].BlockType == BlockType.Solid)
        {
            if (Main.rand.NextBool(FlippedThorns.GrowChance))
            {
                above.ResetToType((ushort)ModContent.TileType<FlippedThorns>());
            } 
            else if (Main.rand.NextBool(ParasiticMushroomTile.ChanceToGrow))
            {
                above.ResetToType((ushort)ModContent.TileType<ParasiticMushroomTile>());
            }
            else if (Main.rand.NextBool(AssimilatedGrassTile.GrowChance))
            {
                above.ResetToType((ushort)ModContent.TileType<AssimilatedGrassTile>());
            }
            else if (Main.rand.NextBool(FlippedGrassPlants.GrowChance))
            {
                above.ResetToType((ushort)ModContent.TileType<FlippedGrassPlants>());
                above.TileFrameX = (short)(Main.rand.Next(0, FlippedGrassPlants.Frames - 1) * 18);
            }

            WorldGen.TileFrame(i, j - 1);
        }
        var below = Framing.GetTileSafely(i, j + 1);
        if (!below.HasTile && Main.tile[i, j].BlockType == BlockType.Solid)
        {
            if (Main.rand.NextBool(FlippedVine.GrowChance))
            {
                below.ResetToType((ushort)ModContent.TileType<FlippedVine>());
            } 
            WorldGen.TileFrame(i, j + 1);
        }
    }

    public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
    {
        if (fail && !effectOnly) Framing.GetTileSafely(i, j).TileType = TileID.Dirt;
    }
}