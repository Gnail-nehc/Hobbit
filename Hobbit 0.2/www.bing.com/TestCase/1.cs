using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using Hobbit.Framework;
using Hobbit.Framework.WebControl;
using System.Configuration;
using www.bing.com.PageService;
using Hobbit.Framework.Core;
using Hobbit.Framework.Helper;

namespace www.bing.com.TestCase
{
    [TestClass]
    public class _1 : TestBase
    {
        public _1() : base("1")
        {
        }

        [TestMethod]
        public void TC_1_HomePage()
        {
            PageServiceProvider.GetService<HomePageService>().Search(TestData["queryText"]);
        }

    }
    
}
