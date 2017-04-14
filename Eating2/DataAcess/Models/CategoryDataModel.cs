using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Eating2.DataAcess.Models
{
    public class CategoryDataModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}