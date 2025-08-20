using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.Metadata;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using TerrariaParadox.Content.Items.Materials;
using TerrariaParadox.Content.Tiles.Blocks;

namespace TerrariaParadox.Content.Tiles.Plants;

public class ParasiticMushroomTile : ModTile
{
    public const int ChanceToGrow = 50; //Lower is higher chance, 1 would be guaranteed
    private const int FrameWidth = 18; // A constant for readability and to kick out those magic numbers

    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileObsidianKill[Type] = true;
        Main.tileCut[Type] = true;
        Main.tileNoFail[Type] = true;
        TileID.Sets.ReplaceTileBreakUp[Type] = true;
        TileID.Sets.IgnoredInHouseScore[Type] = true;
        TileID.Sets.IgnoredByGrowingSaplings[Type] = true;
        TileMaterials.SetForTileId(Type,
            TileMaterials
                ._materialsByName["Plant"]); // Make this tile interact with golf balls in the same way other plants do

        TileObjectData.newTile.CopyFrom(TileObjectData.StyleAlch);
        TileObjectData.newTile.AnchorValidTiles =
        [
            ModContent.TileType<FlippedGrassBlock>(),
            ModContent.TileType<FlippedJungleGrassBlock>()
        ];
        TileObjectData.newTile.AnchorAlternateTiles =
        [
            TileID.ClayPot,
            TileID.PlanterBox
        ];
        TileObjectData.addTile(Type);

        HitSound = SoundID.Grass;
        DustType = DustID.Ambient_DarkBrown;
    }

    public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
    {
        if (i % 2 == 0) spriteEffects = SpriteEffects.FlipHorizontally;
    }

    public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height,
        ref short tileFrameX, ref short tileFrameY)
    {
        offsetY = -2; // This is -1 for tiles using StyleAlch, but vanilla sets to -2 for herbs, which causes a slight visual offset between the placement preview and the placed tile. 
    }

    public override IEnumerable<Item> GetItemDrops(int i, int j)
    {
        yield return new Item(ModContent.ItemType<ParasiticMushroom>());
    }
}