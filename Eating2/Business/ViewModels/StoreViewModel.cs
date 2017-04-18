using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

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
        public int NumberOfFood { get; set; }   
        [Display(Name = "Chủ sở hữu")]
        public string Owner { get; set; }
        [Display(Name = "Quận/Huyện")]
        public string District { get; set; }
        public List<SelectListItem> Districts
        {
            get
            {
                List<SelectListItem> district = new List<SelectListItem>();
                district.Add(new SelectListItem { Text = "Ba Đình", Value = "Ba Đình" });
                district.Add(new SelectListItem { Text = "Ba Vì", Value = "Ba Vì" });
                district.Add(new SelectListItem { Text = "Bắc Từ Liêm", Value = "Bắc Từ Liêm" });
                district.Add(new SelectListItem { Text = "Cầu Giấy", Value = "Cầu Giấy" });
                district.Add(new SelectListItem { Text = "Đông Anh", Value = "Đông Anh" });
                district.Add(new SelectListItem { Text = "Đống Đa", Value = "Đống Đa" });
                district.Add(new SelectListItem { Text = "Gia Lâm", Value = "Gia Lâm" });
                district.Add(new SelectListItem { Text = "Hà Đông", Value = "Hà Đông" });
                district.Add(new SelectListItem { Text = "Hai Bà Trưng", Value = "Hai Bà Trưng" });     
                district.Add(new SelectListItem { Text = "Hoàn Kiếm", Value = "Hoàn Kiếm" });
                district.Add(new SelectListItem { Text = "Hoàng Mai", Value = "Hoàng Mai" });
                district.Add(new SelectListItem { Text = "Long Biên", Value = "Long Biên" });
                district.Add(new SelectListItem { Text = "Nam Từ Liêm", Value = "Nam Từ Liêm" });
                district.Add(new SelectListItem { Text = "Tây Hồ", Value = "Tây Hồ" });
                district.Add(new SelectListItem { Text = "Thanh Xuân", Value = "Thanh Xuân" });
                district.Add(new SelectListItem { Text = "Sóc Sơn", Value = "Sóc Sơn" });

                return district;
            }
           
        }


    }
}
