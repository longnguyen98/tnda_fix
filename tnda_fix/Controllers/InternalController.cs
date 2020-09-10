using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tnda_fix.Models.filters;

namespace tnda.Controllers
{
    
    public class InternalController : Controller
    {
        // GET: Internal
        [Auth]
        public ActionResult Index()
        {
            return View();
        }
    }
}