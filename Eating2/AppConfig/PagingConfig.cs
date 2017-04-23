using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eating2.AppConfig
{
    public class PagingConfig
    {
        public const int PageNumber = 1;
        public const int PageSize = 12;
        public const string PageQueryString = "p";
        public const string FilterKeywordQueryString = "filterKeyword";
        public const string FilterFieldQueryString = "filterField";
        public const string SortFieldQueryString = "sortField";
        public const string SortDirectionQueryString = "sortDirection";
    }
}
