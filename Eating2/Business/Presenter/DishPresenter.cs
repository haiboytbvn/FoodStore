using Eating2.DataAcess.Repositories;
using Eating2.Exception;
using Eating2.Business.ViewModels;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eating2.DataAcess.Models;
using Eating2.Business.Presenter;
using System.IO;
using Microsoft.AspNet.Identity;
using Eating2.AppConfig;

namespace Eating2.Business.Presenter
{
    public class DishPresenter : IDishPresenter
    {
        protected HttpContextBase HttpContext;
        protected IDishRepository DishRepository;
        protected ApplicationUserManager UserManager;
        protected UserPresenter userPresenter;
        protected FoodPresenter FoodPresenterObject;
        protected FoodRepository FoodRepository;
        
        
        public DishPresenter(HttpContextBase context)
        {
            HttpContext = context;
            DishRepository = new DishRepository();
            UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            userPresenter = new UserPresenter(context);
            FoodPresenterObject = new FoodPresenter(context);
            FoodRepository = new FoodRepository();
        }



        public DishViewModel GetDishById(int DishID)
        {
            var Dish = DishRepository.GetDishByID(DishID);
            if (Dish == null)
            {
                throw new NotFoundException("Dish was not found.");
            }
            var DishViewModel = new DishViewModel();
            DishViewModel = Dish.MapTo<DishDataModel, DishViewModel>();

            return DishViewModel;
            
        }

        public List<DishViewModel> ListAllDish()
        {
            List<DishViewModel> listDishViewModel = new List<DishViewModel>();
            var listDish = DishRepository.ListAll();
            foreach(var Dish in listDish)
            {
                var DishViewModel = Dish.MapTo<DishDataModel, DishViewModel>();

                listDishViewModel.Add(DishViewModel);
            }
            return listDishViewModel;
        }

        public List<DishViewModel> ListAllDishForOwner(string id)
        {
            List<DishViewModel> listDishViewModel = new List<DishViewModel>();
            var listDish = DishRepository.ListAllForOwner(id);
            foreach(var Dish in listDish)
            {
                var DishViewModel = Dish.MapTo<DishDataModel, DishViewModel>();
                listDishViewModel.Add(DishViewModel);
            }
            return listDishViewModel;
        }
        public void InsertDish(DishViewModel Dish)
        {
            var DishDataModel = Dish.MapTo<DishViewModel, DishDataModel>();
            DishRepository.InsertDish(DishDataModel);
            DishRepository.Save();
        }

        public void UpdateDish(int DishID, DishViewModel Dish)
        {
            var DishDataModel = DishRepository.GetDishByID(DishID);
            if (DishDataModel == null)
            {
                throw new NotFoundException("Dish was not found.");
            }
            else
            {
                DishDataModel = Dish.MapTo<DishViewModel, DishDataModel>(DishDataModel);
                DishRepository.UpdateDish(DishDataModel);
                DishRepository.Save();
            }
        }

        public void DeleteDish(int DishID)
        {
            List<FoodViewModel> listFoods = FoodPresenterObject.ListAllFoodForDish(DishID);
            foreach (var food in listFoods)
            {
                FoodPresenterObject.DeleteFood(food.ID);
            }

            var DishDataModel =DishRepository.GetDishByID(DishID);
            if (DishDataModel == null)
            {
                throw new NotFoundException("Dish was not found.");
            }
            else
            {
                DishRepository.DeleteDish(DishID);
                DishRepository.Save();
            }
        }

        public string GetDishPictureUrlForUpload(int DishID, string UserName)
        {
            var folderPath = Path.Combine("~/uploads/photo", UserName, "Dish" + DishID.ToString(), "Dish.jpg");
            return folderPath;
        }
        public string GetDishDirectionPicture(int DishID, int number, string UserName)
        {
            var directPath = Path.Combine("~/uploads/photo", UserName, "Dish" + DishID.ToString());
            return directPath;
        }

        
    }

}