using Indigo.Infrastructure.Exceptions;
using System;

namespace Indigo.Security.Exceptions
{
    [Serializable]
    public class SecurityException : BusinessException
    {
        public SecurityException() { }
        public SecurityException(string format, params object[] args) : base(format, args) { }
    }
}
