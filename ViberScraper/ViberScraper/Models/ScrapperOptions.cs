using System.Collections.Generic;

namespace ViberScraper.Models
{
    public class ScrapperOptions
    {
        public string ViberAppId { get; set; }
        public string WinAppDriverUrl { get; set; }
        
        public string TestPhoneNumber { get; set; }
        public string TestContactName { get; set; }

        public string CountryCode { get; set; }
        public IEnumerable<string> Codes { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
        public string Template { get; set; }
    }
}
