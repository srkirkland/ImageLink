using System.Web;
using System.Web.Mvc;

namespace ImageLink.Helpers
{
    public static class ImageNameHelper
    {
        public static string GenerateimageName(string imageName, HttpContextBase httpContext)
        {
            return UrlHelper.GenerateContentUrl(string.Format("~/Images/{0}", imageName), httpContext);
        }
    }
}