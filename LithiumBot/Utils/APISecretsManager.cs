using System;
using System.IO;
using AHpx.Extensions.IOExtensions;
using AHpx.Extensions.JsonExtensions;
using AHpx.Extensions.StringExtensions;

namespace LithiumBot.Utils
{
    public static class APISecretsManager
    {
        private static readonly FileInfo ConfigFile =
            new(@$"{Directory.GetCurrentDirectory()}\Lithium\config\api_confidential.json");

        private static string ConfigFileContent
        {
            get => ConfigFile.ReadAllText();
            set => ConfigFile.WriteAllText(value);
        }

        static APISecretsManager()
        {
            if (!ConfigFile.Directory!.Exists)
            {
                ConfigFile.Directory.Create();
            }

            if (!ConfigFile.Exists)
            {
                ConfigFile.WriteAllText("{}");
            }

            if (!ConfigFile.ReadAllText().IsJsonString())
            {
                ConfigFile.WriteAllText("{}");
            }
        }

        /// <summary>
        /// key: musixmatch
        /// </summary>
        public static string MusixmatchKey
        {
            get => ConfigFileContent.Fetch("musixmatch");
            set
            {
                var json = ConfigFileContent.ToJObject();

                if (json.ContainsKey("musixmatch"))
                {
                    json["musixmatch"] = value;
                }
                else
                {
                    json.Add("musixmatch", value);
                }

                ConfigFileContent = json.ToString();
            }
        }
    }
}