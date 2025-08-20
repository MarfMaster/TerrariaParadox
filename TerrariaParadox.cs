using Terraria.ModLoader;

namespace TerrariaParadox;

// Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
public partial class TerrariaParadox : Mod
{
    #region Unload

    public override void Unload()
    {
    }

    #endregion


    #region Load

    public override void Load()
    {
        ApplyMethodSwaps();
    }

    private void LoadClient()
    {
    }

    #endregion
}