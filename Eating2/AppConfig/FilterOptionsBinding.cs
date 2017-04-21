using System;
using System.Web.ModelBinding;
using System.Web.Mvc;


namespace Eating2.AppConfig
{
    public class FilterOptionsBinding : System.Web.Mvc.IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, System.Web.Mvc.ModelBindingContext bindingContext)
        {
            var request = controllerContext.HttpContext.Request;
            var page = request.QueryString[PagingConfig.PageQueryString].ToInt(1);
            var filterKey = request[PagingConfig.FilterKeywordQueryString] ?? string.Empty;
            var filterField = request[PagingConfig.FilterFieldQueryString] ?? string.Empty;
            var sortField = request[PagingConfig.SortFieldQueryString] ?? string.Empty;
            var sortDirection = request[PagingConfig.SortDirectionQueryString] ?? string.Empty;
            return new FilterOptions
            {
                Keyword = filterKey,
                FilterFields = filterField.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries),
                PagingOptions = new PagingOptions
                {
                    CurrentPage = page,
                    PageSize = PagingConfig.PageSize
                },
                SortOptions = new SortOptions
                {
                    Field = sortField,
                    Direction = string.Equals(sortDirection, "Desc", StringComparison.InvariantCultureIgnoreCase) ?
                    SortDirections.Desc : SortDirections.Asc
                }
            };
        }
    }
}