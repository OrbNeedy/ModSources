using Microsoft.Xna.Framework;
using Terraria.UI;


namespace gvmod.UI.Bars
{
    public class SPBar : UIState
    {
        public SPAmmount spAmmount;

        public override void OnInitialize()
        {
            spAmmount = new SPAmmount();
            Append(spAmmount);
        }

        public override void Update(GameTime gameTime)
        {
            spAmmount.Update(gameTime);
        }
    }
}
