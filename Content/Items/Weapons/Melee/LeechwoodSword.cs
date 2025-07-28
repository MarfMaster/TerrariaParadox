using Terraria.ID;

namespace TerrariaParadox.Content.Items.Weapons.Melee;

public class LeechwoodSword : SwungMeleeItem
{
    public override int Damage => 12;
    public override int UseTime => 18;
    public override int UseAnimation => 18;
    public override float Knockback => 6.5f;
    public override int Width => 32;
    public override int Height => 32;
    public override int Rarity => ItemRarityID.White;
    public override int Value => PriceByRarity.fromItem(Item);
}