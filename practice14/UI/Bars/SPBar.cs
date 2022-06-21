using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace practice14.UI.Bars
{
    internal class SPBar : UIElement
    {
        private static Asset<Texture2D> barBones => ModContent.GetInstance<practice14>().Assets.Request<Texture2D>("Assets/Bars/SPBar", AssetRequestMode.ImmediateLoad);
        Color color = new Color(50, 255, 153);

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw((Texture2D)barBones, new Vector2(Main.screenWidth*0.55f, Main.screenHeight*0.1f), color);
        }
    }
}
