using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using practice14.Content.Tiles;

namespace practice14.Content.Items.Placeable
{
    internal class Septimosome : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Septimosome");
            Tooltip.SetDefault("The remains of a defeated adept.");
        }

		public override void SetDefaults()
		{
			Item.width = 12;
			Item.height = 12;
			Item.maxStack = 999;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<SeptimosomeTile>();
		}
	}
}
