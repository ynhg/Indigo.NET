using System;
using System.Collections.Generic;
using Common.Logging;
using Indigo.Infrastructure.Search;
using Indigo.Modules;
using Indigo.Modules.Data;
using Indigo.Security.Data;
using Indigo.Security.Exceptions;
using Indigo.Security.Search;
using Indigo.Security.Support;
using Spring.Objects.Factory.Attributes;
using Spring.Stereotype;
using Spring.Transaction.Interceptor;

namespace Indigo.Security
{
    [Service]
    public class DefaultSecurityService : ISecurityService
    {
        private static readonly ILog Log = LogManager.GetLogger<DefaultSecurityService>();

        [Autowired]
        public IEncryptor Encryptor { get; set; }

        [Autowired]
        public IUserDao UserDao { get; set; }

        [Autowired]
        public IRoleDao RoleDao { get; set; }

        [Autowired]
        public IFunctionDao FunctionDao { get; set; }

        [Transaction]
        public User SignUp(string name, string password)
        {
            var newUser = new User
            {
                Name = name,
                Password = Encryptor.Encrypt(password)
            };

            UserDao.Save(newUser);

            return newUser;
        }

        [Transaction]
        public virtual User SignIn(string name, string password)
        {
            User user = GetUserByName(name);

            if (user == null)
            {
                throw new UnkownUserNameException(name);
            }
            Log.DebugFormat("找到用户名 [{0}] 对应的用户 [{1}].", name, user);

            if (user.Password != Encryptor.Encrypt(password))
            {
                throw new IncorrectPasswordException(name);
            }

            if (!user.IsOnline)
            {
                user.IsOnline = true;
                user.LastSignInTime = DateTime.Now;
                user.TotalSignInCount += 1;

                Log.InfoFormat("用户 [{0}] 于 [{1}] 登入.", name, user.LastSignInTime);
            }

            return user;
        }

        [Transaction]
        public virtual void SignOut(User user)
        {
            Log.DebugFormat("开始对用户[{0}]进行注销", user);

            if (user != null && user.IsOnline)
            {
                user.IsOnline = false;
                user.LastSignOutTime = DateTime.Now;

                if (user.LastSignInTime != null)
                    user.TotalOnlineTime += user.LastSignOutTime.Value - user.LastSignInTime.Value;

                UserDao.Update(user);

                Log.InfoFormat("用户 [{0}] 于 [{1}] 成功注销", user.Name, user.LastSignOutTime);
            }
            else
            {
                Log.DebugFormat("用户[{0}]不满足注销条件, 注销失败", user);
            }
        }

        [Transaction(ReadOnly = true)]
        public Page<User> Search(UserSearchForm searchForm)
        {
            return UserDao.Search(searchForm);
        }

        [Transaction(ReadOnly = true)]
        public User GetUserById(string id)
        {
            return UserDao.GetById(id);
        }

        [Transaction(ReadOnly = true)]
        public User GetUserByName(string name)
        {
            return UserDao.GetByName(name);
        }

        [Transaction]
        public User AddUser(string name, string password, User oper)
        {
            var newUser = new User
            {
                Name = name,
                Password = Encryptor.Encrypt(password)
            };

            UserDao.Save(newUser, oper);

            return newUser;
        }

        [Transaction]
        public void ChangeUserName(string id, string name, User oper)
        {
            User user = GetUserById(id);
            user.Name = name;

            UserDao.Update(user, oper);
        }

        [Transaction]
        public void ChangeUserPassword(string id, string password, User oper)
        {
            User user = GetUserById(id);
            user.Password = Encryptor.Encrypt(password);

            UserDao.Update(user, oper);
        }

        [Transaction]
        public User DeleteUser(string id, User oper)
        {
            User user = GetUserById(id);

            UserDao.Delete(user, oper);

            return user;
        }

        [Transaction]
        public void GrantUserRole(string userId, string roleId)
        {
            User user = GetUserById(userId);
            Role role = RoleDao.GetById(roleId);

            GrantUserRole(user, role);
        }

        [Transaction]
        public void GrantUserRole(User user, Role role)
        {
            user.AddRole(role);

            UserDao.Update(user);
        }

        [Transaction]
        public void RevokeUserRole(string userId, string roleId)
        {
            User user = GetUserById(userId);
            Role role = RoleDao.GetById(roleId);

            user.RemoveRole(role);
        }

        [Transaction]
        public void SetUserRoles(string userId, params string[] roleIds)
        {
            User user = GetUserById(userId);

            user.ClearRoles();

            foreach (string roleId in roleIds)
            {
                Role role = RoleDao.GetReferenceById(roleId);
                user.AddRole(role);
            }

            UserDao.Update(user);
        }

        [Transaction]
        public void GrantUserPermission(string userId, string functionId)
        {
            User user = GetUserById(userId);
            Function function = FunctionDao.GetReferenceById(functionId);

            user.AddFunction(function);

            UserDao.Update(user);
        }

        [Transaction(ReadOnly = true)]
        public IList<Role> GetAllRoles()
        {
            return RoleDao.FindAll();
        }

        [Transaction(ReadOnly = true)]
        public Page<Role> Search(RoleSearchForm searchForm)
        {
            return RoleDao.Search(searchForm);
        }

        [Transaction]
        public void SetUserPermissions(string userId, ICollection<string> functionIds)
        {
            User user = GetUserById(userId);

            user.ClearFunctions();

            foreach (string functionId in functionIds)
            {
                Function function = FunctionDao.GetById(functionId);
                user.AddFunction(function);
            }

            UserDao.Update(user);
        }

        [Transaction]
        public Role GetAdminRole()
        {
            Role adminRole = RoleDao.GetAdminRole() ?? AddAdminRole("超级管理员", "拥有系统全部权限的超级角色");

            foreach (Function function in FunctionDao.FindAll())
            {
                adminRole.AddFunction(function);
            }

            return adminRole;
        }

        [Transaction(ReadOnly = true)]
        public Role GetRoleById(string id)
        {
            return RoleDao.GetById(id);
        }

        [Transaction(ReadOnly = true)]
        public Role GetRoleByName(string name)
        {
            return RoleDao.GetByName(name);
        }

        [Transaction]
        public Role AddAdminRole(string name, string description)
        {
            var newAdminRole = new Role
            {
                IsAdmin = true,
                Name = name,
                Description = description
            };

            RoleDao.Save(newAdminRole);

            return newAdminRole;
        }

        [Transaction]
        public Role AddRole(string name, string description, User oper)
        {
            var newNormalRole = new Role
            {
                Name = name,
                Description = description
            };

            RoleDao.Save(newNormalRole, oper);

            return newNormalRole;
        }

        [Transaction]
        public void UpdateRole(Role role, User oper)
        {
            RoleDao.Update(role, oper);
        }

        [Transaction]
        public Role DeleteRole(string id)
        {
            Role role = GetRoleById(id);

            RoleDao.Delete(role);

            return role;
        }

        [Transaction]
        public void GrantRolePermission(string roleId, string functionId)
        {
            Role role = GetRoleById(roleId);

            if (role == null) return;

            Function function = FunctionDao.GetById(functionId);
            role.AddFunction(function);
        }

        [Transaction]
        public void SetRolePermissions(string roleId, ICollection<string> functionIds)
        {
            Role role = GetRoleById(roleId);

            if (role == null) return;

            role.ClearFunctions();

            foreach (string functionId in functionIds)
            {
                Function function = FunctionDao.GetById(functionId);
                role.AddFunction(function);
            }

            RoleDao.Update(role);
        }
    }
}