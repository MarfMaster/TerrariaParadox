using Terraria.Localization;

namespace TerrariaParadox;

public static class LangUtils
{
    public static string GetTextValue(string key)
    {
        return Language.GetTextValue("Mods.TerrariaParadox." + key);
    }

    public static string GetTextValue(string key, params object[] args)
    {
        return Language.GetTextValue("Mods.TerrariaParadox." + key, args);
    }
}