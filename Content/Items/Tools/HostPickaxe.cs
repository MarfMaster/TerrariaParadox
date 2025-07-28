using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaParadox.Content.Items.Tools;

public class HostPickaxe : SwungMeleeItem
{
    public override int Damage => 13;
    public override int UseTime => 13;
    public override int UseAnimation => 18;
    public override float Knockback => 4;
    public override int Width => 32;
    public override int Height => 32;
    public override int Rarity => ItemRarityID.Blue;
    public override int Value => PriceByRarity.fromItem(Item);

    public override void CustomSetDefaults()
    {
        Item.pick = 85;
        Item.attackSpeedOnlyAffectsWeaponAnimation = true;
    }
}