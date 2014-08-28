using Indigo.Modules.Attributes;
using Indigo.Organization;
using Indigo.Organization.Search;
using Indigo.Web.Mvc;
using Indigo.WebMatrix.Models.PositionModels;
using Spring.Objects.Factory.Attributes;
using System.Web.Mvc;

namespace Indigo.WebMatrix.Controllers
{
    [Component("职位管理", 91)]
    public class PositionController : BaseController
    {
        [Autowired]
        public IOrganizationService OrganizationService { get; set; }

        [Function("职位列表")]
        public ActionResult Index(PositionSearchForm searchForm)
        {
            ViewBag.SearchResult = OrganizationService.Search(searchForm);

            return View(searchForm);
        }

        [Function("新增职位")]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Add(AddModel model)
        {
            if (ModelState.IsValid)
            {
                Position position = OrganizationService.AddPosition(model.Name, model.Rank, User);

                TempData["Message"] = string.Format("职位【{0}】新增成功！", position);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public JsonResult IsNameUnique(string name, string id)
        {
            Position position = OrganizationService.GetPositionByName(name);
            return Json(position == null || position.Id == id, JsonRequestBehavior.AllowGet);
        }
    }
}
