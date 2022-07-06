using gvmod.Common.Players;
using gvmod.Common.Players.Septimas.Abilities;
using gvmod.Common.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace gvmod.UI.Menus
{
    internal class AbilityMenu : UIState
    {
        public int x = (Main.screenWidth - 160);
        public int y = (int)(Main.screenHeight * 0.4);
        public AbilityMenuBack abilityMenuBack;
        public AbilitySlot[] abilitySlots;
        public UIImage cooldownFill;
        public UIText text;
        public Texture2D filling = ModContent.Request<Texture2D>("gvmod/Assets/Bars/APBarFilling", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
        public bool barReposition = false;

        public override void OnInitialize()
        {
            abilityMenuBack = new AbilityMenuBack();
            abilityMenuBack.Left.Set(x, 0f);
            abilityMenuBack.Top.Set(y, 0f);
            abilityMenuBack.Width.Set(150, 0f);
            abilityMenuBack.Height.Set(140, 0f);

            abilitySlots = new AbilitySlot[4];
            for (int i = 1; i < 3; i++)
            {
                for (int k = 0; k < 2; k++)
                {
                    abilitySlots[i + (k * 2) - 1] = new AbilitySlot(ModContent.Request<Texture2D>("gvmod/Assets/Menus/AbilityEmpty" + (i + (k*2)), ReLogic.Content.AssetRequestMode.ImmediateLoad));
                    abilitySlots[i + (k * 2) - 1].Width.Set(58, 0f);
                    abilitySlots[i + (k * 2) - 1].Height.Set(48, 0f);
                }
            }
            abilitySlots[0].Left.Set(x + 12, 0);
            abilitySlots[0].Top.Set(y + 8, 0);
            abilitySlots[1].Left.Set(x + 76, 0);
            abilitySlots[1].Top.Set(y + 8, 0);
            abilitySlots[2].Left.Set(x + 8, 0);
            abilitySlots[2].Top.Set(y + 78, 0);
            abilitySlots[3].Left.Set(x + 78, 0);
            abilitySlots[3].Top.Set(y + 78, 0);

            text = new UIText("1", 0.8f);
            text.Width.Set(x + 138, 0f);
            text.Height.Set(y + 15, 0f);
            text.Top.Set(y + 10, 0f);
            text.Left.Set(x, 0f);

            for (int i = 0; i < 4; i++)
            {
                abilityMenuBack.Append(abilitySlots[i]);
            }
            abilityMenuBack.Append(text);
            Append(abilityMenuBack);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            var adept = Main.LocalPlayer.GetModPlayer<AdeptPlayer>();
            Vector2[] positions = new Vector2[]
            {
                new Vector2((int)abilityMenuBack.Left.Pixels + 12, (int)abilityMenuBack.Top.Pixels + 8),
                new Vector2((int)abilityMenuBack.Left.Pixels + 76, (int)abilityMenuBack.Top.Pixels + 8),
                new Vector2((int)abilityMenuBack.Left.Pixels + 8, (int)abilityMenuBack.Top.Pixels + 78),
                new Vector2((int)abilityMenuBack.Left.Pixels + 78, (int)abilityMenuBack.Top.Pixels + 78)
            };
            if (adept.septima.Name == "Human") return;

            for (int i = 0; i < 4; i++)
            {
                if (abilitySlots[i].asignedSpecial != null && abilitySlots[i].asignedSpecial.Name != "")
                {
                    if (!abilitySlots[i].asignedSpecial.InCooldown)
                    {
                        if (adept.abilityPower < abilitySlots[i].asignedSpecial.ApUsage)
                        {
                            spriteBatch.Draw(ModContent.Request<Texture2D>("gvmod/Assets/Menus/AbilityActive" + (i + 1), ReLogic.Content.AssetRequestMode.ImmediateLoad).Value, positions[i], Color.Red);
                        } else
                        {
                            spriteBatch.Draw(ModContent.Request<Texture2D>("gvmod/Assets/Menus/AbilityActive" + (i + 1), ReLogic.Content.AssetRequestMode.ImmediateLoad).Value, positions[i], Color.White);
                        }
                        spriteBatch.Draw(ModContent.Request<Texture2D>("gvmod/Assets/Menus/" + adept.activeSlot[i] + "Icon", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value, positions[i] + new Vector2(10, 18), Color.White);
                    } else
                    {
                        spriteBatch.Draw(ModContent.Request<Texture2D>("gvmod/Assets/Menus/AbilityCooldown", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value, positions[i], Color.White);
                    }
                } else
                {
                    spriteBatch.Draw(ModContent.Request<Texture2D>("gvmod/Assets/Menus/AbilityEmpty" + (i + 1), ReLogic.Content.AssetRequestMode.ImmediateLoad).Value, positions[i], Color.White);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            var adept = Main.LocalPlayer.GetModPlayer<AdeptPlayer>();
            for (int i = 0; i < adept.activeSlot.Count; i++)
            {
                abilitySlots[i].asignedSpecial = adept.GetSpecial(adept.activeSlot[i]);
            }
            if (IsMouseHovering)
            {
                Main.hoverItemName = adept.septimalPower.ToString();
            }
        }
    }
}
