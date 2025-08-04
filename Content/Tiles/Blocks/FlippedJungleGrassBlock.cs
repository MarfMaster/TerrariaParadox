using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PegasusLib;
using Terraria.Audio;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Dusts.Tiles.Blocks;

namespace TerrariaParadox.Content.Tiles.Blocks;

public class FlippedJungleGrassBlock : ModdedBlockTile
{
    public override bool SolidBlock => true;
    public override bool MergesWithDirt => true;
    public override int OnMineDustType => DustID.Dirt;
    public override ushort VanillaFallbackTileAndMerge => TileID.Mud;
    public override SoundStyle TileMineSound => SoundID.Dig;
    public override Color MapColor => new Color(69, 79, 101);
    public override int WaterfallStyleID => WaterStyleID.Corrupt;
    public override bool MergesWithItself => false;
    public override bool NameShowsOnMapHover => false;
    public override void CustomSetStaticDefaults()
    {
        TileID.Sets.CanBeDugByShovel[Type] = true;
        TileID.Sets.CanBeClearedDuringGeneration[Type] = true;
        TileID.Sets.GrassSpecial[Type] = true;
        TileID.Sets.Conversion.JungleGrass[Type] = true;
        TileID.Sets.NeedsGrassFraming[Type] = true;
        TileID.Sets.ChecksForMerge[Type] = true;
        Main.tileMerge[Type] = Main.tileMerge[TileID.JungleGrass];
        Main.tileMerge[Type][TileID.Dirt] = true;
        Main.tileMerge[TileID.Dirt][Type] = true;
        Main.tileMerge[Type][TileID.Mud] = true;
        Main.tileMerge[TileID.Mud][Type] = true;
        for (int i = 0; i < TileLoader.TileCount; i++)
        {
            if (TileID.Sets.Grass[i] || TileID.Sets.GrassSpecial[i])
            {
                Main.tileMerge[Type][i] = true;
                Main.tileMerge[i][Type] = true;
            }
        }
        TerrariaParadox.TileTransformsOnKill[Type] = true;
        TileID.Sets.ForcedDirtMerging[Type] = true;
        TileID.Sets.Conversion.MergesWithDirtInASpecialWay[Type] = true;
    }    
    public override bool CanReplace(int i, int j, int tileTypeBeingPlaced)
    {
        if (tileTypeBeingPlaced == TileID.Mud)
        {
            return false;
        }
        return base.CanReplace(i, j, tileTypeBeingPlaced);
    }
    public override void RandomUpdate(int i, int j) 
    {
        Tile above = Framing.GetTileSafely(i, j - 1);
        if (!above.HasTile && Main.tile[i, j].BlockType == BlockType.Solid) 
        {
            /*if (Main.rand.NextBool(250)) {
                above.ResetToType((ushort)ModContent.TileType<Acetabularia>());
            } else {
                above.ResetToType((ushort)ModContent.TileType<Riven_Foliage>());
            }*/
            WorldGen.TileFrame(i, j - 1);
        }
    }
    public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem) 
    {
        if (fail && !effectOnly) 
        {
            Framing.GetTileSafely(i, j).TileType = TileID.Mud;
        }
    }
}