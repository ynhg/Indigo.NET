using System;

namespace Indigo.Infrastructure.Exceptions
{
    [Serializable]
    public class BusinessException : Exception
    {
        public BusinessException()
        {
        }

        public BusinessException(string format, params object[] args) : base(string.Format(format, args))
        {
        }
    }
}