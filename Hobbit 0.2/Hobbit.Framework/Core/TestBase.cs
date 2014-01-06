using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hobbit.Framework.WebControl;
using System.Configuration;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Hobbit.Framework.Interface;
using Hobbit.Framework.Enum;
using Hobbit.Framework.Helper;
using Microsoft.Practices.Unity;

namespace Hobbit.Framework.Core
{
    public class TestBase : TestModel
    {
        private static string browser = ConfigurationManager.AppSettings["BrowserInUse"];
        private static string loadType = ConfigurationManager.AppSettings["LoadType"];
        private static string projectId = ConfigurationManager.AppSettings["CurrentProjectID"];
        private static string dataSource = ConfigurationManager.AppSettings[loadType];
        private static string url = ConfigurationManager.AppSettings[projectId];
        private static IWebDriver _webDriver = null;

        protected TestBase(string testCaseId)
        {
            IDataModel global,test;DataTable control;
            DataInit(loadType, dataSource, projectId, testCaseId, out global, out test, out control);
            base.GlobalData = global;
            base.TestData = test;
            _webDriver = WebDriverInit(browser);
            InjectDependecies(_webDriver, control);
        }

        public IWebDriver Selenium2
        {
            get 
            {
                return _webDriver;
            }
        }

        [TestInitialize]
        public override void Init()
        {
            if (!url.ToLower().Contains("http://"))
                url = "http://" + url;
            _webDriver.Url = url;
        }

        [TestCleanup]
        public override void End()
        {
            _webDriver.Quit();
        }

        private void DataInit(string loadType, string dataSource, string projectId, string testCaseId,
            out IDataModel globalData, out IDataModel testData, out DataTable controlInfo)
        {
            LoadType lt;
            if ("db".Equals(loadType.ToLower()))
                lt = LoadType.DB;
            else
            {
                if ("excel".Equals(loadType.ToLower()))
                    lt = LoadType.Excel;
                else
                    lt = LoadType.Xml;
                dataSource = GetCurrentWorkingDir() + dataSource;
            }
            DataLoadProvider.LoadData(lt, dataSource, projectId, testCaseId, out globalData, out testData, out controlInfo);
        }

        private IWebDriver WebDriverInit(string browserType)
        {
            WebBrowser b;
            if ("IE".Equals(browserType, StringComparison.CurrentCultureIgnoreCase) ||
                "InternetExplorer".Equals(browserType, StringComparison.CurrentCultureIgnoreCase))
                b = WebBrowser.IE;
            else if ("Chrome".Equals(browserType, StringComparison.CurrentCultureIgnoreCase))
                b = WebBrowser.Chrome;
            else if ("Firefox".Equals(browserType, StringComparison.CurrentCultureIgnoreCase))
                b = WebBrowser.Firefox;
            else if ("Safari".Equals(browserType, StringComparison.CurrentCultureIgnoreCase))
                b = WebBrowser.Safari;
            else if ("Opera".Equals(browserType, StringComparison.CurrentCultureIgnoreCase))
                b = WebBrowser.Opera;
            else
                b = WebBrowser.IE;
            string baseDir = GetCurrentWorkingDir();
            baseDir += "Dependencies\\";
            return WebDriverFactory.Create(b, baseDir);
        }

        private string GetCurrentWorkingDir()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            if ("\\" != baseDir.Substring(baseDir.Length - 1, 1))
                baseDir += '\\';
            return baseDir;
        }

        private void InjectDependecies(IWebDriver driver, DataTable controlInfo)
        {
            IUnityContainer uc = new UnityContainer();
            uc.RegisterInstance<IWebDriver>(driver);
            uc.RegisterInstance<DataTable>(controlInfo);
            uc.RegisterType<IWebControlBase, WebControlBase>();
            uc.Resolve<IWebControlBase>();
        }
    }
}
