using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eating2.Business.ViewModels
{
    public class StoreViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Bắt buộc")]
        [Display(Name = "Tên cửa hàng")]
        public string Name { get; set; }
        [Display(Name = "Thời gian mở cửa")]
        public string OpenTime { get; set; }
        [Display(Name = "Thời gian đóng cửa")]
        public string CloseTime { get; set; }
        [Display(Name = "Địa chỉ")]
        public string Place { get; set; }
        [Display(Name = "Mô tả")]
        public string Description { get; set; }
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Chủ sở hữu")]
        public string Owner { get; set; }
        
    }
}
