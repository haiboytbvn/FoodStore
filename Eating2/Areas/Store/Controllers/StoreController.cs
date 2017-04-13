using Eating2.Business.Presenter;
using Eating2.Business.ViewModels;
using Eating2.Business.Presenter;
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
    public class StoreController : Controller
    {
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


        // GET: Store/Store
        public ActionResult Index()
        {

            var Stores = StorePresenterObject.ListAllStoreForOwner(User.Identity.GetUserId());
            return View("Index", Stores);
        }

        // GET: Store/Store/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: Store/Store/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name, Place, PhoneNumber")] StoreViewModel Store)
        {

            if (ModelState.IsValid)
            {
                Store.Owner = UserPresenterObject.FindUserByUserName(User.Identity.Name).ID;
                StorePresenterObject.InsertStore(Store);
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Details(int? Id)
        {
            try
            {
                if (!Id.HasValue)
                {
                    throw new NotFoundException("Id was not valid.");
                }
                var Store = StorePresenterObject.GetStoreById(Id.Value);
                return View("Details", Store);
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
                var updatedStore = StorePresenterObject.GetStoreById(id.Value);
                return View("Edit", updatedStore);
            }
            catch (NotFoundException e)
            {
                return View("ResultNotFoundError");
            }

        }

        // Post Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind(Include = "Name, Place, PhoneNumber, Description, OpenTime, CloseTime, ID")] StoreViewModel Store, string details)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var updatedStore = new StoreViewModel
                    {
                        Name = Store.Name,
                        Place = Store.Place,
                        PhoneNumber = Store.PhoneNumber,
                        Description = Store.Description,
                        OpenTime = Store.OpenTime,
                        CloseTime = Store.CloseTime,
                        Owner = UserPresenterObject.FindUserByUserName(User.Identity.Name).ID
                    };
                    StorePresenterObject.UpdateStore(id, updatedStore);
                    if (details == null)
                        return RedirectToAction("Index");
                    else
                        return RedirectToAction("Details",new { Id = id });
                    

                    
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
                //var deletedStore = new StoreViewModel();
                var deletedStore = StorePresenterObject.GetStoreById(id.Value);
                return View("Delete", deletedStore);
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
                StorePresenterObject.DeleteStore(id);
                return RedirectToAction("Index");
            }
            catch (NotFoundException e)
            {
                return View("ResultNotFoundError");
            }
        }

        public ActionResult AddFood(int? id)
        {
            try
            {
                return RedirectToAction("Create", "Food", new { StoreID = id.Value });
            }
            catch (NotFoundException e)
            {
                return View("ResultNotFoundError");
            }
        }
    }
}