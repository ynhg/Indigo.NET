using System;
using System.Web.Mvc;
using Spring.Web.Mvc;

namespace Indigo.Web.Mvc
{
    public abstract class BaseMvcApplication : SpringMvcApplication
    {
        private static bool _isDependencyResolverRegistered;
        private static readonly object LockObject = new object();

        protected virtual void PostDependencyResolverRegistered()
        {
        }

        protected override void Application_BeginRequest(object sender, EventArgs e)
        {
            if (_isDependencyResolverRegistered) return;

            lock (LockObject)
            {
                if (_isDependencyResolverRegistered) return;

                IDependencyResolver resolver = BuildDependencyResolver();
                RegisterDependencyResolver(resolver);

                System.Web.Http.Dependencies.IDependencyResolver webApiResolver = BuildWebApiDependencyResolver();
                RegisterDependencyResolver(webApiResolver);

                PostDependencyResolverRegistered();

                _isDependencyResolverRegistered = true;
            }
        }
    }
}