using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hobbit.Framework.Interface;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;

namespace Hobbit.Framework.WebDriverProvider
{
    public class IEService : IWebDriverService
    {
        private static IWebDriver _webDriver = null;

        public IWebDriver CreateWebDriver(string driverLocation)
        {
            if (null == _webDriver)
            {
                _webDriver = new InternetExplorerDriver(driverLocation,
                    new InternetExplorerOptions
                    {
                        IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                    });
            }
            return _webDriver;
        }
    }
}
