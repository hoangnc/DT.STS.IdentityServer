using IdentityServer3.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DT.STS.IdentityServer.Mvc.Areas.Administration.Models.Scopes
{
    public class ScopeCreateModel
    {
        [Display(Name = "Tên")]
        [Required(ErrorMessage = "{0} không được bỏ trống.")]
        [StringLength(128, ErrorMessage = "{0} không được lớn hơn {1} ký tự.")]
        public string Name { get; set; }

        [Display(Name = "Tên hiển thị")]
        [Required(ErrorMessage = "{0} không được bỏ trống.")]
        [StringLength(128, ErrorMessage = "{0} không được lớn hơn {1} ký tự.")]
        public string DisplayName { get; set; }

        [Display(Name = "Mô tả")]
        [Required(ErrorMessage = "{0} không được bỏ trống.")]
        [StringLength(1000, ErrorMessage = "{0} không được lớn hơn {1} ký tự.")]
        public string Description { get; set; }

        [Display(Name = "Loại")]
        public ScopeType Type { get; set; }

        [Display(Name = "Bắt buộc")]
        public bool Required { get; set; }

        [Display(Name = "Emphasize")]
        public bool Emphasize { get; set; }

        [Display(Name="Include all claims for user")]
        public bool IncludeAllClaimsForUser { get; set; }

        [Display(Name = "Claims rule")]
        public string ClaimsRule { get; set; }

        [Display(Name = "Hiển thị trong Discovery")]
        public bool ShowInDiscoveryDocument { get; set; }

        [Display(Name = "Claims")]
        [Required(ErrorMessage = "{0} không được bỏ trống.")]
        public List<int> ScopeClaims { get; set; }

        public ICollection<SelectListItem> AvailableScopeTypes { get; set; }
        public ICollection<SelectListItem> AvailableClaims { get; set; }

        public ScopeCreateModel()
        {
            AvailableClaims = new List<SelectListItem>();
            AvailableScopeTypes = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value = ScopeType.Identity.ToString(),
                    Selected = true,
                    Text = "Identity data",
                },
                new SelectListItem
                {
                    Value = ScopeType.Resource.ToString(),
                    Text= "Resource data"
                }
            };
        }
    }
}