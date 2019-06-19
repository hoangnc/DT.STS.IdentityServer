
using Wdc.DirectoryLib.Types;

namespace DT.STS.IdentityServer.Application.Users.Commands
{
    public class AuthenticateDto
    {
        public UserAccount User { get; set; }
        public AccountStatus AccountStatus { get; set; }
        public string Message { get; set; }
    }
}
