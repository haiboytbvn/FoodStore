using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eating2.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public MenuController()
        {

        }

        // GET: Menu
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return PartialView("~/Views/Shared/_StoreDishPartial.cshtml");
            }
            else
            {
                return PartialView("~/Views/Shared/_UnAuthenticatedPartial.cshtml");
            }
            //if (User.IsInRole(PrivilegedUsersConfig.AdminRole))
            //{
            //    return PartialView("~/Views/Shared/_AdminPartial.cshtml");
            //}

            //if (User.IsInRole(PrivilegedUsersConfig.TesterRole))
            //{
            //    return PartialView("~/Views/Shared/_TesterPartial.cshtml");
            //}

            //return PartialView("~/Views/Shared/_UnprivilegedPartial.cshtml");
        }
    }
}