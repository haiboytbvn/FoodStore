using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Eating2.DataAcess.Models
{
    public class RateDataModel
    {
        public int ID { get; set; }
        public int Point { get; set; }
        public string Comment { get; set; }
        public string Customer { get; set; }
        public DateTime TimeComment { get; set; }
        public int FoodID { get; set; }
        [ForeignKey("StoreID")]
        public virtual FoodDataModel Food { get; set; }
    }
}