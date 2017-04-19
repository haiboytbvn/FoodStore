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
using Eating2.AppConfig;
using System.IO;

namespace Eating2.Business.Presenter
{
    public class FoodPresenter : IFoodPresenter
    {
        protected HttpContextBase HttpContext;
        protected IFoodRepository FoodRepository;
        protected ApplicationUserManager UserManager;
        protected UserPresenter userPresenter;
        protected IStoreRepository StoreRepository;
        protected RatePresenter RatePresenterObject;
        protected RateRepository RateRepository;

        public FoodPresenter(HttpContextBase context)
        {
            HttpContext = context;
            FoodRepository = new FoodRepository();
            UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            userPresenter = new UserPresenter(context);
            StoreRepository = new StoreRepository();
            RatePresenterObject = new RatePresenter(context);
            RateRepository = new RateRepository();
        }



        public FoodViewModel GetFoodById(int FoodID)
        {
            var Food = FoodRepository.GetFoodByID(FoodID);
            if (Food == null)
            {
                throw new NotFoundException("Food was not found.");
            }
            var FoodViewModel = new FoodViewModel()
            {
                ID = Food.ID,
                Name = Food.Name,
                Cost = Food.Cost,
                Processing = Food.Processing,
                StoreID = Food.StoreID,
                inStore = Food.Store.Name,
                numberOfFoodPicture = Food.numberOfFoodPicture
            };
            return FoodViewModel;

        }

        public List<FoodViewModel> ListAllFood()
        {
            List<FoodViewModel> listFoodViewModel = new List<FoodViewModel>();
            var listFood = FoodRepository.ListAll();
            foreach (var Food in listFood)
            {
                var FoodViewModel = new FoodViewModel()
                {
                    ID = Food.ID,
                    Name = Food.Name,
                    Cost = Food.Cost,
                    Processing = Food.Processing,
                    inStore = Food.Store.Name,
                    numberOfFoodPicture = Food.numberOfFoodPicture
                };
                listFoodViewModel.Add(FoodViewModel);
            }
            return listFoodViewModel;
        }

        public List<FoodViewModel> ListAllFoodForStore(int id)
        {
            List<FoodViewModel> listFoodViewModel = new List<FoodViewModel>();
            var listFood = FoodRepository.ListAllForStore(id);
            foreach (var Food in listFood)
            {
                var FoodViewModel = new FoodViewModel()
                {
                    ID = Food.ID,
                    Name = Food.Name,
                    Cost = Food.Cost,
                    Processing = Food.Processing,
                    inStore = Food.Store.Name,
                    NumberOfRate = RateRepository.TotalRate(Food.ID),
                    numberOfFoodPicture = Food.numberOfFoodPicture

                };
                listFoodViewModel.Add(FoodViewModel);
            }
            return listFoodViewModel;
        }
        public void InsertFood(FoodViewModel Food)
        {
            var FoodDataModel = new FoodDataModel()
            {
                ID = Food.ID,
                Name = Food.Name,
                Cost = Food.Cost,
                Processing = Food.Processing,
                StoreID = Food.StoreID,
                numberOfFoodPicture = Food.numberOfFoodPicture

            };

            FoodRepository.InsertFood(FoodDataModel);
            FoodRepository.Save();
        }

        public void UpdateFood(int FoodID, FoodViewModel Food)
        {
            var FoodDataModel = FoodRepository.GetFoodByID(FoodID);
            if (FoodDataModel == null)
            {
                throw new NotFoundException("Food was not found.");
            }
            else
            {
                FoodDataModel.ID = Food.ID;
                FoodDataModel.Name = Food.Name;
                FoodDataModel.Cost = Food.Cost;
                FoodDataModel.Processing = Food.Processing;
                FoodDataModel.numberOfFoodPicture = Food.numberOfFoodPicture;

                FoodRepository.UpdateFood(FoodDataModel);
                FoodRepository.Save();
            }
        }

        public void DeleteFood(int FoodID)
        {

            List<RateViewModel> listRates = RatePresenterObject.ListAllRateForFood(FoodID);
            foreach(var rate in listRates)
            {
                RatePresenterObject.DeleteRate(rate.ID);
            }

            var FoodDataModel = FoodRepository.GetFoodByID(FoodID);
            if (FoodDataModel == null)
            {
                throw new NotFoundException("Food was not found.");
            }
            else
            {
                FoodRepository.DeleteFood(FoodID);
                FoodRepository.Save();
            }
        }

        public string GetFoodPictureUrlForUpload(string FoodName, int number, string StoreName, string UserName)
        {
            var folderPath = Path.Combine("~/uploads/photo", UserName, StoreName + "Store",
                FoodName + "Food", "Food" + number.ToString() + ".jpg");
            return folderPath;
        }

        public string GetFoodDirectionPicture(string FoodName, string StoreName, string UserName)
        {
            var directPath = Path.Combine("~/uploads/photo", UserName, StoreName + "Store", FoodName + "Food");
            return directPath;
        }
    }

}