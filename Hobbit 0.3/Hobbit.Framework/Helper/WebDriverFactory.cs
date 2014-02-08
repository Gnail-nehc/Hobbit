using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using System.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Remote;
using Hobbit.Framework.Interface;
using Hobbit.Framework.Enum;
using Hobbit.Framework.WebDriverProvider;

namespace Hobbit.Framework.Helper
{
    public class WebDriverFactory
    {
        public static IWebDriver Create(Browser browser, string driverLocation)
        {
            switch (browser)
            {
                case Browser.IE:
                    return Create<IEService>(driverLocation);
                case Browser.Chrome:
                    return Create<ChromeService>(driverLocation);
                case Browser.Firefox:
                    return Create<FirefoxService>(driverLocation);
                case Browser.Safari:
                    return Create<SafariService>(driverLocation);
                case Browser.Opera:
                    return Create<OperaService>(driverLocation);
                default:
                    throw new System.Exception("Not found matched value for BrowserInUse in App.Config. Please provide value as following:\r\nIE,Chrome,Firefox,Safari,Opera");
            }
        }

        private static IWebDriver Create<T>(string driverLocation) where T : IWebDriverService
        {
            var service = (T)Activator.CreateInstance(typeof(T));
            return service.CreateWebDriver(driverLocation);
        }
    }
    
}
