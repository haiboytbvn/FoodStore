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
    public class StorePresenter : IStorePresenter
    {
        protected HttpContextBase HttpContext;
        protected IStoreRepository StoreRepository;
        protected ApplicationUserManager UserManager;
        protected UserPresenter userPresenter;
        protected FoodPresenter FoodPresenterObject;
        protected FoodRepository FoodRepository;
        
        
        public StorePresenter(HttpContextBase context)
        {
            HttpContext = context;
            StoreRepository = new StoreRepository();
            UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            userPresenter = new UserPresenter(context);
            FoodPresenterObject = new FoodPresenter(context);
            FoodRepository = new FoodRepository();
        }



        public StoreViewModel GetStoreById(int StoreID)
        {
            var Store = StoreRepository.GetStoreByID(StoreID);
            if (Store == null)
            {
                throw new NotFoundException("Store was not found.");
            }
            var StoreViewModel = new StoreViewModel();
            StoreViewModel = Store.MapTo<StoreDataModel, StoreViewModel>();

            return StoreViewModel;
            
        }

        public List<StoreViewModel> ListAllStore()
        {
            List<StoreViewModel> listStoreViewModel = new List<StoreViewModel>();
            var listStore = StoreRepository.ListAll();
            foreach(var Store in listStore)
            {
                var StoreViewModel = Store.MapTo<StoreDataModel, StoreViewModel>();

                listStoreViewModel.Add(StoreViewModel);
            }
            return listStoreViewModel;
        }

        public List<StoreViewModel> ListAllStoreForOwner(string id)
        {
            List<StoreViewModel> listStoreViewModel = new List<StoreViewModel>();
            var listStore = StoreRepository.ListAllForOwner(id);
            foreach(var Store in listStore)
            {
                var StoreViewModel = Store.MapTo<StoreDataModel, StoreViewModel>();
                listStoreViewModel.Add(StoreViewModel);
            }
            return listStoreViewModel;
        }
        public void InsertStore(StoreViewModel Store)
        {
            var StoreDataModel = Store.MapTo<StoreViewModel, StoreDataModel>();
            StoreRepository.InsertStore(StoreDataModel);
            StoreRepository.Save();
        }

        public void UpdateStore(int StoreID, StoreViewModel Store)
        {
            var StoreDataModel = StoreRepository.GetStoreByID(StoreID);
            if (StoreDataModel == null)
            {
                throw new NotFoundException("Store was not found.");
            }
            else
            {
                StoreDataModel = Store.MapTo<StoreViewModel, StoreDataModel>(StoreDataModel);
                StoreRepository.UpdateStore(StoreDataModel);
                StoreRepository.Save();
            }
        }

        public void DeleteStore(int StoreID)
        {
            List<FoodViewModel> listFoods = FoodPresenterObject.ListAllFoodForStore(StoreID);
            foreach (var food in listFoods)
            {
                FoodPresenterObject.DeleteFood(food.ID);
            }

            var StoreDataModel =StoreRepository.GetStoreByID(StoreID);
            if (StoreDataModel == null)
            {
                throw new NotFoundException("Store was not found.");
            }
            else
            {
                StoreRepository.DeleteStore(StoreID);
                StoreRepository.Save();
            }
        }

        public string GetStorePictureUrlForUpload(int storeID, string UserName)
        {
            var folderPath = Path.Combine("~/uploads/photo", UserName, "Store" + storeID.ToString(), "store.jpg");
            return folderPath;
        }
        public string GetStoreDirectionPicture(int storeID, string UserName)
        {
            var directPath = Path.Combine("~/uploads/photo", UserName, "Store" + storeID.ToString());
            return directPath;
        }

    }

}