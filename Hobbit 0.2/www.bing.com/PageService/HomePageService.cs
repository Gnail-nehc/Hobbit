using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hobbit.Framework;
using Hobbit.Framework.WebControl;
using Hobbit.Framework.Interface;
using Hobbit.Framework.Helper;

namespace www.bing.com.PageService
{
    class HomePageService : IPageService
    {
        public void Search(string content)
        {
            ControlFactory.Create<WebEdit>("SearchBox").Set(content);
            ControlFactory.Create<WebInputButton>("SearchBtn").Click();
        }

    }
}
