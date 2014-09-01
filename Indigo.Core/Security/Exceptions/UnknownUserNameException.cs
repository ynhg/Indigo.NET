using System;

namespace Indigo.Security.Exceptions
{
    [Serializable]
    public class UnkownUserNameException : IncorrectUserNameOrPasswordException
    {
        public UnkownUserNameException(string userName) : base("未知的用户名 [{0}]", userName)
        {
        }
    }
}