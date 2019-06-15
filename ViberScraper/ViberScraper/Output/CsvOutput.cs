using System;
using System.IO;
using System.Text;
using ViberScraper.Models;

namespace ViberScraper.Output
{
    public class CsvOutput : IOutputMethod
    {
        private readonly string _filePath;

        public CsvOutput(string filePath)
        {
            _filePath = filePath;
            PrepareFile();
        }

        public void Write(Contact contact)
        {
            var line = $"{contact.Name},{contact.PhoneNumber},{DateTime.Now.ToString("G")}";
            File.AppendAllLines(_filePath, new[] { line }, Encoding.Unicode);
        }

        private void PrepareFile()
        {
            File.Create(_filePath).Close();
            File.WriteAllLines(_filePath, new[] { $"Name,PhoneNumber,Date" }, Encoding.Unicode);
        }
    }
}
