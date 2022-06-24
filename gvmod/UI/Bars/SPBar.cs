using gvmod.Common.Players;
using gvmod.Common.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;


namespace gvmod.UI.Bars
{
    public class SPBar : UIState
    {
        public int x = (int)(Main.screenWidth * 0.55f);
        public int y = (int)(Main.screenHeight * 0.02f);
        public SPBarBack spBarBack;
        public SPBarFill spBarFill;
        public AdeptPlayer player;
        public UIText text;
        public bool barReposition = false;

        public override void OnInitialize()
        {
            spBarBack = new SPBarBack();
            spBarFill = new SPBarFill();

            // TODO: Find out why no text works
            /*text = new UIText("?%");
            text.HAlign = 0.5f;
            text.VAlign = 0.5f;
            spBarFill.Append(text);*/

            Append(spBarBack);
            Append(spBarFill);
        }

        public override void Update(GameTime gameTime)
        {
            /*if (Main.LocalPlayer.GetModPlayer<AdeptPlayer>() != null)
            {
                player = Main.LocalPlayer.GetModPlayer<AdeptPlayer>();
            }
            if (player != null)
            {
                if (spBarBack.IsMouseHovering || spBarFill.IsMouseHovering)
                {
                    Main.hoverItemName = "SP: " + player.GetSeptimalPower();
                }
                text.SetText((int)(player.SeptimalPowerToFraction()*100) + "%");
            }*/
            base.Update(gameTime);
            spBarFill.x = spBarBack.x + 4;
            spBarFill.y = spBarBack.y;
            spBarBack.Update(gameTime);
            spBarFill.Update(gameTime);
            if (KeybindSystem.barReposition.JustPressed)
            {
                if (barReposition)
                {
                    barReposition = false;
                    Main.NewText("Bar reposition mode Off");
                } else
                {
                    barReposition = true;
                    Main.NewText("Bar reposition mode On");
                }
            }
        }

        public override void MouseDown(UIMouseEvent evt)
        {
            if (barReposition)
            {
                if (spBarBack.IsMouseHovering || spBarFill.IsMouseHovering)
                {
                    spBarBack.x = (int)evt.MousePosition.X - 60;
                    spBarBack.y = (int)evt.MousePosition.Y - 15;
                }
            }
        }
    }
}
