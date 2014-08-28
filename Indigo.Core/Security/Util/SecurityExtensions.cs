using Indigo.Modules;
using Indigo.Security;
using System.Collections.Generic;

namespace Indigo.Security.Util
{
    public static class SecurityExtensions
    {
        public static bool IsPermitted(this IEnumerable<Role> roles, Function function)
        {
            foreach (var r in roles)
            {
                if (r.IsPermitted(function)) return true;
            }

            return false;
        }
    }
}
