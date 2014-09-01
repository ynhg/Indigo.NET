using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using Indigo.Modules;
using Indigo.Security;
using Indigo.Security.Util;

namespace Indigo.Web.Mvc.Util
{
    public static class SecurityExtensions
    {
        private static IMvcModuleService _mvcModuleService;

        private static IMvcModuleService MvcModuleService
        {
            get {
                return _mvcModuleService ??
                       (_mvcModuleService = DependencyResolver.Current.GetService<IMvcModuleService>());
            }
        }

        public static bool IsPermitted(this User user, string actionName, string controllerName)
        {
            return user.IsPermitted(actionName, controllerName, null);
        }

        public static bool IsPermitted(this User user, string actionName, string controllerName, string area)
        {
            Function function = GetFunction(actionName, controllerName, area);
            return user.IsPermitted(function);
        }

        public static MvcHtmlString ActionLinkAuthorized(this HtmlHelper htmlHelper, string linkText, string actionName,
            object routeValues, object htmlAttributes)
        {
            string controllerName = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");
            return htmlHelper.ActionLinkAuthorized(linkText, actionName, controllerName, routeValues, htmlAttributes);
        }

        public static MvcHtmlString ActionLinkAuthorized(this HtmlHelper htmlHelper, string linkText, string actionName,
            string controllerName, object routeValues, object htmlAttributes)
        {
            User user = SecurityUtils.CurrentUser;

            var routeValueDict = new RouteValueDictionary(routeValues);

            object area;
            routeValueDict.TryGetValue("Area", out area);

            Function function = GetFunction(actionName, controllerName, (string) area);

            if (user != null && user.IsPermitted(function))
            {
                return htmlHelper.ActionLink(linkText, actionName, controllerName, routeValues, htmlAttributes);
            }
            return MvcHtmlString.Empty;
        }

        private static Function GetFunction(string actionName, string controllerName, string area)
        {
            Component component = MvcModuleService.GetComponent(controllerName, area);
            if (component == null) return null;

            Function function = component.GetFunction(actionName);
            return function;
        }
    }
}