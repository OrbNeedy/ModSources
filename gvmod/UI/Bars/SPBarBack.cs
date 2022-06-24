using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using Terraria;
using Terraria.ModLoader;

namespace gvmod.UI.Bars
{
    public class SPBarBack : UIElement
    {
        public Color color = Color.White;
        public Texture2D texture = (Texture2D)ModContent.Request<Texture2D>("gvmod/Assets/Bars/SPBar", ReLogic.Content.AssetRequestMode.ImmediateLoad);
        public Vector2 position = new Vector2(Main.screenWidth * 0.6f, Main.screenHeight * 0.3f);


        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, color);
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
