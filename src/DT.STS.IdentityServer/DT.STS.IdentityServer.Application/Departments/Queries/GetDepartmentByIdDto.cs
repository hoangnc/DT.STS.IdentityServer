namespace DT.STS.IdentityServer.Application.Departments.Queries
{
    public class GetDepartmentByIdDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
