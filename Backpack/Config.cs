using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backpack
{
    public static class Config
    {
        private static ConfigFile genSettings;

        static Config()
        {
            genSettings = new ConfigFile(Path.Combine(BepInEx.Paths.ConfigPath, Main.GUID + ".cfg"), true);
            genSettings.Bind("SETTINGS", "NEW SETTING", "", "Does things");
        }
    }
}