using Terraria;
using Terraria.ModLoader;

namespace TerrariaParadox;

public partial class TerrariaParadox : Mod
{
    private void ApplyMethodSwaps()
    {
        On_WorldGen.KillWall_CheckFailure += (orig, fail, tileCache) =>
        {
            fail = orig(fail, tileCache);
            if (Main.LocalPlayer.HeldItem.hammer < ParadoxSystem.MinHammer[tileCache.WallType]) fail = true;
            {
                return fail;
            }
        };
        On_Player.DoesPickTargetTransformOnKill +=
            (orig, self, hitCounter, damage, x, y, pickPower, bufferIndex, tileTarget) =>
            {
                if (orig(self, hitCounter, damage, x, y, pickPower, bufferIndex, tileTarget)) return true;

                if (hitCounter.AddDamage(bufferIndex, damage, false) >= 100 &&
                    ParadoxSystem.TileTransformsOnKill[tileTarget.TileType])
                    return true;
                return false;
            };
    }
}