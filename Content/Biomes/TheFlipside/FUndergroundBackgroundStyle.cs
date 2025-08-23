using Terraria.ModLoader;

namespace TerrariaParadox.Content.Biomes.TheFlipside;

public class FUndergroundBackgroundStyle : ModUndergroundBackgroundStyle
{
    public override void FillTextureArray(int[] textureSlots)
    {
        textureSlots[0] =
            BackgroundTextureLoader.GetBackgroundSlot(Mod, "Textures/Backgrounds/TheFlipside/Underground0");
    }
}