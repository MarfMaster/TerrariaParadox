using System.Collections.Generic;
using Terraria.ModLoader;

namespace TerrariaParadox;

public partial class ParadoxItem : GlobalItem
{
    public static List<int> ItemsWithExtraTooltips = new List<int>();
    public static Dictionary<int, string> ExtraTooltipsWithKeys1 = new Dictionary<int, string>();
    public static Dictionary<int, string> ExtraTooltipsWithKeys2 = new Dictionary<int, string>();
}