using Microsoft.Xna.Framework;
using MLib.Common.Tiles;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Dusts.Tiles.Blocks;
using TerrariaParadox.Content.Tiles.Misc;
using TerrariaParadox.Content.Tiles.Plants;

namespace TerrariaParadox.Content.Tiles.Blocks;

public class AssecstoneBlockTile : ModdedBlockTile
{
    public override bool SolidBlock => true;
    public override bool MergesWithDirt => true;
    public override int OnMineDustType => ModContent.DustType<AssecstoneDust>();
    public override ushort VanillaFallbackTileAndMerge => TileID.Ebonstone;
    public override SoundStyle TileMineSound => SoundID.Tink;
    public override Color MapColor => new(69, 79, 101);
    public override bool MergesWithItself => true;
    public override bool NameShowsOnMapHover => false;

    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();
        MinPick = 65;
        MineResist = 2f;
        TileID.Sets.Conversion.Stone[Type] = true;
        Main.tileStone[Type] = true;
        TileID.Sets.Stone[Type] = true;
        TileID.Sets.GeneralPlacementTiles[Type] = false;
        TileID.Sets.CanBeClearedDuringGeneration[Type] = false;
    }

    public override void RandomUpdate(int i, int j)
    {
        var coordinates = new Point(i, j);
        var above = Framing.GetTileSafely(i, j - 1); //get tiles above
        if (coordinates.Y > Main.worldSurface) //below underground layer
        {
            var frameX = Main.rand.Next(0, 6); //generate a random tileframe for alternate styles

            var above2 = Framing.GetTileSafely(i, j - 2);
            if (!above.HasTile &&
                Main.tile[i, j].BlockType == BlockType.Solid) //check for empty space and whether this block is solid
            {
                if (Main.rand.NextBool(AssecstoneStalagmitesSmallNatural.GrowChance)) //1 in X chance
                {
                    WorldGen.Place1x1(i, j - 1,
                        (ushort)ModContent.TileType<AssecstoneStalagmitesSmallNatural>()); //place tile
                    above.TileFrameX = (short)(frameX * 18); //reframe it so it cna show alternate styles
                }
                else if (Main.rand.NextBool(AssecstoneStalagmitesNatural.GrowChance) &&
                         !above2.HasTile) //1 in X chance and whether there's enough space
                {
                    WorldGen.Place1x2(i, j - 1, (ushort)ModContent.TileType<AssecstoneStalagmitesNatural>(), 0);
                    above.TileFrameX = (short)(frameX * 18); //need to reframe both tiles to the same frame
                    above2.TileFrameX = (short)(frameX * 18);
                }

                WorldGen.TileFrame(i, j - 1);
                return;
            }

            //everything for hanging tiles it the same but adjusted for it hanging below instead of being grounded on top of this tile
            var below = Framing.GetTileSafely(i, j + 1);
            var below2 = Framing.GetTileSafely(i, j + 2);
            if (!below.HasTile && Main.tile[i, j].BlockType == BlockType.Solid)
            {
                if (Main.rand.NextBool(AssecstoneStalactitesSmallNatural.GrowChance))
                {
                    below.ResetToType(
                        (ushort)ModContent
                            .TileType<
                                AssecstoneStalactitesSmallNatural>()); //using resettotype here because there is no worldgen method for hanging 1x1 rubble
                    below.TileFrameX = (short)(frameX * 18);
                }
                else if (Main.rand.NextBool(AssecstoneStalactitesNatural.GrowChance) && !below2.HasTile)
                {
                    WorldGen.Place1x2Top(i, j + 1, (ushort)ModContent.TileType<AssecstoneStalactitesNatural>(), 0);
                    below.TileFrameX = (short)(frameX * 18);
                    below2.TileFrameX = (short)(frameX * 18);
                }

                WorldGen.TileFrame(i, j + 1);
            }
        }

        if (!above.HasTile && Main.tile[i, j].BlockType == BlockType.Solid)
        {
            if (Main.rand.NextBool(AssimilatedGrassTile.GrowChance / 2))
                above.ResetToType((ushort)ModContent.TileType<AssimilatedGrassTile>());
            WorldGen.TileFrame(i, j - 1);
        }
    }
}