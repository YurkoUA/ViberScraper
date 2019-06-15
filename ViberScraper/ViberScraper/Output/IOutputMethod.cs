using ViberScraper.Models;

namespace ViberScraper.Output
{
    public interface IOutputMethod
    {
        void Write(Contact contact);
    }
}
