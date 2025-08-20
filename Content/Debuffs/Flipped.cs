using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaParadox.Content.Debuffs;

public class Flipped : ModBuff
{
    public override string LocalizationCategory => "Debuffs";

    public override void SetStaticDefaults()
    {
        Main.debuff[Type] = true;
        Main.buffNoTimeDisplay[Type] = true;
        BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
    }

    public override void Update(Player player, ref int buffIndex)
    {
        //player.GetModPlayer<ParadoxPlayer>().FlippedGravity = true;
    }
}