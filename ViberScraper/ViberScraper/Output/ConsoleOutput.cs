using System;
using ViberScraper.Models;

namespace ViberScraper.Output
{
    public class ConsoleOutput : IOutputMethod
    {
        public void Write(Contact contact)
        {
            Console.WriteLine($"Name: {contact.Name}, Number: {contact.PhoneNumber} ({DateTime.Now.ToString("G")})");
        }
    }
}
