using System.Collections.Generic;
using AltLibrary.Common.Hooks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PegasusLib;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using TerrariaParadox.Content.Dusts.Tiles.Misc;
using TerrariaParadox.Content.Tiles.Blocks;

namespace TerrariaParadox.Content.Tiles.Plants;

public class FlippedVine : ModTile
{
    public const int GrowChance = 6;

    public override void SetStaticDefaults()
    {
        TileID.Sets.IsVine[Type] = true;
        AltVines.VineTypeForAnchor[Type] = Type;
        AltVines.AddVine(Type, ModContent.TileType<FlippedGrassBlock>(), 
            ModContent.TileType<FlippedJungleGrassBlock>());
        Main.tileCut[Type] = true;
        HitSound = SoundID.Grass;
        DustType = ModContent.DustType<BioluminescentBulbDust>();
        Main.tileLavaDeath[Type] = true;
        Main.tileFrameImportant[Type] = true;
        AddMapEntry(new Color(238, 145, 105));
        //TileID.Sets.VineThreads[Type] = true;
        TileID.Sets.ReplaceTileBreakDown[Type] = true;
    }

    public override void RandomUpdate(int i, int j)
    {
        var below = Framing.GetTileSafely(i, j + 1);
        var nineAbove = Framing.GetTileSafely(i, j - 9);
        if (!below.HasTile && Main.tile[i, j].BlockType == BlockType.Solid && nineAbove.TileType != Type)
        {
            if (Main.rand.NextBool(GrowChance / 3))
            {
                below.ResetToType((ushort)ModContent.TileType<FlippedVine>());
            }
            WorldGen.TileFrame(i, j);
            WorldGen.TileFrame(i, j + 1);
        }
    }

    public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
    {
        Tile tile = Main.tile[i, j];
        var below = Framing.GetTileSafely(i, j + 1);
        if (below.HasTile && below.TileType == Type)
        {
            WorldGen.KillTile(i, j + 1);
        }
    }

    public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
    {
        List<ushort> ValidTiles = new List<ushort>()
        {
            (ushort)ModContent.TileType<FlippedGrassBlock>(),
            (ushort)ModContent.TileType<FlippedJungleGrassBlock>(),
            (ushort)ModContent.TileType<FlippedVine>(),
        };
        var above = Framing.GetTileSafely(i, j - 1);
        if (!above.HasTile | above.BlockType != BlockType.Solid | !ValidTiles.Contains(above.TileType))
        {
            WorldGen.KillTile(i, j);
        }
        
        if (i % 2 == 1)
            spriteEffects = SpriteEffects.FlipHorizontally;
    }
}