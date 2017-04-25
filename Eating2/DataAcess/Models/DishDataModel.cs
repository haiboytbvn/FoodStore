using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Eating2.DataAcess.Models
{
    public class DishDataModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Cost { get; set; }
        public string Processing { get; set; }
        public string Owner { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<FoodDataModel> Foods { get; set; }
    }
}