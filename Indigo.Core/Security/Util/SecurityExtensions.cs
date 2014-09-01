using System.Collections.Generic;
using System.Linq;
using Indigo.Modules;

namespace Indigo.Security.Util
{
    public static class SecurityExtensions
    {
        public static bool IsPermitted(this IEnumerable<Role> roles, Function function)
        {
            return roles.Any(r => r.IsPermitted(function));
        }
    }
}