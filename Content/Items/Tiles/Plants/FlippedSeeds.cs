using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Tiles.Blocks;

namespace TerrariaParadox.Content.Items.Tiles.Plants;

public class FlippedSeeds : ModItem
{
    public override string LocalizationCategory => "Items.Tiles.Plants";

    public override void SetStaticDefaults()
    {
        ItemID.Sets.GrassSeeds[Type] = true;
        Item.ResearchUnlockCount = 25;
    }

    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.CrimsonSeeds);
        Item.placeStyle = ModContent.TileType<FlippedGrassBlock>();
    }

    public override bool ConsumeItem(Player player)
    {
        ref var tileType = ref Main.tile[Player.tileTargetX, Player.tileTargetY].TileType;
        switch (tileType)
        {
            case TileID.CrimsonGrass:
                tileType = (ushort)ModContent.TileType<FlippedGrassBlock>();
                break;
            case TileID.CrimsonJungleGrass:
                tileType = (ushort)ModContent.TileType<FlippedJungleGrassBlock>();
                break;
        }

        if (Main.netMode != NetmodeID.SinglePlayer)
            NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 1, Player.tileTargetX, Player.tileTargetY,
                tileType);
        return true;
    }
}