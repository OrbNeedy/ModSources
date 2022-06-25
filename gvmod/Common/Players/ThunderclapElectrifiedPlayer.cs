using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace gvmod.Common.Players
{
    public class ThunderclapElectrifiedPlayer : ModPlayer
    {
        public bool thunderclapElectricityDebuff;

        public override void ResetEffects()
        {
            thunderclapElectricityDebuff = false;
        }

        public override void UpdateBadLifeRegen()
        {
            if (thunderclapElectricityDebuff)
            {
                Player.lifeRegen -= 16;
                //TODO: Make this do knockback without making it a projectile
                //Vector2 kb = Player.position.RotatedBy(MathHelper.ToRadians(180));
            }
        }
    }
}
