using System;

namespace DT.STS.IdentityServer.Domain.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullNameUnicode { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public bool Active { get; set; }
        public byte[] JpegPhoto { get; set; }
        public string ManagerName { get; set; }
        public string Email { get; set; }
        public string Domain { get; set; }
        //public User Manager { get; set; }
        public string DepartmentName { get; set; }
        //public Department Department { get; set; }
        public string DirectReports { get; set; }
        public string Groups { get; set; }
    }
}
