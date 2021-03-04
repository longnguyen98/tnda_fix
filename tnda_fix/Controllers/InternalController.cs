using System.Web.Mvc;
using tnda_fix.Models.filters;

namespace tnda.Controllers
{
    [Auth]
    public class InternalController : Controller
    {
        // GET: Internal

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult listByClass()
        {
            return View();
        }

        public ActionResult detail()
        {
            return View();
        }

        public ActionResult allClasses()
        {
            return View();
        }
    }
}