using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hobbit.Framework.WebControl;
using Hobbit.Framework.Helper;
using Hobbit.Framework.Interface;
using System.Threading;

namespace Meridian.PageService
{
    struct BusinessPartner
    {
        public string PartnerType;
        public string LegalName;
        public string ShortName;
        public string Parent;
        public string DomicileCountry;
    }

    class BusinessPartnerService : IPageService
    {
        public void Create(BusinessPartner bp)
        {
            if (!string.IsNullOrEmpty(bp.PartnerType))
                ControlFactory.Create<WebList>("ParentTypeList").SelectByText(bp.PartnerType);
            if (!string.IsNullOrEmpty(bp.LegalName))
                ControlFactory.Create<WebEdit>("LegalNameEdit").Set(bp.LegalName);
            if (!string.IsNullOrEmpty(bp.ShortName))
                ControlFactory.Create<WebEdit>("ShortNameEdit").Set(bp.ShortName);
            if (!string.IsNullOrEmpty(bp.Parent))
                ControlFactory.Create<WebEdit>("ParentEdit").Set(bp.Parent);
            if (!string.IsNullOrEmpty(bp.DomicileCountry))
                ControlFactory.Create<WebEdit>("DomicileCountryEdit").Set(bp.DomicileCountry);
            ControlFactory.Create<WebLink>("SaveLink").Click();
        }

    }
}
