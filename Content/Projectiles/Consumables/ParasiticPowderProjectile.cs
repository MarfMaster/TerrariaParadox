using AltLibrary.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using TerrariaParadox.Content.Biomes.TheFlipside;
using TerrariaParadox.Content.Dusts.Items.Consumables;

namespace TerrariaParadox.Content.Projectiles.Consumables;

public class ParasiticPowderProjectile : ModProjectile
{
    public static int ConversionType;

    public override void SetStaticDefaults()
    {
        // Cache the conversion type here instead of repeately fetching it every frame
        ConversionType = ModContent.GetInstance<Flipping>().Type;
    }

    public override void SetDefaults()
    {
        Projectile.width = 48;
        Projectile.height = 48;
        Projectile.aiStyle = 0;
        Projectile.friendly = true;
        Projectile.tileCollide = false;
        Projectile.penetrate = -1;
        Projectile.alpha = 255;
        Projectile.ignoreWater = true;
    }

    public override bool? CanCutTiles()
    {
        return false;
    }

    public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
        var crimson = Projectile.type == 463;
        for (var num41 = 0; num41 < 200; num41++)
            if (Main.npc[num41].active)
            {
                var value2 = new Rectangle((int)Main.npc[num41].position.X, (int)Main.npc[num41].position.Y,
                    Main.npc[num41].width, Main.npc[num41].height);
                if (projHitbox.Intersects(value2))
                    Main.npc[num41]
                        .AttemptToConvertNPCToEvil(
                            crimson); //replace Projectile with a hook that allows for flipside conversion
            }

        return Projectile.Colliding(projHitbox, targetHitbox);
    }

    public override void AI()
    {
        Projectile.velocity *= 0.95f;
        Projectile.ai[0] += 1f;
        if (Projectile.ai[0] == 180f) Projectile.Kill();
        if (Projectile.ai[1] == 0f)
        {
            Projectile.ai[1] = 1f;
            var num966 = 30;
            var dustColor = new Color(36, 36, 83);
            for (var num977 = 0; num977 < num966; num977++)
            {
                var dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height,
                    ModContent.DustType<ParasiticPowderDust>(), Projectile.velocity.X, Projectile.velocity.Y, 50,
                    dustColor);
            }
        }

        var playerIsOwner = Main.myPlayer == Projectile.owner;
        if (playerIsOwner)
        {
            var num988 = (int)(Projectile.position.X / 16f) - 1;
            var num999 = (int)((Projectile.position.X + Projectile.width) / 16f) + 2;
            var num1010 = (int)(Projectile.position.Y / 16f) - 1;
            var num1021 = (int)((Projectile.position.Y + Projectile.height) / 16f) + 2;
            if (num988 < 0) num988 = 0;
            if (num999 > Main.maxTilesX) num999 = Main.maxTilesX;
            if (num1010 < 0) num1010 = 0;
            if (num1021 > Main.maxTilesY) num1021 = Main.maxTilesY;
            var vector57 = default(Vector2);

            for (var num1032 = num988; num1032 < num999; num1032++)
            for (var num1043 = num1010; num1043 < num1021; num1043++)
            {
                vector57.X = num1032 * 16;
                vector57.Y = num1043 * 16;
                if (!(Projectile.position.X + Projectile.width > vector57.X) ||
                    !(Projectile.position.X < vector57.X + 16f) ||
                    !(Projectile.position.Y + Projectile.height > vector57.Y) ||
                    !(Projectile.position.Y < vector57.Y + 16f) || !Main.tile[num1032, num1043].HasTile) continue;
                ALConvert.Convert<AltBiomeMain>(num1032, num1043, 1);
                IL_510c: ;
            }
        }
    }
}