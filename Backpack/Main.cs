using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.IO;
using System.Reflection;
using BepInEx.Configuration;
using Jotunn.Utils;

namespace Backpack
{
    [BepInDependency(Jotunn.Main.ModGuid)]
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
         
        public Main()
        {
            log = Logger;
            harmony = new Harmony(GUID);
            assembly = Assembly.GetExecutingAssembly();
            modFolder = Path.GetDirectoryName(assembly.Location);
            
        }

        public void Start()
        { 
            //Configs.genSettings = Config;
            Hooks.Init();
            ModAssets.Instance.Init();
            BoneReorder.ApplyOnEquipmentChanged();
        }
    }
}