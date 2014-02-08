using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hobbit.Framework.Core;
using Hobbit.Framework.Helper;
using Meridian.PageService;
using System.Threading;

namespace Meridian.TestCase
{
    [TestClass]
    public class _6749 : TestBase
    {
        public _6749() : base("6749")
        {
        }

        [TestMethod]
        public void TC_6749_CreateBusinessPartner()
        {
            this.Log.ScenarioStart(1, "Go to New -> Business Partners.");
            PageServiceProvider.GetService<HomePageService>().NavigateTo(Menu.BusinessPartners, SubMenu.New);
            this.Log.ScenarioEnd(1);

            this.Log.ScenarioStart(2, "Create a Business Partners.");
            PageServiceProvider.GetService<BusinessPartnerService>().Create(new BusinessPartner
            {
                PartnerType = TestData["parentType"],
                LegalName = TestData["legalName"],
                DomicileCountry = TestData["domicileCountry"]
            });
            this.Log.ScenarioEnd(2);
        }
    }
}
