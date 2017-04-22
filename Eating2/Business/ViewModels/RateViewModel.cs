using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Eating2.Business.ViewModels
{
    public class RateViewModel
    {
        public int ID { get; set; }
        [Display(Name = "Đánh giá:")]
        public string StringPoint { get; set; }
        public int Point { get; set; }
        [Display(Name = "Nhận xét")]
        public string Comment { get; set; }
        [Display(Name = "Tên bạn")]
        public string Customer { get; set; }
        [Display(Name = "Lúc")]
        public DateTime TimeComment { get; set; }
        public int FoodID { get; set; }

        public List<SelectListItem> Points 
        {
            get
            {
                List<SelectListItem> point = new List<SelectListItem>();
                point.Add(new SelectListItem { Text = "♥ ♥ ♥ ♥ ♥", Value = "5" });
                point.Add(new SelectListItem { Text = "♥ ♥ ♥ ♥", Value = "4" });
                point.Add(new SelectListItem { Text = "♥ ♥ ♥", Value = "3" });
                point.Add(new SelectListItem { Text = "♥ ♥", Value = "2" });
                point.Add(new SelectListItem { Text = "♥", Value = "1" });

                return point;
            }

            set { }
            
        }
        public static int ToIntPoint(string s)
        {
            int p = 0;
            if (s == "1")   p = 1;
            if (s == "2")   p = 2;
            if (s == "3")   p = 3;
            if (s == "4")   p = 4;
            if (s == "5")   p = 5;
            return p;
        }
    }
}