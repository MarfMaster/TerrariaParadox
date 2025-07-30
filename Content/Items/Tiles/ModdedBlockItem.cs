using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaParadox.Content.Items.Tiles;

public abstract class ModdedBlockItem : ModItem
{
    public override string LocalizationCategory => "Items.Tiles.Blocks";
    public abstract int TileType { get; }
    public virtual void CustomSetStaticDefaults() {}
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 100;
        CustomSetStaticDefaults();
    }
    public virtual void CustomSetDefaults() {}
    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(TileType);
        CustomSetDefaults();
    }
}