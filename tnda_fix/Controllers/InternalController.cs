using System;
using System.Web.Mvc;
using tnda_fix.Models;
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

        [HttpGet]
        public void alive()
        {
            return;
        }

        public ActionResult allClasses()
        {
            return View();
        }
        public ActionResult listingInternal()
        {
            return View();
        }
        public ActionResult listingWithQuery()
        {
            return Redirect("~/Internal/ListingInternal?query=" + Tools.convert(Request.QueryString["query"]));
        }
    }
}