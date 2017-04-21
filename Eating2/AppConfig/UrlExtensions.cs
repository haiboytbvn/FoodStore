using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eating2.AppConfig
{
    public static class UrlExtensions
    {
        public static string AddQueryString(this HttpRequestBase requestBase, params KeyValuePair<string, string>[] queries)
        {
            var baseUrl = new UriBuilder(requestBase.Url.AbsoluteUri);
            var queryStrings = HttpUtility.ParseQueryString(baseUrl.Query.ToString());
            foreach (var query in queries)
            {
                queryStrings[query.Key] = query.Value;
            }
            baseUrl.Query = queryStrings.ToString();
            return baseUrl.ToString();
        }
    }
}