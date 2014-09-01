using System.Web.Mvc;
using Indigo.Modules;
using Indigo.Security;
using Indigo.Security.Util;

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
                    string functionName = ViewContext.RouteData.GetRequiredString("action");
                    return baseController.Component.GetFunction(functionName);
                }

                return null;
            }
        }

        public new virtual User User
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
                    string functionName = ViewContext.RouteData.GetRequiredString("action");
                    return baseController.Component.GetFunction(functionName);
                }

                return null;
            }
        }

        public new virtual User User
        {
            get { return SecurityUtils.CurrentUser; }
        }
    }
}