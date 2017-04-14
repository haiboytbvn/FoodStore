using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Eating2.DataAcess.Models
{
    public class FoodDataModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Cost { get; set; }
        public string Processing { get; set; }
        public int StoreID { get; set; }
        [ForeignKey("StoreID")]
        public virtual StoreDataModel Store { get; set; }
        public virtual ICollection<RateDataModel> Rates { get; set; }
    }
}