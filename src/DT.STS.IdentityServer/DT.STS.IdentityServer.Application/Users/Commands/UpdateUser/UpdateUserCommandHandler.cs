using DT.STS.IdentityServer.Application.Mapper;
using DT.STS.IdentityServer.Domain.Entities;
using DT.STS.IdentityServer.Persistence;
using MediatR;
using System.Data.Entity.Migrations;
using System.Threading;
using System.Threading.Tasks;

namespace DT.STS.IdentityServer.Application.Users.Commands
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, int>
    {
        private readonly STSDbContext _context;
        public UpdateUserCommandHandler(STSDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            User user = request.ToUser();
            _context.Users.AddOrUpdate(u => new { u.Id }, user);
            return await _context.SaveChangesAsync();
        }
    }
}
