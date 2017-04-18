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
using System.IO;
using Eating2.AppConfig;

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

            var store = new StoreViewModel();
            return View(store);
        }
        // POST: Store/Store/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name, Place, PhoneNumber, District")] StoreViewModel Store)
        {

            if (ModelState.IsValid)
            {
                Store.Owner = UserPresenterObject.FindUserByUserName(User.Identity.Name).ID;
                StorePresenterObject.InsertStore(Store);


                return RedirectToAction("Index");
            }
            return View();
        }

        //get
        public ActionResult Details(int? Id, string uploadMessage, string uploadState)
        {
            try
            {
                if (!Id.HasValue)
                {
                    throw new NotFoundException("Id was not valid.");
                }
                var Store = StorePresenterObject.GetStoreById(Id.Value);

                var folderPath = StorePresenterObject.GetStorePictureUrlForUpload(Store.Name, User.Identity.Name);
                Store.StorePictureURL = folderPath + "?time=" + DateTime.Now.Ticks.ToString();
                Store.HasStorePicture = Server.IsRelativePathExisted(folderPath);

                ViewBag.upload = uploadState;
                ViewBag.Message = uploadMessage;
               
               
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
        public ActionResult Edit(int id, [Bind(Include = "Name, Place, PhoneNumber, Description, OpenTime, CloseTime, ID, District")] StoreViewModel Store, string details)
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
                        Owner = UserPresenterObject.FindUserByUserName(User.Identity.Name).ID,
                        District = Store.District
                    };
                    StorePresenterObject.UpdateStore(id, updatedStore);
                    if (details == null)
                        return RedirectToAction("Index");
                    else
                        return RedirectToAction("Details", new { Id = id });



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

        public ActionResult AddFood(int id)
        {
            try
            {
                return RedirectToAction("Create", "Food", new { StoreID = id });
            }
            catch (NotFoundException e)
            {
                return View("ResultNotFoundError");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadStoreImage(HttpPostedFileBase file, int StoreId)
        {
            var store = StorePresenterObject.GetStoreById(StoreId);
            string message = "";
            string upload = "no";
            if (file != null)
            {
                var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (ext == ".jpg" || ext == ".png" || ext == ".jpeg" || ext == ".gif")
                {
                    var folderPath = StorePresenterObject.GetStorePictureUrlForUpload(store.Name, User.Identity.Name);
                    var serverPath = Server.MapPath(folderPath);
                    var dirs = Path.GetDirectoryName(serverPath);
                    if (!Directory.Exists(dirs))
                    {
                        Directory.CreateDirectory(dirs);
                    }
                    file.SaveAs(serverPath);
                    message = "Tải ảnh lên thành công!";
                    upload = "yes";
                }
                else
                {
                    store.HasStorePicture = false;
                    message = "Chỉ hỗ trợ tải lên ảnh có đuôi: jpg, jpeg, png, gif";
                }
            }
            else
            {
                store.HasStorePicture = false;
                message = "Có lỗi xảy ra! Tải ảnh lên không thành công.";
            }
            return RedirectToAction("Details", new { id = StoreId, uploadMessage = message, uploadState = upload});
        }
        //public ActionResult DisplayStoreImage(int StoreId)
        //{
        //    var store = StorePresenterObject.GetStoreById(StoreId);
        //    var folderPath = StorePresenterObject.GetStorePictureUrlForUpload(store.Name, User.Identity.Name);
        //    store.StorePictureURL = Path.Combine(folderPath, "store.jpg");
        //    return View("Details", store);
        //}
    }
}