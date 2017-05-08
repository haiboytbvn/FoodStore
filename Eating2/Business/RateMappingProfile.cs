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
    public class RateMappingProfile : Profile
    {
        private FoodRepository FoodRepository;
        private RateRepository RateRepository;

        public RateMappingProfile(string profileName) : base(profileName)
        {

            this.CreateMap<RateDataModel, RateViewModel>();

            this.CreateMap<RateViewModel, RateDataModel>();

            this.CreateMap<IPagedList<RateDataModel>, IPagedList<RateViewModel>>()
                .ConvertUsing<PagedListConverter<RateDataModel, RateViewModel>>();

        }
    }
}