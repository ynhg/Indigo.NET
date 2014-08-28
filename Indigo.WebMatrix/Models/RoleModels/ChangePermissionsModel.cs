using Indigo.Modules;
using Indigo.Security;
using System.Collections.Generic;

namespace Indigo.WebMatrix.Models.RoleModels
{
    public class ChangePermissionsModel
    {
        public ChangePermissionsModel()
        {
            FunctionIds = new string[0];
        }

        public ChangePermissionsModel(Role targetRole, IEnumerable<Module> modules)
            : this()
        {
            Modules = modules;
            TargetRole = targetRole;
            Id = targetRole.Id;
        }

        /// <summary>
        /// 模块列表
        /// </summary>
        public IEnumerable<Module> Modules { get; protected internal set; }

        /// <summary>
        /// 目标角色
        /// </summary>
        public Role TargetRole { get; protected internal set; }

        /// <summary>
        /// 目标角色Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 已选择的方法标识集
        /// </summary>
        public string[] FunctionIds { get; set; }
    }
}