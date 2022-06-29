using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using gvmod.Common.Players;

namespace gvmod.UI.Bars
{
    public class SPBarFill : UIElement
    {
        public int x = (int)(Main.screenWidth * 0.55f) + 4;
        public int y = (int)(Main.screenHeight * 0.02f) + 1;
        public int height = 30;
        public int width = 112;
        public Color color = Color.White;
        public Texture2D texture = (Texture2D)ModContent.Request<Texture2D>("gvmod/Assets/Bars/SPBarBody", ReLogic.Content.AssetRequestMode.ImmediateLoad);

        public override void OnInitialize()
        {
            base.OnInitialize();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, 
                new Rectangle(x, y, width, height), 
                color);
        }

        public override void Update(GameTime gameTime)
        {
            if (IsMouseHovering)
            {
                Main.hoverItemName = "?/300";
            }
            AdeptPlayer player = Main.LocalPlayer.GetModPlayer<AdeptPlayer>();
            if (player.isOverheated)
            {
                color = Color.Red;
            } else
            {
                color = Color.White;
            }
            width = (int)(player.SeptimalPowerToFraction() * 112);
            base.Update(gameTime);
        }
    }
}
