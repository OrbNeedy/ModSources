using Terraria;
using Terraria.ModLoader;

namespace gvmod.Common.Players
{
    public class AzureElectrifiedPlayer : ModPlayer
    {
        public bool azureElectricityDebuff;

        public override void ResetEffects()
        {
            azureElectricityDebuff = false;
        }

        public override void UpdateBadLifeRegen()
        {
            if (azureElectricityDebuff)
            {
                Player.lifeRegen -= 8;
            }
        }
    }
}
