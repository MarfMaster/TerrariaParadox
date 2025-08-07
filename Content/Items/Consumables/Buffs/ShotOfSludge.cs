using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Buffs;
using TerrariaParadox.Content.Items.Tiles.Blocks;

namespace TerrariaParadox.Content.Items.Consumables.Buffs;

public class ShotOfSludge : ModdedBuffItem
{
    public override int BuffType => BuffID.WellFed;
    public override int BuffTime => 25 * 60 * 60;
    public override int ItemUseStyle => ItemUseStyleID.DrinkLiquid;
    public override SoundStyle ItemUseSound => SoundID.Item3;
    public override int Rarity => ItemRarityID.Green;
    public override int Value => 6200;
    public override void AddRecipes()
    {
        CreateRecipe().
            AddIngredient(ModContent.ItemType<Akebi>()).
            AddIngredient(ModContent.ItemType<Moonana>()).
            AddIngredient(ItemID.Bottle).
            AddTile(TileID.CookingPots).
            Register();
    }
}