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
using System.Configuration;
using Hobbit.Framework.Extension;

namespace Hobbit.Framework.WebControl
{
    public class WebControlBase : IWebControlBase, IParameterizableControl
    {
        private static IWebDriver _webDriver = null;
        private static string seperator = ConfigurationManager.AppSettings["SeperatorForQueryIframeInside"];
        
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

        public bool Displayed
        {
            get
            {
                return Self.Displayed;
            }
        }

        public string JsOfWebElement { get; set; }

        public IWebElement GetWebElement(string controlName)
        {
            string findBy, value;
            ReturnedControlInfoByControlName(controlName, out findBy, out value);
            var target = GetWebElementByString(findBy, value);
            if (target != null)
            {
                return target;
            }
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

        public void ReturnedControlInfoByControlName(string controlName, out string findBy, out string value)
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
        
        private By ConvertStringToBy(string findBy, string value)
        {
            value = value.Trim();
            By by;
            switch (findBy)
            {
                case "Id":
                    by = By.Id(value);
                    JsOfWebElement = "document.getElementBy" + findBy + "(" + value + ")";
                    break;
                case "XPath":
                    by = By.XPath(value);
                    value=value.Replace("'","\"");
                    JsOfWebElement = "document.evaluate('" + value + "' ,document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue";
                    break;
                case "Name":
                    by = By.Name(value);
                    JsOfWebElement = "document.getElementBy" + findBy + "(" + value + ")";
                    break;
                case "CssSelector":
                    by = By.CssSelector(value); break;
                case "ClassName":
                    by = By.ClassName(value);
                    JsOfWebElement = "document.getElementBy" + findBy + "(" + value + ")";
                    break;
                case "LinkText":
                    by = By.LinkText(value); break;
                case "PartialLinkText":
                    by = By.PartialLinkText(value); break;
                case "TagName":
                    by = By.TagName(value);
                    JsOfWebElement = "document.getElementBy" + findBy + "(" + value + ")[0]";
                    break;
                default:
                    by = null;
                    string[] xpaths = value.Split(seperator.ToArray(), StringSplitOptions.RemoveEmptyEntries);
                    //string firstXpath = xpaths[0].Replace("'", "\"");
                    //string insideDoc = "document.evaluate('" + firstXpath + "' ,document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue.contentDocument";
                    string secondXpath = xpaths[1].Replace("'", "\"");
                    JsOfWebElement = "document.evaluate('" + secondXpath + "' , document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue";
                    break;
            }
            return by;
        }

        private IWebElement GetWebElementByString(string findBy, string value)
        {
            this.WebDriver = this.WebDriver.SwitchTo().DefaultContent();
            var by = ConvertStringToBy(findBy, value);
            if (null != by)
            {
                return this.Self.ReturnElementTileExist(() => this.WebDriver.FindElement(by));
            }
            if (value.Contains(seperator))
            {
                var arr = value.Split(seperator.ToArray(), StringSplitOptions.RemoveEmptyEntries);
                IWebElement frame = default(IWebElement);
                frame = frame.ReturnElementTileExist(() => this.WebDriver.FindElement(By.XPath(arr[0].Trim())));
                this.WebDriver = this.WebDriver.SwitchTo().Frame(frame);
                return this.Self.ReturnElementTileExist(() => this.WebDriver.FindElement(By.XPath(arr[1].Trim())));
            }
            else
                throw new System.Exception("\r\nWrong format of query string for IframeInside, lack of " + seperator);
        }

        private ReadOnlyCollection<IWebElement> GetWebElementsByString(string findBy, string value)
        {
            ReadOnlyCollection<IWebElement> controls = default(ReadOnlyCollection<IWebElement>);
            this.WebDriver = this.WebDriver.SwitchTo().DefaultContent();
            var by = ConvertStringToBy(findBy, value);
            if (null != by)
            {
                return controls.ReturnElementsTileExist(() => this.WebDriver.FindElements(by));
            }
            if (value.Contains(seperator))
            {
                var arr = value.Split(seperator.ToArray(), StringSplitOptions.RemoveEmptyEntries);
                IWebElement frame = default(IWebElement);
                frame = frame.ReturnElementTileExist(() => this.WebDriver.FindElement(By.XPath(arr[0].Trim())));
                this.WebDriver = this.WebDriver.SwitchTo().Frame(frame);
                return controls.ReturnElementsTileExist(() => this.WebDriver.FindElements(By.XPath(arr[1].Trim())));
            }
            else
                throw new System.Exception("\r\nWrong format of query string for IframeInside, lack of " + seperator);
        }


    }
}
