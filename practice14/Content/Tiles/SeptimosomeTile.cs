using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using practice14.Content.Items.Placeable;

namespace practice14.Content.Tiles
{
	internal class SeptimosomeTile : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = false;
			ItemDrop = ModContent.ItemType<Septimosome>();
			AddMapEntry(new Color(200, 200, 200));
		}
	}
}
