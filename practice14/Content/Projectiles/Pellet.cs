using Microsoft.Xna.Framework;
using practice14.Content.Buffs;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace practice14.Content.Projectiles
{
    internal class Pellet : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pellet");
        }

		public override void SetDefaults()
		{
			Projectile.width = 10;
			Projectile.height = 10;
			Projectile.friendly = true;
		}

        public override void AI()
        {
			Projectile.ai[0] += 1f;
			if (Projectile.ai[0] == 10)
            {
				Projectile.ai[1] += 1f;
            }
			if (Projectile.ai[0] >= 10f)
			{
				Projectile.ai[0] = 0f;
				Projectile.netUpdate = true;
				Projectile.velocity *= 0.8f;
			}
			if (Projectile.ai[1] >= 90)
            {
				Projectile.Kill();
            }
		}
    }
}
