using Terraria.ModLoader;

namespace TerrariaParadox.Content.Biomes.TheFlipside;

public class UndergroundIceBackgroundStyle : ModUndergroundBackgroundStyle
{
    public override void FillTextureArray(int[] textureSlots)
    {
        textureSlots[0] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "Textures/Backgrounds/TheFlipside/UndergroundIce0");
    }
}