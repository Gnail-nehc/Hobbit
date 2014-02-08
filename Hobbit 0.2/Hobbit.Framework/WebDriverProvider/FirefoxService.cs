using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hobbit.Framework.Interface;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Hobbit.Framework.WebDriverProvider
{
    public class FirefoxService : IWebDriverService
    {
        private static IWebDriver _webDriver = null;

        public IWebDriver CreateWebDriver(string driverLocation)
        {
            if (null == _webDriver)
            {
                _webDriver = new FirefoxDriver();
            }
            return _webDriver;
        }

    }
}
