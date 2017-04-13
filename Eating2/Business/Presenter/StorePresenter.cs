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

namespace Eating2.Business.Presenter
{
    public class StorePresenter : IStorePresenter
    {
        protected HttpContextBase HttpContext;
        protected IStoreRepository StoreRepository;
        protected ApplicationUserManager UserManager;
        protected UserPresenter userPresenter;
        
        
        public StorePresenter(HttpContextBase context)
        {
            HttpContext = context;
            StoreRepository = new StoreRepository();
            UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            userPresenter = new UserPresenter(context);
        }



        public StoreViewModel GetStoreById(int StoreID)
        {
            var Store = StoreRepository.GetStoreByID(StoreID);
            if (Store == null)
            {
                throw new NotFoundException("Store was not found.");
            }
            var StoreViewModel = new StoreViewModel()
            {
                ID = Store.ID,
                Name = Store.Name,
                OpenTime = Store.OpenTime,
                CloseTime = Store.CloseTime,
                Description = Store.Description,
                Owner = userPresenter.FindUserByID(Store.Owner).Email,
                PhoneNumber = Store.PhoneNumber,
                Place = Store.Place
            };
            return StoreViewModel;
            
        }

        public List<StoreViewModel> ListAllStore()
        {
            List<StoreViewModel> listStoreViewModel = new List<StoreViewModel>();
            var listStore = StoreRepository.ListAll();
            foreach(var Store in listStore)
            {
                var StoreViewModel = new StoreViewModel()
                {
                    ID = Store.ID,
                    Name = Store.Name,
                    OpenTime = Store.OpenTime,
                    CloseTime = Store.CloseTime,
                    Description = Store.Description,
                    Owner = Store.Owner,
                    PhoneNumber = Store.PhoneNumber,
                    Place = Store.Place
                };
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
                var StoreViewModel = new StoreViewModel()
                { 
                    ID = Store.ID,
                    Name = Store.Name,
                    OpenTime = Store.OpenTime,
                    CloseTime = Store.CloseTime,
                    Description = Store.Description,
                    Owner = Store.Owner,
                    PhoneNumber = Store.PhoneNumber,
                    Place = Store.Place
                };
                listStoreViewModel.Add(StoreViewModel);
            }
            return listStoreViewModel;
        }
        public void InsertStore(StoreViewModel Store)
        {
            var StoreDataModel = new StoreDataModel()
            {
                Name = Store.Name,
                CloseTime = Store.CloseTime,
                OpenTime = Store.OpenTime,
                PhoneNumber = Store.PhoneNumber,
                Place = Store.Place,
                Description = Store.Description,
                Owner = Store.Owner

            };
            
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
                StoreDataModel.Name = Store.Name;
                StoreDataModel.Place = Store.Place;
                StoreDataModel.PhoneNumber = Store.PhoneNumber;
                StoreDataModel.Owner = Store.Owner;
                StoreDataModel.Description = Store.Description;
                StoreDataModel.OpenTime = Store.OpenTime;
                StoreDataModel.CloseTime = Store.CloseTime;

                StoreRepository.UpdateStore(StoreDataModel);
                StoreRepository.Save();
            }
        }

        public void DeleteStore(int StoreID)
        {
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

    }

}