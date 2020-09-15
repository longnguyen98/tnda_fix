using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tnda_fix.Models.filters;

namespace tnda.Controllers
{
    //[Auth]
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
        public ActionResult addPerson()
        {
            return View();
        }
    }
}