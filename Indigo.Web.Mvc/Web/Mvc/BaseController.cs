using Indigo.Modules;
using Indigo.Security;
using Indigo.Security.Util;
using Spring.Context.Attributes;
using Spring.Objects.Factory.Attributes;
using Spring.Objects.Factory.Support;
using Spring.Stereotype;
using System.Net;
using System.Web.Mvc;

namespace Indigo.Web.Mvc
{
    [Controller, Scope(ObjectScope.Prototype)]
    public abstract class BaseController : Controller
    {
        private Component component;

        public Component Component
        {
            get
            {
                if (component == null)
                    component = ModuleService.GetComponent(GetType().FullName);

                return component;
            }
        }

        public new User User
        {
            get { return SecurityUtils.CurrentUser; }
        }

        protected ActionResult RedirectToLocal(string url)
        {
            if (Url.IsLocalUrl(url))
            {
                return Redirect(url);
            }
            else
            {
                return Redirect("/");
            }
        }

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            string functionName = filterContext.RouteData.GetRequiredString("action");
            Function function = Component.GetFunction(functionName);

            //如果请求的方法被保护,则进入授权流程
            if (function != null && function.Protect)
            {
                //如果用户登录则验证用户权限,否则返回HTTP状态码401,跳转到登录页面
                if (User != null && User.Identity.IsAuthenticated)
                {
                    //如果用户有权限则跳出方法并继续请求,否则返回HTTP状态码403,跳转到错误页面
                    if (!User.IsPermitted(function))
                    {
                        filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                    }
                }
                else
                {
                    filterContext.Result = new HttpUnauthorizedResult();
                }
            }
        }

        [Autowired]
        public IModuleService ModuleService { get; set; }
    }
}
