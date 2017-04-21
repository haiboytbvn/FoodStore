using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Eating2.AppConfig
{
    public static class MapperExtensions
    {
        public static Destination MapTo<Source, Destination>(this Source source)
        {
            return Mapper.Map<Destination>(source);
        }

        public static Destination MapTo<Source, Destination>(this Source source, Destination des)
        {
            return Mapper.Map(source, des);
        }

        public static IMappingExpression<TSource, TDestination> Ignore<TSource, TDestination>(
    this IMappingExpression<TSource, TDestination> map,
    Expression<Func<TDestination, object>> selector)
        {
            map.ForMember(selector, config => config.Ignore());
            return map;
        }
    }
}