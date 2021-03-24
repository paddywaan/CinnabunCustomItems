using System.Collections.Generic;
using UnityEngine;
using ValheimLib;
using ValheimLib.ODB;

namespace Backpack
{
    public class ModAssets
    {
        public AssetBundle BackpackAssetBundle;
        private GameObject BackpackPrefab;
        public CustomItem BackpackItem;
        public CustomRecipe BackpackRecipe;
        private const string BackPackToken = "$item_cape_ironbackpack";
        private const string BackPackName = "Rugged Backpack";
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
                var recipeIngredients = new List<Piece.Requirement>
                {
                    MockRequirement.Create("LeatherScraps", 10),
                    MockRequirement.Create("DeerHide", 2),
                    MockRequirement.Create("Iron", 4),
                };
                recipe.m_resources = recipeIngredients.ToArray();
                BackpackRecipe = new CustomRecipe(recipe, false, true);
                ObjectDBHelper.Add(BackpackRecipe);
                Language.AddToken(BackPackToken, BackPackName);
            }
            // SkillUp = Assets.LoadAsset<GameObject>("SkillUpFixed");
            // //var notify = SkillUp.GetComponent<SkillNotify>();
            //
            //
            //
            // if (!SkillUp) Main.log.LogError($"Asset loading failed for:{SkillUp}");
            // NotificationLayer = Assets.LoadAsset<GameObject>("NotificationLayer");
        }
    }
}