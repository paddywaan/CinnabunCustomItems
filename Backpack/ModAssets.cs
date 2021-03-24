using UnityEngine;
using ValheimLib;

namespace Backpack
{
    public class ModAssets
    {
        public AssetBundle BackpackAssetBundle;
        public GameObject Backpack;
        private static ModAssets instance;
        public GameObject NotificationLayer;
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
            Backpack = BackpackAssetBundle.LoadAsset<GameObject>("Assets/Evie/IronBackpack/CapeIronBackpack.prefab");
            if(!Backpack) Main.log.LogError($"Failed to load backpack.");
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