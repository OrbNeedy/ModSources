using System.Collections.Generic;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using gvmod.Common.Systems;
using System;
using Microsoft.Xna.Framework;

namespace gvmod.Common.Players
{
    public class AdeptPlayer : ModPlayer
    {
        private bool isUsingPrimary;
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (KeybindSystem.primaryAbility.JustPressed)
            {
                isUsingPrimary = true;
            }
            if (KeybindSystem.primaryAbility.JustReleased)
            {
                isUsingPrimary = false;
            }
        }

        public override void PostUpdateMiscEffects()
        {
            if (isUsingPrimary)
            {
                Vector2 pos = new Vector2(64);
                for (int i = 0; i < 360; i++)
                {
                    pos = pos.RotatedBy(MathHelper.ToRadians(1));
                    if (i%2 == 0) Dust.NewDustDirect(Player.Center + pos, 10, 10, DustID.Electric, 0, 0);
                }
            }
        }
    }
}