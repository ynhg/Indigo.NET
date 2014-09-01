using System;

namespace Indigo.Security.Exceptions
{
    [Serializable]
    public class IncorrectUserNameOrPasswordException : SecurityException
    {
        public IncorrectUserNameOrPasswordException(string format, params object[] args) : base(format, args)
        {
        }
    }
}