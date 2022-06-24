using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using gvmod.Common.Systems;
using gvmod.Content.Buffs;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace gvmod.Common.Players
{
    public class AdeptPlayer : ModPlayer
    {
        private float maxSeptimalPower = 300;
        private float septimalPower = 300;

        private bool isUsingPrimary;
        private int timeSinceAbility = 0;
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
            if (KeybindSystem.secondaryAbility.JustPressed)
            {
                Main.NewText("Current septimal power: " + septimalPower);
                Main.NewText("Max septimal power: " + maxSeptimalPower);
            }
        }

        public override void PostUpdateMiscEffects()
        {
            if (isUsingPrimary && septimalPower > 0)
            {
                Vector2 pos = new Vector2(128);
                for (int i = 0; i < 360; i++)
                {
                    pos = pos.RotatedBy(MathHelper.ToRadians(1));
                    if (i%6 == 0) Dust.NewDustDirect(Player.Center + pos, 10, 10, DustID.MartianSaucerSpark, 0, 0, 0, Color.DeepSkyBlue);
                }
            }
        }

        public override void PostUpdate()
        {
            if (isUsingPrimary && septimalPower > 0)
            {
                List<NPC> closeNPCs = GetNPCsInRadius(128);
                foreach (NPC npc in closeNPCs)
                {
                    if (!npc.friendly)
                    {
                        npc.AddBuff(ModContent.BuffType<AzureElectrifiedDebuff>(), 10);
                    }
                }
            }
            UpdateSeptimalPower();
        }

        public List<NPC> GetNPCsInRadius(int radius)
        {
            var closeNPCs = new List<NPC>();
            for (int i = 0; i < Main.npc.Length; i++)
            {
                var npc = Main.npc[i];
                if (npc == null) continue;
                if (Vector2.Distance(npc.Center, Player.Center) > radius) continue;
                closeNPCs.Add(Main.npc[i]);
            }
            return closeNPCs;
        }

        public float SeptimalPowerToFraction()
        {
            return septimalPower / maxSeptimalPower;
        }

        public void UpdateSeptimalPower()
        {
            if (isUsingPrimary)
            {
                if (septimalPower > 0) septimalPower--;
                timeSinceAbility = 0;
            }
            if (!isUsingPrimary) timeSinceAbility++;
            if (timeSinceAbility >= 60)
            {
                septimalPower++;
                if (septimalPower > maxSeptimalPower) septimalPower = maxSeptimalPower;
                timeSinceAbility = 60;
            }
        }
    }
}