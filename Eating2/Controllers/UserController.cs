using Eating2.AppConfig;
using Eating2.Business.Presenter;
using Eating2.Business.ViewModels;
using Eating2.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eating2.Controllers
{
    public class UserController : Controller
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

        public ActionResult DetailsForUser(int? Id)
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
                for (int i = 0; i < 6; i++)
                {
                    var folderPath = FoodPresenterObject.GetFoodPictureUrlForUpload(food.Name, i, store.Name, store.Owner);
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

                return View("DetailsForUser", food);
            }
            catch (NotFoundException e)
            {
                return View("ResultNotFoundError");
            }

        }

    }
}