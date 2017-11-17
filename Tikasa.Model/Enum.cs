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
}
