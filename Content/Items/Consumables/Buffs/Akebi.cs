using Terraria.Audio;
using Terraria.ID;

namespace TerrariaParadox.Content.Items.Consumables.Buffs;

public class Akebi : ModdedBuffItem
{
    public override int Width => 32;
    public override int Height => 32;
    public override int BuffType => BuffID.WellFed;
    public override int BuffTime => 6 * 60 * 60;
    public override int ItemUseStyle => ItemUseStyleID.EatFood;
    public override SoundStyle ItemUseSound => SoundID.Item2;
    public override int Rarity => ItemRarityID.Blue;
    public override int Value => 3000;
}