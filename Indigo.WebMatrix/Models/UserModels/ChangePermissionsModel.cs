using Indigo.Modules;
using Indigo.Security;
using System.Collections.Generic;

namespace Indigo.WebMatrix.Models.UserModels
{
    public class ChangePermissionsModel
    {
        public ChangePermissionsModel()
        {
            FunctionIds = new string[0];
        }

        public ChangePermissionsModel(User targetUser, IList<Module> modules)
            : this()
        {
            Modules = modules;
            TargetUser = targetUser;
            Id = targetUser.Id;
        }

        /// <summary>
        /// 模块列表
        /// </summary>
        public IList<Module> Modules { get; protected internal set; }

        /// <summary>
        /// 目标用户
        /// </summary>
        public User TargetUser { get; protected internal set; }

        /// <summary>
        /// 目标用户Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 已选择的方法标识集
        /// </summary>
        public string[] FunctionIds { get; set; }
    }
}