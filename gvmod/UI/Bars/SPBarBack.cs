using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using Terraria;
using Terraria.ModLoader;

namespace gvmod.UI.Bars
{
    public class SPBarBack : UIElement
    {
        public int x = (int)(Main.screenWidth * 0.55f);
        public int y = (int)(Main.screenHeight * 0.02f);
        public Color color = Color.White;
        public Texture2D texture = (Texture2D)ModContent.Request<Texture2D>("gvmod/Assets/Bars/SPBar", 
            ReLogic.Content.AssetRequestMode.ImmediateLoad);


        public override void Draw(SpriteBatch spriteBatch)
        {
            Width.Set(120, 100);
            Height.Set(30, 100);
            spriteBatch.Draw(texture, new Vector2(x, y), color);
        }
    }
}
