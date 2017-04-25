using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Eating2.Business.ViewModels
{
    public class DishViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Bắt buộc")]
        [Display(Name = "Tên món ăn")]
        public string Name { get; set; }
        [Display(Name = "Giá")]
        public string Cost { get; set; }
        [Display(Name = "Mô tả")]
        public string Processing { get; set; }
        public int NumberOfStoreHas { get; set; }
        [Display(Name = "Chủ sở hữu")]
        public string Owner { get; set; }
        public List<SelectListItem> Stores { get; set; }
        public string DishPictureURL { get; set; }
        public bool HasDishPicture { get; set; }

    }
}
