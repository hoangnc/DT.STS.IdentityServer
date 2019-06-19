using DT.STS.IdentityServer.Application.Mapper;
using DT.STS.IdentityServer.Domain.Entities;
using DT.STS.IdentityServer.Persistence;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DT.STS.IdentityServer.Application.Scopes.Commands
{
    public class CreateScopeCommandHandler : IRequestHandler<CreateScopeCommand, int>
    {
        private readonly STSDbContext _context;
        public CreateScopeCommandHandler(STSDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(CreateScopeCommand request, CancellationToken cancellationToken)
        {
            Scope scope = request.ToScope();
            using (System.Data.Entity.DbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {                 
                    _context.Scopes.Add(scope);
                    await _context.SaveChangesAsync();                   
              
                    System.Collections.Generic.IEnumerable<ScopeScopeClaim> scopeClaims = request.ScopeClaims.Select(id => new ScopeScopeClaim
                    {
                        ScopeClaimId = id,
                        ScopeId = scope.Id,
                        CreatedBy = request.CreatedBy,
                        CreatedOn = request.CreatedOn,
                        Deleted = false
                    });

                    _context.ScopeScopeClaims.AddRange(scopeClaims);
                     await _context.SaveChangesAsync();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
                return scope.Id;
            }
        }
    }
}
