using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using System.Collections.ObjectModel;
using Hobbit.Framework.Enum;

namespace Hobbit.Framework.Interface
{
    public interface IWebControlBase
    {
        IWebDriver WebDriver { get; set; }

        IWebElement GetWebElement(string controlName);

        IWebElement GetWebElement(FindBy by, string value);

        ReadOnlyCollection<IWebElement> GetWebElements(string controlName);

        ReadOnlyCollection<IWebElement> GetWebElements(FindBy by, string value);

    }
}
