using MLib.Common.Items;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Projectiles.Consumables.Ammo;

namespace TerrariaParadox.Content.Items.Consumables.Ammo;

public class MurkySolution : ModdedAmmo
{
    public override string LocalizationCategory => "Items.Consumables.Ammo";
    public override int ProjectileType => ModContent.ProjectileType<MurkySolutionProjectile>();
    public override int AmmoType => AmmoID.Solution;
    public override int Rarity => ItemRarityID.Orange;
    public override int Value => 1500;

    public override void SetDefaults()
    {
        Item.DefaultToSolution(ModContent.ProjectileType<MurkySolutionProjectile>());
    }

    public override void CustomSetStaticDefaults()
    {
        ItemID.Sets.SortingPriorityTerraforming[Type] = 101; // One past dirt solution
    }

    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
    {
        itemGroup = ContentSamples.CreativeHelper.ItemGroup.Solutions;
    }
}