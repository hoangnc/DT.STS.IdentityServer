using MediatR;

namespace DT.STS.IdentityServer.Application.Users.Queries
{
    public class GetUserByUserNameFromAdQuery : IRequest<GetUserByUserNameFromAdDto>
    {
        public string UserName { get; set; }
    }
}
