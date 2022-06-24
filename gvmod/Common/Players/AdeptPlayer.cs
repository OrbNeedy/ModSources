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
        private int maxSeptimalPower;
        private int septimalPower;
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
            List<NPC> closeNPCs = getNPCsInRadius(128);
            foreach (NPC npc in closeNPCs)
            {
                if (!npc.friendly)
                {
                    npc.AddBuff(ModContent.BuffType<AzureElectrifiedDebuff>(), 10);
                }
            }
        }

        public List<NPC> getNPCsInRadius(int radius)
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
    }
}