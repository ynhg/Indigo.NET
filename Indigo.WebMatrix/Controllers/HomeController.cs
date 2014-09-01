using System.Web.Mvc;
using Indigo.Modules.Attributes;
using Indigo.Security;
using Indigo.Web.Mvc;
using Spring.Objects.Factory.Attributes;

namespace Indigo.WebMatrix.Controllers
{
    [Component("首页", null, true)]
    public class HomeController : BaseController
    {
        [Autowired]
        public ISecurityService SecurityService { get; set; }

        [Function("首页")]
        public ActionResult Index()
        {
            return RedirectToAction("Dashboard");
        }

        [Function("仪表板")]
        public ActionResult Dashboard()
        {
            return View();
        }

        [Function("关于", "企业信息介绍")]
        public ActionResult About()
        {
            return View();
        }
    }
}