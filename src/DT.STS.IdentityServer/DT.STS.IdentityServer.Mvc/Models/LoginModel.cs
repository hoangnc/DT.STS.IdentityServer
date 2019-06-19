using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DT.STS.IdentityServer.Mvc.Models
{
    public class LoginModel
    {
        [Display(Name = "Domain")]
        [Required(ErrorMessage = "Xin vui lòng chọn domain.")]
        public string Domain { get; set; }

        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Xin vui lòng nhập tên đăng nhập")]
        public string UserName { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Xin vui lòng nhập mật khẩu")]
        public string Password { get; set; }
        public IList<SelectListItem> AvailableDomains { get; set; }
        public string Rememberme { get; set; }
        public LoginModel()
        {
            AvailableDomains = new List<SelectListItem>();
        }
    }
}