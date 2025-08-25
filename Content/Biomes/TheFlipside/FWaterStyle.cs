using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;
using TerrariaParadox.Content.Dusts.Tiles.Blocks;

namespace TerrariaParadox.Content.Biomes.TheFlipside;

public class FWaterStyle : ModWaterStyle
{
    private Asset<Texture2D> rainTexture;

    public override void Load()
    {
        rainTexture = Mod.Assets.Request<Texture2D>("Content/Biomes/TheFlipside/FRain");
    }

    public override int ChooseWaterfallStyle()
    {
        return ModContent.GetInstance<FWaterfallStyle>().Slot;
    }

    public override int GetSplashDust()
    {
        return ModContent.DustType<AssecstoneDust>();
    }

    public override int GetDropletGore()
    {
        return ModContent.GoreType<FDroplet>();
    }

    public override void LightColorMultiplier(ref float r, ref float g, ref float b)
    {
        r = 1f;
        g = 1f;
        b = 1f;
    }

    public override Color BiomeHairColor()
    {
        return new Color(0.57f, 0.63f, 0.75f);
    }

    public override byte GetRainVariant()
    {
        return (byte)Main.rand.Next(3);
    }

    public override Asset<Texture2D> GetRainTexture()
    {
        return rainTexture;
    }
}