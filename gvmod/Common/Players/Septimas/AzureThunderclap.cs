using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using gvmod.Content.Buffs;
using gvmod.Content.Projectiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace gvmod.Common.Players.Septimas
{
    internal class AzureThunderclap : Septima
    {
        private float thunderclapSpUsage = 0.5f;
        private int secondaryCooldown = 600;
        private Vector2 velocityMultiplier = new Vector2(1, 1);

        public AzureThunderclap(AdeptPlayer adept, Player player) : base(adept, player)
        {
            this.adept = adept;
            secondaryDuration = 0;
        }

        public override float SpUsage { get => thunderclapSpUsage; set => thunderclapSpUsage = value; }

        public override string Name => "Azure Thunderclap";

        public override int SecondaryCooldownTime { get => secondaryCooldown; set => secondaryCooldown = value; }

        public override void FirstAbilityEffects()
        {
            if (player.wet)
            {
                thunderclapSpUsage = adept.maxSeptimalPower;
                return;
            }
            else
            {
                thunderclapSpUsage = 0.5f;
            }
            Vector2 pos = new Vector2(128);
            for (int i = 0; i < 360; i++)
            {
                pos = pos.RotatedBy(MathHelper.ToRadians(1));
                if (i % 5 == 0) Dust.NewDustDirect(player.Center + pos, 10, 10, DustID.MartianSaucerSpark, 0, 0, 0, Color.DeepSkyBlue);
            }
        }

        public override void FirstAbility()
        {
            List<NPC> closeNPCs = GetNPCsInRadius(176);
            foreach (NPC npc in closeNPCs)
            {
                npc.AddBuff(ModContent.BuffType<ThunderclapElectrifiedDebuff>(), 10);
            }
        }

        public override void SecondAbilityEffects()
        {
            if (secondaryDuration <= 5 && adept.secondaryInUse)
            {
                for (int i = 0; i < 7; i++)
                {
                    Dust.NewDust(new Vector2(player.Center.X, player.position.Y + player.height), 10, 10, DustID.MartianSaucerSpark, 46);
                    Dust.NewDust(new Vector2(player.Center.X, player.position.Y + player.height), 10, 10, DustID.MartianSaucerSpark, -46);
                }
            }
        }

        public override void SecondAbility()
        {
            if (secondaryDuration <= 1 && adept.secondaryInUse)
            {
                Projectile.NewProjectile(player.GetSource_FromThis(), new Vector2(player.Center.X, player.position.Y + player.height), new Vector2(10, 0), ModContent.ProjectileType<LightningCreeper>(), 35, 10, player.whoAmI);
                Projectile.NewProjectile(player.GetSource_FromThis(), new Vector2(player.Center.X, player.position.Y + player.height), new Vector2(-10, 0), ModContent.ProjectileType<LightningCreeper>(), 35, 10, player.whoAmI);
            }
        }

        public override void MiscEffects()
        {
            if (adept.isOverheated)
            {
                velocityMultiplier.X = 0.2f;
            } else
            {
                velocityMultiplier.X = 1f;
            }
        }

        public override void Updates()
        {
            if (adept.isUsingSecondaryAbility && adept.timeSinceSecondary >= secondaryCooldown)
            {
                secondaryDuration++;
                if (secondaryDuration >= 5)
                {
                    adept.secondaryInUse = false;
                    secondaryDuration = 0;
                    adept.secondaryInCooldown = true;
                    adept.timeSinceSecondary = 0;
                    adept.isUsingSecondaryAbility = false;
                }
            }
            player.velocity *= velocityMultiplier;
        }

        public List<NPC> GetNPCsInRadius(int radius)
        {
            var closeNPCs = new List<NPC>();
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                var npc = Main.npc[i];
                if (!npc.active && npc.life <= 0) continue;
                if (Vector2.Distance(npc.Center, player.Center) > radius) continue;
                closeNPCs.Add(Main.npc[i]);
            }
            return closeNPCs;
        }
    }
}
