using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hobbit.Framework.Enum;
using OpenQA.Selenium;
using Hobbit.Framework.Core;
using OpenQA.Selenium.Support.UI;

namespace Hobbit.Framework.WebControl
{
    public class WebList : WebControlBase
    {
        private SelectElement selection;

        public WebList(IWebElement element)
        {
            this.Self = element;
            selection = new SelectElement(Self);
        }

        public WebList(FindBy by, string value)
        {
            this.Self = base.GetWebElement(by, value);
            selection = new SelectElement(Self);
        }

        public WebList(string controlName)
        {
            this.Self = base.GetWebElement(controlName);
            selection = new SelectElement(Self);
        }

        public void Select(int index)
        {
            selection = new SelectElement(Self);
            selection.SelectByIndex(index);
        }

        public void SelectByText(string content)
        {
            selection.SelectByText(content);
        }

        public void SelectByValue(string content)
        {
            selection.SelectByValue(content);
        }

        public string DisplayText
        { 
            get
            {
                return selection.SelectedOption.Text;
            }
        }
    }

}
