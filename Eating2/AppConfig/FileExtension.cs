using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Eating2.AppConfig
{
    public static class FileExtension
    {
        public static bool IsRelativePathExisted(this HttpServerUtilityBase server, string relativeUrl)
        {
            var serverUrl = server.MapPath(relativeUrl);
            return File.Exists(serverUrl);
        }
    }
}