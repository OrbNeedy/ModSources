using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;
using gvmod.Common.Systems;
using gvmod.Common.Players.Septimas;
using Terraria.ID;
using Terraria.ModLoader.IO;

namespace gvmod.Common.Players
{
    public class AdeptPlayer : ModPlayer
    {
        public Septima septima;
        public float maxSeptimalPower;
        public float septimalPower;
        public int level;
        public int experience;

        public bool isUsingPrimaryAbility;
        public bool isUsingSecondaryAbility;
        public bool isUsingSpecialAbility;

        public bool secondaryInUse = false;
        public int timeSinceSecondary = 1;
        public bool secondaryInCooldown = false;
        public bool isOverheated = false;
        public int timeSinceAbility = 0;

        public override void Initialize()
        {
            base.Initialize();
            maxSeptimalPower = 300;
            septimalPower = maxSeptimalPower;
            septima = GetSeptima(Main.rand.Next(2));
            level = 1;
            experience = 0;
        }

        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey("Level"))
            {
                level = tag.GetInt("Level");
            }
            if (tag.ContainsKey("Experience"))
            {
                experience = tag.GetInt("Experience");
            }
            if (tag.ContainsKey("Septima"))
            {
                septima = GetSeptima(tag.GetString("Septima"));
            }
        }

        public override void SaveData(TagCompound tag)
        {
            tag["Level"] = level;
            tag["Experience"] = experience;
            tag["Septima"] = septima.Name;
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (KeybindSystem.primaryAbility.JustPressed)
            {
                isUsingPrimaryAbility = true;
                Main.NewText("Septima: " + septima.Name);
            }
            if (KeybindSystem.primaryAbility.JustReleased)
            {
                isUsingPrimaryAbility = false;
            }
            if (KeybindSystem.secondaryAbility.JustPressed)
            {
                isUsingSecondaryAbility = true;
                Main.NewText("Cooldown: " + timeSinceSecondary);
            }
        }

        public override void PostUpdateMiscEffects()
        {
            if (septimalPower <= 0 && timeSinceAbility <= 10)
            {
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDust(Player.position, 10, 10, DustID.Electric, 0, 0);
                }
            }
            if (isUsingPrimaryAbility && !isOverheated)
            {
                septima.FirstAbilityEffects();
            }
            if (secondaryInUse)
            {
                septima.SecondAbilityEffects();
            }
        }
        
        public override void PostUpdate()
        {
            if (isUsingPrimaryAbility && !isOverheated)
            {
                septima.FirstAbility();
            }
            if (secondaryInUse)
            {
                septima.SecondAbility();
            }
            UpdateSeptimalPower();
            if (experience >= level * 500)
            {
                level++;
                experience = 0;
            }
            septima.Updates();
            septima.MiscEffects();
        }

        public float SeptimalPowerToFraction()
        {
            return septimalPower / maxSeptimalPower;
        }

        public void UpdateSeptimalPower()
        {
            if (septimalPower <= 0)
            {
                isOverheated = true;
            }
            if (septimalPower >= maxSeptimalPower)
            {
                isOverheated = false;
            }
            if (timeSinceSecondary < septima.SecondaryCooldownTime)
            {
                timeSinceSecondary++;
                secondaryInCooldown = true;
            }
            if (timeSinceSecondary >= septima.SecondaryCooldownTime)
            {
                secondaryInCooldown = false;
            }
            UpdateSeptimaForFirst();
            UpdateSeptimaForSecond();
        }

        public void UpdateSeptimaForFirst()
        {
            if (isUsingPrimaryAbility && !isOverheated)
            {
                if (septimalPower > 0) septimalPower -= septima.SpUsage;
                timeSinceAbility = 0;
            }
            if (!isUsingPrimaryAbility || isOverheated) timeSinceAbility++;
            if (timeSinceAbility >= 60)
            {
                if (!isOverheated)
                {
                    septimalPower += 2;
                }
                else
                {
                    septimalPower++;
                }
                if (septimalPower > maxSeptimalPower) septimalPower = maxSeptimalPower;
                timeSinceAbility = 60;
            }
        }

        public void UpdateSeptimaForSecond()
        {
            if (isUsingSecondaryAbility)
            {
                if (!secondaryInCooldown)
                {
                    secondaryInUse = true;
                } else
                {
                    if (timeSinceSecondary > septima.SecondaryCooldownTime) timeSinceSecondary = septima.SecondaryCooldownTime;
                    isUsingSecondaryAbility = false;
                }
            } else
            {
                if (timeSinceSecondary > septima.SecondaryCooldownTime) timeSinceSecondary = septima.SecondaryCooldownTime;
            }
        }

        public Septima GetSeptima(int choice)
        {
            return choice switch
            {
                0 => new AzureStriker(this, Player),
                1 => new AzureThunderclap(this, Player),
                _ => new AzureStriker(this, Player),
            };
        }

        public Septima GetSeptima(string name)
        {
            return name switch
            {
                "Azure Striker" => new AzureStriker(this, Player),
                "Azure Thunderclap" => new AzureThunderclap(this, Player),
                _ => new AzureStriker(this, Player),
            };
        }

        public bool GetOverheatedState()
        {
            return isOverheated;
        }
    }
}