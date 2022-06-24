using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;


namespace gvmod.UI.Bars
{
    public class SPBar : UIState
    {
        public SPBarBack spBarBack;
        public SPBarFill spBarFill;

        public override void OnInitialize()
        {
            spBarBack = new SPBarBack();
            spBarFill = new SPBarFill();
            Append(spBarBack);
            Append(spBarFill);
        }

        public override void Update(GameTime gameTime)
        {
            spBarBack.Update(gameTime);
            spBarFill.Update(gameTime);
        }
    }
}
