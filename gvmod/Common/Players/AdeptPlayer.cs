using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;
using gvmod.Common.Systems;
using gvmod.Common.Players.Septimas;
using Terraria.ID;

namespace gvmod.Common.Players
{
    public class AdeptPlayer : ModPlayer
    {
        protected Septima septima;
        protected float maxSeptimalPower = 300;
        protected float septimalPower = 300;

        protected bool isUsingAbility;
        protected bool isOverheated = false;
        protected int timeSinceAbility = 0;

        public override void Initialize()
        {
            base.Initialize();
            if (Main.rand.NextBool())
            {
                septima = new AzureThunderclap(Player, this);
            } else
            {
                septima = new AzureStriker(Player, this);
            }
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (KeybindSystem.primaryAbility.JustPressed)
            {
                isUsingAbility = true;
            }
            if (KeybindSystem.primaryAbility.JustReleased)
            {
                isUsingAbility = false;
            }
            if (KeybindSystem.secondaryAbility.JustPressed)
            {
                Main.NewText("Current septimal power: " + (int)septimalPower);
                Main.NewText("Max septimal power: " + (int)maxSeptimalPower);
                Main.NewText("Septima: " + septima.Name);
            }
        }

        public override void PostUpdateMiscEffects()
        {
            if (septimalPower <= 0)
            {
                for (int i = 0; i < 20; i++)
                {
                    Dust.NewDust(Player.position, 10, 10, DustID.Electric, 0, 0);
                }
            }
            if (isUsingAbility && !isOverheated)
            {
                septima.FirstAbilityEffects();
            }
        }

        public override void PostUpdate()
        {
            if (isUsingAbility && !isOverheated)
            {
                septima.FirstAbility();
            }
            UpdateSeptimalPower();
        }

        public float SeptimalPowerToFraction()
        {
            return septimalPower / maxSeptimalPower;
        }

        public void UpdateSeptimalPower()
        {
            if (isUsingAbility && !isOverheated)
            {
                if (septimalPower > 0) septimalPower -= septima.SpUsage;
                timeSinceAbility = 0;
            }
            if (!isUsingAbility || isOverheated) timeSinceAbility++;
            if (timeSinceAbility >= 60)
            {
                if (!isOverheated)
                {
                    septimalPower += 2;
                } else
                {
                    septimalPower++;
                }
                if (septimalPower > maxSeptimalPower) septimalPower = maxSeptimalPower;
                timeSinceAbility = 60;
            }
            if (septimalPower <= 0)
            {
                isOverheated = true;
            }
            if (septimalPower >= maxSeptimalPower)
            {
                isOverheated = false;
            }
        }

        public int GetSeptimalPower()
        {
            return (int)septimalPower;
        }

        public void SetSeptimalPower(float sp)
        {
            this.septimalPower = sp;
        }

        public bool GetOverheatedState()
        {
            return isOverheated;
        }
    }
}