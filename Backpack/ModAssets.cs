using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using UnityEngine;
using JotunnLib;
using JotunnLib.Entities;
using Resources = Backpack.Properties.Resources;

namespace Backpack
{
    public class ModAssets
    {
        private static ModAssets instance;
        public static ModAssets Instance
        {
            get
            {
                if (instance == null) instance = new ModAssets();
                return instance;
            }
        
        }
        
        private GameObject IronBackpackPrefab;

        private GameObject SilverBackpackPrefab;

        ModAssets()
        {
           
        }
        

        public void Init()
        {
            var ab = AssetBundle.LoadFromMemory(Properties.Resources.eviesbackpacks);
            IronBackpackPrefab = InitPrefab(ab,
                "Assets/Evie/CapeIronBackpack.prefab");
            LoadCraftedItem(IronBackpackPrefab, new List<Piece.Requirement>
            {
                MockRequirement.Create("LeatherScraps", 10),
                MockRequirement.Create("DeerHide", 2),
                MockRequirement.Create("Iron", 4),
            });
            SilverBackpackPrefab = InitPrefab(ab,
                "Assets/Evie/CapeSilverBackpack.prefab");
            LoadCraftedItem(SilverBackpackPrefab, new List<Piece.Requirement>
            {
                MockRequirement.Create("LeatherScraps", 5),
                MockRequirement.Create("DeerHide", 10),
                MockRequirement.Create("Silver", 4),
            });
            InitLocalisation();
        }

        private GameObject InitPrefab(AssetBundle ab, string loc)
        {
            var prefab = ab.LoadAsset<GameObject>(loc);
            if(!prefab) Main.log.LogWarning($"Failed to load prefab: {loc}");
            return prefab;
        }

        private void LoadCraftedItem(GameObject prefab, List<Piece.Requirement> ingredients, string craftingStation = "piece_workbench")
        {
            if(prefab) 
            {
                var CI = new CustomItem(prefab, true);
                var recipe = ScriptableObject.CreateInstance<Recipe>();
                recipe.m_item = prefab.GetComponent<ItemDrop>();
                recipe.m_craftingStation = Mock<CraftingStation>.Create(craftingStation);
                recipe.m_resources = ingredients.ToArray();
                var CR = new CustomRecipe(recipe, true, true);
                JotunnLib.Managers.ItemManager.Instance.AddItem(CI);
                JotunnLib.Managers.ItemManager.Instance.AddRecipe(CR);
                Main.log.LogDebug($"Successfully loaded new CraftedItem {prefab.name} for {craftingStation}.");
            }
        }

        private static void InitLocalisation()
        {
            ResourceSet resourceSet =   Resources.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            foreach (DictionaryEntry token in resourceSet)
            {
                if (token.Key.ToString().StartsWith("$"))
                {
                    JotunnLib.Managers.LocalizationManager.Instance.AddToken(token.Key.ToString(), token.Value.ToString(), false);
                    Main.log.LogDebug($"Added language token for {token.Key}:{token.Value}");
                }
            }
        }
    }
}