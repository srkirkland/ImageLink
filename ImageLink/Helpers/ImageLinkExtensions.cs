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

        public static string ImageLink(this HtmlHelper htmlHelper, string imageUrl, string linkText, string actionName)
        {
            return ImageLink(htmlHelper /* controllerName */, imageUrl, linkText, actionName, null, new RouteValueDictionary(), new RouteValueDictionary(), new RouteValueDictionary());
        }

        public static string ImageLink(this HtmlHelper htmlHelper, string imageUrl, string linkText, string actionName, object routeValues)
        {
            return ImageLink(htmlHelper /* controllerName */, imageUrl, linkText, actionName, null, new RouteValueDictionary(routeValues), new RouteValueDictionary(), new RouteValueDictionary());
        }

        public static string ImageLink(this HtmlHelper htmlHelper, string imageUrl, string linkText, string actionName, object routeValues, object htmlAttributes, object imageHtmlAttributes)
        {
            return ImageLink(htmlHelper /* controllerName */, imageUrl, linkText, actionName, null, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes), new RouteValueDictionary(imageHtmlAttributes));
        }

        public static string ImageLink(this HtmlHelper htmlHelper, string imageUrl, string linkText, string actionName, RouteValueDictionary routeValues)
        {
            return ImageLink(htmlHelper /* controllerName */, imageUrl, linkText, actionName, null, routeValues, new RouteValueDictionary(), new RouteValueDictionary());
        }

        public static string ImageLink(this HtmlHelper htmlHelper, string imageUrl, string linkText, string actionName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes, IDictionary<string, object> imageHtmlAttributes)
        {
            return ImageLink(htmlHelper /* controllerName */, imageUrl, linkText, actionName, null, routeValues, htmlAttributes, imageHtmlAttributes);
        }

        public static string ImageLink(this HtmlHelper htmlHelper, string imageUrl, string linkText, string actionName, string controllerName)
        {
            return ImageLink(htmlHelper, imageUrl, linkText, actionName, controllerName, new RouteValueDictionary(), new RouteValueDictionary(), new RouteValueDictionary());
        }

        public static string ImageLink(this HtmlHelper htmlHelper, string imageUrl, string linkText, string actionName, string controllerName, object routeValues, object htmlAttributes, object imageHtmlAttributes)
        {
            return ImageLink(htmlHelper, imageUrl, linkText, actionName, controllerName, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes), new RouteValueDictionary(imageHtmlAttributes));
        }

        public static string ImageLink(this HtmlHelper htmlHelper, string imageUrl, string linkText, string actionName, string controllerName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes, IDictionary<string, object> imageHtmlAttributes)
        {
            if (String.IsNullOrEmpty(linkText))
            {
                throw new ArgumentException(STR_NULL_OR_EMPTY, "linkText");
            }
            return GenerateImageLink(htmlHelper.ViewContext.RequestContext, htmlHelper.RouteCollection /* routeName */, imageUrl, linkText, null, actionName, controllerName, routeValues, htmlAttributes, imageHtmlAttributes);
        }

        public static string ImageLink(this HtmlHelper htmlHelper, string imageUrl, string linkText, string actionName, string controllerName, string protocol, string hostName, string fragment, object routeValues, object htmlAttributes, object imageHtmlAttributes)
        {
            return ImageLink(htmlHelper, imageUrl, linkText, actionName, controllerName, protocol, hostName, fragment, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes), new RouteValueDictionary(imageHtmlAttributes));
        }

        public static string ImageLink(this HtmlHelper htmlHelper, string imageUrl, string linkText, string actionName, string controllerName, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes, IDictionary<string, object> imageHtmlAttributes)
        {
            if (String.IsNullOrEmpty(linkText))
            {
                throw new ArgumentException(STR_NULL_OR_EMPTY, "linkText");
            }

            return GenerateImageLink(htmlHelper.ViewContext.RequestContext, htmlHelper.RouteCollection /* routeName */, imageUrl, linkText, null, actionName, controllerName, protocol, hostName, fragment, routeValues, htmlAttributes, imageHtmlAttributes);
        }

        public static string GenerateImageLink(RequestContext requestContext, RouteCollection routeCollection, string imageUrl, string linkText, string routeName, string actionName, string controllerName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes, IDictionary<string, object> imageHtmlAttributes)
        {
            return GenerateImageLink(requestContext, routeCollection /* protocol */ /* hostName */ /* fragment */, imageUrl, linkText, routeName, actionName, controllerName, null, null, null, routeValues, htmlAttributes, imageHtmlAttributes);
        }

        public static string GenerateImageLink(RequestContext requestContext, RouteCollection routeCollection, string imageUrl, string linkText, string routeName, string actionName, string controllerName, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes, IDictionary<string, object> imageHtmlAttributes)
        {
            return GenerateImageLinkInternal(requestContext, routeCollection, imageUrl, linkText, routeName, actionName,
                                             controllerName, protocol, hostName, fragment, routeValues, htmlAttributes,
                                             imageHtmlAttributes, true
                /* includeImplicitMvcValues */);
        }

        private static string GenerateImageLinkInternal(RequestContext requestContext, RouteCollection routeCollection, string imageUrl, string linkText, string routeName, string actionName, string controllerName, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes, IDictionary<string, object> imageHtmlAttributes, bool includeImplicitMvcValues)
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