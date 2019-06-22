using DT.STS.IdentityServer.Application.Mapper;
using DT.STS.IdentityServer.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DT.STS.IdentityServer.Application.UserScopes.Commands
{
    public class InsertOrUpdateUserScopeCommandHandler : IRequestHandler<InsertOrUpdateUserScopeCommand, int>
    {
        private readonly STSDbContext _context;
        public InsertOrUpdateUserScopeCommandHandler(STSDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(InsertOrUpdateUserScopeCommand request, CancellationToken cancellationToken)
        {
            var userScope = request.ToUserScope();
            _context.UserScopes.AddOrUpdate(us => new { us.ScopeName }, userScope);
            await _context.SaveChangesAsync();
            return userScope.Id;
        }
    }
}
