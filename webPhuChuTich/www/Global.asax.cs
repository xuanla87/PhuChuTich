using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using www.Models;

namespace www
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Application["Totaluser"] = 0;
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Session_Start()
        {
            Application.Lock();
            HitCounters db = new HitCounters();
            db.AddHitCounter();
            Application["Totaluser"] = (int)Application["Totaluser"] + 1;
            Application.UnLock();
        }

        protected void Session_End()
        {
            Application.Lock();
            Application["Totaluser"] = (int)Application["Totaluser"] - 1;
            Application.UnLock();
        }
    }
}
