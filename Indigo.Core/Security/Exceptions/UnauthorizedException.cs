using System;

namespace Indigo.Security.Exceptions
{
    [Serializable]
    public class UnauthorizedException : SecurityException
    {
        public UnauthorizedException(string format, params object[] args) : base(format, args)
        {
        }
    }
}