using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hobbit.Framework.Interface;
using OpenQA.Selenium;
using System.Data;
using Microsoft.Practices.Unity;
using Hobbit.Framework.Exception;
using Hobbit.Framework.Enum;
using System.Collections.ObjectModel;
using Hobbit.Framework.Struct;

namespace Hobbit.Framework.WebControl
{
    public class WebControlBase : IWebControlBase, IParameterizableControl
    {
        private static IWebDriver _webDriver = null;
        [Dependency]
        public IWebDriver WebDriver
        {
            get
            {
                return _webDriver;
            }
            set
            {
                _webDriver = value;
            }
        }

        private static DataTable _controlInfo = null;
        [Dependency]
        public DataTable ControlInfo
        {
            get
            {
                return _controlInfo;
            }
            set
            {
                _controlInfo = value;
            }
        }

        public IWebElement Self { get; set; }

        public IWebElement GetWebElement(string controlName)
        {
            string findBy, value;
            ReturnedControlInfoByControlName(controlName, out findBy, out value);
            var target = GetWebElementByString(findBy, value);
            if (target != null)
                return target;
            else
            {
                string error = "FindBy: " + findBy + " Value: " + value + "\r\n";
                throw new NotFoundControlException(controlName, error);
            }
        }

        public IWebElement GetWebElement(FindBy by, string value)
        {
            return GetWebElementByString(by.ToString(), value);
        }

        public ReadOnlyCollection<IWebElement> GetWebElements(string controlName)
        {
            string findBy, value;
            ReturnedControlInfoByControlName(controlName, out findBy, out value);
            var target = GetWebElementsByString(findBy, value);
            if (target != null)
                return target;
            else
            {
                string error = "FindBy: " + findBy + " Value: " + value + "\r\n";
                throw new NotFoundControlException(controlName, error);
            }
        }

        public ReadOnlyCollection<IWebElement> GetWebElements(FindBy by, string value)
        {
            return GetWebElementsByString(by.ToString(), value);
        }

        private void ReturnedControlInfoByControlName(string controlName, out string findBy, out string value)
        {
            findBy = value = "";
            if (null == _controlInfo)
                throw new System.Exception("The control data should be loaded first!");
            try
            {
                IEnumerable<DataRow> query = from row in _controlInfo.AsEnumerable()
                                             where row.Field<string>(XlsControlInfoStruct.ControlName) == controlName
                                             select row;
                if (query.Count() != 0)
                {
                    DataTable dt = query.CopyToDataTable<DataRow>();
                    var firstRow = dt.Rows[0];
                    findBy = firstRow.Field<string>(XlsControlInfoStruct.FindBy);
                    value = firstRow.Field<string>(XlsControlInfoStruct.Value);
                }
                else
                {
                    throw new FailToLoadControlException(controlName);
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        private IWebElement GetWebElementByString(string findBy, string value)
        {
            var by = ConvertStringToBy(findBy, value);
            return (by != null) ? this.WebDriver.FindElement(by) : null;
        }

        private By ConvertStringToBy(string findBy, string value)
        {
            By by;
            switch (findBy)
            {
                case "Id":
                    by = By.Id(value); break;
                case "XPath":
                    by = By.XPath(value); break;
                case "Name":
                    by = By.Name(value); break;
                case "CssSelector":
                    by = By.CssSelector(value); break;
                case "ClassName":
                    by = By.ClassName(value); break;
                case "LinkText":
                    by = By.LinkText(value); break;
                case "PartialLinkText":
                    by = By.PartialLinkText(value); break;
                case "TagName":
                    by = By.TagName(value); break;
                default:
                    by = null; break;
            }
            return by;
        }

        private ReadOnlyCollection<IWebElement> GetWebElementsByString(string findBy, string value)
        {
            var by = ConvertStringToBy(findBy, value);
            return (by != null) ? this.WebDriver.FindElements(by) : null;
        }


    }
}
