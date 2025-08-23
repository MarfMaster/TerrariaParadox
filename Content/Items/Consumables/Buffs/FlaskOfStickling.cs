using MLib.Common.Items;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Buffs;
using TerrariaParadox.Content.Items.Materials;

namespace TerrariaParadox.Content.Items.Consumables.Buffs;

public class FlaskOfStickling : ModdedBuffItem
{
    public override int Width => 22;
    public override int Height => 30;
    public override int BuffType => ModContent.BuffType<WeaponImbueStickling>();
    public override int BuffTime => 20 * 60 * 60;
    public override int ItemUseStyle => ItemUseStyleID.DrinkLiquid;
    public override SoundStyle ItemUseSound => SoundID.Item3;
    public override int Rarity => ItemRarityID.LightRed;
    public override int Value => 500;

    public override void AddRecipes()
    {
        CreateRecipe().AddIngredient(ItemID.BottledWater).AddIngredient(ModContent.ItemType<Stickler>(), 2)
            .AddTile(TileID.ImbuingStation).Register();
    }
}