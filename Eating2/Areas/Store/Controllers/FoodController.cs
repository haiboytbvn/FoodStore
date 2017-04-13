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
    public class FoodController : Controller
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

        // GET: Food/Food
        public ActionResult Index(int storeId)
        {

            var Foods = FoodPresenterObject.ListAllFoodForStore(storeId);
            return View("Index", Foods);
        }

        // GET: Food/Food/Create
        public ActionResult Create(int STOREID)
        {
            ViewBag.STOREID = STOREID;
            return View();
        }
        // POST: Food/Food/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int STOREID, [Bind(Include = "Name, Cost, Processing,")] FoodViewModel Food)
        {

            if (ModelState.IsValid)
            {
                Food.StoreID = STOREID;
                FoodPresenterObject.InsertFood(Food);
                return RedirectToAction("Details", "Store", new { Id = STOREID });

            }
            return View();
        }

        //Get method
        public ActionResult Details(int? Id)
        {
            try
            {
                if (!Id.HasValue)
                {
                    throw new NotFoundException("Id was not valid.");
                }
                var Food = FoodPresenterObject.GetFoodById(Id.Value);
                
                return View("Details", Food);
            }
            catch (NotFoundException e)
            {
                return View("ResultNotFoundError");
            }

        }

        //Get method
        public ActionResult Edit(int? id, string beforePage)
        {
            try
            {
                ViewBag.beforePage = beforePage;
                if (!id.HasValue)
                {
                    throw new NotFoundException("Id was not valid.");
                }
                var updatedFood = FoodPresenterObject.GetFoodById(id.Value);
                return View("Edit", updatedFood);
            }
            catch (NotFoundException e)
            {
                return View("ResultNotFoundError");
            }

        }

        // Post Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind(Include = "ID, Name, Cost, StoreID ")] FoodViewModel Food, string details)
        {
            try
            {
                int storeID = FoodPresenterObject.GetFoodById(id).StoreID;
                if (ModelState.IsValid)
                {
                    var updatedFood = new FoodViewModel
                    {
                        ID = Food.ID,
                        Name = Food.Name,
                        Cost = Food.Cost,
                        StoreID = storeID,
                    };
                    FoodPresenterObject.UpdateFood(id, updatedFood);
                    if(details == null)
                        return RedirectToAction("Details", "Store", new { Id = storeID });
                    else
                        return RedirectToAction("Details", "Food", new { Id = id });
                }
                return View();
            }
            catch (NotFoundException e)
            {
                return View("ResultNotFoundError");
            }
        }

        //Get method
        public ActionResult Delete(int? id)
        {
            try
            {
                if (!id.HasValue)
                {
                    throw new NotFoundException("Id was not valid.");
                }
                //var deletedFood = new FoodViewModel();
                var deletedFood = FoodPresenterObject.GetFoodById(id.Value);
                return View("Delete", deletedFood);
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
                int storeID = FoodPresenterObject.GetFoodById(id).StoreID;
                FoodPresenterObject.DeleteFood(id);
                return RedirectToAction("Details", "Store", new { Id = storeID });
            }
            catch (NotFoundException e)
            {
                return View("ResultNotFoundError");
            }
        }
    }
}