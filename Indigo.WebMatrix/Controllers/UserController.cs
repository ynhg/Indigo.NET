using Indigo.Modules;
using Indigo.Modules.Attributes;
using Indigo.Security;
using Indigo.Security.Exceptions;
using Indigo.Security.Search;
using Indigo.Security.Util;
using Indigo.Web.Mvc;
using Indigo.WebMatrix.Models.UserModels;
using Spring.Objects.Factory.Attributes;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace Indigo.WebMatrix.Controllers
{
    [Component("用户管理", null, true, 110)]
    public class UserController : BaseController
    {
        [Autowired]
        public IMvcModuleService MvcModuleService { get; set; }

        [Autowired]
        public ISecurityService SecurityService { get; set; }

        [Function("注册", "注册新用户", false)]
        public ActionResult SignUp(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult SignUp(SignUpModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    User user = SecurityService.SignUp(model.UserName, model.Password);

                    return RedirectToLocal(returnUrl);
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "注册失败");
                }
            }

            return View();
        }

        [Function("登录", "使用用户名和密码登录系统", false)]
        public ActionResult SignIn(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult SignIn(SignInModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    User user = SecurityService.SignIn(model.UserName, model.Password);

                    FormsAuthentication.SetAuthCookie(user.Id, model.RememberMe);

                    return RedirectToLocal(returnUrl);
                }
                catch (IncorrectUserNameOrPasswordException)
                {
                    ModelState.AddModelError("", "用户名或密码错误");
                }
                catch (SecurityException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(model);
        }

        [HttpPost]
        [Function("登出", "退出登录", false)]
        public ActionResult SignOut()
        {
            if (User.Identity.IsAuthenticated)
            {
                SecurityUtils.SignOut();

                FormsAuthentication.SignOut();
            }

            return RedirectToAction("Index", "Home");
        }

        [Function("用户列表", "浏览用户基本信息")]
        public ActionResult Index(UserSearchForm searchForm)
        {
            ViewBag.SearchResult = SecurityService.Search(searchForm);

            return View(searchForm);
        }

        [Function("用户信息", "用户的详细信息")]
        public ActionResult Detail(string id)
        {
            ViewBag.TargetUser = SecurityService.GetUserById(id);

            return View();
        }

        [Function("创建用户", "创建新的用户账号")]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Add(AddModel model)
        {
            if (ModelState.IsValid)
            {
                var user = SecurityService.AddUser(model.UserName, model.Password, User);

                TempData["Message"] = string.Format("用户【{0}】创建成功！", user.Name);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        [Function("修改权限", "设置用户的访问权限")]
        public ActionResult ChangePermissions(string id)
        {
            var targetUser = SecurityService.GetUserById(id);
            var allModules = MvcModuleService.GetModules();

            var model = new ChangePermissionsModel(targetUser, allModules);
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ChangePermissions(ChangePermissionsModel model)
        {
            var targetUser = SecurityService.GetUserById(model.Id);

            if (ModelState.IsValid)
            {
                SecurityService.SetUserPermissions(model.Id, model.FunctionIds);

                TempData["Message"] = string.Format("用户【{0}】的权限修改成功！", targetUser.Name);

                return RedirectToAction("Index");
            }

            model.Modules = MvcModuleService.GetModules();
            model.TargetUser = targetUser;

            return View(model);
        }

        [Function("修改角色", "为用户分配角色")]
        public ActionResult ChangeRoles(string id)
        {
            var model = new ChangeRolesModel();
            model.Modules = MvcModuleService.GetModules();
            model.Id = id;
            model.TargetUser = SecurityService.GetUserById(model.Id);
            model.AllRoles = SecurityService.GetAllRoles();
            model.SelectedRoleIds = model.TargetUser.GetRoles().Select(r => r.Id).ToList();
            model.RemainRoles = model.AllRoles
                .Where(r => User.Contains(r))
                .Where(r => !model.SelectedRoleIds.Contains(r.Id))
                .Select(r => new SelectListItem { Text = r.Name, Value = r.Id });

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ChangeRoles(ChangeRolesModel model)
        {
            var targetUser = SecurityService.GetUserById(model.Id);

            if (ModelState.IsValid)
            {
                if (model.Commit)
                {
                    SecurityService.SetUserRoles(model.Id, model.SelectedRoleIds.ToArray());

                    TempData["Message"] = string.Format("用户【{0}】的角色修改成功！", targetUser.Name);

                    return RedirectToAction("Index");
                }

                if (!string.IsNullOrWhiteSpace(model.SelectedRoleId))
                {
                    model.SelectedRoleIds.Add(model.SelectedRoleId);
                }
            }

            model.Modules = MvcModuleService.GetModules();
            model.TargetUser = targetUser;
            model.AllRoles = SecurityService.GetAllRoles();
            model.RemainRoles = model.AllRoles
                .Where(r => User.Contains(r))
                .Where(r => !model.SelectedRoleIds.Contains(r.Id))
                .Select(r => new SelectListItem { Text = r.Name, Value = r.Id });

            return View(model);
        }

        [Function("删除用户", "删除用户账号")]
        public ActionResult Delete(string id)
        {
            ViewBag.TargetUser = SecurityService.GetUserById(id);

            return View();
        }

        [ActionName("Delete"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult DoDelete(string id)
        {
            var targetUser = SecurityService.GetUserById(id);

            SecurityService.DeleteUser(id, User);

            TempData["Message"] = string.Format("用户【{0}】删除成功！", targetUser.Name);

            return RedirectToAction("Index");
        }

        public JsonResult IsNameUnique(string userName, string id)
        {
            User user = SecurityService.GetUserByName(userName);

            return Json(user == null || user.Id == id, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Active()
        {
            return Json(User != null, JsonRequestBehavior.AllowGet);
        }
    }
}
