using Terraria.GameContent.ItemDropRules;
using practice14.Content.Items.Weapons;
using Terraria.ModLoader;
using Terraria;

namespace practice14.Common.IteItemDropRules.Conditions
{
	public class WeaponCondition : IItemDropRuleCondition, IProvideItemConditionDescription
	{
		public bool CanDrop(DropAttemptInfo info)
		{
			if (!info.IsInSimulation)
            {
				if (Main.player[info.npc.lastInteraction].HasItem(ModContent.ItemType<BasicGun>()))
				{
					return true;
				}
			}
			return false;
		}

		public bool CanShowItemDropInUI()
		{
			return true;
		}

		public string GetConditionDescription()
		{
			return "Drops if player has basic gun during death";
		}
	}
}