using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using Hobbit.Framework.Enum;
using Hobbit.Framework.Extension;

namespace Hobbit.Framework.WebControl
{
    public class WebLink : WebControlBase
    {
        private IWebElement self;

        public WebLink(IWebElement element)
        {
            self = element;
        }

        public WebLink(FindBy by, string value)
        {
            self = self.ReturnElementTileEnable(() => base.GetWebElement(by, value));
        }

        public WebLink(string controlName)
        {
            self = self.ReturnElementTileEnable(() => base.GetWebElement(controlName));
        }

        public void Click()
        {
            self.Click();
        }
        
    }
}
