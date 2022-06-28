using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using gvmod.Content.Buffs;
using gvmod.Content.Projectiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace gvmod.Common.Players.Septimas
{
    internal class AzureStriker : Septima
    {
        private float strikerSpUsage = 1f;
        private int secondaryCooldown = 300;
        private Vector2 velocityMultiplier = new Vector2(1, 1);
        private bool isFalling = false;
        public AzureStriker(AdeptPlayer adept, Player player) : base(adept, player)
        {
            this.player = player;
            this.adept = adept;
            secondaryDuration = 0;
        }

        public override float SpUsage { get => strikerSpUsage; set => strikerSpUsage = value; }

        public override string Name => "Azure Striker";

        public override int SecondaryCooldownTime { get => secondaryCooldown; set => secondaryCooldown = value; }

        public override void FirstAbilityEffects()
        {
            if (player.wet)
            {
                strikerSpUsage = adept.maxSeptimalPower;
                return;
            }
            else
            {
                strikerSpUsage = 1f;
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
            //TODO: Figure out a way of making this without having to change collision to be circular
            //Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, new Vector2(0), ModContent.ProjectileType<FlashfieldStriker>(), 4, 0, player.whoAmI);
            List<NPC> closeNPCs = GetNPCsInRadius(176);
            foreach (NPC npc in closeNPCs)
            {
                if (!npc.friendly)
                {
                    npc.AddBuff(ModContent.BuffType<StrikerElectrifiedDebuff>(), 10);
                }
            }
            if (player.velocity.Y > 0)
            {
                isFalling = true;
            } else
            {
                isFalling = false;
            }
        }

        public override void SecondAbilityEffects()
        {
            for (int i = 0; i < 20; i++)
            {
                float xPos = Main.rand.NextFloat(-16, 16);
                float yPos = Main.rand.NextFloat(-500, 500);
                Dust.NewDust(player.Center + new Vector2(xPos, yPos), 10, 10, DustID.MartianSaucerSpark, 0, 0, 100, Color.DeepSkyBlue);
            }
        }

        public override void SecondAbility()
        {
            if (secondaryDuration <= 1 && adept.secondaryInUse)
            {
                Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, new Vector2(0), ModContent.ProjectileType<Thunder>(), 50, 8, player.whoAmI);
            }
        }

        public override void MiscEffects()
        {
            if (isFalling && adept.isUsingPrimaryAbility && !adept.isOverheated)
            {
                velocityMultiplier.Y = 0.7f;
            }
            else
            {
                if (adept.secondaryInUse)
                {
                    velocityMultiplier *= 0f;
                } else
                {
                    velocityMultiplier = new Vector2(1f);
                }
            }
        }

        public override void Updates()
        {
            if (adept.isUsingSecondaryAbility && adept.timeSinceSecondary >= secondaryCooldown)
            {
                secondaryDuration++;
                if (secondaryDuration >= 15)
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
