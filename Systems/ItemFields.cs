using System.Collections.Generic;
using Terraria.ModLoader;

namespace TerrariaParadox;

public partial class ParadoxItem : GlobalItem
{
    public static List<int> ItemsWithExtraTooltips = new();
    public static Dictionary<int, string> ExtraTooltipsWithKeys1 = new();
    public static Dictionary<int, string> ExtraTooltipsWithKeys2 = new();
}