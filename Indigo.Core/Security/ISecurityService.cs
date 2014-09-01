using System.Collections.Generic;
using Indigo.Infrastructure.Search;
using Indigo.Security.Search;

namespace Indigo.Security
{
    public interface ISecurityService
    {
        User SignUp(string name, string password);
        User SignIn(string name, string password);
        void SignOut(User user);

        User AddUser(string name, string password, User oper);
        void ChangeUserName(string id, string name, User oper);
        void ChangeUserPassword(string id, string password, User oper);
        User DeleteUser(string id, User oper);
        void GrantUserRole(string userId, string roleId);
        void GrantUserRole(User user, Role role);
        void RevokeUserRole(string userId, string roleId);
        void SetUserRoles(string userId, params string[] roleIds);
        void GrantUserPermission(string userId, string functionId);
        void SetUserPermissions(string userId, ICollection<string> functionIds);
        User GetUserById(string id);
        User GetUserByName(string name);
        Page<User> Search(UserSearchForm searchForm);

        Role AddAdminRole(string name, string description);
        Role AddRole(string name, string description, User oper);
        void UpdateRole(Role role, User oper);
        Role DeleteRole(string id);
        void GrantRolePermission(string roleId, string functionId);
        void SetRolePermissions(string roleId, ICollection<string> functionIds);
        Role GetAdminRole();
        Role GetRoleById(string id);
        Role GetRoleByName(string name);
        IList<Role> GetAllRoles();
        Page<Role> Search(RoleSearchForm searchForm);
    }
}