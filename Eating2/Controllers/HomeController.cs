using Eating2.AppConfig;
using Eating2.Business.Presenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eating2.Controllers
{
    public class HomeController : Controller
    {
        private IFoodPresenter foodPresenterObject;
        protected IFoodPresenter FoodPresenterObject
        {
            get
            {
                if (foodPresenterObject == null)
                {
                    foodPresenterObject = new FoodPresenter(HttpContext);
                }
                return foodPresenterObject;
            }
        }

        private IUserPresenter userPresenterObject;
        protected IUserPresenter UserPresenterObject
        {
            get
            {
                if (userPresenterObject == null)
                {
                    userPresenterObject = new UserPresenter(HttpContext);
                }
                return userPresenterObject;
            }
        }

        private IStorePresenter storePresenterObject;
        protected IStorePresenter StorePresenterObject
        {
            get
            {
                if (storePresenterObject == null)
                {
                    storePresenterObject = new StorePresenter(HttpContext);
                }
                return storePresenterObject;
            }
        }

        private IRatePresenter ratePresenterObject;
        protected IRatePresenter RatePresenterObject
        {
            get
            {
                if (ratePresenterObject == null)
                {
                    ratePresenterObject = new RatePresenter(HttpContext);
                }
                return ratePresenterObject;
            }
        }

        public ActionResult Index()
        {
            return RedirectToAction("Search", "Home", new { search = 0 });
        }

        public ActionResult Search(int? search, [ModelBinder(typeof(FilterOptionsBinding))] FilterOptions filterOptions)
        {

            if (search.HasValue)
            {
                return View("Index");
            }

            if (filterOptions.Keyword == "")
            {
                ViewBag.Message = "Nhập cụm từ tìm kiếm !";
                return View("Index");
            }

            var foods = FoodPresenterObject.GetFoodsForSearch(filterOptions);
            if(foods.Count() == 0)
            {
                ViewBag.NoResult = "Không tìm thấy món ăn nào !";
                return View("Index");
            }
            return View("SearchResult", foods);

        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}