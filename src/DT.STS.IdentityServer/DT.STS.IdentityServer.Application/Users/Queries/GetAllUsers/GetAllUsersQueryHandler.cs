using DT.STS.IdentityServer.Application.Mapper;
using DT.STS.IdentityServer.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DT.STS.IdentityServer.Application.Users.Queries
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<GetAllUsersDto>>
    {
        private readonly STSDbContext _context;
        public GetAllUsersQueryHandler(STSDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetAllUsersDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            List<Domain.Entities.User> users = await _context.Users.ToListAsync();
            List<GetAllUsersDto> result = users.Select(u => u.ToGetAllUsersDto()).ToList();
            return result;
        }
    }
}
