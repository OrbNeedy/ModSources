using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using Terraria;
using Terraria.ModLoader;
using gvmod.Common.Players;
using System;
//new Vector2(Main.screenWidth * 0.6f, Main.screenHeight * 0.3f)
namespace gvmod.UI.Bars
{
    public class SPBarFill : UIElement
    {
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
                new Rectangle((int)(Main.screenWidth * 0.6f), (int)(Main.screenHeight * 0.3f), width, height), 
                color);
        }

        public override void Update(GameTime gameTime)
        {
            AdeptPlayer player = Main.LocalPlayer.GetModPlayer<AdeptPlayer>();
            width = (int)(player.SeptimalPowerToFraction() * 112);
            base.Update(gameTime);
        }
    }
}
