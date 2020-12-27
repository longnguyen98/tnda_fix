using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tnda_fix.Models.filters;

namespace tnda_fix.Controllers
{
    [Auth]
    [AdminAuth]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AllGLV()
        {
            return View();
        }
        public ActionResult CheckNew()
        {
            return View();
        }
        public ActionResult AllClasses()
        {
            return View();
        }
        public ActionResult dashBoard()
        {
            return View();
        }
        public ActionResult duplicates()
        {
            return View("duplicates");
        }
    }
}