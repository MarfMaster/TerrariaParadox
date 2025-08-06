using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using TerrariaParadox.Content.Dusts.Tiles.Blocks;

namespace TerrariaParadox.Content.Debuffs;

public class Stickled : ModBuff
{
    public const float DamageReduction = 10f;
    
    public override string LocalizationCategory => "Debuffs";
    public override LocalizedText Description => base.Description.WithFormatArgs(DamageReduction);
    
    public override void SetStaticDefaults()
    {
        Main.debuff[Type] = true;
    }

    public override void Update(NPC npc, ref int buffIndex)
    {
        npc.GetGlobalNPC<ParadoxNPC>().Stickled = true;
        Dust.NewDust(npc.position, npc.width, npc.height, ModContent.DustType<ChitiniteOreDust>());
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.GetModPlayer<ParadoxPlayer>().Stickled = true;
        Dust.NewDust(player.position, player.width, player.height, ModContent.DustType<ChitiniteOreDust>());
    }
}
