using IdentityServer3.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DT.STS.IdentityServer.Mvc.Areas.Administration.Models.Clients
{
    public class ClientCreateModel
    {
        [Display(Name = "Client Id (*)")]
        [Required(ErrorMessage = "{0} không được bỏ trống.")]
        [StringLength(128, ErrorMessage = "{0} không được lớn hơn {1} ký tự.")]
        public string ClientId { get; set; }

        [Display(Name = "Client Name (*)")]
        [Required(ErrorMessage = "{0} không được bỏ trống.")]
        [StringLength(256, ErrorMessage = "{0} không được lớn hơn {1} ký tự.")]
        public string ClientName { get; set; }

        [Display(Name = "Client Uri")]
        [StringLength(256, ErrorMessage = "{0} không được lớn hơn {1} ký tự.")]
        public string ClientUri { get; set; }

        [Display(Name = "Logo Uri")]
        [StringLength(256, ErrorMessage = "{0} không được lớn hơn {1} ký tự.")]
        public string LogoUri { get; set; }

        [Display(Name = "Require consent")]
        public bool RequireConsent { get; set; }

        [Display(Name = "Allow remember consent")]
        public bool AllowRememberConsent { get; set; }

        [Display(Name = "Flow")]
        public Flows Flow { get; set; }

        [Display(Name = "Allow Client Credentials only")]
        public bool AllowClientCredentialsOnly { get; set; }

        [Display(Name = "Redirect Uris")]
        public string RedirectUris { get; set; }

        [Display(Name = "Post logout redirect uris")]
        public string PostLogoutRedirectUris { get; set; }

        [Display(Name = "Logout uri")]
        public string LogoutUri { get; set; }

        [Display(Name = "Logout session required")]
        public bool LogoutSessionRequired { get; set; }

        [Display(Name = "Require sigout prompt")]
        public bool RequireSignOutPrompt { get; set; }

        [Display(Name = "Allow access tokens via browser")]
        public bool AllowAccessTokensViaBrowser { get; set; }

        [Display(Name = "Allowed custom grant types")]
        public bool AllowedCustomGrantTypes { get; set; }

        [Display(Name = "Identity token lifetime")]
        public int IdentityTokenLifetime { get; set; }

        [Display(Name = "Access token lifetime")]
        public int AccessTokenLifetime { get; set; }

        [Display(Name= "Authorization code lifetime")]
        public int AuthorizationCodeLifetime { get; set; }

        [Display(Name = "Absolute refresh token lifetime")]
        public int AbsoluteRefreshTokenLifetime { get; set; }

        [Display(Name = "Sliding refresh token lifetime")]
        public int SlidingRefreshTokenLifetime { get; set; }

        [Display(Name = "Refresh token usage")]
        public TokenUsage RefreshTokenUsage { get; set; }

        [Display(Name = "Refresh token expiration")]
        public TokenExpiration RefreshTokenExpiration { get; set; }

        [Display(Name = "Update access token claims on refresh")]
        public bool UpdateAccessTokenClaimsOnRefresh { get; set; }

        [Display(Name = "Access token type")]
        public AccessTokenType AccessTokenType { get; set; }

        [Display(Name = "Enable local login")]
        public bool EnableLocalLogin { get; set; }

        [Display(Name ="Include jwtid")]
        public bool IncludeJwtId { get; set; }

        [Display(Name = "Allowed Cors origins")]
        public string AllowedCorsOrigins { get; set; }

        [Display(Name = "Always send client claims")]
        public bool AlwaysSendClientClaims { get; set; }

        [Display(Name = "Prefix client claims")]
        public bool PrefixClientClaims { get; set; }

        public ICollection<SelectListItem> AvailableClaims { get; set; }
        public ICollection<SelectListItem> AvailableFlows { get; set; }
        public ICollection<SelectListItem> AvailableTokenExpirations { get; set; }
        public ClientCreateModel()
        {
            AvailableClaims = new List<SelectListItem>();

            AvailableTokenExpirations = new List<SelectListItem> {
                new SelectListItem
                {
                    Text = nameof(TokenExpiration.Absolute),
                    Value = TokenExpiration.Absolute.ToString()
                },
                new SelectListItem
                {
                    Text = nameof(TokenExpiration.Sliding),
                    Value = TokenExpiration.Sliding.ToString()
                }
            };

            AvailableFlows = new List<SelectListItem> {
                new SelectListItem
                {
                    Text = nameof(Flows.AuthorizationCode),
                    Value = Flows.AuthorizationCode.ToString()
                },
                new SelectListItem
                {
                    Text = nameof(Flows.AuthorizationCodeWithProofKey),
                    Value = Flows.AuthorizationCodeWithProofKey.ToString()
                },
                new SelectListItem
                {
                    Text = nameof(Flows.ClientCredentials),
                    Value = Flows.ClientCredentials.ToString()
                },
                new SelectListItem
                {
                    Text = nameof(Flows.Custom),
                    Value = Flows.Custom.ToString()
                },
                new SelectListItem
                {
                    Text = nameof(Flows.Hybrid),
                    Value = Flows.Hybrid.ToString()
                },
                new SelectListItem
                {
                    Text = nameof(Flows.HybridWithProofKey),
                    Value = Flows.HybridWithProofKey.ToString()
                },
                new SelectListItem
                {
                    Text = nameof(Flows.Implicit),
                    Value = Flows.Implicit.ToString()
                },
                new SelectListItem
                {
                    Text = nameof(Flows.ResourceOwner),
                    Value = Flows.ResourceOwner.ToString()
                }
            };
        }

        public void InitData()
        {
            AvailableTokenExpirations = new List<SelectListItem> {
                new SelectListItem
                {
                    Text = nameof(TokenExpiration.Absolute),
                    Value = "1"
                },
                new SelectListItem
                {
                    Text = nameof(TokenExpiration.Sliding),
                    Value = "0"
                }
            };

            AvailableFlows = new List<SelectListItem> {
                new SelectListItem
                {
                    Text = nameof(Flows.AuthorizationCode),
                    Value = "0"
                },
                new SelectListItem
                {
                    Text = nameof(Flows.AuthorizationCodeWithProofKey),
                    Value = "6"
                },
                new SelectListItem
                {
                    Text = nameof(Flows.ClientCredentials),
                    Value = "3"
                },
                new SelectListItem
                {
                    Text = nameof(Flows.Custom),
                    Value = "5"
                },
                new SelectListItem
                {
                    Text = nameof(Flows.Hybrid),
                    Value = "2"
                },
                new SelectListItem
                {
                    Text = nameof(Flows.HybridWithProofKey),
                    Value = "7"
                },
                new SelectListItem
                {
                    Text = nameof(Flows.Implicit),
                    Value = "1"
                },
                new SelectListItem
                {
                    Text = nameof(Flows.ResourceOwner),
                    Value = "4"
                }
            };
        }
    }
}