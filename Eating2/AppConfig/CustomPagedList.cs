using System.Linq;
using PagedList;
using AutoMapper;
using System.Collections.Generic;

namespace Eating2.AppConfig
{
    public class CustomPagedList<T> : PagedList<T>
    {
        private IQueryable<T> query;

        public CustomPagedList(IQueryable<T> superset, int pageNumber, int pageSize) : base(superset, pageNumber, pageSize)
        {
            query = superset;
        }

        public CustomPagedList() : base(new List<T>(), 1, 1)
        {
        }

        public void CopyFrom<Z>(CustomPagedList<Z> pagedList)
        {
            base.TotalItemCount = pagedList.TotalItemCount;
            base.PageSize = pagedList.TotalItemCount;
            base.PageNumber = pagedList.PageNumber;
            base.PageCount = pagedList.PageCount;
            base.HasPreviousPage = pagedList.HasPreviousPage;
            base.HasNextPage = pagedList.HasNextPage;
            base.IsFirstPage = pagedList.IsFirstPage;
            base.IsLastPage = pagedList.IsLastPage;
            base.FirstItemOnPage = pagedList.FirstItemOnPage;
            base.LastItemOnPage = pagedList.LastItemOnPage;
            this.Subset.AddRange(pagedList.Select(p => Mapper.Map<T>(p)));
        }
    }
}
