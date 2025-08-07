using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Buffs;

namespace TerrariaParadox.Content.Items.Consumables.Buffs;

public class Akebi : ModdedBuffItem
{
    public override int BuffType => BuffID.WellFed;
    public override int BuffTime => 6 * 60 * 60;
    public override int ItemUseStyle => ItemUseStyleID.EatFood;
    public override SoundStyle ItemUseSound => SoundID.Item2;
    public override int Rarity => ItemRarityID.Blue;
    public override int Value => 3000;
}