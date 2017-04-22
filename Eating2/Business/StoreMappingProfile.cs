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
    public class StoreMappingProfile : Profile
    {
        private UserManager<ApplicationUser> UserManager;
        private FoodRepository FoodRepository;
        private StoreRepository StoreRepository;
        private RateRepository RateRepository;

        public StoreMappingProfile(string profileName) : base(profileName)
        {
            FoodRepository = new FoodRepository();
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            StoreRepository = new StoreRepository();
            RateRepository = new RateRepository();

            this.CreateMap<StoreDataModel, StoreViewModel>()
            .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => UserManager.FindById(src.Owner).Email));
            //.ForMember(dest => dest.NumberOfRate, opt => opt.MapFrom(src => RateRepository.TotalRate(src.ID)));

            this.CreateMap<StoreViewModel, StoreDataModel>();

            this.CreateMap<IPagedList<StoreDataModel>, IPagedList<StoreViewModel>>()
                .ConvertUsing<PagedListConverter<StoreDataModel, StoreViewModel>>();

        }
    }
}