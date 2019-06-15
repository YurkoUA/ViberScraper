using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;
using ViberScraper.Models;
using ViberScraper.Observers;

namespace ViberScraper
{
    public class Scraper
    {
        private readonly ScrapperOptions _options;
        private readonly List<IContactObserver> _observers = new List<IContactObserver>();

        private WindowsDriver<WindowsElement> _session = null;
        private WindowsElement _searchTextBox = null;

        private string _contactControlId;
        private IEnumerable<string> _numbersPool;

        public Scraper(ScrapperOptions options)
        {
            _options = options;
        }

        public void AddObserver(IContactObserver observer)
        {
            _observers.Add(observer);
        }

        public void Run()
        {
            var appCapabilities = new DesiredCapabilities();
            appCapabilities.SetCapability("app", _options.ViberAppId);
            _session = new WindowsDriver<WindowsElement>(new Uri(_options.WinAppDriverUrl), appCapabilities);
            _session.LaunchApp();

            Thread.Sleep(5000);

            _session.FindElementByAccessibilityId("SearchIcon").Click();
            _searchTextBox = _session.FindElementByAccessibilityId("SearchTextBox");

            FindContactControlId();
            ProceedNumbers();
        }

        public void Stop()
        {
            _session?.CloseApp();
        }

        private void ProceedNumbers()
        {
            _numbersPool = GetPhoneNumbers();

            foreach (var num in _numbersPool)
            {
                InputNumber(num);
                Thread.Sleep(3000);

                var name = GetContactName();

                if (!string.IsNullOrEmpty(name))
                {
                    OnFind(name, num);
                }

                ClearNumber();
            }
        }

        private IEnumerable<string> GetPhoneNumbers()
        {
            var count = _options.End - _options.Start + 1;
            var range = Enumerable.Range(_options.Start, count);
            var numbers = new List<string>();

            foreach (var code in _options.Codes)
            {
                numbers.AddRange(range.Select(n => CreateConcreteNumber(code, n)));
            }

            return numbers;
        }

        private string CreateConcreteNumber(string code, int index)
        {
            const int NUM_LENGTH = 7;

            var numBuilder = new StringBuilder();
            numBuilder.Append(string.Format(_options.Template, index));

            while (numBuilder.Length < NUM_LENGTH)
            {
                numBuilder.Insert(0, "0");
            }

            numBuilder.Insert(0, _options.CountryCode + code);
            return numBuilder.ToString();
        }

        private void InputNumber(string number)
        {
            _searchTextBox.SendKeys(number);
        }

        private void ClearNumber()
        {
            _searchTextBox.Clear();
        }

        private void FindContactControlId()
        {
            InputNumber(_options.TestPhoneNumber);
            Thread.Sleep(6000);
            _contactControlId = _session.FindElementsByClassName("TextBlock")
                .FirstOrDefault(e => e.Text.Contains(_options.TestContactName)).Id;
            ClearNumber();
        }

        private void OnFind(string name, string number)
        {
            _observers.ForEach(o => o.Update(new Contact { Name = name, PhoneNumber = number }));
        }

        private string GetContactName()
        {
            var control = _session.FindElementById(_contactControlId);
            return control.Displayed ? control.Text : null;
        }
    }
}
