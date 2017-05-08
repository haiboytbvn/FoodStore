using AutoMapper;
using Eating2.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eating2.App_Start
{
    public class MapperConfig
    {
        public static void Start()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new FoodMappingProfile("FoodMapping"));
                cfg.AddProfile(new StoreMappingProfile("StoreMapping"));
                cfg.AddProfile(new RateMappingProfile("RateMapping"));

            });
        }
    }
}