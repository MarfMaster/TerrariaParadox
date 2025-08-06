using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaParadox.Content.Buffs;

public class WeaponImbueStickling : ModBuff
{
    public override string LocalizationCategory => "Buffs";

    public override void SetStaticDefaults()
    {
        BuffID.Sets.IsAFlaskBuff[Type] = true;
        Main.meleeBuff[Type] = true;
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.GetModPlayer<ParadoxPlayer>().WeaponImbueStickling = true;
    }
}