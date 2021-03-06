using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;
using gvmod.Common.Systems;
using gvmod.Common.Players.Septimas;
using Terraria.ID;
using Terraria.ModLoader.IO;
using gvmod.Common.Players.Septimas.Abilities;
using System.Collections.Generic;
using System;

namespace gvmod.Common.Players
{
    public class AdeptPlayer : ModPlayer
    {
        public Septima septima;
        public float maxSeptimalPower;
        public float septimalPower;
        public float maxAbilityPower;
        public float abilityPower;
        public int level;
        public int experience;
        public int maxEXP;
        public int extraEXP;
        public List<string> activeSlot;
        //TODO: Make a method that changes these to increase the damage
        public float genericDamageMult;
        public float primaryDamageMult;
        public float secondaryDamageMult;
        public float specialDamageMult;

        public bool isUsingPrimaryAbility;
        public bool isUsingSecondaryAbility;
        public bool isUsingSpecialAbility;

        public bool secondaryInUse = false;
        public int timeSinceSecondary = 0;
        public bool secondaryInCooldown = false;
        public bool isOverheated = false;
        public int timeSincePrimary = 0;

        public override void Initialize()
        {
            base.Initialize();
            maxSeptimalPower = 300;
            septimalPower = maxSeptimalPower;
            maxAbilityPower = 3;
            abilityPower = maxAbilityPower;
            septima = GetSeptima(Main.rand.Next(2));
            level = 1;
            experience = 0;
            extraEXP = 0;
            maxEXP = 1999999999;
            genericDamageMult = 1;
            primaryDamageMult = 1;
            secondaryDamageMult = 1;
            specialDamageMult = 1;
            activeSlot = new List<string>() { "", "", "", ""};
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
            if (KeybindSystem.special1.JustPressed)
            {
                Special special1 = GetSpecial(activeSlot[0]);
                if (!special1.BeingUsed && abilityPower >= special1.ApUsage && !special1.InCooldown && (special1 != null || special1.Name != "") && !isUsingSpecialAbility)
                {
                    abilityPower -= special1.ApUsage;
                    special1.SpecialTimer = 0;
                }
            }
            if (KeybindSystem.special2.JustPressed)
            {
                Special special2 = GetSpecial(activeSlot[1]);
                if (!special2.BeingUsed && abilityPower >= special2.ApUsage && !special2.InCooldown && (special2 != null || special2.Name != "") && !isUsingSpecialAbility)
                {
                    abilityPower -= special2.ApUsage;
                    special2.SpecialTimer = 0;
                }
            }
            if (KeybindSystem.special3.JustPressed)
            {
                Special special3 = GetSpecial(activeSlot[2]);
                if (!special3.BeingUsed && abilityPower >= special3.ApUsage && !special3.InCooldown && (special3 != null || special3.Name != "") && !isUsingSpecialAbility)
                {
                    abilityPower -= special3.ApUsage;
                    special3.SpecialTimer = 0;
                }
            }
            if (KeybindSystem.special4.JustPressed)
            {
                Special special4 = GetSpecial(activeSlot[3]);
                if (!special4.BeingUsed && abilityPower >= special4.ApUsage && !special4.InCooldown && (special4 != null || special4.Name != "") && !isUsingSpecialAbility)
                {
                    abilityPower -= special4.ApUsage;
                    special4.SpecialTimer = 0;
                }
            }

        }

        public override void PostUpdateMiscEffects()
        {
            if (septimalPower <= 0 && timeSincePrimary <= 10)
            {
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDust(Player.position, 10, 10, DustID.Electric, 0, 0);
                }
            }
            if (isUsingPrimaryAbility && !isOverheated && !isUsingSpecialAbility)
            {
                septima.FirstAbilityEffects();
            }
            if (secondaryInUse && !isUsingSpecialAbility)
            {
                septima.SecondAbilityEffects();
            }
            if (isUsingSpecialAbility)
            {
                for (int i = 0; i < activeSlot.Count; i++)
                {
                    GetSpecial(activeSlot[i])?.Effects();
                }
            }
        }
        
        public override void PostUpdate()
        {
            if (isUsingPrimaryAbility && !isOverheated && !isUsingSpecialAbility)
            {
                septima.FirstAbility();
            }
            if (secondaryInUse && !isUsingSpecialAbility)
            {
                septima.SecondAbility();
            }
            if (isUsingSpecialAbility)
            {
                for (int i = 0; i < activeSlot.Count; i++)
                {
                    GetSpecial(activeSlot[i])?.Attack();
                }
            }
            UpdateSeptimalPower();
            maxEXP = (int)Math.Pow(level * 60, 1.8f);
            if (experience >= maxEXP)
            {
                level++;
                if (experience > maxEXP)
                {
                    extraEXP = experience - maxEXP;
                } 
                experience = 0;
            } else
            {
                experience += extraEXP;
                extraEXP = 0;
            }
            septima.Updates();
            septima.MiscEffects();
        }

        public void UpdateLevelMultipliers()
        {
            genericDamageMult = (1 + level * 0.2f);
            primaryDamageMult = (1 + level * 0.5f);
            secondaryDamageMult = (1 + level * 0.5f);
            specialDamageMult = (1 + level * 0.4f);
        }

        public float SeptimalPowerToFraction()
        {
            return septimalPower / maxSeptimalPower;
        }

        public float ExperienceToFraction()
        {
            float exp = experience;
            float maxExp = maxEXP;
            return exp / maxExp;
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
            if (abilityPower < maxAbilityPower)
            {
                abilityPower += (float)(1f/4020f);
            }
            UpdateSeptimaForFirst();
            UpdateSeptimaForSecond();
            UpdateSeptimaForSpecial();
        }

        public void UpdateSeptimaForFirst()
        {
            if (isUsingPrimaryAbility && !isOverheated)
            {
                if (septimalPower > 0) septimalPower -= septima.SpUsage;
                timeSincePrimary = 0;
            }
            if (!isUsingPrimaryAbility || isOverheated) timeSincePrimary++;
            if (timeSincePrimary >= 60)
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
                timeSincePrimary = 60;
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

        public void UpdateSeptimaForSpecial()
        {
            foreach (Special special in septima.Abilities)
            {
                special.Update();
            }
            if (isUsingSpecialAbility)
            {
                Player.immune = true;
                Player.mouseInterface = true;
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

        public Special GetSpecial(string name)
        {
            for (int i = 0; i < septima.Abilities.Count; i++)
            {
                Special temp = septima.Abilities[i];
                if (temp.Name == name)
                {
                    return temp;
                }
            }
            return null;
        }
    }
}