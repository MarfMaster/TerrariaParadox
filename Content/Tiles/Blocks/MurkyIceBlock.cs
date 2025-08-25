using Microsoft.Xna.Framework;
using MLib.Common.Tiles;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Dusts.Tiles.Blocks;
using TerrariaParadox.Content.Tiles.Misc;

namespace TerrariaParadox.Content.Tiles.Blocks;

public class MurkyIceBlockTile : ModdedBlockTile
{
    public override bool SolidBlock => true;
    public override bool MergesWithDirt => true;
    public override int OnMineDustType => ModContent.DustType<MurkyIceDust>();
    public override ushort VanillaFallbackTileAndMerge => TileID.IceBlock;
    public override SoundStyle TileMineSound => SoundID.Item50; //Ice Block mine sound
    public override Color MapColor => new(42, 42, 79);
    public override bool MergesWithItself => true;
    public override bool NameShowsOnMapHover => false;

    public override void CustomSetStaticDefaults()
    {
        TileID.Sets.Conversion.Ice[Type] = true;
        TileID.Sets.Ices[Type] = true;
        TileID.Sets.IceSkateSlippery[Type] = true;
        TileID.Sets.IcesSlush[Type] = true;
        TileID.Sets.IcesSnow[Type] = true;
        TileID.Sets.CanBeClearedDuringGeneration[Type] = true;
        Main.tileMerge[Type][TileID.SnowBlock] = true;
        Main.tileMerge[TileID.SnowBlock][Type] = true;
        Main.tileMerge[TileID.Slush][Type] = true;
        Main.tileMerge[Type][TileID.Slush] = true;
    }

    public override void RandomUpdate(int i, int j)
    {
        var worldCoordinates = new Vector2(i, j).ToWorldCoordinates();
        if (worldCoordinates.Y > Main.worldSurface) //below underground layer
        {
            var frameX = Main.rand.Next(0, 3); //generate a random tileframe for alternate styles
            var below = Framing.GetTileSafely(i, j + 1);
            var below2 = Framing.GetTileSafely(i, j + 2);
            if (!below.HasTile && Main.tile[i, j].BlockType == BlockType.Solid)
            {
                if (Main.rand.NextBool(MurkyIcicles1x1Natural.GrowChance))
                {
                    below.ResetToType((ushort)ModContent.TileType<MurkyIcicles1x1Natural>());
                    below.TileFrameX = (short)(frameX * 18);
                }
                else if (Main.rand.NextBool(MurkyIcicles1x2Natural.GrowChance) && !below2.HasTile)
                {
                    WorldGen.Place1x2Top(i, j + 1, (ushort)ModContent.TileType<MurkyIcicles1x2Natural>(), 0);
                    below.TileFrameX = (short)(frameX * 18);
                    below2.TileFrameX = (short)(frameX * 18);
                }

                WorldGen.TileFrame(i, j + 1);
            }
        }
    }
}