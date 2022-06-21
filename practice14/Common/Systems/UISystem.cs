using Terraria.UI;
using Terraria.ModLoader;
using practice14.UI.Buttons;
using practice14.UI.Bars;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;

namespace practice14.Common.Systems
{
    internal class UISystem : ModSystem
    {
        internal SPBarBack spBar;
        private UserInterface _spBar;

        public override void Load()
        {
            spBar = new SPBarBack();
            spBar.Activate();
            _spBar = new UserInterface();
            _spBar.SetState(spBar);
        }

        public override void UpdateUI(GameTime gameTime)
        {
            _spBar.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "YourMod: Septimal power",
                    delegate
                    {
                        _spBar.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
