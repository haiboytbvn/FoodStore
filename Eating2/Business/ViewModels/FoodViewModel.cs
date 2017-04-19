using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Eating2.Business.ViewModels
{
    public class FoodViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Bắt buộc")]
        [Display(Name = "Tên món ăn")]
        public string Name { get; set; }
        [Display(Name = "Giá")]
        public double Cost { get; set; }
        [Display(Name = "Cách chế biến")]
        public string Processing { get; set; }
        public int StoreID { get; set; }
        [Display(Name = "Cửa hàng")]
        public string inStore { get; set; }
        public int NumberOfRate { get; set; }

        public string FoodPictureURL { get; set; }
        public bool HasFoodPicture { get; set; }
        public List<Image> listFoodPicturesURL { get; set; }
        public int numberOfFoodPicture { get; set; }

    }

    public class Image
    {
        public int number;
        public string path;
    }
}