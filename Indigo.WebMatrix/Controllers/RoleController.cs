using Indigo.Modules;
using Indigo.Modules.Attributes;
using Indigo.Security;
using Indigo.Security.Search;
using Indigo.Web.Mvc;
using Indigo.WebMatrix.Models.RoleModels;
using Spring.Objects.Factory.Attributes;
using System.Web.Mvc;

namespace Indigo.WebMatrix.Controllers
{
    [Component("角色管理", null, true, 120)]
    public class RoleController : BaseController
    {
        [Autowired]
        public IMvcModuleService MvcModuleService { get; set; }

        [Autowired]
        public ISecurityService SecurityService { get; set; }

        [Function("角色列表", "浏览角色信息")]
        public ActionResult Index(RoleSearchForm searchForm)
        {
            ViewBag.SearchResult = SecurityService.Search(searchForm);

            return View(searchForm);
        }

        [Function("角色信息", "角色的详细信息")]
        public ActionResult Detail(string id)
        {
            ViewBag.TargetRole = SecurityService.GetRoleById(id);

            return View();
        }

        [Function("新增角色", "创建新的角色")]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Add(AddModel model)
        {
            if (ModelState.IsValid)
            {
                var role = SecurityService.AddRole(model.RoleName, model.Description, User);

                TempData["Message"] = string.Format("角色【{0}】新增成功！", role.Name);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        [Function("修改角色", "修改角色信息")]
        public ActionResult Edit(string id)
        {
            var targetRole = SecurityService.GetRoleById(id);

            var model = new EditModel();
            model.Id = targetRole.Id;
            model.RoleName = targetRole.Name;
            model.Description = targetRole.Description;

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(EditModel model)
        {
            if (ModelState.IsValid)
            {
                var targetRole = SecurityService.GetRoleById(model.Id);
                targetRole.Name = model.RoleName;
                targetRole.Description = model.Description;

                SecurityService.UpdateRole(targetRole, User);

                TempData["Message"] = string.Format("角色【{0}】修改成功！", targetRole.Name);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        [Function("修改权限", "设置角色的访问权限")]
        public ActionResult ChangePermissions(string id)
        {
            var targetRole = SecurityService.GetRoleById(id);
            var allModules = MvcModuleService.GetModules();

            var model = new ChangePermissionsModel(targetRole, allModules);
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ChangePermissions(ChangePermissionsModel model)
        {
            var targetRole = SecurityService.GetRoleById(model.Id);

            if (ModelState.IsValid)
            {
                SecurityService.SetRolePermissions(model.Id, model.FunctionIds);

                TempData["Message"] = string.Format("角色【{0}】权限修改成功！", targetRole.Name);

                return RedirectToAction("Index");
            }

            model.Modules = MvcModuleService.GetModules();
            model.TargetRole = targetRole;

            return View(model);
        }

        [Function("删除角色", "删除角色")]
        public ActionResult Delete(string id)
        {
            ViewBag.TargetRole = SecurityService.GetRoleById(id);

            return View();
        }

        [ActionName("Delete"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult DoDelete(string id)
        {
            var targetRole = SecurityService.GetRoleById(id);

            SecurityService.DeleteRole(id);

            TempData["Message"] = string.Format("角色【{0}】删除成功！", targetRole.Name);

            return RedirectToAction("Index");
        }

        public JsonResult IsNameUnique(string roleName, string id)
        {
            var role = SecurityService.GetRoleByName(roleName);

            return Json(role == null || role.Id == id, JsonRequestBehavior.AllowGet);
        }
    }
}
