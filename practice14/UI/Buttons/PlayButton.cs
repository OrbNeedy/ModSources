using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace practice14.UI.Buttons
{
    public class PlayButton : UIElement
    {
        private static Asset<Texture2D> playIcon => ModContent.GetInstance<practice14>().Assets.Request<Texture2D>("Assets/Icons/PlayIcon", AssetRequestMode.ImmediateLoad);
        Color color = new Color(50, 255, 153);

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw((Texture2D)playIcon, new Vector2(Main.screenWidth + 20, Main.screenHeight - 20) / 2f, color);
        }
    }
}