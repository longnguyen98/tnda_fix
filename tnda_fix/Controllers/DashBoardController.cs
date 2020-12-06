using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tnda_fix.Models;

namespace tnda_fix.Controllers
{
    public class DashBoardController : Controller
    {
        // GET: DashBoard
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult countGLV()
        {
            tndaEntities db = new tndaEntities();
            List<object> json = new List<object>();
            List<Person> people = db.People.Where(p => p.ID_role == 1).ToList();
            int count = people.Count();
            int slKT, slRL, slTS, slSD;
            slKT = slRL = slTS = slSD = 0;
            foreach (Person p in people)
            {
                if (p.ID_Class != null)
                    if (p.Class.ID_Grade == 1)
                        slKT++;
                    else if (p.Class.ID_Grade == 2)
                        slRL++;
                    else if (p.Class.ID_Grade == 3)
                        slTS++;
                    else
                        slSD++;
            }
            int ot = count - slKT - slRL - slTS - slSD;
            var ob = new { total = count, KT = slKT, RL = slRL, TS = slTS, SD = slSD, other = ot };
            json.Add(ob);
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        public JsonResult countTN()
        {
            tndaEntities db = new tndaEntities();
            List<object> json = new List<object>();
            List<Person> people = db.People.Where(p => p.ID_role == 4).ToList();
            int count = people.Count();
            int slKT, slRL, slTS, slSD;
            slKT = slRL = slTS = slSD = 0;
            foreach (Person p in people)
            {
                if (p.ID_Class != null)
                    if (p.Class.ID_Grade == 1)
                        slKT++;
                    else if (p.Class.ID_Grade == 2)
                        slRL++;
                    else if (p.Class.ID_Grade == 3)
                        slTS++;
                    else
                        slSD++;
            }
            int ot = count - slKT - slRL - slTS - slSD;
            var ob = new { total = count, KT = slKT, RL = slRL, TS = slTS, SD = slSD, other = ot };
            json.Add(ob);
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        public JsonResult countByClass()
        {
            int idGr = int.Parse(Request.QueryString["id_grade"]);
            tndaEntities db = new tndaEntities();
            List<object> json = new List<object>();
            List<Person> people = db.People.Where(p => p.ID_role == 4 && p.Class.ID_Grade == idGr ).ToList();
            List<Class> classes = db.Classes.Where(c => c.ID_Grade == idGr).ToList();
            foreach (Class c in classes)
            {
                int count = people.Where(p => p.ID_Class == c.ID).Count();
                var ob = new { id = c.ID, name = c.ClassName, total = count};
                json.Add(ob);
            }
            
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}