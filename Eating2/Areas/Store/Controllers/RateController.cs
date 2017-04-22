using Eating2.Business.Presenter;
using Eating2.Business.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eating2.DataAcess.Repositories;
using Eating2.Exception;
using Microsoft.AspNet.Identity;
using Eating2.DataAcess.Models;

namespace Eating2.Areas.Store.Controllers
{
    [Authorize]
    public class RateController : Controller
    {
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

        // GET: Rate/Rate
        public ActionResult Index(int foodId)
        {

            var Rates = RatePresenterObject.ListAllRateForFood(foodId);
            return View("Index", Rates);
        }

        // GET: Rate/Rate/Create
        //public ActionResult Create(int FoodID)
        //{
        //    ViewBag.FoodID = FoodID;
        //    return View();
        //}
        // POST: Rate/Rate/Create
        [HttpPost]
      
        public ActionResult Create(int id, [Bind(Include = "Point, Comment, Customer")] RateViewModel Rate)
        {

            if (ModelState.IsValid)
            {
                Rate.FoodID = id;
                RatePresenterObject.InsertRate(Rate);
                return RedirectToAction("Details", "Food", new { Id = id });

            }
            return View();
        }

        //Get method
        //public ActionResult Details(int? Id)
        //{
        //    try
        //    {
        //        if (!Id.HasValue)
        //        {
        //            throw new NotFoundException("Id was not valid.");
        //        }
        //        var Rate = RatePresenterObject.GetRateById(Id.Value);

        //        return View("Details", Rate);
        //    }
        //    catch (NotFoundException e)
        //    {
        //        return View("ResultNotFoundError");
        //    }

        //}

        //Get method
        //public ActionResult Edit(int? id, string beforePage)
        //{
        //    try
        //    {
        //        ViewBag.beforePage = beforePage;
        //        if (!id.HasValue)
        //        {
        //            throw new NotFoundException("Id was not valid.");
        //        }
        //        var updatedRate = RatePresenterObject.GetRateById(id.Value);
        //        return View("Edit", updatedRate);
        //    }
        //    catch (NotFoundException e)
        //    {
        //        return View("ResultNotFoundError");
        //    }

        //}

        // Post Method
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, [Bind(Include = "ID, Name, Cost, FoodID ")] RateViewModel Rate, string details)
        //{
        //    try
        //    {
        //        int foodID = RatePresenterObject.GetRateById(id).FoodID;
        //        if (ModelState.IsValid)
        //        {
        //            var updatedRate = new RateViewModel
        //            {
        //                ID = Rate.ID,
        //                Name = Rate.Name,
        //                Cost = Rate.Cost,
        //                FoodID = foodID,
        //            };
        //            RatePresenterObject.UpdateRate(id, updatedRate);
        //            if(details == null)
        //                return RedirectToAction("Details", "Food", new { Id = foodID });
        //            else
        //                return RedirectToAction("Details", "Rate", new { Id = id });
        //        }
        //        return View();
        //    }
        //    catch (NotFoundException e)
        //    {
        //        return View("ResultNotFoundError");
        //    }
        //}

        //Get method delete
        public ActionResult Delete(int? id)
        {
            try
            {
                if (!id.HasValue)
                {
                    throw new NotFoundException("Id was not valid.");
                }
                //var deletedRate = new RateViewModel();
                var deletedRate = RatePresenterObject.GetRateById(id.Value);
                return View("Delete", deletedRate);
            }
            catch (NotFoundException e)
            {
                return View("ResultNotFoundError");
            }
        }

        //Post Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                int foodID = RatePresenterObject.GetRateById(id).FoodID;
                RatePresenterObject.DeleteRate(id);
                return RedirectToAction("Details", "Food", new { Id = foodID });
            }
            catch (NotFoundException e)
            {
                return View("ResultNotFoundError");
            }
        }
    }
}