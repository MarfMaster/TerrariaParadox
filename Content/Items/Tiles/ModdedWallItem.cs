using Terraria.ModLoader;

namespace TerrariaParadox.Content.Items.Tiles;

public abstract class ModdedWallItem : ModItem
{
    public override string LocalizationCategory => "Items.Tiles.Walls";
    public abstract int WallType { get; }

    public virtual void CustomSetStaticDefaults()
    {
    }

    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 400;
        CustomSetStaticDefaults();
    }

    public virtual void CustomSetDefaults()
    {
    }

    public override void SetDefaults()
    {
        Item.DefaultToPlaceableWall(WallType);
        CustomSetDefaults();
    }
}