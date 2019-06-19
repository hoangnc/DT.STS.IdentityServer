using MediatR;
using System.Collections.Generic;

namespace DT.STS.IdentityServer.Application.Users.Queries
{
    public class GetAllUsersQuery : IRequest<List<GetAllUsersDto>>
    {
    }
}
