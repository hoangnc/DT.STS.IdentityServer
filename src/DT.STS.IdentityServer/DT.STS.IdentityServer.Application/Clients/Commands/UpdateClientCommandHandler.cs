using DT.STS.IdentityServer.Application.Mapper;
using DT.STS.IdentityServer.Domain.Entities;
using DT.STS.IdentityServer.Persistence;
using MediatR;
using System.Data.Entity.Migrations;
using System.Threading;
using System.Threading.Tasks;

namespace DT.STS.IdentityServer.Application.Clients.Commands
{
    public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, int>
    {
        private readonly STSDbContext _context;
        public UpdateClientCommandHandler(STSDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            Client client = request.ToClient();
            _context.Clients.AddOrUpdate(c => new { c.Id, c.ClientId }, client);
            return await _context.SaveChangesAsync();
        }
    }
}
