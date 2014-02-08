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
        public static IWebDriver Create(WebBrowser browser, string driverLocation)
        {
            switch (browser)
            {
                case WebBrowser.IE:
                    return Create<IEService>(driverLocation);
                case WebBrowser.Chrome:
                    return Create<ChromeService>(driverLocation);
                case WebBrowser.Firefox:
                    return Create<FirefoxService>(driverLocation);
                case WebBrowser.Safari:
                    return Create<SafariService>(driverLocation);
                case WebBrowser.Opera:
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
