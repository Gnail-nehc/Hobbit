using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hobbit.Framework.Core;
using Hobbit.Framework.Enum;
using OpenQA.Selenium;

namespace Hobbit.Framework.WebControl
{
    public class WebCheckBox : WebControlBase
    {
        private IWebElement self;

        public WebCheckBox(IWebElement element)
        {
            self = element;
        }

        public WebCheckBox(FindBy by, string value)
        {
            self = base.GetWebElement(by, value);
        }

        public WebCheckBox(string controlName)
        {
            self = base.GetWebElement(controlName);
        }

        public bool IsSelected
        {
            get
            {
                return self.Selected;
            }
        }

        public void Check(bool isCheckin)
        {
            if (isCheckin)
                if (!IsSelected)
                    self.Click();
            else
                if(IsSelected)
                    self.Click();
        }

    }
}
