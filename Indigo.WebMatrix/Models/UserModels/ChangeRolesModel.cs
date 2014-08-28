using Indigo.Modules;
using Indigo.Security;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Indigo.WebMatrix.Models.UserModels
{
    public class ChangeRolesModel
    {
        private IEnumerable<Role> selectedRoles;

        public ChangeRolesModel()
        {
            SelectedRoleIds = new List<string>();
        }

        public IEnumerable<Module> Modules { get; protected internal set; }

        /// <summary>
        /// 目标用户Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 新选择的角色Id
        /// </summary>
        public string SelectedRoleId { get; set; }

        /// <summary>
        /// 已选择的角色Id列表
        /// </summary>
        public List<string> SelectedRoleIds { get; set; }

        /// <summary>
        /// 提交修改
        /// </summary>
        public bool Commit { get; set; }

        /// <summary>
        /// 目标用户
        /// </summary>
        public User TargetUser { get; protected internal set; }

        /// <summary>
        /// 可供选择的全部角色列表
        /// </summary>
        public IEnumerable<Role> AllRoles { get; protected internal set; }

        /// <summary>
        /// 返回由未选角色组成的下拉列表项
        /// </summary>
        public IEnumerable<SelectListItem> RemainRoles { get; protected internal set; }

        /// <summary>
        /// 已选择的角色列表
        /// </summary>
        public IEnumerable<Role> SelectedRoles
        {
            get
            {
                if (selectedRoles == null)
                {
                    selectedRoles = from r in AllRoles
                                     where SelectedRoleIds.Contains(r.Id)
                                     select r;
                }

                return selectedRoles;
            }
        }
    }
}