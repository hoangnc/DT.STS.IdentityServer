namespace DT.STS.IdentityServer.Application.Departments.Queries
{
    public class SearchDepartmentsByTokenPagedDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
