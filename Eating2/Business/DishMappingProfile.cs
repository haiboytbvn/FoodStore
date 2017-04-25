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
    public class DishMappingProfile : Profile
    {
        private UserManager<ApplicationUser> UserManager;
        private FoodRepository FoodRepository;
        private DishRepository DishRepository;
        private RateRepository RateRepository;

        public DishMappingProfile(string profileName) : base(profileName)
        {
            FoodRepository = new FoodRepository();
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            DishRepository = new DishRepository();
            RateRepository = new RateRepository();

            this.CreateMap<DishDataModel, DishViewModel>()
            .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => UserManager.FindById(src.Owner).Email))
            .ForMember(dest => dest.NumberOfStoreHas, opt => opt.MapFrom(src => FoodRepository.ListAllForDish(src.ID).Count()));

            this.CreateMap<DishViewModel, DishDataModel>();

            this.CreateMap<IPagedList<DishDataModel>, IPagedList<DishViewModel>>()
                .ConvertUsing<PagedListConverter<DishDataModel, DishViewModel>>();

        }
    }
}