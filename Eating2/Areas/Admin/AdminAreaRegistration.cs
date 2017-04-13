using System.Web.Mvc;

namespace Eating2.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "{get}Admin/{controller}/{action}/{id}",
                new { get="get", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}