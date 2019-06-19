using System.Web.Mvc;

namespace DT.STS.IdentityServer.Common.Models
{
    public class DataSourceRequestAttribute : CustomModelBinderAttribute
    {
        public string Prefix { get; set; }

        public override IModelBinder GetBinder()
        {
            return new DataSourceRequestModelBinder() { Prefix = Prefix };
        }
    }
}
