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
using PagedList;
using Eating2.App_Start;

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
            MapperConfig.Start();
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
            var FoodViewModel = new FoodViewModel();
            FoodViewModel = Food.MapTo<FoodDataModel, FoodViewModel>();
           
            return FoodViewModel;

        }

        public List<FoodViewModel> ListAllFood()
        {
            List<FoodViewModel> listFoodViewModel = new List<FoodViewModel>();
            var listFood = FoodRepository.ListAll();
            foreach (var Food in listFood)
            {
                var FoodViewModel = Food.MapTo<FoodDataModel, FoodViewModel>();
              
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
                var FoodViewModel = Food.MapTo<FoodDataModel, FoodViewModel>();
             
                listFoodViewModel.Add(FoodViewModel);
            }
            return listFoodViewModel;
        }

        public void InsertFood(FoodViewModel Food)
        {
            var FoodDataModel = Food.MapTo<FoodViewModel, FoodDataModel>();

            var store = StoreRepository.GetStoreByID(Food.StoreID);
            FoodDataModel.DistrictDisplayOnly = store.District;
            FoodDataModel.StoreNameDisplayOnly = store.Name;
            
            FoodRepository.InsertFood(FoodDataModel);
            FoodRepository.Save();
        }

        public void UpdateFood(int FoodID, FoodViewModel Food)
        {
            var FoodDataModel = FoodRepository.GetFoodByID(FoodID);
            var currentPicture = Food.FoodPictureURL;
            if(currentPicture == null)
            {
                currentPicture = FoodDataModel.FoodPictureURL;
            }
                
            if (FoodDataModel == null)
            {
                throw new NotFoundException("Food was not found.");
            }
            else
            {
                FoodDataModel = Food.MapTo<FoodViewModel, FoodDataModel>(FoodDataModel);
                var store = StoreRepository.GetStoreByID(Food.StoreID);
                FoodDataModel.DistrictDisplayOnly = store.District;
                FoodDataModel.StoreNameDisplayOnly = store.Name;
                FoodDataModel.FoodPictureURL = currentPicture;
                

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

        public string GetFoodPictureUrlForUpload(int foodID, int number, int storeID, string UserName)
        {
            var folderPath = Path.Combine("~/uploads/photo", UserName, "Store" + storeID,
                "Food" + foodID, "Food" + number.ToString() + ".jpg");
            return folderPath;
            
        }

        public string GetFoodDirectionPicture(int foodID, int storeID, string UserName)
        {
            var directPath = Path.Combine("~/uploads/photo", UserName, "Store" + storeID, "Food" + foodID);
            return directPath;
        }

        public IPagedList<FoodViewModel> GetFoodsForSearch(FilterOptions filterOptions)
        {
            var list = FoodRepository.GetFoodsForSearch(filterOptions);
            var mappedList = list.MapTo<IPagedList<FoodDataModel>, IPagedList<FoodViewModel>>();
            return mappedList;
        }

        public List<FoodViewModel> ListFoodByRate()
        {
            List<FoodViewModel> listFoodViewModel = new List<FoodViewModel>();
            var listFood = FoodRepository.ListByRate();
            foreach (var Food in listFood)
            {
                var FoodViewModel = Food.MapTo<FoodDataModel, FoodViewModel>();

                listFoodViewModel.Add(FoodViewModel);
            }
            return listFoodViewModel;
        }
        public List<FoodViewModel> ListFoodByArea(string district)
        {
            List<FoodViewModel> listFoodViewModel = new List<FoodViewModel>();
            var listFood = FoodRepository.ListByArea(district);
            foreach (var Food in listFood)
            {
                var FoodViewModel = Food.MapTo<FoodDataModel, FoodViewModel>();

                listFoodViewModel.Add(FoodViewModel);
            }
            return listFoodViewModel;
        }

        public List<FoodViewModel> ListFoodByRateIndex()
        {
            List<FoodViewModel> listFoodViewModel = new List<FoodViewModel>();
            var listFood = FoodRepository.ListByRateIndex();
            foreach (var Food in listFood)
            {
                var FoodViewModel = Food.MapTo<FoodDataModel, FoodViewModel>();

                listFoodViewModel.Add(FoodViewModel);
            }
            return listFoodViewModel;
        }

        public List<FoodViewModel> ListFoodByAreaIndex(string district)
        {
            List<FoodViewModel> listFoodViewModel = new List<FoodViewModel>();
            var listFood = FoodRepository.ListByAreaIndex(district);
            foreach (var Food in listFood)
            {
                var FoodViewModel = Food.MapTo<FoodDataModel, FoodViewModel>();

                listFoodViewModel.Add(FoodViewModel);
            }
            return listFoodViewModel;
        }

        public List<FoodViewModel> ListFoodByTime()
        {
            List<FoodViewModel> listFoodViewModel = new List<FoodViewModel>();
            var listFood = FoodRepository.ListByTime();
            foreach (var Food in listFood)
            {
                var FoodViewModel = Food.MapTo<FoodDataModel, FoodViewModel>();

                listFoodViewModel.Add(FoodViewModel);
            }
            return listFoodViewModel;
        }

        public List<FoodViewModel> ListFoodByTimeIndex()
        {
            List<FoodViewModel> listFoodViewModel = new List<FoodViewModel>();
            var listFood = FoodRepository.ListByTimeIndex();
            foreach (var Food in listFood)
            {
                var FoodViewModel = Food.MapTo<FoodDataModel, FoodViewModel>();

                listFoodViewModel.Add(FoodViewModel);
            }
            return listFoodViewModel;
        }

        public IPagedList<FoodViewModel> ListAllFoodByRate(FilterOptions filterOptions)
        {
            var list = FoodRepository.ListAllFoodByRate(filterOptions);
            var mappedList = list.MapTo<IPagedList<FoodDataModel>, IPagedList<FoodViewModel>>();
            return mappedList;
        }

        public IPagedList<FoodViewModel> ListAllFoodByArea(string district, FilterOptions filterOptions)
        {
            var list = FoodRepository.ListAllFoodByArea(district, filterOptions);
            var mappedList = list.MapTo<IPagedList<FoodDataModel>, IPagedList<FoodViewModel>>();
            return mappedList;
        }

        public IPagedList<FoodViewModel> ListAllFoodByTime(FilterOptions filterOptions)
        {
            var list = FoodRepository.ListAllFoodByTime(filterOptions);
            var mappedList = list.MapTo<IPagedList<FoodDataModel>, IPagedList<FoodViewModel>>();
            return mappedList;
        }

        public List<FoodViewModel> ListFoodByCostRecomment(double cost)
        {
            List<FoodViewModel> listFoodViewModel = new List<FoodViewModel>();
            var listFood = FoodRepository.ListByCostRecomment(cost);
            foreach (var Food in listFood)
            {
                var FoodViewModel = Food.MapTo<FoodDataModel, FoodViewModel>();

                listFoodViewModel.Add(FoodViewModel);
            }
            return listFoodViewModel;
        }
    }

}