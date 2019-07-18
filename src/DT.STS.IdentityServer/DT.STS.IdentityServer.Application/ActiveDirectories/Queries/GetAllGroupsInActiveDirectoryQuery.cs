using MediatR;
using System.Collections.Generic;

namespace DT.STS.IdentityServer.Application.ActiveDirectories.Queries
{
    public class GetAllGroupsInActiveDirectoryQuery : IRequest<List<string>>
    {

    }
}
