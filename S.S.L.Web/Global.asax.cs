﻿using Ninject.Mvc;
using S.S.L.Web.Infrastructure;
using System.Web.Mvc;
using System.Web.Routing;

namespace S.S.L.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            NinjectContainer.RegisterAssembly();
            DatabaseMigrator.UpdateDatabase();
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
