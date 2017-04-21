using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Eating2.AppConfig
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString SortableLink(this HtmlHelper htmlHelper, HttpRequestBase request, string sortFieldName, string title)
        {
            var sortDirection = GetSortDirection(request, sortFieldName);
            var url = request.AddQueryString(new KeyValuePair<string, string>(PagingConfig.SortFieldQueryString, sortFieldName),
                new KeyValuePair<string, string>(PagingConfig.SortDirectionQueryString, sortDirection));

            var link = string.Format("<a class=\"sort-field\" href=\"{0}\" title=\"{1}\">{1}</a>", url, title);
            return new MvcHtmlString(link);
        }

        private static string GetSortDirection(HttpRequestBase request, string sortFieldName)
        {
            var sortField = request.QueryString[PagingConfig.SortFieldQueryString];
            if (string.Equals(sortField, sortFieldName, System.StringComparison.InvariantCultureIgnoreCase))
            {
                var sortDirection = request.QueryString[PagingConfig.SortDirectionQueryString];
                return string.Equals(sortDirection, "Asc", System.StringComparison.InvariantCultureIgnoreCase) ? "Desc" : "Asc";
            }
            return "Asc";
        }

        public static MvcForm BeginFormGet(this HtmlHelper htmlHelper)
        {
            string rawUrl = htmlHelper.ViewContext.HttpContext.Request.RawUrl;
            return FormHelper(htmlHelper, rawUrl, FormMethod.Get, new RouteValueDictionary());
        }

        private static MvcForm FormHelper(HtmlHelper htmlHelper, string formAction, FormMethod method, IDictionary<string, object> htmlAttributes)
        {
            TagBuilder tagBuilder = new TagBuilder("form");
            tagBuilder.MergeAttributes<string, object>(htmlAttributes);
            tagBuilder.MergeAttribute("action", formAction);
            tagBuilder.MergeAttribute("method", HtmlHelper.GetFormMethodString(method), true);
            htmlHelper.ViewContext.Writer.Write(tagBuilder.ToString(TagRenderMode.StartTag));
            MvcForm result = new MvcForm(htmlHelper.ViewContext);

            return result;
        }
    }
}