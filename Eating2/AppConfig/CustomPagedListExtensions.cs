using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eating2.AppConfig
{
    public static class CustomPagedListExtensions
    {
        public static IPagedList<T> ToCustomPagedList<T>(this IQueryable<T> superset, int pageNumber, int pageSize)
        {
            var customPagedList = new CustomPagedList<T>(superset, pageNumber, pageSize);
            return customPagedList;
        }
    }
}
