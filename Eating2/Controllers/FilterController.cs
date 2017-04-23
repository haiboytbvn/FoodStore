using Eating2.AppConfig;
using Eating2.Business.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eating2.Controllers
{
    public class FilterController : Controller
    {
        public ActionResult FilterForFood([ModelBinder(typeof(FilterOptionsBinding))] FilterOptions filterOptions)
        {
            var filterFields = new List<FilterOptionViewModel>();
            filterFields.Add(new FilterOptionViewModel
            {
                Name = "Name",
                DisplayName = "Món ăn",
                IsChecked = filterOptions.FilterFields.Length > 0 ? filterOptions.FilterFields.FirstOrDefault(f => f == "Name") != null : true
            });

            
            //filterFields.Add(new KeyValuePair<string, string>("Team", "Team"));
            var viewModel = new FilterViewModel
            {
                Controller = "Home",
                Action = "Search",
                Area = "",
                FilterFields = filterFields,
                FilterOptions = filterOptions
            };
            return PartialView("~/Views/Shared/Filter/_FilterPartialView.cshtml", viewModel);
        }
    }
}