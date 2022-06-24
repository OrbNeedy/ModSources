using Microsoft.Xna.Framework;
using Terraria.UI;
using Terraria;
using Terraria.ModLoader;
using gvmod.UI.Bars;
using System.Collections.Generic;

namespace gvmod.Common.Systems
{
    public class BarSystem : ModSystem
    {
        internal SPBar spBar;
        private UserInterface _spBar;

        public override void Load()
        {
            spBar = new SPBar();
            spBar.Activate();
            _spBar = new UserInterface();
            _spBar.SetState(spBar);
        }

        public override void UpdateUI(GameTime gameTime)
        {
            _spBar?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Septimal power"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "Gunvolt Mod: Septimal Power",
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
