using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using Hobbit.Framework.Enum;

namespace Hobbit.Framework.WebControl
{
    public class WebDiv : WebControlBase
    {
        private IWebElement self;

        public WebDiv(IWebElement element)
        {
            self = element;
        }

        public WebDiv(FindBy by, string value)
        {
            self = base.GetWebElement(by, value);
        }

        public WebDiv(string controlName)
        {
            self = base.GetWebElement(controlName);
        }

        public void Click()
        {
            self.Click();
        }
    }
}
