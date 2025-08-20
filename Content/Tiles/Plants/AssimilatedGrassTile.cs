using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Enums;
using Terraria.GameContent.Metadata;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using TerrariaParadox.Content.Items.Materials;
using TerrariaParadox.Content.Items.Tiles.Plants;
using TerrariaParadox.Content.Tiles.Blocks;

namespace TerrariaParadox.Content.Tiles.Plants;

// An enum for the 3 stages of herb growth
public enum PlantStage : byte
{
    Planted,
    Growing,
    Grown
}

// A plant with 3 stages, planted, growing and grown
// Sadly, modded plants are unable to be grown by the flower boots
// TODO smart cursor support for herbs, see SmartCursorHelper.Step_AlchemySeeds
// TODO Staff of Regrowth:
// - Player.PlaceThing_Tiles_BlockPlacementForAssortedThings: check where type == 84 (grown herb)
// - Player.ItemCheck_GetTileCutIgnoreList: maybe generalize?
// TODO vanilla seeds to replace fully grown herb
public class AssimilatedGrassTile : ModTile
{
    public const int GrowChance = 250;
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

        // We do not use this because our tile should only be spelunkable when it's fully grown. That's why we use the IsTileSpelunkable hook instead
        //Main.tileSpelunker[Type] = true;

        // Do NOT use this, it causes many unintended side effects
        //Main.tileAlch[Type] = true;

        var name = CreateMapEntryName();
        AddMapEntry(new Color(128, 128, 128), name);

        TileObjectData.newTile.CopyFrom(TileObjectData.StyleAlch);
        TileObjectData.newTile.AnchorValidTiles =
        [
            ModContent.TileType<FlippedGrassBlock>(),
            ModContent.TileType<FlippedJungleGrassBlock>(),
            ModContent.TileType<AssecstoneBlockTile>()
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

    public override bool CanPlace(int i, int j)
    {
        var tile = Framing.GetTileSafely(i, j); // Safe way of getting a tile instance

        if (tile.HasTile)
        {
            int tileType = tile.TileType;
            if (tileType == Type)
            {
                var stage = GetStage(i, j); // The current stage of the herb

                // Can only place on the same herb again if it's grown already
                return stage == PlantStage.Grown;
            }

            // Support for vanilla herbs/grasses:
            if (Main.tileCut[tileType] || TileID.Sets.BreakableWhenPlacing[tileType] || tileType == TileID.WaterDrip ||
                tileType == TileID.LavaDrip || tileType == TileID.HoneyDrip || tileType == TileID.SandDrip)
            {
                var foliageGrass = tileType == TileID.Plants || tileType == TileID.Plants2;
                var moddedFoliage = tileType >= TileID.Count &&
                                    (Main.tileCut[tileType] || TileID.Sets.BreakableWhenPlacing[tileType]);
                var harvestableVanillaHerb = Main.tileAlch[tileType] &&
                                             WorldGen.IsHarvestableHerbWithSeed(tileType, tile.TileFrameX / 18);

                if (foliageGrass || moddedFoliage || harvestableVanillaHerb)
                {
                    WorldGen.KillTile(i, j);
                    if (!tile.HasTile && Main.netMode == NetmodeID.MultiplayerClient)
                        NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, i, j);

                    return true;
                }
            }

            return false;
        }

        return true;
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

    public override bool CanDrop(int i, int j)
    {
        var stage = GetStage(i, j);

        if (stage == PlantStage.Planted)
            // Do not drop anything when just planted
            return false;
        return true;
    }

    public override IEnumerable<Item> GetItemDrops(int i, int j)
    {
        var stage = GetStage(i, j);

        var worldPosition = new Vector2(i, j).ToWorldCoordinates();
        var nearestPlayer = Main.player[Player.FindClosest(worldPosition, 16, 16)];

        var herbItemType = ModContent.ItemType<AssimilatedGrass>();
        var herbItemStack = 1;

        var seedItemType = ModContent.ItemType<AssimilatedGrassSeeds>();
        var seedItemStack = 1;

        if (nearestPlayer.active && (nearestPlayer.HeldItem.type == ItemID.StaffofRegrowth ||
                                     nearestPlayer.HeldItem.type == ItemID.AcornAxe))
        {
            // Increased yields with Staff of Regrowth, even when not fully grown
            herbItemStack = Main.rand.Next(1, 3);
            seedItemStack = Main.rand.Next(1, 6);
        }
        else if (stage == PlantStage.Grown)
        {
            // Default yields, only when fully grown
            herbItemStack = 1;
            seedItemStack = Main.rand.Next(1, 4);
        }

        if (herbItemType > 0 && herbItemStack > 0) yield return new Item(herbItemType, herbItemStack);

        if (seedItemType > 0 && seedItemStack > 0) yield return new Item(seedItemType, seedItemStack);
    }

    public override bool IsTileSpelunkable(int i, int j)
    {
        var stage = GetStage(i, j);

        // Only glow if the herb is grown
        return stage == PlantStage.Grown;
    }

    /*public override void NearbyEffects(int i, int j, bool closer) //enable this for instant guaranteed blooming
    {
        var tile = Framing.GetTileSafely(i, j);
        var stage = GetStage(i, j);

        // Only blooms during new moon and blood moons
        if (stage == PlantStage.Growing && !Main.IsItDay() && (Main.GetMoonPhase() == MoonPhase.Empty || Main.bloodMoon))
        {
            // Increase the x frame to change the stage
            tile.TileFrameX += FrameWidth;

            // If in multiplayer, sync the frame change
            if (Main.netMode != NetmodeID.SinglePlayer) NetMessage.SendTileSquare(-1, i, j, 1);
        }
        else if (stage == PlantStage.Grown && Main.IsItDay())
        {
            // Decrease the x frame to change the stage
            tile.TileFrameX -= FrameWidth;

            // If in multiplayer, sync the frame change
            if (Main.netMode != NetmodeID.SinglePlayer) NetMessage.SendTileSquare(-1, i, j, 1);
        }
    }*/

    public override void RandomUpdate(int i, int j)
    {
        var tile = Framing.GetTileSafely(i, j);
        var stage = GetStage(i, j);

        // Only grow to the next stage if there is a next stage. We don't want our tile turning pink!
        // Only grow at night by chance
        if (stage == PlantStage.Planted && !Main.IsItDay() && Main.rand.NextBool(5))
        {
            // Increase the x frame to change the stage
            tile.TileFrameX += FrameWidth;

            // If in multiplayer, sync the frame change
            if (Main.netMode != NetmodeID.SinglePlayer) NetMessage.SendTileSquare(-1, i, j, 1);
        }

        // Only blooms during new moon and blood moons
        if (stage == PlantStage.Growing && !Main.IsItDay() &&
            (Main.GetMoonPhase() == MoonPhase.Empty || Main.bloodMoon))
        {
            // Increase the x frame to change the stage
            tile.TileFrameX += FrameWidth;

            // If in multiplayer, sync the frame change
            if (Main.netMode != NetmodeID.SinglePlayer) NetMessage.SendTileSquare(-1, i, j, 1);
        }
        else if (stage == PlantStage.Grown && Main.IsItDay())
        {
            // Decrease the x frame to change the stage
            tile.TileFrameX -= FrameWidth;

            // If in multiplayer, sync the frame change
            if (Main.netMode != NetmodeID.SinglePlayer) NetMessage.SendTileSquare(-1, i, j, 1);
        }
    }

    // A helper method to quickly get the current stage of the herb (assuming the tile at the coordinates is our herb)
    private static PlantStage GetStage(int i, int j)
    {
        var tile = Framing.GetTileSafely(i, j);
        return (PlantStage)(tile.TileFrameX / FrameWidth);
    }
}