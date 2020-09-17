using System.IO;
using System.Web;
using System.Web.Mvc;
using tnda_fix.Models;


namespace tnda.Controllers
{
    public class ExternalController : Controller
    {
        // GET: External
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult listing()
        {
            return View();
        }
        public ActionResult listingWithQuery()
        {
            string query = Request.QueryString["query"];
            query = Tools.convert(query);
            return Redirect("~/External/Listing?query=" + query);
        }
        public ActionResult detail()
        {
            return View();
        }
        [HttpPost]
        public JsonResult upload(HttpPostedFileBase file)
        {
            string _FileName = Tools.getUniqueNum() + Path.GetExtension(file.FileName);
            string _path = Path.Combine(Server.MapPath("~/img/upload"), _FileName);
            string result = Tools.uploadAndResizeImg(file, _path, _FileName);
            //
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}