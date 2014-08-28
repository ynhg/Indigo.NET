using Common.Logging;
using Indigo.Infrastructure.Util;
using Spring.Context.Support;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Indigo.Security.Util
{
    public static class SecurityUtils
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(SecurityUtils));

        private static IDictionary<string, DateTime> onlineUsers = new ConcurrentDictionary<string, DateTime>();

        [ThreadStatic]
        private static User currentUser;

        public static User CurrentUser
        {
            get
            {
                var identity = Thread.CurrentPrincipal.Identity;

                if (currentUser == null || !ObjectUtils.Equals(currentUser.Identity, identity))
                {
                    currentUser = SecurityService.GetUserById(identity.Name);

                    if (currentUser != null)
                        currentUser.Identity = identity;
                }

                if (currentUser != null)
                {
                    if (onlineUsers.ContainsKey(currentUser.Id))
                    {
                        onlineUsers[currentUser.Id] = DateTime.Now;
                    }
                    else
                    {
                        onlineUsers.Add(currentUser.Id, DateTime.Now);
                    }
                }

                return currentUser;
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
            onlineUsers.Remove(user.Id);
        }

        /// <summary>
        /// 注销所有给定时间内无任何活动的用户
        /// </summary>
        /// <param name="Expires">过期时间:秒</param>
        public static void SignOut(int expires)
        {
            log.DebugFormat("对{0}秒内无活动的用户进行批量注销", expires);

            DateTime now = DateTime.Now;

            var expireUserIds = onlineUsers.Where(p => now.Subtract(p.Value).TotalSeconds >= expires).Select(p => p.Key).ToList();

            foreach (var expireUserId in expireUserIds)
            {
                User expiredUser = securityService.GetUserById(expireUserId);

                SignOut(expiredUser);
            }

            log.DebugFormat("用户批量注销结束, 共{0}个用户完成注销", expireUserIds.Count);
        }

        private static ISecurityService SecurityService
        {
            get
            {
                if (securityService == null)
                {
                    securityService = ContextRegistry.GetContext().GetObject<ISecurityService>();
                }

                return securityService;
            }
        }

        private static ISecurityService securityService;
    }
}
