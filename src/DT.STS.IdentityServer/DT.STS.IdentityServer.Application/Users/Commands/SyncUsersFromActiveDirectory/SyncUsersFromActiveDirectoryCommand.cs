using MediatR;

namespace DT.STS.IdentityServer.Application.Users.Commands
{
    public class SyncUsersFromActiveDirectoryCommand : BaseCommand, IRequest<int>
    {
    }
}
