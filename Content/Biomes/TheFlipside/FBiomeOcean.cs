using Terraria;
using Terraria.ModLoader;

namespace TerrariaParadox.Content.Biomes.TheFlipside;

public class FBiomeOcean : ModBiome
{
    public override string LocalizationCategory => "Biomes.TheFlipside";

    // Select all the scenery
    public override ModUndergroundBackgroundStyle UndergroundBackgroundStyle =>
        ModContent.GetInstance<FUndergroundBackgroundStyle>();

    // Select Music
    //public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/MysteriousMystery");

    public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;

    // Populate the Bestiary Filter
    public override string BestiaryIcon => base.BestiaryIcon;

    // Calculate when the biome is active.
    public override bool IsBiomeActive(Player player)
    {
        return player.ZoneBeach && player.InModBiome<FBiomeMainSurface>();
    }
}