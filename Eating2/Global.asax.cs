using AutoMapper;
using Eating2.AppConfig;
using Eating2.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Eating2
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ModelBinders.Binders.Add(typeof(FilterOptions), new FilterOptionsBinding());
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new FoodMappingProfile("FoodMapping"));
            });
        }
    }
}
