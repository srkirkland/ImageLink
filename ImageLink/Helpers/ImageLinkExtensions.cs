using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ImageLink.Helpers
{
    public static class ImageLinkExtensions
    {
        public const string STR_NULL_OR_EMPTY = "Value cannot be null or empty.";

        public static string ImageLink(this HtmlHelper htmlHelper, string linkText, string actionName, string imageUrl)
        {
            return ImageLink(htmlHelper, linkText, actionName, null /* controllerName */, new RouteValueDictionary(), new RouteValueDictionary(), imageUrl, new RouteValueDictionary());
        }

        public static string ImageLink(this HtmlHelper htmlHelper, string linkText, string actionName, object routeValues, string imageUrl)
        {
            return ImageLink(htmlHelper, linkText, actionName, null /* controllerName */, new RouteValueDictionary(routeValues), new RouteValueDictionary(), imageUrl, new RouteValueDictionary());
        }

        public static string ImageLink(this HtmlHelper htmlHelper, string linkText, string actionName, object routeValues, object htmlAttributes, string imageUrl, object imageHtmlAttributes)
        {
            return ImageLink(htmlHelper, linkText, actionName, null /* controllerName */, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes), imageUrl, new RouteValueDictionary(imageHtmlAttributes));
        }

        public static string ImageLink(this HtmlHelper htmlHelper, string linkText, string actionName, RouteValueDictionary routeValues, string imageUrl)
        {
            return ImageLink(htmlHelper, linkText, actionName, null /* controllerName */, routeValues, new RouteValueDictionary(), imageUrl, new RouteValueDictionary());
        }

        public static string ImageLink(this HtmlHelper htmlHelper, string linkText, string actionName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes, string imageUrl, IDictionary<string, object> imageHtmlAttributes)
        {
            return ImageLink(htmlHelper, linkText, actionName, null /* controllerName */, routeValues, htmlAttributes, imageUrl, imageHtmlAttributes);
        }

        public static string ImageLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, string imageUrl)
        {
            return ImageLink(htmlHelper, linkText, actionName, controllerName, new RouteValueDictionary(), new RouteValueDictionary(), imageUrl, new RouteValueDictionary());
        }

        public static string ImageLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues, object htmlAttributes, string imageUrl, object imageHtmlAttributes)
        {
            return ImageLink(htmlHelper, linkText, actionName, controllerName, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes), imageUrl, new RouteValueDictionary(imageHtmlAttributes));
        }

        public static string ImageLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes, string imageUrl, IDictionary<string, object> imageHtmlAttributes)
        {
            if (String.IsNullOrEmpty(linkText))
            {
                throw new ArgumentException(STR_NULL_OR_EMPTY, "linkText");
            }
            return GenerateImageLink(htmlHelper.ViewContext.RequestContext, htmlHelper.RouteCollection, linkText, null/* routeName */, actionName, controllerName, routeValues, htmlAttributes, imageUrl, imageHtmlAttributes);
        }

        public static string ImageLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, string protocol, string hostName, string fragment, object routeValues, object htmlAttributes, string imageUrl, object imageHtmlAttributes)
        {
            return ImageLink(htmlHelper, linkText, actionName, controllerName, protocol, hostName, fragment, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes), imageUrl, new RouteValueDictionary(imageHtmlAttributes));
        }

        public static string ImageLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes, string imageUrl, IDictionary<string, object> imageHtmlAttributes)
        {
            if (String.IsNullOrEmpty(linkText))
            {
                throw new ArgumentException(STR_NULL_OR_EMPTY, "linkText");
            }

            return GenerateImageLink(htmlHelper.ViewContext.RequestContext, htmlHelper.RouteCollection, linkText, null /* routeName */, actionName, controllerName, protocol, hostName, fragment, routeValues, htmlAttributes, imageUrl, imageHtmlAttributes);
        }

        public static string GenerateImageLink(RequestContext requestContext, RouteCollection routeCollection, string linkText, string routeName, string actionName, string controllerName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes, string imageUrl, IDictionary<string, object> imageHtmlAttributes)
        {
            return GenerateImageLink(requestContext, routeCollection, linkText, routeName, actionName, controllerName, null/* protocol */, null/* hostName */, null/* fragment */, routeValues, htmlAttributes, imageUrl, imageHtmlAttributes);
        }

        public static string GenerateImageLink(RequestContext requestContext, RouteCollection routeCollection, string linkText, string routeName, string actionName, string controllerName, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes, string imageUrl, IDictionary<string, object> imageHtmlAttributes)
        {
            return GenerateImageLinkInternal(requestContext, routeCollection, linkText, routeName, actionName, controllerName, protocol, hostName, fragment, routeValues, htmlAttributes, imageUrl, imageHtmlAttributes, true /* includeImplicitMvcValues */);
        }

        private static string GenerateImageLinkInternal(RequestContext requestContext, RouteCollection routeCollection, string linkText, string routeName, string actionName, string controllerName, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes, string imageUrl, IDictionary<string, object> imageHtmlAttributes, bool includeImplicitMvcValues)
        {
            string url = UrlHelper.GenerateUrl(routeName, actionName, controllerName, protocol, hostName, fragment, routeValues, routeCollection, requestContext, includeImplicitMvcValues);

            TagBuilder imageTagBuilder = new TagBuilder("img");
            imageTagBuilder.MergeAttribute("src", imageUrl); //TODO: Fix!
            imageTagBuilder.MergeAttribute("alt", linkText);
            imageTagBuilder.MergeAttributes(imageHtmlAttributes);
            
            TagBuilder tagBuilder = new TagBuilder("a")
            {
                InnerHtml = imageTagBuilder.ToString(TagRenderMode.SelfClosing)
            };
            tagBuilder.MergeAttributes(htmlAttributes);
            tagBuilder.MergeAttribute("href", url);
            return tagBuilder.ToString(TagRenderMode.Normal);
        }

    }

}