using DT.STS.IdentityServer.Application.Mapper;
using DT.STS.IdentityServer.Domain.Entities;
using DT.STS.IdentityServer.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DT.STS.IdentityServer.Application.Scopes.Commands
{
    public class UpdateScopeCommandHandler : IRequestHandler<UpdateScopeCommand, int>
    {
        private readonly STSDbContext _context;
        public UpdateScopeCommandHandler(STSDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(UpdateScopeCommand request, CancellationToken cancellationToken)
        {
            Scope scope = request.ToScope();
            using (System.Data.Entity.DbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Scopes.AddOrUpdate(sc => new { Id = sc.Id }, scope);
                    await _context.SaveChangesAsync();
                    var scopeClaims = _context.ScopeScopeClaims;
                    _context.ScopeScopeClaims.RemoveRange(scopeClaims);
                    await _context.SaveChangesAsync();
                    var newScopeClaims = request.ScopeClaims.Select(id => new ScopeScopeClaim
                    {
                        ScopeClaimId = id,
                        ScopeId = scope.Id,
                        CreatedBy = request.CreatedBy,
                        CreatedOn = request.CreatedOn,
                        Deleted = false
                    });

                    _context.ScopeScopeClaims.AddRange(newScopeClaims);
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
