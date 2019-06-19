using DT.STS.IdentityServer.Application.Mapper;
using DT.STS.IdentityServer.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DT.STS.IdentityServer.Application.Clients.Commands
{
    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, int>
    {
        private readonly STSDbContext _context;
        public CreateClientCommandHandler(STSDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Client client = request.ToClient();
            _context.Clients.Add(client);
            return await _context.SaveChangesAsync();
        }
    }
}
