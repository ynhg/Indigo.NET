using System;

namespace Indigo.Security.Exceptions
{
    [Serializable]
    public class IncorrectPasswordException : IncorrectUserNameOrPasswordException
    {
        public IncorrectPasswordException(string userName) : base("用户 [{0}] 的密码错误", userName)
        {
        }
    }
}