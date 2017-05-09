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
using Eating2.AppConfig;

namespace Eating2.Business.Presenter
{
    public class RatePresenter : IRatePresenter
    {
        protected HttpContextBase HttpContext;
        protected IRateRepository RateRepository;
        protected IFoodRepository FoodRepository;

        public RatePresenter(HttpContextBase context)
        {
            MapperConfig.Start();
            HttpContext = context;
            RateRepository = new RateRepository();
            FoodRepository = new FoodRepository();
        }



        public RateViewModel GetRateById(int RateID)
        {
            var Rate = RateRepository.GetRateByID(RateID);
            if (Rate == null)
            {
                throw new NotFoundException("Rate was not found.");
            }
            var RateViewModel = Rate.MapTo<RateDataModel, RateViewModel>();

            return RateViewModel;

        }

       

        public List<RateViewModel> ListAllRateForFood(int id)
        {
            List<RateViewModel> listRateViewModel = new List<RateViewModel>();
            var listRate = RateRepository.ListAllForFood(id);
            foreach (var Rate in listRate)
            {
                var RateViewModel = Rate.MapTo<RateDataModel, RateViewModel>();
                listRateViewModel.Add(RateViewModel);
            }
            return listRateViewModel;
        }
        public void InsertRate(RateViewModel Rate)
        {
            var RateDataModel = Rate.MapTo<RateViewModel, RateDataModel>();

            RateRepository.InsertRate(RateDataModel);
            RateRepository.Save();

            var food = FoodRepository.GetFoodByID(Rate.FoodID);
            food.AveragePoint = RateRepository.AveragePoint(food.ID);
            food.numberOfComment++;
            FoodRepository.UpdateFood(food);
            FoodRepository.Save();
        }

        
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

                var food = FoodRepository.GetFoodByID(RateDataModel.FoodID);
                food.AveragePoint = RateRepository.AveragePoint(food.ID);
                food.numberOfComment--;
                FoodRepository.UpdateFood(food);
                FoodRepository.Save();

            }
        }

    }

}