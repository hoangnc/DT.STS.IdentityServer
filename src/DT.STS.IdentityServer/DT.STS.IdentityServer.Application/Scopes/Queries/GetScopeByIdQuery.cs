using MediatR;

namespace DT.STS.IdentityServer.Application.Scopes.Queries
{
    public class GetScopeByIdQuery : IRequest<GetScopeByIdDto>
    {
        public int Id { get; set; }
    }
}
