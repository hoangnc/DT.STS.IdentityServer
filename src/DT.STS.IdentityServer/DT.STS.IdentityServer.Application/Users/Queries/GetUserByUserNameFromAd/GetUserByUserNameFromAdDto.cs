using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DT.STS.IdentityServer.Application.Users.Queries
{
    public class GetUserByUserNameFromAdDto
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ManagerName { get; set; }
        public string DepartmentName { get; set; }
        public string DirectReports { get; set; }
        public string Email { get; set; }
        public byte[] JpegPhoto { get; set; }
    }
}
