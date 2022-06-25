using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using gvmod.Content.Buffs;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace gvmod.Common.Players.Septimas
{
    internal class AzureThunderclap : Septima
    {
        private Player thunderclapPlayer;
        private AdeptPlayer thunderclapAdept;
        private float thunderclapSpUsage = 0.5f;

        public AzureThunderclap(Player player, AdeptPlayer adept) : base(player, adept)
        {
            this.thunderclapPlayer = player;
            this.thunderclapAdept = adept;
        }

        public override float SpUsage => thunderclapSpUsage;

        public override string Name => "Azure Thunderclap";

        public override void FirstAbilityEffects()
        {
            if (thunderclapPlayer.wet)
            {
                for (int i = 0; i < 15; i++)
                {
                    Dust.NewDust(thunderclapPlayer.position, 10, 10, DustID.Electric, 0, 0);
                }
                return;
            }
            Vector2 pos = new Vector2(128);
            for (int i = 0; i < 360; i++)
            {
                pos = pos.RotatedBy(MathHelper.ToRadians(1));
                if (i % 5 == 0) Dust.NewDustDirect(thunderclapPlayer.Center + pos, 10, 10, DustID.MartianSaucerSpark, 0, 0, 0, Color.DeepSkyBlue);
            }
        }

        public override void FirstAbility()
        {
            if (thunderclapPlayer.wet)
            {
                thunderclapAdept.SetSeptimalPower(0);
                return;
            }
            List<NPC> closeNPCs = GetNPCsInRadius(176);
            foreach (NPC npc in closeNPCs)
            {
                npc.AddBuff(ModContent.BuffType<ThunderclapElectrifiedDebuff>(), 10);
            }
        }

        public override void SecondAbilityEffects()
        {
            throw new System.NotImplementedException();
        }

        public override void SecondAbility()
        {
            throw new System.NotImplementedException();
        }

        public List<NPC> GetNPCsInRadius(int radius)
        {
            var closeNPCs = new List<NPC>();
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                var npc = Main.npc[i];
                if (!npc.active && npc.life <= 0) continue;
                if (Vector2.Distance(npc.Center, thunderclapPlayer.Center) > radius) continue;
                closeNPCs.Add(Main.npc[i]);
            }
            return closeNPCs;
        }
    }
}
