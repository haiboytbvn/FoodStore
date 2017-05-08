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
using Eating2.App_Start;

namespace Eating2.Business.Presenter
{
    public class RatePresenter : IRatePresenter
    {
        protected HttpContextBase HttpContext;
        protected IRateRepository RateRepository;
        protected ApplicationUserManager UserManager;
        protected UserPresenter userPresenter;
        protected IStoreRepository StoreRepository;


        public RatePresenter(HttpContextBase context)
        {
            MapperConfig.Start();
            HttpContext = context;
            RateRepository = new RateRepository();
            UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            userPresenter = new UserPresenter(context);
            StoreRepository = new StoreRepository();
        }



        public RateViewModel GetRateById(int RateID)
        {
            var Rate = RateRepository.GetRateByID(RateID);
            if (Rate == null)
            {
                throw new NotFoundException("Rate was not found.");
            }
            var RateViewModel = new RateViewModel()
            {
                ID = Rate.ID,
                Comment = Rate.Comment,
                Point = Rate.Point,
                TimeComment = Rate.TimeComment,
                FoodID = Rate.FoodID,
                Customer = Rate.Customer
            };
            return RateViewModel;

        }

        public List<RateViewModel> ListAllRateForFood(int id)
        {
            List<RateViewModel> listRateViewModel = new List<RateViewModel>();
            var listRate = RateRepository.ListAllForFood(id);
            foreach (var Rate in listRate)
            {
                var RateViewModel = new RateViewModel()
                {
                    ID = Rate.ID,
                    Comment = Rate.Comment,
                    Point = Rate.Point,
                    TimeComment = Rate.TimeComment,
                    FoodID = Rate.FoodID,
                    Customer = Rate.Customer

                };
                listRateViewModel.Add(RateViewModel);
            }
            return listRateViewModel;
        }
        public void InsertRate(RateViewModel Rate)
        {
            var RateDataModel = new RateDataModel()
            {
                ID = Rate.ID,
                Comment = Rate.Comment,
                Point = Rate.Point,
                TimeComment = Rate.TimeComment,
                FoodID = Rate.FoodID,
                Customer = Rate.Customer
            };

            RateRepository.InsertRate(RateDataModel);
            RateRepository.Save();

           
        }

        //public void UpdateRate(int RateID, RateViewModel Rate)
        //{
        //    var RateDataModel = RateRepository.GetRateByID(RateID);
        //    if (RateDataModel == null)
        //    {
        //        throw new NotFoundException("Rate was not found.");
        //    }
        //    else
        //    {
        //        RateDataModel.ID = Rate.ID;
        //        RateDataModel.Name = Rate.Name;
        //        RateDataModel.Cost = Rate.Cost;
        //        RateDataModel.Processing = Rate.Processing;

        //        RateRepository.UpdateRate(RateDataModel);
        //        RateRepository.Save();
        //    }
        //}

        public void DeleteRate(int RateID)
        {
            var RateDataModel = RateRepository.GetRateByID(RateID);
            if (RateDataModel == null)
            {
                throw new NotFoundException("Rate was deleted");
            }
            else
            {
                RateRepository.DeleteRate(RateID);
                RateRepository.Save();
            }
        }

    }

}