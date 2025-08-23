using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Dusts.Tiles.Blocks;
using TerrariaParadox.Content.Dusts.Tiles.Misc;

namespace TerrariaParadox.Content.Tiles.Plants;

public class FlippedVine : ModTile
{
    public const int GrowChance = 70;
    public override void SetStaticDefaults() 
    {
        Main.tileCut[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileLavaDeath[Type] = true;
        Main.tileNoFail[Type] = true;

        TileID.Sets.TileCutIgnore.Regrowth[Type] = true;
        TileID.Sets.IsVine[Type] = true;
        TileID.Sets.ReplaceTileBreakDown[Type] = true;
        TileID.Sets.VineThreads[Type] = true;

        AddMapEntry(new Color(160, 160, 160)); // Slightly darker than ExampleBlock

        DustType = ModContent.DustType<AssecstoneDust>();
        HitSound = SoundID.Grass;
    }

    public override bool PreDraw(int i, int j, SpriteBatch spriteBatch) 
    {
        // This method is used to make a vine tile draw in the wind. Note that i and j are reversed for this method, this is not a typo.
        Main.instance.TilesRenderer.CrawlToTopOfVineAndAddSpecialPoint(j, i);

        // We must return false here to prevent the normal tile drawing code from drawing the default static tile. Without this a duplicate tile will be drawn.
        return false;
    }

    public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY) 
    {
        offsetY = -2;
    }

    public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects) 
    {
        if (i % 2 == 0) 
        {
            spriteEffects = SpriteEffects.FlipHorizontally;
        }
    }
}