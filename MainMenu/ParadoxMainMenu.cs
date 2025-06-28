using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace TerrariaParadox.MainMenu
{
    public class ParadoxMainMenu:ModMenu
    {
        public override string DisplayName => "TERRARIA: PARADOX";
        
        public override Asset<Texture2D> Logo => ModContent.Request<Texture2D>("TerrariaParadox/MainMenu/MenuLogo");
    }
}
