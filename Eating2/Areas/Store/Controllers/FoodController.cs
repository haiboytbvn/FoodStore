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
using System.IO;
using Eating2.AppConfig;

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
        public ActionResult Details(int? Id, string uploadMessage, string uploadState)
        {
            try
            {
                if (!Id.HasValue)
                {
                    throw new NotFoundException("Id was not valid.");
                }
                var food = FoodPresenterObject.GetFoodById(Id.Value);
                var store = StorePresenterObject.GetStoreById(food.StoreID);
                //if (food.HasFoodPicture == false)
                //    food.numberOfFoodPicture = 0;
                food.listFoodPicturesURL = new List<Image>();
                for (int i = 0; i < 5; i++)
                {
                    var folderPath = FoodPresenterObject.GetFoodPictureUrlForUpload(food.Name, i, store.Name, User.Identity.Name);
                    var foodPictureURL = folderPath + "?time=" + DateTime.Now.Ticks.ToString();
                    var image = new Image
                    {
                        exist = Server.IsRelativePathExisted(folderPath),
                        number = i,
                        path = foodPictureURL
                    };
                    if (image.exist == true)
                    {
                        food.listFoodPicturesURL.Add(image);
                        food.HasFoodPicture = Server.IsRelativePathExisted(folderPath);
                    }
                }


                ViewBag.upload = uploadState;
                ViewBag.Message = uploadMessage;


                return View("Details", food);
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
                    if (details == null)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadFoodImage(HttpPostedFileBase file, int FoodId)
        {
            var food = FoodPresenterObject.GetFoodById(FoodId);
            var store = StorePresenterObject.GetStoreById(food.StoreID);
            string message = "";
            string upload = "no";

            if (file == null)
            {

                message = "Có lỗi xảy ra! Tải ảnh lên không thành công.";
                return RedirectToAction("Details", new { id = FoodId, uploadMessage = message, uploadState = upload });
            }

            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!(ext == ".jpg" || ext == ".png" || ext == ".jpeg" || ext == ".gif"))
            {
                message = "Chỉ hỗ trợ tải lên ảnh có đuôi: jpg, jpeg, png, gif";
                return RedirectToAction("Details", new { id = FoodId, uploadMessage = message, uploadState = upload });
            }

            

            int numberOfFoodImage = 0;
            while (numberOfFoodImage < 5)
            {
                var path = FoodPresenterObject.GetFoodPictureUrlForUpload(food.Name, numberOfFoodImage, store.Name, User.Identity.Name);
                if (Server.IsRelativePathExisted(path))
                {
                    numberOfFoodImage++;
                }
                else
                {
                    break;
                }
            }
            if (numberOfFoodImage == 5)
            {
                message = "Chỉ được phép tải lên không quá 5 ảnh!";
                return RedirectToAction("Details", new { id = FoodId, uploadMessage = message, uploadState = upload });
            }
            var folderPath = FoodPresenterObject.GetFoodPictureUrlForUpload(food.Name, numberOfFoodImage, store.Name, User.Identity.Name);
            var serverPath = Server.MapPath(folderPath);
            var dirs = Path.GetDirectoryName(serverPath);
            if (!Directory.Exists(dirs))
            {
                Directory.CreateDirectory(dirs);
            }
            file.SaveAs(serverPath);
            //food.numberOfFoodPicture++;
            //FoodPresenterObject.UpdateFood(food.ID, food);
            message = "Tải ảnh lên thành công!";
            upload = "yes";
            return RedirectToAction("Details", new { id = FoodId, uploadMessage = message, uploadState = upload });
        }

        public ActionResult RemoveFoodImage(int foodId, int number)
        {
            var food = FoodPresenterObject.GetFoodById(foodId);
            var store = StorePresenterObject.GetStoreById(food.StoreID);
            string message = "";
            string remove = "no";

            var folderPath = FoodPresenterObject.GetFoodPictureUrlForUpload(food.Name, number, store.Name, User.Identity.Name);
            var serverPath = Server.MapPath(folderPath);
            FileInfo deleteFile = new FileInfo(serverPath);
            if (!(deleteFile.Exists))
            {
                message = "Hình ảnh này không còn tồn tại!";
                remove = "no";
                return RedirectToAction("Details", new { id = foodId, uploadMessage = message, removeState = remove });
            }
            deleteFile.Delete();
            //food.numberOfFoodPicture--;
            //FoodPresenterObject.UpdateFood(food.ID, food);
            message = "Đã xóa hình ảnh";
            remove = "yes";
            return RedirectToAction("Details", new { id = foodId, uploadMessage = message, removeState = remove });
        }

    }
}