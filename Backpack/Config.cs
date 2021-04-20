using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backpack
{
    public static class Configs
    {
        public static ConfigFile genSettings;
        private static ConfigEntry<float> testSetting;

        static Configs()
        {
            genSettings = new ConfigFile(Path.Combine(BepInEx.Paths.ConfigPath, Main.GUID + ".cfg"), true);
            testSetting = genSettings.Bind("Section", "Key", 1f, new ConfigDescription("Description", new AcceptableValueRange<float>(0f, 100f)));
        }
    }
}