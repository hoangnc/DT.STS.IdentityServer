using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;

namespace DT.STS.IdentityServer.Common.Models
{
    public class DataSourceParameterBinding : HttpParameterBinding
    {
        public DataSourceParameterBinding(HttpParameterDescriptor descriptor)
           : base(descriptor) {
        }

        public override Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider, HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            IEnumerable<KeyValuePair<string, string>> nameVal = actionContext.Request.GetQueryNameValuePairs();
            return Task.FromResult(0);
        }
    }
}
