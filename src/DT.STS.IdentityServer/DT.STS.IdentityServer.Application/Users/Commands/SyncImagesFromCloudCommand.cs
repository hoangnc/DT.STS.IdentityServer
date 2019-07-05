using MediatR;
using System.Collections.Generic;

namespace DT.STS.IdentityServer.Application.Users.Commands
{
    public class SyncImagesFromCloudCommand : IRequest<int>
    {
        public List<dynamic> Users { get; set; }
    }
}
