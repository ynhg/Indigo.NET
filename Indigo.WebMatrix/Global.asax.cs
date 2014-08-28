using Indigo.Modules;
using Indigo.Security;
using Indigo.Web.Mvc;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Indigo.WebMatrix
{
    public class MvcApplication : BaseMvcApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected override void PostDependencyResolverRegistered()
        {
            var mvcModuleService = DependencyResolver.Current.GetService<IMvcModuleService>();
            mvcModuleService.BuildModules(Assembly.GetExecutingAssembly());

            var securityService = DependencyResolver.Current.GetService<ISecurityService>();

            Role admins = securityService.GetAdminRole();

            if (admins.GetUsers().Count == 0)
            {
                User admin = securityService.GetUserByName("admin");
                if (admin == null)
                {
                    admin = securityService.AddUser("admin", "111111", null);
                }
                securityService.GrantUserRole(admin.Id, admins.Id);
            }
        }
    }
}