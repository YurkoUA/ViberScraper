using ViberScraper.Models;
using ViberScraper.Output;

namespace ViberScraper.Observers
{
    public class ViberObserver : IContactObserver
    {
        private readonly IOutputMethod _outputMethod;

        public ViberObserver(IOutputMethod outputMethod, Scraper scraper)
        {
            _outputMethod = outputMethod;
        }

        public void Update(Contact contact)
        {
            _outputMethod.Write(contact);
        }
    }
}
