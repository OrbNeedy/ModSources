using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using ReLogic.Content;
using gvmod.Common.Players.Septimas.Abilities;
using Terraria.ModLoader;
using gvmod.Common.Players;

namespace gvmod.UI.Menus
{
    internal class AbilitySlot : UIImageButton
    {
        public Special asignedSpecial;

        public AbilitySlot(Asset<Texture2D> texture, Special asignedSpecial) : base(texture)
        {
            this.asignedSpecial = asignedSpecial;
        }

        public AbilitySlot(Asset<Texture2D> texture) : base(texture)
        {
            asignedSpecial = null;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            if (IsMouseHovering)
            {
                if (asignedSpecial == null)
                {
                    Main.hoverItemName = "Empty.";
                } else
                {
                    Main.hoverItemName = asignedSpecial.Name;
                }
            }
        }
    }
}
