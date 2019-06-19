using System.Collections;

namespace DT.STS.IdentityServer.Common.Models
{
    public class DataSourceResult
    {
        public IEnumerable Data { get; set; }
        public int Total { get; set; }
    }
}
