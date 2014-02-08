using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using Hobbit.Framework.WebControl;
using System.Threading;
using System.Collections.ObjectModel;

namespace Hobbit.Framework.Extension
{
    public static class WaitUtil
    {
        public static IWebElement ReturnElementTileExist(this IWebElement control, Func<IWebElement> condition, int timeOutSecond = 30)
        {
            int sec = timeOutSecond;
            while (timeOutSecond > 0)
            {
                try
                {
                    if ((null != condition()))
                    {
                        control = condition();
                        return control;
                    }
                }
                catch { }
                Thread.Sleep(1000);
                timeOutSecond--;
            }
            throw new System.Exception("Not found target control after " + sec + " seconds.");
        }

        public static ReadOnlyCollection<IWebElement> ReturnElementsTileExist(this ReadOnlyCollection<IWebElement> controls,
            Func<ReadOnlyCollection<IWebElement>> condition, int timeOutSecond = 30)
        {
            int sec = timeOutSecond;
            while (timeOutSecond > 0)
            {
                try
                {
                    if ((null != condition()))
                    {
                        controls = condition();
                        return controls;
                    }
                }
                catch { }
                Thread.Sleep(1000);
                timeOutSecond--;
            }
            throw new System.Exception("Not found target controls after " + sec + " seconds.");
        }


        public static IWebElement ReturnElementTileEnable(this IWebElement control, Func<IWebElement> condition, int timeOutSecond = 30)
        {
            int sec = timeOutSecond;
            while (timeOutSecond > 0)
            {
                try
                {
                    if ((null != condition() && condition().Enabled))
                    {
                        control = condition();
                        return control;
                    }
                }
                catch { }
                Thread.Sleep(1000);
                timeOutSecond--;
            }
            throw new System.Exception("Not found target control after " + sec + " seconds.");
        }

        public static ReadOnlyCollection<IWebElement> ReturnElementsTileEnable(this ReadOnlyCollection<IWebElement> controls,
            Func<ReadOnlyCollection<IWebElement>> condition, int timeOutSecond = 30)
        {
            int sec = timeOutSecond;
            while (timeOutSecond > 0)
            {
                try
                {
                    if ((null != condition() && condition().First().Enabled))
                    {
                        controls = condition();
                        return controls;
                    }
                }
                catch { }
                Thread.Sleep(1000);
                timeOutSecond--;
            }
            throw new System.Exception("Not found target controls after " + sec + " seconds.");
        }

        public static void WaitForPageLoad(this IWebDriver driver, int timeOutSecond = 30)
        {
            int sec = timeOutSecond;
            while (!((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"))
            {
                if (timeOutSecond <= 0)
                    throw new System.Exception("Page load not complete after " + sec + " seconds.");
                Thread.Sleep(1000);
                timeOutSecond--;
            }
        }
    }
}
