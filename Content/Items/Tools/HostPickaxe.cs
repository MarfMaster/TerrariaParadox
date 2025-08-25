using System.Collections.Generic;
using MLib.Common.Items;
using MLib.Common.Utilities;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using TerrariaParadox.Content.Items.Materials;
using TerrariaParadox.Content.Items.Tiles.Bars;

namespace TerrariaParadox.Content.Items.Tools;

public class HostPickaxe : ModdedBasicItem
{
    public override string LocalizationCategory => "Items.Tools";
    public override int Damage => 13;
    public override int UseTime => 13;
    public override int ItemUseAnimation => 18;
    public override float Knockback => 4;
    public override DamageClass DamageType => DamageClass.Melee;
    public override int ItemUseStyle => ItemUseStyleID.Swing;
    public override bool DealsContactDamage => true;
    public override int Width => 32;
    public override int Height => 32;
    public override int Rarity => ItemRarityID.Blue;
    public override int Value => PriceByRarity.fromItem(Item);
    public override SoundStyle UseSound => SoundID.Item1;
    public override bool UseTurn => true;

    public override void SetDefaults()
    {
        base.SetDefaults();
        Item.pick = 85;
        Item.attackSpeedOnlyAffectsWeaponAnimation = true;
    }

    public override void AddRecipes()
    {
        CreateRecipe().AddIngredient(ModContent.ItemType<ChitiniteBar>(), 12)
            .AddIngredient(ModContent.ItemType<BioluminescentGoop>(), 6).AddTile(TileID.Anvils).Register();
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var ttindex = tooltips.FindLastIndex(t => t.Mod == "Terraria");
        if (ttindex != -1)
            tooltips.Insert(ttindex + 1,
                new TooltipLine(Mod, "PickaxePower", Language.GetTextValue("ItemTooltip.NightmarePickaxe")));
    }
}