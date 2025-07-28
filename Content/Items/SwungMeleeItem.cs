using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaParadox.Content.Items;

public abstract class SwungMeleeItem : ModItem
{
    public abstract int Damage { get; }
    public abstract int UseTime { get; }
    public abstract int UseAnimation { get; }
    public abstract float Knockback { get; }
    public abstract int Width { get; }
    public abstract int Height { get; }
    public abstract int Rarity { get; }
    public abstract int Value { get; }
    public virtual void CustomSetDefaults() {}
    public override void SetDefaults()
    {
        Item.damage = Damage;
        Item.useTime = UseTime;
        Item.useAnimation = UseAnimation;
        Item.knockBack = Knockback;
        Item.width = Width;
        Item.height = Height;
        Item.rare = Rarity;
        Item.value = Value;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        CustomSetDefaults();
    }
}