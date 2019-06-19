using MediatR;

namespace DT.STS.IdentityServer.Application.Departments.Commands
{
    public class SyncDepartmentsFromActiveDirectoryCommand : BaseCommand, IRequest<int>
    {
    }
}
