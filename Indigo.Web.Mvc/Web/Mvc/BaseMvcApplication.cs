using Spring.Web.Mvc;
using System;

namespace Indigo.Web.Mvc
{
    public abstract class BaseMvcApplication : SpringMvcApplication
    {
        private static bool isDependencyResolverRegistered;
        private static readonly object lockObject = new object();

        protected virtual void PostDependencyResolverRegistered()
        {
        }

        protected override void Application_BeginRequest(object sender, EventArgs e)
        {
            if (isDependencyResolverRegistered) return;

            lock (lockObject)
            {
                if (isDependencyResolverRegistered) return;

                var resolver = BuildDependencyResolver();
                RegisterDependencyResolver(resolver);

                var webApiResolver = BuildWebApiDependencyResolver();
                RegisterDependencyResolver(webApiResolver);

                PostDependencyResolverRegistered();

                isDependencyResolverRegistered = true;
            }
        }
    }
}
