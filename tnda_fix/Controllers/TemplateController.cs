using System.Web.Mvc;

namespace tnda.Controllers
{
    public class TemplateController : Controller
    {
        // GET: Template
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult landing_index()
        {
            return View();
        }
    }
}