using DT.STS.IdentityServer.Domain.Entities;
using DT.STS.IdentityServer.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DT.STS.IdentityServer.Application.Permissions.Queries
{
    public class GetPermissionsByUserAndScopesQueryHandler : IRequestHandler<GetPermissionsByUserAndScopesQuery, List<GetPermissionsByUserAndScopesDto>>
    {
        private readonly STSDbContext _context;
        public GetPermissionsByUserAndScopesQueryHandler(STSDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetPermissionsByUserAndScopesDto>> Handle(GetPermissionsByUserAndScopesQuery request, CancellationToken cancellationToken)
        {
            List<UserScope> userScopes = await _context.UserScopes.Where(us => !us.Deleted
            && request.Scopes.Contains(us.ScopeName)
            && us.Users.Contains(request.UserName)).ToListAsync();

            List<GetPermissionsByUserAndScopesDto> result = new List<GetPermissionsByUserAndScopesDto>();
            foreach (UserScope userScope in userScopes)
            {
                if (!string.IsNullOrEmpty(userScope.Permissions))
                {
                    string[] permissions = userScope.Permissions.Split(';');
                    if (permissions != null && permissions.Any())
                    {
                        result.AddRange(permissions.Select(permission =>new GetPermissionsByUserAndScopesDto {
                            ClaimName = permission.Split('_')[0],
                            ClaimValue = permission.Split('_')[1],
                            ScopeName = userScope.ScopeName
                        }));
                    }
                }
            }

            return result;
        }
    }
}
