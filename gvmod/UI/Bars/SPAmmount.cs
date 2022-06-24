using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using Terraria;
using Terraria.ModLoader;

namespace gvmod.UI.Bars
{
    public class SPAmmount : UIElement
    {
        public Color color = Color.MediumSeaGreen;
        public Texture2D texture = (Texture2D)ModContent.Request<Texture2D>("gvmod/Assets/Bars/SPBar", ReLogic.Content.AssetRequestMode.ImmediateLoad);
        
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(Main.screenWidth + 20, Main.screenHeight - 20) / 2f, color);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
