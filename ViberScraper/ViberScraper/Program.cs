using System;
using ViberScraper.Observers;
using ViberScraper.Output;

namespace ViberScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            Scraper scraper = null;

            try
            {
                var outputFile = GetFileName();
                Console.WriteLine($"Output: {outputFile}");

                var options = OptionsHelper.GetOptions();
                scraper = new Scraper(options);
                scraper.AddObserver(new ViberObserver(new ConsoleOutput(), scraper));
                scraper.AddObserver(new ViberObserver(new CsvOutput(outputFile), scraper));

                scraper.Run();
                Console.WriteLine("Completed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                scraper?.Stop();
                Console.ReadKey();
            }
        }

        static string GetFileName()
        {
            var date = DateTime.Now.ToString("s").Replace(':', '-');
            var file = $"{Environment.CurrentDirectory}\\Results-{date}.csv";
            return file;
        }
    }
}
