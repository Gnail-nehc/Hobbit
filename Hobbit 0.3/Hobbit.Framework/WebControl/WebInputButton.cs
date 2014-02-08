using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using Hobbit.Framework.Core;
using Hobbit.Framework.Enum;

namespace Hobbit.Framework.WebControl
{
    public class WebInputButton : WebControlBase
    {
        private IWebElement self;

        public WebInputButton(IWebElement element)
        {
            self = element;
        }

        public WebInputButton(FindBy by, string value)
        {
            self = base.GetWebElement(by, value);
        }

        public WebInputButton(string controlName)
        {
            self = base.GetWebElement(controlName);
        }

        public void Click()
        {
            self.Click();
        }

    }
}
