using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hobbit.Framework.Interface;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Hobbit.Framework.WebDriverProvider
{
    public class OperaService : IWebDriverService
    {
        private static IWebDriver _webDriver = null;

        public IWebDriver CreateWebDriver(string driverLocation)
        {
            if (null == _webDriver)
            {
                DesiredCapabilities opera = DesiredCapabilities.Opera();
                opera.SetCapability("opera.profile", driverLocation);
                _webDriver = new RemoteWebDriver(opera);
            }
            return _webDriver;
        }

    }
}
