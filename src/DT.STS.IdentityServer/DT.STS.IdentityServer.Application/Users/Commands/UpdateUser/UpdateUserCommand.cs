﻿using MediatR;
using System;

namespace DT.STS.IdentityServer.Application.Users.Commands
{
    public class UpdateUserCommand : BaseCommand, IRequest<int>
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string FullNameUnicode { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public bool Active { get; set; }
        public string ManagerName { get; set; }
        public string DepartmentName { get; set; }
        public string DirectReports { get; set; }
    }
}
