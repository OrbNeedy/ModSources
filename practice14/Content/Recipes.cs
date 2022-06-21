using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace practice14.Content
{
    internal class Recipes : ModSystem
    {
        public static RecipeGroup PracticeRecipeGroup;

        public override void Unload()
        {
            PracticeRecipeGroup = null;
        }

        public override void AddRecipes()
        {
            var resultItem = ModContent.GetInstance<Items.Ammo.PelletAmmo>();

            resultItem.CreateRecipe()
                .AddIngredient(ItemID.IronBar)
                .AddTile(TileID.DemonAltar)
                .AddCondition(NetworkText.FromKey("RecipeConditions.LowHealth"), r => Main.LocalPlayer.statLife < Main.LocalPlayer.statLifeMax / 2)
                .Register();
        }

        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];
                recipe.AddIngredient(ItemID.Acorn, 5);
            }
        }
    }
}
