using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using Common.Logging;
using Indigo.Infrastructure.Util;
using Spring.Context.Support;

namespace Indigo.Security.Util
{
    public static class SecurityUtils
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof (SecurityUtils));

        private static readonly IDictionary<string, DateTime> OnlineUsers = new ConcurrentDictionary<string, DateTime>();

        [ThreadStatic] private static User _currentUser;
        private static ISecurityService _securityService;

        public static User CurrentUser
        {
            get
            {
                IIdentity identity = Thread.CurrentPrincipal.Identity;

                if (_currentUser == null || !ObjectUtils.Equals(_currentUser.Identity, identity))
                {
                    _currentUser = SecurityService.GetUserById(identity.Name);

                    if (_currentUser != null)
                        _currentUser.Identity = identity;
                }

                if (_currentUser != null)
                {
                    if (OnlineUsers.ContainsKey(_currentUser.Id))
                    {
                        OnlineUsers[_currentUser.Id] = DateTime.Now;
                    }
                    else
                    {
                        OnlineUsers.Add(_currentUser.Id, DateTime.Now);
                    }
                }

                return _currentUser;
            }
        }

        private static ISecurityService SecurityService
        {
            get
            {
                return _securityService ??
                       (_securityService = ContextRegistry.GetContext().GetObject<ISecurityService>());
            }
        }

        /// <summary>
        /// 注销当前用户
        /// </summary>
        public static void SignOut()
        {
            SignOut(CurrentUser);
        }

        /// <summary>
        /// 注销指定用户
        /// </summary>
        /// <param name="user">指定用户</param>
        public static void SignOut(User user)
        {
            SecurityService.SignOut(user);
            OnlineUsers.Remove(user.Id);
        }

        /// <summary>
        /// 注销所有给定时间内无任何活动的用户
        /// </summary>
        /// <param name="expires">过期时间:秒</param>
        public static void SignOut(int expires)
        {
            Log.DebugFormat("对{0}秒内无活动的用户进行批量注销", expires);

            DateTime now = DateTime.Now;

            List<string> expireUserIds =
                OnlineUsers.Where(p => now.Subtract(p.Value).TotalSeconds >= expires).Select(p => p.Key).ToList();

            foreach (string expireUserId in expireUserIds)
            {
                User expiredUser = _securityService.GetUserById(expireUserId);

                SignOut(expiredUser);
            }

            Log.DebugFormat("用户批量注销结束, 共{0}个用户完成注销", expireUserIds.Count);
        }
    }
}