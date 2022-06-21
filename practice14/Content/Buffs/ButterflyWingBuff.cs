using Terraria;
using Terraria.ModLoader;
using practice14.Content.Projectiles;

namespace practice14.Content.Buffs
{
    internal class ButterflyWingBuff : ModBuff
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Butterfly wing");
			Description.SetDefault("A magnificent butterfly wing will fight for you.");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			if (player.ownedProjectileCounts[ModContent.ProjectileType<ButterflyWingMinion>()] > 0)
			{
				player.buffTime[buffIndex] = 18000;
			}
			else
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
