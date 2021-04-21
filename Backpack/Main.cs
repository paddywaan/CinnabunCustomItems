using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.IO;
using System.Reflection;
using BepInEx.Configuration;
using JotunnLib.Utils;


namespace Backpack
{
    [BepInDependency(JotunnLib.Main.ModGuid)]
    [BepInPlugin(GUID, MODNAME, VERSION)]
    public class Main : BaseUnityPlugin
    {
        #region[Declarations]

        public const string
            MODNAME = "Backpacks",
            AUTHOR = "Cinnabun",
            GUID = AUTHOR + "_" + MODNAME,
            VERSION = "1.0.0";

        public static ManualLogSource log;
        internal readonly Harmony harmony;
        internal readonly Assembly assembly;
        public readonly string modFolder;

        #endregion

        private ConfigEntry<float> testEntry;
        public Main()
        {
            log = Logger;
            harmony = new Harmony(GUID);
            assembly = Assembly.GetExecutingAssembly();
            modFolder = Path.GetDirectoryName(assembly.Location);
            
        }

        public void Start()
        {
            testEntry = Config.Bind("Section", "Key", 1f, new ConfigDescription("Description", new AcceptableValueRange<float>(0f, 100f)));
            //Configs.genSettings = Config;
            Hooks.Init();
            ModAssets.Instance.Init();
            BoneReorder.ApplyOnEquipmentChanged();
        }
    }
}