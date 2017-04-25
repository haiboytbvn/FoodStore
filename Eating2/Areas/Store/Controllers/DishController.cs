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

namespace Eating2.Areas.Dish.Controllers
{
    [Authorize]
    public class DishController : Controller
    {
        private IDishPresenter dishPresenterObject;
        protected IDishPresenter DishPresenterObject
        {
            get
            {
                if (dishPresenterObject == null)
                {
                    dishPresenterObject = new DishPresenter(HttpContext);
                }
                return dishPresenterObject;
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


        // GET: Store/Dish
        public ActionResult Index()
        {

            var Dishs = DishPresenterObject.ListAllDishForOwner(User.Identity.GetUserId());
            return View("Index", Dishs);
        }

        // GET: Dish/Dish/Create
        public ActionResult Create()
        {

            var Dish = new DishViewModel();
            return View(Dish);
        }
        // POST: Dish/Dish/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name, Place, PhoneNumber, District")] DishViewModel Dish)
        {

            if (ModelState.IsValid)
            {
                Dish.Owner = UserPresenterObject.FindUserByUserName(User.Identity.Name).ID;
                DishPresenterObject.InsertDish(Dish);


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
                var Dish = DishPresenterObject.GetDishById(Id.Value);

                var folderPath = DishPresenterObject.GetDishPictureUrlForUpload(Dish.ID, User.Identity.Name);
                Dish.DishPictureURL = folderPath + "?time=" + DateTime.Now.Ticks.ToString();
                Dish.HasDishPicture = Server.IsRelativePathExisted(folderPath);

                ViewBag.upload = uploadState;
                ViewBag.Message = uploadMessage;


                return View("Details", Dish);
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
                var updatedDish = DishPresenterObject.GetDishById(id.Value);
                return View("Edit", updatedDish);
            }
            catch (NotFoundException e)
            {
                return View("ResultNotFoundError");
            }

        }

        // Post Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind(Include = "Name, Place, PhoneNumber, Description, OpenTime, CloseTime, ID, District")] DishViewModel Dish, string details)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var updatedDish = new DishViewModel
                    {
                        ID = Dish.ID,
                        Name = Dish.Name,
                        Cost = Dish.Cost,
                        Processing = Dish.Processing,
                        Owner = UserPresenterObject.FindUserByUserName(User.Identity.Name).ID,
                    };
                    DishPresenterObject.UpdateDish(id, updatedDish);
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
                //var deletedDish = new DishViewModel();
                var deletedDish = DishPresenterObject.GetDishById(id.Value);
                return View("Delete", deletedDish);
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
                //var Dish = DishPresenterObject.GetDishById(id);
                //var directPath = DishPresenterObject.GetDishDirectionPicture(Dish.ID, User.Identity.Name);
                //var serverPath = Server.MapPath(directPath);
                //DirectoryInfo dir = new DirectoryInfo(serverPath);
                //if (dir.Exists)
                //{
                //    dir.Delete(true);
                //}
                DishPresenterObject.DeleteDish(id);
                return RedirectToAction("Index");
            }
            catch (NotFoundException e)
            {
                return View("ResultNotFoundError");
            }
        }

        //public ActionResult AddFood(int id)
        //{
        //    try
        //    {
        //        return RedirectToAction("Create", "Food", new { DishID = id });
        //    }
        //    catch (NotFoundException e)
        //    {
        //        return View("ResultNotFoundError");
        //    }
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult UploadDishImage(HttpPostedFileBase file, int DishId)
        //{
        //    var Dish = DishPresenterObject.GetDishById(DishId);
        //    string message = "";
        //    string upload = "no";

        //    //kiem tra file upload
        //    if (file == null)
        //    {
        //        Dish.HasDishPicture = false;
        //        message = "Có lỗi xảy ra! Tải ảnh lên không thành công.";
        //        return RedirectToAction("Details", new { id = DishId, uploadMessage = message, uploadState = upload });
        //    }

        //    //kiem tra phan mo rong dinh dang anh
        //    var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        //    if (!(ext == ".jpg" || ext == ".png" || ext == ".jpeg" || ext == ".gif"))
        //    {
        //        Dish.HasDishPicture = false;
        //        message = "Chỉ hỗ trợ tải lên ảnh có đuôi: jpg, jpeg, png, gif";
        //        return RedirectToAction("Details", new { id = DishId, uploadMessage = message, uploadState = upload });
        //    }

        //    // upload anh
        //    var folderPath = DishPresenterObject.GetDishPictureUrlForUpload(Dish.ID, User.Identity.Name);
        //    var serverPath = Server.MapPath(folderPath);
        //    var dirs = Path.GetDirectoryName(serverPath);
        //    if (!Directory.Exists(dirs))
        //    {
        //        Directory.CreateDirectory(dirs);
        //    }
        //    file.SaveAs(serverPath);
        //    message = "Tải ảnh lên thành công!";
        //    upload = "yes";
        //    return RedirectToAction("Details", new { id = DishId, uploadMessage = message, uploadState = upload });
        //}
        


        //public ActionResult RemoveDishImage(int DishId)
        //{
        //    var Dish = DishPresenterObject.GetDishById(DishId);
        //    string message = "";
        //    string remove = "no";

        //    //xac dinh ten anh can xoa
        //    var folderPath = DishPresenterObject.GetDishPictureUrlForUpload(Dish.ID, User.Identity.Name);
        //    var serverPath = Server.MapPath(folderPath);
        //    FileInfo deleteFile = new FileInfo(serverPath);

        //    //kiem tra anh con ton tai ko
        //    if (!(deleteFile.Exists))
        //    {
        //        message = "Hình ảnh này không còn tồn tại!";
        //        remove = "no";
        //        return RedirectToAction("Details", new { id = DishId, uploadMessage = message, removeState = remove });
        //    }

        //    //xoa anh
        //    deleteFile.Delete();
        //    message = "Đã xóa hình ảnh";
        //    remove = "yes";
        //    return RedirectToAction("Details", new { id = DishId, uploadMessage = message, removeState = remove });
        //}

    }
}