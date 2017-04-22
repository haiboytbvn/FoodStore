using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eating2.AppConfig
{
    public class FilterOptions : SearchOptions
    {
       
        public string[] FilterFields { get; set; }
    }
}
