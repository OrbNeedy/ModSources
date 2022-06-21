using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using practice14.Content.Items.Placeable;
using practice14.Common.IteItemDropRules.Conditions;

namespace practice14.Common.GlobalNPCs
{
    public class Loot : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.CultistBoss)
            {
                IItemDropRule SeptimosomeDropRule = new LeadingConditionRule(new WeaponCondition());
                SeptimosomeDropRule.OnSuccess(new CommonDrop(ModContent.ItemType<Septimosome>(), 1, 11, 19));
                npcLoot.Add(SeptimosomeDropRule);
            }
        }
    }
}