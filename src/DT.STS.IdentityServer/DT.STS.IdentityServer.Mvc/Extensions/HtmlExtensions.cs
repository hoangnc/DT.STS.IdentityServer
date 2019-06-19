using System.Web.Mvc;

namespace DT.STS.IdentityServer.Mvc.Extensions
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString Image(this HtmlHelper html, string imageBase64String)
        {
            string img = string.Format("data:image/jpg;base64,{0}", imageBase64String);
            return new MvcHtmlString("<img src='" + img + "' />");
        }
    }
}