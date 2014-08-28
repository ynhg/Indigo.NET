using Indigo.Modules;
using Indigo.Security;
using Indigo.Security.Util;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Indigo.Web.Mvc.Util
{
    public static class SecurityExtensions
    {
        public static bool IsPermitted(this User user, string actionName, string controllerName)
        {
            return user.IsPermitted(actionName, controllerName, null);
        }

        public static bool IsPermitted(this User user, string actionName, string controllerName, string area)
        {
            var function = GetFunction(actionName, controllerName, area);
            return user.IsPermitted(function);
        }

        public static MvcHtmlString ActionLinkAuthorized(this HtmlHelper htmlHelper, string linkText, string actionName, object routeValues, object htmlAttributes)
        {
            var controllerName = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");
            return htmlHelper.ActionLinkAuthorized(linkText, actionName, controllerName, routeValues, htmlAttributes);
        }

        public static MvcHtmlString ActionLinkAuthorized(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues, object htmlAttributes)
        {
            User user = SecurityUtils.CurrentUser;

            var routeValueDict = new RouteValueDictionary(routeValues);

            object area = null;
            routeValueDict.TryGetValue("Area", out area);

            var function = GetFunction(actionName, controllerName, (string)area);

            if (user != null && user.IsPermitted(function))
            {
                return htmlHelper.ActionLink(linkText, actionName, controllerName, routeValues, htmlAttributes);
            }
            else
            {
                return MvcHtmlString.Empty;
            }
        }

        private static Function GetFunction(string actionName, string controllerName, string area)
        {
            var component = MvcModuleService.GetComponent(controllerName, area);
            if (component == null) return null;

            var function = component.GetFunction(actionName);
            return function;
        }

        private static IMvcModuleService MvcModuleService
        {
            get
            {
                if (mvcModuleService == null)
                    mvcModuleService = DependencyResolver.Current.GetService<IMvcModuleService>();

                return mvcModuleService;
            }
        }

        private static IMvcModuleService mvcModuleService;
    }
}
