using Terraria.UI;

namespace practice14.UI.Bars
{
    internal class SPBarBack : UIState
    {
        public SPBar spMeter;

        public override void OnInitialize()
        {
            spMeter = new SPBar();

            Append(spMeter);
        }
    }
}
