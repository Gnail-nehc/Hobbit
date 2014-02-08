using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using Hobbit.Framework.Core;
using Hobbit.Framework.Enum;

namespace Hobbit.Framework.WebControl
{
    public class WebEdit : WebControlBase
    {
        private IWebElement self;
        
        public WebEdit(IWebElement element)
        {
            self = element;
        }

        public WebEdit(FindBy by, string value)
        {
            self = base.GetWebElement(by, value);
        }

        public WebEdit(string controlName)
        {
            self = base.GetWebElement(controlName);
        }

        public void Set(string text, string javaScript = "")
        {
            if (self.Displayed)
                self.SendKeys(text);
            else
            {
                IJavaScriptExecutor jsExe = base.WebDriver as IJavaScriptExecutor;
                if (string.IsNullOrEmpty(javaScript))
                {
                    javaScript = base.JsOfWebElement + ".value = '" + text + "'";
                }
                jsExe.ExecuteScript(javaScript);
            }
        }

        public string DisplayText
        {
            get
            {
                return self.Text;
            }
        }
    }
}
