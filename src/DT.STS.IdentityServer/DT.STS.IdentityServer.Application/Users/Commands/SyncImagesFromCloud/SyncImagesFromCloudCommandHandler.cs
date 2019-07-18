using DT.STS.IdentityServer.Domain.Entities;
using DT.STS.IdentityServer.Persistence;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DT.STS.IdentityServer.Application.Users.Commands
{
    public class SyncImagesFromCloudCommandHandler : IRequestHandler<SyncImagesFromCloudCommand, int>
    {
        private readonly STSDbContext _context;
        public SyncImagesFromCloudCommandHandler(STSDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(SyncImagesFromCloudCommand request, CancellationToken cancellationToken)
        {
            if (request.Users.Any())
            {
                var users = request.Users.Select(user => new User {
                    Id = user.Id,
                    JpegPhoto = user.JpegPhoto
                });

                foreach(var user in users)
                {
                    _context.Entry(user)
                        .Property(nameof(user.JpegPhoto))
                        .IsModified = true;

                    await _context.SaveChangesAsync();
                }
            }

            return 1;
        }
    }
}
