using System.Linq;
using System.Web.Mvc;
using Indigo.Modules.Attributes;
using Indigo.Organization;
using Indigo.Organization.Search;
using Indigo.Web.Mvc;
using Indigo.Web.Mvc.Util;
using Indigo.WebMatrix.Models.EmployeeModels;
using Spring.Objects.Factory.Attributes;

namespace Indigo.WebMatrix.Controllers
{
    [Component("员工管理", 92)]
    public class EmployeeController : BaseController
    {
        [Autowired]
        public IOrganizationService OrganizationService { get; set; }

        [Function("员工列表")]
        public ActionResult Index(EmployeeSearchForm searchForm)
        {
            ViewBag.SearchResult = OrganizationService.Search(searchForm);

            return View(searchForm);
        }

        [Function("新增员工")]
        public ActionResult Add()
        {
            ViewBag.Genders = HtmlUtils.GetSelectList(typeof (Gender));
            ViewBag.Departments = OrganizationService.GetDepartments().Select(d => new SelectListItem {Text = d.Name, Value = d.Id});
            ViewBag.Positions = OrganizationService.GetPositions().Select(p => new SelectListItem {Text = p.Name, Value = p.Id});

            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Add(AddModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee
                {
                    Number = model.Number,
                    Name = model.Name,
                    Gender = model.Gender,
                    Birthday = model.Birthday,
                    Age = model.Age
                };

                if (!string.IsNullOrWhiteSpace(model.IdentityCardNumber))
                    employee.SetIdentityCardNumber(model.IdentityCardNumber);

                Department department = model.DepartmentId != null ? OrganizationService.GetDepartmentById(model.DepartmentId) : null;
                Position position = model.PositionId != null ? OrganizationService.GetPositionById(model.PositionId) : null;

                OrganizationService.AddEmployee(employee, department, position, User);

                TempData["Message"] = string.Format("员工【{0}】新增成功！", employee);

                return RedirectToAction("Index");
            }

            ViewBag.Genders = HtmlUtils.GetSelectList(typeof (Gender));
            ViewBag.Departments = OrganizationService.GetDepartments().Select(d => new SelectListItem {Text = d.Name, Value = d.Id});
            ViewBag.Positions = OrganizationService.GetPositions().Select(p => new SelectListItem {Text = p.Name, Value = p.Id});

            return View(model);
        }

        public JsonResult IsNumberUnique(string number, string id)
        {
            Employee employee = OrganizationService.GetEmployeeByNumber(number);

            return Json(employee == null || employee.Id == id, JsonRequestBehavior.AllowGet);
        }
    }
}