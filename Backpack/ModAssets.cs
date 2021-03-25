using System.Collections.Generic;
using UnityEngine;
using ValheimLib;
using ValheimLib.ODB;

namespace Backpack
{
    public class ModAssets
    {
        #region Localisation
        private const string BackpackToken = "$item_cape_ironbackpack",
             BackpackName = "Rugged Backpack",
             BackpackDescriptionToken = "$item_cape_ironbackpack_description",
             BackpackDescriptionText = "A Rugged backpack, complete with buckles and fine leather straps.";
        #endregion
        public AssetBundle BackpackAssetBundle;
        private GameObject BackpackPrefab;
        public CustomItem BackpackItem;
        public CustomRecipe BackpackRecipe;
        
        private static ModAssets instance;
        public static ModAssets Instance
        {
            get
            {
                if (instance == null) instance = new ModAssets();
                return instance;
            }
        
        }

        ModAssets()
        {
            LoadAssets();
        }

        private void LoadAssets()
        {
            BackpackAssetBundle = AssetBundle.LoadFromMemory(Properties.Resources.capeironbackpack);
            BackpackPrefab = BackpackAssetBundle.LoadAsset<GameObject>("Assets/Evie/CapeIronBackpack.prefab");
            if(!BackpackPrefab) Main.log.LogError($"Failed to load backpack.");
            else
            {
                BackpackItem = new CustomItem(BackpackPrefab, true);
                var recipe = ScriptableObject.CreateInstance<Recipe>();
                recipe.m_item = BackpackPrefab.GetComponent<ItemDrop>();
                recipe.m_craftingStation = Mock<CraftingStation>.Create("piece_workbench");
                var recipeIngredients = new List<Piece.Requirement>
                {
                    MockRequirement.Create("LeatherScraps", 10),
                    MockRequirement.Create("DeerHide", 2),
                    MockRequirement.Create("Iron", 4),
                };
                recipe.m_resources = recipeIngredients.ToArray();
                BackpackRecipe = new CustomRecipe(recipe, true, true);
                
                ObjectDBHelper.Add(BackpackRecipe);
                Language.AddToken(BackpackToken, BackpackName);
                Language.AddToken(BackpackDescriptionToken, BackpackDescriptionText);
            }
        }
    }
}