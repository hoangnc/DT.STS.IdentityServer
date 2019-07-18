using MediatR;

namespace DT.STS.IdentityServer.Application.Users.Queries
{
    public class GetUserByIdQuery : IRequest<GetUserByIdDto>
    {
        public int Id { get; set; }
    }
}
