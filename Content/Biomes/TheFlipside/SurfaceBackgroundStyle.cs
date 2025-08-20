using Terraria.ModLoader;

namespace TerrariaParadox.Content.Biomes.TheFlipside;

public class SurfaceBackgroundStyle : ModSurfaceBackgroundStyle
{
    // Use this to keep far Backgrounds like the mountains.
    public override void ModifyFarFades(float[] fades, float transitionSpeed)
    {
        for (var i = 0; i < fades.Length; i++)
            if (i == Slot)
            {
                fades[i] += transitionSpeed;
                if (fades[i] > 1f) fades[i] = 1f;
            }
            else
            {
                fades[i] -= transitionSpeed;
                if (fades[i] < 0f) fades[i] = 0f;
            }
    }

    public override int ChooseFarTexture()
    {
        return BackgroundTextureLoader.GetBackgroundSlot(Mod, "Textures/Backgrounds/TheFlipside/SurfaceFar");
    }

    public override int ChooseMiddleTexture()
    {
        return BackgroundTextureLoader.GetBackgroundSlot(Mod, "Textures/Backgrounds/TheFlipside/SurfaceMiddle");
    }

    /*public override int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b)
    {
        return BackgroundTextureLoader.GetBackgroundSlot(Mod, "Textures/Backgrounds/TheFlipside/SurfaceClose");
    }*/
}