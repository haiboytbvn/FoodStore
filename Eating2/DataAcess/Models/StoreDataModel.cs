using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Eating2.DataAcess.Models
{
    public class StoreDataModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string OpenTime { get; set; }
        public string CloseTime { get; set; }
        public string Place { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public string District { get; set; }
        public string Owner { get; set; }
        [ForeignKey("Owner")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<FoodDataModel> Foods { get; set; }
    }
}