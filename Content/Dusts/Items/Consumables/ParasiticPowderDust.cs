using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace TerrariaParadox.Content.Dusts.Items.Consumables;

public class ParasiticPowderDust : ModDust
{
    public override Color? GetAlpha(Dust dust, Color lightColor)
    {
        return new Color(lightColor.R, lightColor.G, lightColor.B, 25);
    }

    public override bool Update(Dust dust)
    {
        dust.scale *= 0.96f;
        dust.velocity.Y -= 0.01f;
        
        dust.position += dust.velocity;
        dust.scale += 0.005f;
        dust.velocity.Y *= 0.94f;
        dust.velocity.X *= 0.94f;
        float num105 = dust.scale * 0.8f;
        if (num105 > 1f)
        {
            num105 = 1f;
        }
        num105 = dust.scale * 0.4f;
        Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num105, num105 * 0.5f, num105 * 0.3f);
        if (dust.scale < 0.15f)
        {
            dust.active = false;
        }
        if (!dust.active)
        {
            return true;
        }
        return false;
    }
}