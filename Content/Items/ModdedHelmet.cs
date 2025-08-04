using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TerrariaParadox.Content.Items;

public abstract class ModdedHelmet : ModItem
{
    public abstract int Defense { get; }
    public abstract int Rarity { get; }
    public abstract int Value { get; }
    /// <summary>
    /// Whether this helmet belongs to an armor set and has a bonus or not. Name this something like "Chitinite" for the Chitinite armor set.
    /// If it has no set bonus, don't assign a value to this. 
    /// Override ArmorSetBonus to add the effects of the set bonus when the full set is worn.
    /// </summary>
    public abstract string HasArmorSetBonusName { get; }
    /// <summary>
    /// The ItemID of the body type that belongs to this helmets armor set, set to 0 if there is none.
    /// </summary>
    public abstract int BodyType { get; }
    /// <summary>
    /// The ItemID of the legs type that belongs to this helmets armor set, set to 0 if there is none.
    /// </summary>
    public abstract int LegsType { get; }

    public override string LocalizationCategory => "Items.Armor";
    public static string SetBonusTextLocation;

    public override void SetStaticDefaults()
    {
        if (HasArmorSetBonusName != null)
        {
            SetBonusTextLocation = LocalizationCategory + "." + HasArmorSetBonusName + "SetBonus";
            Mod.GetLocalization(SetBonusTextLocation, () => "This armor set bonus hasn't been described yet.");
        }
    }

    public override void SetDefaults()
    {
        Item.defense = Defense;
        Item.rare = Rarity;
        Item.value = Value;
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        if (HasArmorSetBonusName == null)
        {
            return false;
        }
        if (BodyType != 0 && LegsType != 0)
        {
            return body.type == BodyType && legs.type == LegsType;
        }
        if  (BodyType != 0 && LegsType == 0)
        {
            return body.type == BodyType;
        }
        if (BodyType == 0 && LegsType != 0)
        {
            return legs.type == LegsType;
        }
        return false;
    }
    public virtual void ArmorSetBonus(Player player) {}
    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = LangUtils.GetTextValue(SetBonusTextLocation);
        ArmorSetBonus(player);
    }
}