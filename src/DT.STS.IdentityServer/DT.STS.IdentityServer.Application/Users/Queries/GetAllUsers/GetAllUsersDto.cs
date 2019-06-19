using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DT.STS.IdentityServer.Application.Users.Queries
{
    public class GetAllUsersDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string DepartmentName { get; set; }
    }
}
