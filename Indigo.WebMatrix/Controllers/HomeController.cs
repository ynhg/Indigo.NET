using Indigo.Modules.Attributes;
using Indigo.Security;
using Indigo.Web.Mvc;
using Spring.Objects.Factory.Attributes;
using System.Web.Mvc;

namespace Indigo.WebMatrix.Controllers
{
    [Component("首页", null, true)]
    public class HomeController : BaseController
    {
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

        [Autowired]
        public ISecurityService SecurityService { get; set; }
    }
}
