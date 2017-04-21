using AutoMapper;
using Eating2.AppConfig;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eating2.Business
{
    public class PagedListConverter<Source, Destination> : ITypeConverter<IPagedList<Source>, IPagedList<Destination>>
    {
        public IPagedList<Destination> Convert(IPagedList<Source> source, IPagedList<Destination> destination, ResolutionContext context)
        {
            var tempDestination = new CustomPagedList<Destination>();
            tempDestination.CopyFrom<Source>(source as CustomPagedList<Source>);
            return tempDestination;
        }
    }
}