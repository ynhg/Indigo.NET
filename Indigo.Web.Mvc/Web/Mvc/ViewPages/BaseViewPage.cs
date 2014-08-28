using Indigo.Modules;
using Indigo.Security;
using Indigo.Security.Util;
using System.Web.Mvc;

namespace Indigo.Web.Mvc.ViewPages
{
    public abstract class BaseViewPage : WebViewPage
    {
        public virtual Function Function
        {
            get
            {
                var baseController = ViewContext.Controller as BaseController;
                if (baseController != null)
                {
                    var functionName = ViewContext.RouteData.GetRequiredString("action");
                    return baseController.Component.GetFunction(functionName);
                }

                return null;
            }
        }

        public virtual new User User
        {
            get { return SecurityUtils.CurrentUser; }
        }
    }

    public abstract class BaseViewPage<TModel> : WebViewPage<TModel>
    {
        public virtual Function Function
        {
            get
            {
                var baseController = ViewContext.Controller as BaseController;
                if (baseController != null)
                {
                    var functionName = ViewContext.RouteData.GetRequiredString("action");
                    return baseController.Component.GetFunction(functionName);
                }

                return null;
            }
        }

        public virtual new User User
        {
            get { return SecurityUtils.CurrentUser; }
        }
    }
}
