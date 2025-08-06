using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaParadox.Content.Items.Debug;

public class DamageDebugTome : ModItem
{
    public override string LocalizationCategory => "Items.Debug";
    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Master;
        Item.value = PriceByRarity.fromItem(Item) / 35;
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.useTime = 30;
        Item.useAnimation = 30;
        Item.UseSound = SoundID.Item11;
    }

    public override bool? UseItem(Player player)
    {
        player.GetModPlayer<ParadoxPlayer>().DebugNoDamageSpread = !player.GetModPlayer<ParadoxPlayer>().DebugNoDamageSpread;
        Main.NewText("Damage spread " + !player.GetModPlayer<ParadoxPlayer>().DebugNoDamageSpread);
        return true;
    }
}