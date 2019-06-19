using System.ComponentModel.DataAnnotations;

namespace DT.STS.IdentityServer.Models
{
    public class SigInModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string Rememberme { get; set; }
    }
}