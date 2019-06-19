using DT.STS.IdentityServer.Application.Mapper;
using DT.STS.IdentityServer.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DT.STS.IdentityServer.Application.Users.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly STSDbContext _context;
        public CreateUserCommandHandler(STSDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = request.ToUser();
            _context.Users.Add(user);
             await _context.SaveChangesAsync();
            return user.Id;
        }
    }
}
