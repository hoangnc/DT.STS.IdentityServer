using System;

namespace DT.STS.IdentityServer.Application.Users.Queries
{
    public class SearchUsersByTokenPagedDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullNameUnicode { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public bool Active { get; set; }
        public string JpegPhoto { get; set; }
        public string ManagerName { get; set; }
        public string Email { get; set; }
        public string DepartmentName { get; set; }
        public string DirectReports { get; set; }
        public string Groups { get; set; }
    }
}
