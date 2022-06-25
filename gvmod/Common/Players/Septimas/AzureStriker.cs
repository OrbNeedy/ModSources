using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using gvmod.Content.Buffs;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace gvmod.Common.Players.Septimas
{
    internal class AzureStriker : Septima
    {
        private Player strikerPlayer;
        private AdeptPlayer strikerAdept;
        private float strikerSpUsage = 1;

        public AzureStriker(Player player, AdeptPlayer adept) : base(player, adept)
        {
            this.strikerPlayer = player;
            this.strikerAdept = adept;
        }

        public override float SpUsage => strikerSpUsage;

        public override string Name => "Azure Striker";

        public override void FirstAbilityEffects()
        {
            Vector2 pos = new Vector2(128);
            for (int i = 0; i < 360; i++)
            {
                pos = pos.RotatedBy(MathHelper.ToRadians(1));
                if (i % 5 == 0) Dust.NewDustDirect(strikerPlayer.Center + pos, 10, 10, DustID.MartianSaucerSpark, 0, 0, 0, Color.DeepSkyBlue);
            }
        }

        public override void FirstAbility()
        {
            if (strikerPlayer.wet)
            {
                strikerAdept.SetSeptimalPower(0);
                return;
            }
            List<NPC> closeNPCs = GetNPCsInRadius(176);
            foreach (NPC npc in closeNPCs)
            {
                if (!npc.friendly)
                {
                    npc.AddBuff(ModContent.BuffType<StrikerElectrifiedDebuff>(), 10);
                }
            }
            if (strikerPlayer.velocity.Y > 0)
            {
                strikerPlayer.velocity.Y *= 0.7f;
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
                if (Vector2.Distance(npc.Center, strikerPlayer.Center) > radius) continue;
                closeNPCs.Add(Main.npc[i]);
            }
            return closeNPCs;
        }
    }
}
