using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Capture;
using Terraria.ModLoader;

namespace TerrariaParadox.Content.Biomes.TheFlipside;

/// <summary>
///     This is the main file of the biome, use this to combine it with any variations. It's also the surface variation.
/// </summary>
public class FBiomeMainSurface : ModBiome
{
    public override string LocalizationCategory => "Biomes.TheFlipside";

    // Select all the scenery
    public override ModWaterStyle WaterStyle =>
        ModContent.GetInstance<FWaterStyle>(); // Sets a water style for when inside this biome

    public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle =>
        ModContent.GetInstance<FSurfaceBackgroundStyle>();

    public override ModUndergroundBackgroundStyle UndergroundBackgroundStyle =>
        ModContent.GetInstance<FUndergroundBackgroundStyle>();

    public override CaptureBiome.TileColorStyle TileColorStyle => CaptureBiome.TileColorStyle.Corrupt;

    // Select Music
    //public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/MysteriousMystery");

    //public override int BiomeTorchItemType => ModContent.ItemType<ExampleTorch>();
    //public override int BiomeCampfireItemType => ModContent.ItemType<ExampleCampfire>();

    // Populate the Bestiary Filter
    public override string BestiaryIcon => base.BestiaryIcon;
    public override string BackgroundPath => base.BackgroundPath;
    public override Color? BackgroundColor => base.BackgroundColor;
    public override string MapBackground => BackgroundPath;

    public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;

    // Calculate when the biome is active.
    public override bool IsBiomeActive(Player player)
    {
        var infestedBlockCount = ModContent.GetInstance<WorldgenMain>().FlippedBlockCount >= 300;

        return infestedBlockCount;
    }
}