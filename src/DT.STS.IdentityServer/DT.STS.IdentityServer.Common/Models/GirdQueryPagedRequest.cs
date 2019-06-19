
namespace DT.STS.IdentityServer.Common.Models
{
    public class GirdQueryPagedRequest
    {
        public int FiltersCount { get; set; }
        public int GroupsCount { get; set; }
        public int PageNum { get; set; }
        public int PageSize { get; set; }
        public int RecordStartIndex { get; set; }
        public int RecordEndIndex { get; set; }
    }
}
