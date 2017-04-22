using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eating2.Business.ViewModels
{
    public class SelectList : SelectListItem
    {
        public int NumberValue { get; set; }
    }
}