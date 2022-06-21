using Terraria.UI;

namespace practice14.UI.Buttons
{
    internal class PlayMenu : UIState
    {
        public PlayButton playButton;

        public override void OnInitialize()
        {
            playButton = new PlayButton();

            Append(playButton);
        }
    }
}
