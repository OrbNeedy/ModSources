using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using practice14.Content.Projectiles;

namespace practice14.Content.Items.Ammo
{
    public class PelletAmmo : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Stray pellets.");
        }

        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 12;
            Item.consumable = false;

            Item.damage = 12;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 0f;

            Item.value = 100;
            Item.maxStack = 10;
            Item.rare = ItemRarityID.Green;

            Item.shoot = ModContent.ProjectileType<Pellet>();
            Item.shootSpeed = 4f;
            Item.ammo = AmmoID.Bullet;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.DirtBlock, 1)
                .Register();
        }
    }
}