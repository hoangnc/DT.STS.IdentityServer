using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DT.STS.IdentityServer.Common.Models
{
    class DataSourceRequestModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            DataSourceRequest request = new DataSourceRequest();

            int currentPage;
            int pageSize;

            if (TryGetValue(bindingContext, DataSourceParameters.PageNum, out currentPage))
            {
                request.PageNum = currentPage;
            }

            if (TryGetValue(bindingContext, DataSourceParameters.PageSize, out pageSize))
            {
                request.PageSize = pageSize;
            }

            return request;
        }

        public string Prefix { get; set; }

        private bool TryGetValue<T>(ModelBindingContext bindingContext, string key, out T result)
        {
            if (!string.IsNullOrEmpty(Prefix))
            {
                key = Prefix + "-" + key;
            }

            var value = bindingContext.ValueProvider.GetValue(key);

            if (value == null)
            {
                result = default(T);

                return false;
            }

            result = (T)value.ConvertTo(typeof(T));

            return true;
        }
    }
}
