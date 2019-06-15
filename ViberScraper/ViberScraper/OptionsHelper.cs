using System;
using System.IO;
using Newtonsoft.Json;
using ViberScraper.Models;

namespace ViberScraper
{
    public static class OptionsHelper
    {
        const string FILE_NAME = "scrapper-options.json";

        public static ScrapperOptions GetOptions()
        {
            var path = $"{Environment.CurrentDirectory}/{FILE_NAME}";
            var content = File.ReadAllText(path);
            var options = JsonConvert.DeserializeObject<ScrapperOptions>(content);
            return options;
        }
    }
}
