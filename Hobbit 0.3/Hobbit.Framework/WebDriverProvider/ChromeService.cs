using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hobbit.Framework.Interface;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Hobbit.Framework.WebDriverProvider
{
    public class ChromeService : IWebDriverService
    {
        private static IWebDriver _webDriver = null;

        public IWebDriver CreateWebDriver(string driverLocation)
        {
            if (null == _webDriver)
            {
                _webDriver = new ChromeDriver(driverLocation);
            }
            return _webDriver;
        }
    }
}
