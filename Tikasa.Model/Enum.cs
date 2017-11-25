using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tikasa.Model
{
    public enum UserStatus
    {
        [Display(Name = "Mới")]
        New = 1,
        [Display(Name = "Đang hoạt động")]
        Active = 2,
        [Display(Name="Đang khóa")]
        Locked = 3,
        [Display(Name = "Đã xóa")]
        Deleted = 4,
    }

    public enum WebsiteStatus
    {
        [Display(Name = "Nháp")]
        Draft = 1,
        [Display(Name = "Đang hoạt động")]
        Active = 2,
        [Display(Name = "Hết hạn")]
        Expired = 3,
        [Display(Name = "Đã xóa")]
        Deleted = 4,
    }

    public enum Platform
    {
        [Display(Name = "Không xác định")]
        NA = 1,
        [Display(Name = "ASP.NET")]
        ASPNET =2,
        [Display(Name = "Wordpress")]
        Wordpress = 3,
        [Display(Name = "eCommerce")]
        eCommerce = 4
       
    }
    public enum TypeOfCate
    {
        [Display(Name = "Loại Website")]
        TypeOfWebsite = 1,
        [Display(Name = "Loại lĩnh vực")]
        TypeOfIndustry = 2,      

    }
}
