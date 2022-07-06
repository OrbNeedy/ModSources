using gvmod.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace gvmod.Content.Projectiles
{
    internal class ElectricSphere : ModProjectile
    {
        private int aiForm = 1;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Electric Sphere");
        }

        public override void SetDefaults()
        {
            Projectile.damage = 60;
            Projectile.knockBack = 8;
            Projectile.Size = new Vector2(42);
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.scale = 1f;
            Projectile.timeLeft = 1;
            Projectile.DamageType = DamageClass.Generic;
            Projectile.ownerHitCheck = false;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            switch (aiForm)
            {
                case 0:
                    Projectile.timeLeft = 150;
                    aiForm = -1;
                    break;
                case 1:
                    Projectile.timeLeft = 2;
                    aiForm = -1;
                    break;
            }
        }

        public override void OnSpawn(IEntitySource source)
        {
            base.OnSpawn(source);
            Player player = Main.player[Projectile.owner];
            
        }
    }
}
