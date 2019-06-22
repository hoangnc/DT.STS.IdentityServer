using MediatR;

namespace DT.STS.IdentityServer.Application.Clients.Queries
{
    public class GetClientByClientIdQuery : IRequest<GetClientByClientIdDto>
    {
        public string ClientId { get; set; }
    }
}
