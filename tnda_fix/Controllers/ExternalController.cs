

using System;
using System.Collections.Generic;
using System.Linq;
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
       
        

        
       
    }
}