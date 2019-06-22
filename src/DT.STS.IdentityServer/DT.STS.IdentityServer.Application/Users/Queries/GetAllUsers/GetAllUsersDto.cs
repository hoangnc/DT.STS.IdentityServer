﻿namespace DT.STS.IdentityServer.Application.Users.Queries
{
    public class GetAllUsersDto
    {
        public int Id { get; set; }
        public string Text => $"{LastName} {FirstName}";
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string DepartmentName { get; set; }
    }
}
