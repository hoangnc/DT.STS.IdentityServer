using DT.STS.IdentityServer.Persistence;
using DT.STS.IdentityServer.Application.Mapper;
using MediatR;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using DT.STS.IdentityServer.Domain.Entities;

namespace DT.STS.IdentityServer.Application.Users.Queries
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, GetUserByIdDto>
    {
        private readonly STSDbContext _context;
        public GetUserByIdQueryHandler(STSDbContext context)
        {
            _context = context;
        }
        public async Task<GetUserByIdDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            GetUserByIdDto getUserByIdDto = new GetUserByIdDto();
            User user =  await _context.Users.FirstOrDefaultAsync(u => !u.Deleted && u.Id == request.Id);
            if (user != null)
            {
                getUserByIdDto = user.ToGetUserByIdDto();
            }
            return getUserByIdDto;
        }
    }
}
