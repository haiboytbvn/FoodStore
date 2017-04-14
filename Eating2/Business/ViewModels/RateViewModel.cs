using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Eating2.Business.ViewModels
{
    public class RateViewModel
    {
        public int ID { get; set; }
        [Display(Name = "Điểm")]
        public int Point { get; set; }
        [Display(Name = "Nhận xét")]
        public string Comment { get; set; }
        [Display(Name = "Khách hàng")]
        public string Customer { get; set; }
        [Display(Name = "Lúc")]
        public DateTime TimeComment { get; set; }
        public int FoodID { get; set; }
        public string Star
        {
            get
            {
                string s = "";
                for(int i=0; i<Point; i++)
                {
                    s.Insert(0, "*");
                }
                return s;
            }
        }

    }
}