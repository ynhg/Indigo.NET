using System;
using Indigo.Infrastructure.Exceptions;

namespace Indigo.Security.Exceptions
{
    [Serializable]
    public class SecurityException : BusinessException
    {
        public SecurityException()
        {
        }

        public SecurityException(string format, params object[] args) : base(format, args)
        {
        }
    }
}