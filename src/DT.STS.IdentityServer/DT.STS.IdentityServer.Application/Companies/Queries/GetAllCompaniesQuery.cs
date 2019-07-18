using MediatR;
using System.Collections.Generic;

namespace DT.STS.IdentityServer.Application.Companies.Queries
{
    public class GetAllCompaniesQuery : IRequest<List<GetAllCompaniesDto>>
    {
    }
}
