using AutoMapper;
using Eating2.Business.ViewModels;
using Eating2.DataAcess;
using Eating2.DataAcess.Models;
using Eating2.DataAcess.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eating2.Business
{
    public class FoodMappingProfile : Profile
    {
        private UserManager<ApplicationUser> UserManager;
        private FoodRepository FoodRepository;
        private StoreRepository StoreRepository;
        private RateRepository RateRepository;

        public FoodMappingProfile(string profileName) : base(profileName)
        {
            FoodRepository = new FoodRepository();
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            StoreRepository = new StoreRepository();
            RateRepository = new RateRepository();
        
            this.CreateMap<FoodDataModel, FoodViewModel>()
                .ForMember(dest => dest.DetailsPlaceDisplayOnly, opt => opt.MapFrom(src => StoreRepository.GetStoreByID(src.StoreID).Place + " - " + StoreRepository.GetStoreByID(src.StoreID).District))
                .ForMember(dest => dest.NumberOfRate, opt => opt.MapFrom(src => RateRepository.TotalRate(src.ID)))
                .ForMember(dest => dest.TimeService, opt => opt.MapFrom(src => StoreRepository.GetStoreByID(src.StoreID).OpenTime +" - "+ StoreRepository.GetStoreByID(src.StoreID).CloseTime))
                .ForMember(dest => dest.StorePhoneNumber, opt => opt.MapFrom(src => StoreRepository.GetStoreByID(src.StoreID).PhoneNumber));
                
            this.CreateMap<FoodViewModel, FoodDataModel>();

            this.CreateMap<IPagedList<FoodDataModel>, IPagedList<FoodViewModel>>()
                .ConvertUsing<PagedListConverter<FoodDataModel, FoodViewModel>>();

        }
    }
}