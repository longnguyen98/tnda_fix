using System;
using System.Web.Mvc;
using tnda_fix.Models;
using tnda_fix.Models.filters;
using tnda_fix.Services;

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
        public Boolean editable()
        {
            int personClassId =  int.Parse(Request.QueryString["personClassId"]);
            ACC acc = (ACC)Session["acc"];
            Person p = (Person)Session["person"];
            int accLevel = acc.accLevel.Value;
            int loginClassID = p.ID_Class.Value;
            using (var ps = new PersonService())
            {
                return ps.isEditable(personClassId, accLevel, loginClassID);
            }
        }
    }
}