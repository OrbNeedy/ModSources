using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using gvmod.Content.Items.Drops;

namespace gvmod.Common.GlobalNPCs
{
    public class NPCExpLoot : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (!NPCID.Sets.CountsAsCritter[npc.type] && !npc.friendly)
            {
                int amount = (int)(((npc.defense * 0.1) + (npc.damage * 0.1)) * (npc.lifeMax * 0.02));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Experience>(), 1, amount, amount));
            }
        }
    }
}
