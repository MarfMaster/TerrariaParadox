using Microsoft.Xna.Framework;
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
    public override ushort VanillaFallbackTileAndMerge => TileID.Stone;
    public override SoundStyle TileMineSound => SoundID.Tink;
    public override Color MapColor => new Color(69, 79, 101);
    public override int WaterfallStyleID => WaterStyleID.Corrupt;
    public override bool MergesWithItself => true;
    public override bool NameShowsOnMapHover => false;
    public override void CustomSetStaticDefaults()
    {
        MinPick = 65;
        MineResist = 2f;
        TileID.Sets.Conversion.Stone[Type] = true;
        TileID.Sets.Stone[Type] = true;
        TileID.Sets.CanBeClearedDuringOreRunner[Type] = true;
        TileID.Sets.CanBeClearedDuringGeneration[Type] = true;
    }    
    public override void RandomUpdate(int i, int j) 
    {
        Vector2 worldCoordinates = new Vector2(i, j).ToWorldCoordinates();
        Tile above = Framing.GetTileSafely(i, j - 1); //get tiles above
        if (worldCoordinates.Y > Main.worldSurface) //below underground layer
        {
            int frameX = Main.rand.Next(0, 6); //generate a random tileframe for alternate styles
            
            Tile above2 = Framing.GetTileSafely(i, j - 2);
            if (!above.HasTile && Main.tile[i, j].BlockType == BlockType.Solid)  //check for empty space and whether this block is solid
            {
                if (Main.rand.NextBool(AssecstoneRubble1x1GroundedNatural.GrowChance)) //1 in X chance
                {
                    WorldGen.Place1x1(i, j - 1, (ushort)ModContent.TileType<AssecstoneRubble1x1GroundedNatural>(), 0); //place tile
                    above.TileFrameX = (short)(frameX * 18); //reframe it so it cna show alternate styles
                }
                else if (Main.rand.NextBool(AssecstoneRubble1x2GroundedNatural.GrowChance) && !above2.HasTile) //1 in X chance and whether there's enough space
                {
                    WorldGen.Place1x2(i, j - 1, (ushort)ModContent.TileType<AssecstoneRubble1x2GroundedNatural>(), 0);
                    above.TileFrameX = (short)(frameX * 18); //need to reframe both tiles to the same frame
                    above2.TileFrameX = (short)(frameX * 18);
                }
                WorldGen.TileFrame(i, j - 1);
                return;
            }
            //everything for hanging tiles it the same but adjusted for it hanging below instead of being grounded on top of this tile
            Tile below = Framing.GetTileSafely(i, j + 1);
            Tile below2 = Framing.GetTileSafely(i, j + 2);
            if (!below.HasTile && Main.tile[i, j].BlockType == BlockType.Solid) 
            {
                if (Main.rand.NextBool(AssecstoneRubble1x1HangingNatural.GrowChance))
                {
                    below.ResetToType((ushort)ModContent.TileType<AssecstoneRubble1x1HangingNatural>()); //using resettotype here because there is no worldgen method for hanging 1x1 rubble
                    below.TileFrameX = (short)(frameX * 18);
                }
                else if (Main.rand.NextBool(AssecstoneRubble1x2HangingNatural.GrowChance) && !below2.HasTile)
                {
                    WorldGen.Place1x2Top(i, j + 1, (ushort)ModContent.TileType<AssecstoneRubble1x2HangingNatural>(), 0);
                    below.TileFrameX = (short)(frameX * 18);
                    below2.TileFrameX = (short)(frameX * 18);
                }
                WorldGen.TileFrame(i, j + 1);
            }
        }
        if (!above.HasTile && Main.tile[i, j].BlockType == BlockType.Solid) 
        {
            if (Main.rand.NextBool(FlippedHerbTile.GrowChance / 3))
            {
                above.ResetToType((ushort)ModContent.TileType<FlippedHerbTile>());
            }
            WorldGen.TileFrame(i, j - 1);
        }
    }
}