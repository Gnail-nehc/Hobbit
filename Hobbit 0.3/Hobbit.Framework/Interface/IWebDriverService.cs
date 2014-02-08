using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;

namespace Hobbit.Framework.Interface
{
    public interface IWebDriverService
    {
        IWebDriver CreateWebDriver(string driverLocation);
    }
}
