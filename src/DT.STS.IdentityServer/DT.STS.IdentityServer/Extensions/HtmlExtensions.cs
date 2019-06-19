using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DT.STS.IdentityServer.Extensions
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString Image(this HtmlHelper html, string imageBase64String)
        {
            var img = string.Format("data:image/jpg;base64,{0}", imageBase64String);
            return new MvcHtmlString("<img src='" + img + "' />");
        }
    }
}