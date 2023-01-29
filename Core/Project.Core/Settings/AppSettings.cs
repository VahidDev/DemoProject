using Newtonsoft.Json;
using System;
using System.IO;

namespace Project.Core.Settings
{
    public class AppSettings
    {
        private const string JsonFileName = "settings.json";

        private readonly static string JsonFilePath =  Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Settings",JsonFileName);

        public static AppSettings Settings { get; private set; }
        public DbConnectionModel AppDbConnectionModel { get; set; }

        private AppSettings()
        {

        }

        static AppSettings()
        {
            ReloadSettings();
        }

        private static void ReloadSettings()
        {
            var serializer = new JsonSerializer();

            using (var sr = new StreamReader(JsonFilePath))
            using (var reader = new JsonTextReader(sr))
            {
                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.StartObject)
                    {
                        Settings = serializer.Deserialize<AppSettings>(reader);
                    }
                }
            }
        }
    }
}
