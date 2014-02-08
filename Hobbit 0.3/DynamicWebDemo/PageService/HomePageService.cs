using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hobbit.Framework.Helper;
using Hobbit.Framework.WebControl;
using Hobbit.Framework.Interface;
using System.Threading;

namespace Meridian.PageService
{
    enum Menu
    { 
        Workplace,
        BusinessPartners
    }

    enum SubMenu
    {
        BusinessPartners,
        New,
    }

    class HomePageService : IPageService
    {
        public void NavigateTo(Menu menu, SubMenu subMenu)
        {
            if (ControlFactory.Create<WebDiv>("NavigationTour") != null)
            {
                ControlFactory.Create<WebLink>("CloseTourLink").Click();
            }
            string menuControlname = "";
            switch (menu)
            { 
                case Menu.Workplace:
                    menuControlname = "WorkplaceMenu";break;
                case Menu.BusinessPartners:
                    menuControlname = "BusinessPartnersMenu";break;
                default:
                    menuControlname = "BusinessPartnersMenu";break;
            }
            ControlFactory.Create<WebLink>(menuControlname).Click();
            switch (subMenu)
            {
                case SubMenu.BusinessPartners:
                    menuControlname = "BusinessPartnersSubMenu"; break;
                case SubMenu.New:
                    menuControlname = "NewSubMenu"; break;
                default:
                    menuControlname = "NewSubMenu"; break;
            }
            ControlFactory.Create<WebLink>(menuControlname).Click();
        }


    }
}
