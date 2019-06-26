using MediatR;

namespace DT.STS.IdentityServer.Application.UserScopes.Commands
{
    public class InsertOrUpdateUserScopeCommand : BaseCommand, IRequest<int>
    {
        public string Users { get; set; }
        public string ScopeName { get; set; }
        public string Permissions { get; set; }
    }
}
