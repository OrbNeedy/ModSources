using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;
using practice14.Content.Items.Ammo;

namespace practice14.Content.Items.Weapons
{
	public class BasicGun : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Broken gun");
			Tooltip.SetDefault("Fixable.");
		}

		public override void SetDefaults()
		{
			Item.width = 40;
			Item.height = 30;
			Item.scale = 0.75f;
			Item.rare = ItemRarityID.Green;
			Item.value = 10000;

			Item.useTime = 18;
			Item.useAnimation = 18;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.autoReuse = true;

			Item.damage = 2;
			Item.DamageType = DamageClass.Ranged;
			Item.knockBack = 1;
			Item.noMelee = true;

			Item.UseSound = SoundID.Item1;

			Item.shoot = ProjectileID.PurificationPowder;
			Item.shootSpeed = 4f;
			Item.useAmmo = AmmoID.Bullet;
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			if (player.HasItem(ModContent.ItemType<PelletAmmo>()))
            {
				Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
				for (int i = 1; i < player.CountItem(ModContent.ItemType<PelletAmmo>(), 0); i++)
                {
					Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(360));
					Projectile.NewProjectile(source, position, newVelocity, type, damage, knockback, player.whoAmI);
                }
            }
            return false;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			Vector2 muzzleOffset = Vector2.Normalize(velocity) * 25f;

			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
		}

		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
		{
			Texture2D texture = Mod.Assets.Request<Texture2D>("Content/Items/Weapons/BasicGun_glowmask").Value;
			spriteBatch.Draw
			(
				texture,
				new Vector2
				(
					Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
					Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f + 2f
				),
				new Rectangle(0, 0, texture.Width, texture.Height),
				Color.White,
				rotation,
				texture.Size() * 0.5f,
				scale,
				SpriteEffects.None,
				0f
			);
		}

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DirtBlock, 1);
			recipe.Register();
		}
	}
}