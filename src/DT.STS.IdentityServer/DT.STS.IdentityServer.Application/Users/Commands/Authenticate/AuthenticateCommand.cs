using MediatR;

namespace DT.STS.IdentityServer.Application.Users.Commands
{
    public class AuthenticateCommand : BaseCommand, IRequest<AuthenticateDto>
    {
        public string Domain { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
