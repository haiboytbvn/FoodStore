using Eating2.DataAcess.Repositories;
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
        public string Recipe { get; set; }
        public string DistrictDisplayOnly { get; set; }
        public string StoreNameDisplayOnly { get; set; }
        public double AveragePoint { get; set; }
        public string FoodPictureURL { get; set; }
        public int StoreID { get; set; }
        [ForeignKey("StoreID")]
        public virtual StoreDataModel Store { get; set; }
        public int DishID { get; set; }
        [ForeignKey("DishID")]
        public virtual DishDataModel Dish { get; set; }
        public virtual ICollection<RateDataModel> Rates { get; set; }
    }
}