using System;
using System.Reflection;
using Terraria;
using Terraria.ModLoader;

namespace TerrariaParadox;

public partial class TerrariaParadox : Mod
{
    private void ApplyMethodSwaps()
    {
        MonoModHooks.Add(
            typeof(ModContent).GetMethod("ResizeArrays", BindingFlags.NonPublic | BindingFlags.Static),
            (Action<bool> orig, bool unloading) =>
            {
                orig(unloading);
                if (!unloading) PopulateArrays();
            }
        );
        On_WorldGen.KillWall_CheckFailure += (orig, fail, tileCache) =>
        {
            fail = orig(fail, tileCache);
            if (Main.LocalPlayer.HeldItem.hammer < WallHammerRequirement[tileCache.WallType]) fail = true;
            return fail;
        };
        On_Player.DoesPickTargetTransformOnKill +=
            (orig, self, hitCounter, damage, x, y, pickPower, bufferIndex, tileTarget) =>
            {
                if (orig(self, hitCounter, damage, x, y, pickPower, bufferIndex, tileTarget)) return true;
                if (hitCounter.AddDamage(bufferIndex, damage, false) >= 100 &&
                    TileTransformsOnKill[tileTarget.TileType]) return true;
                return false;
            };
    }
}