using Microsoft.Xna.Framework;
using PegasusLib;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Dusts.Tiles.Blocks;

namespace TerrariaParadox.Content.Tiles.Plants;

public class FlippedThorns : ModdedBlockTile
{
    public const int GrowChance = 10;
    public override bool SolidBlock => false;
    public override bool MergesWithDirt => false;
    public override int OnMineDustType => ModContent.DustType<AssecstoneDust>();
    public override ushort VanillaFallbackTileAndMerge => TileID.CorruptThorns;
    public override SoundStyle TileMineSound => SoundID.Grass;
    public override Color MapColor => new(57, 63, 75);
    public override int WaterfallStyleID => WaterStyleID.Corrupt;
    public override bool MergesWithItself => false;
    public override bool NameShowsOnMapHover => true;

    public override void CustomSetStaticDefaults()
    {
        Main.tileCut[Type] = true;
        Main.tileLavaDeath[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileNoAttach[Type] = true;
        Main.tileWaterDeath[Type] = true;
        TileID.Sets.TouchDamageImmediate[Type] = 20;
        TileID.Sets.TouchDamageDestroyTile[Type] = true;
    }

    public override bool IsTileDangerous(int i, int j, Player player)
    {
        return true;
    }
    public override void RandomUpdate(int i, int j)
    {
        if (Main.rand.NextBool(GrowChance))
        {
            var right = Framing.GetTileSafely(i + 1, j);
            var left = Framing.GetTileSafely(i - 1, j);
            var above = Framing.GetTileSafely(i, j - 1);
            var aboveRight = Framing.GetTileSafely(i + 1, j - 1);
            var aboveLeft = Framing.GetTileSafely(i - 1, j - 1);
            if (!above.HasTile)
            {
                above.ResetToType((ushort)ModContent.TileType<FlippedThorns>());
                WorldGen.TileFrame(i + 1, j);
                WorldGen.TileFrame(i - 1, j);
                WorldGen.TileFrame(i, j - 1);
                WorldGen.TileFrame(i + 1, j - 1);
                WorldGen.TileFrame(i - 1, j - 1);
                WorldGen.TileFrame(i, j - 2);
                WorldGen.TileFrame(i + 1, j - 2);
                WorldGen.TileFrame(i - 1, j - 2);
            }
            else if (!right.HasTile)
            {
                right.ResetToType((ushort)ModContent.TileType<FlippedThorns>());
                WorldGen.TileFrame(i + 1, j);
                WorldGen.TileFrame(i + 2, j);
                WorldGen.TileFrame(i, j - 1);
                WorldGen.TileFrame(i + 1, j - 1);
                WorldGen.TileFrame(i + 2, j - 1);
                WorldGen.TileFrame(i, j + 1);
                WorldGen.TileFrame(i + 1, j + 1);
                WorldGen.TileFrame(i + 2, j + 1);
            }
            else if (!left.HasTile)
            {
                left.ResetToType((ushort)ModContent.TileType<FlippedThorns>());
                WorldGen.TileFrame(i - 1, j);
                WorldGen.TileFrame(i - 2, j);
                WorldGen.TileFrame(i, j - 1);
                WorldGen.TileFrame(i - 1, j - 1);
                WorldGen.TileFrame(i - 2, j - 1);
                WorldGen.TileFrame(i, j + 1);
                WorldGen.TileFrame(i - 1, j + 1);
                WorldGen.TileFrame(i - 2, j + 1);
            }
            else if (!aboveRight.HasTile)
            {
                aboveRight.ResetToType((ushort)ModContent.TileType<FlippedThorns>());
                WorldGen.TileFrame(i, j - 1);
                WorldGen.TileFrame(i + 1, j - 1);
                WorldGen.TileFrame(i + 2, j - 1);
                WorldGen.TileFrame(i, j - 2);
                WorldGen.TileFrame(i + 1, j - 2);
                WorldGen.TileFrame(i + 2, j - 2);
                WorldGen.TileFrame(i, j);
                WorldGen.TileFrame(i + 1, j);
                WorldGen.TileFrame(i + 2, j);
            }
            else if (!aboveLeft.HasTile)
            {
                aboveLeft.ResetToType((ushort)ModContent.TileType<FlippedThorns>());
                WorldGen.TileFrame(i, j - 1);
                WorldGen.TileFrame(i - 1, j - 1);
                WorldGen.TileFrame(i - 2, j - 1);
                WorldGen.TileFrame(i, j - 2);
                WorldGen.TileFrame(i - 1, j - 2);
                WorldGen.TileFrame(i - 2, j - 2);
                WorldGen.TileFrame(i - 1, j);
                WorldGen.TileFrame(i - 2, j);
            }
            WorldGen.TileFrame(i, j);
        }
    }
    public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
    {
        Vector2 tileCoords = new Vector2(i, j).ToWorldCoordinates();
        Rectangle tileHitbox = Utils.CenteredRectangle(tileCoords, new Vector2(20, 20));
        foreach (var player in Main.ActivePlayers)
        {
            if (tileHitbox.Intersects(player.Hitbox))
            {
                player.GetModPlayer<ParadoxPlayer>().HitByThorns = true;
            }
        }
    }
}

public class FlippedThornsEnt : ModTileEntity
{
    public override bool IsTileValidForEntity(int x, int y)
    {
        Tile tile = Main.tile[x, y];
        return tile.HasTile && tile.TileType == ModContent.TileType<FlippedThorns>();
    }
}