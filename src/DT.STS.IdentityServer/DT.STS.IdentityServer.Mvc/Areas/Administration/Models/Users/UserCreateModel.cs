using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DT.STS.IdentityServer.Mvc.Areas.Administration.Models.Users
{
    public class UserCreateModel
    {
        [Display(Name = "Domain")]
        [Required(ErrorMessage = "Xin vui lòng chọn domain.")]
        public string Domain { get; set; }

        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "{0} không được bỏ trống.")]
        [StringLength(50, ErrorMessage = "{0} không được lớn hơn {1} ký tự.")]
        public string UserName { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "{0} không được bỏ trống.")]
        [StringLength(20, ErrorMessage = "{0} không được lớn hơn {1} ký tự.")]
        public string Password { get; set; }

        [Display(Name = "Tên")]
        [Required(ErrorMessage = "{0} không được bỏ trống.")]
        [StringLength(50, ErrorMessage = "{0} không được lớn hơn {1} ký tự.")]
        public string FirstName { get; set; }

        [Display(Name = "Họ và tên lót")]
        [Required(ErrorMessage = "{0} không được bỏ trống.")]
        [StringLength(50, ErrorMessage = "{0} không được lớn hơn {1} ký tự.")]
        public string LastName { get; set; }

        [Display(Name = "Tên đầy đủ(Tiếng Việt)")]
        [StringLength(100, ErrorMessage = "{0} không được lớn hơn {1} ký tự.")]
        public string FullNameUnicode { get; set; }

        [Display(Name = "Email address")]
        [EmailAddress(ErrorMessage = "Địa chỉ email nhập chưa đúng")]
        public string Email { get; set; }

        [Display(Name = "Tình trạng")]
        public bool Active { get; set; }

        [Display(Name = "Quản lý trực tiếp")]
        public string ManagerName { get; set; }

        [Display(Name = "Phòng ban")]
        public string DepartmentName { get; set; }

        public IList<SelectListItem> AvailableDomains { get; set; }
        public IList<SelectListItem> Departments { get; set; }
        public IList<SelectListItem> Users { get; set; }

        public UserCreateModel()
        {
            AvailableDomains = new List<SelectListItem>();
            Departments = new List<SelectListItem>();
            Users = new List<SelectListItem>();
        }
    }
}