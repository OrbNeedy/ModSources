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


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Width.Set(120, 1f);
            Height.Set(30, 1f);
            MaxWidth = new StyleDimension(120, 1f);
            MaxHeight = new StyleDimension(30, 1f);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(x, y), color);
        }
    }
}
