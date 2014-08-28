using Indigo.Modules.Attributes;
using Indigo.Organization;
using Indigo.Organization.Search;
using Indigo.Web.Mvc;
using Spring.Objects.Factory.Attributes;
using System.Web.Mvc;

namespace Indigo.WebMatrix.Controllers
{
    [Component("人员管理", 91)]
    public class EmployeeController : BaseController
    {
        [Autowired]
        public IOrganizationService OrganizationService { get; set; }

        [Function("人员列表")]
        public ActionResult Index(EmployeeSearchForm searchForm)
        {
            ViewBag.SearchResult = OrganizationService.Search(searchForm);

            return View(searchForm);
        }

        [Function("创建人员")]
        public ActionResult Add()
        {
            return View();
        }
    }
}
