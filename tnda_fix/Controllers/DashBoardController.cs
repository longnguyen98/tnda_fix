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
            List<Person> people = db.People.Where(p => p.ID_role == 4 && p.Class.ID_Grade == idGr).ToList();
            List<Class> classes = db.Classes.Where(c => c.ID_Grade == idGr).ToList();
            foreach (Class c in classes)
            {
                int count = people.Where(p => p.ID_Class == c.ID).Count();
                var ob = new { id = c.ID, name = c.ClassName, total = count };
                json.Add(ob);
            }

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public List<Person> checkDuplicate()
        {
            tndaEntities db = new tndaEntities();
            List<Person> ps = new List<Person>();
            List<Person> people = db.People.Where(p => p.ID_role == 4).ToList();
            for (int i = 0; i < people.Count(); i++)
            {
                for (int j = i + 1; j < people.Count(); j++)
                    if (people[i].ChristianName == people[j].ChristianName)
                    {
                        if (people.FirstOrDefault(p => p.ID == people[i].ID) != null)
                            ps.Add(people[i]);
                        ps.Add(people[j]);
                    }
            }
            return ps;
        }
        public JsonResult allTN()
        {
            tndaEntities db = new tndaEntities();
            List<Class> classes = db.Classes.ToList();
            List<Object> json = new List<object>();
            foreach (Class c in classes)
            {
                List<Person> children = db.People.Where(p => p.ID_role == 4 && p.ID_Class == c.ID).ToList();
                if(children.Count == 0)
                {
                    continue;
                }
                List<Object> ob = new List<object>();
                foreach (Person p in children)
                {

                    Family family = db.Families.Find(p.ID_Farmily);
                    //
                    Person father = db.People.Find(family.ID_Dad);
                    //            
                    Person mother = db.People.Where(mo => mo.ID_Farmily == family.ID && mo.ID != father.ID && mo.ID != p.ID).FirstOrDefault();
                    //
                    Person gv = db.People.Where(glv => glv.ID_Class == p.ID_Class && (glv.ID_role == 1 || glv.ID_role == 2) && glv.ID_Class != null).FirstOrDefault();
                    string glv_name = gv != null ? (gv.ChristianName + " " + gv.FirstName + " " + gv.Name) : "";
                    string birthString = p.Birth != null ? p.Birth.Value.ToString("dd.MM.yyy") : "";
                    string classString = p.Class != null ? (p.Class.Grade.GradeName + " " + p.Class.ClassName) : "";
                    //
                    var fatherJson = new { fa_id = father.ID, ch_name = father.ChristianName, fname = father.FirstName, name = father.Name, role = father.Role.RoleName, phone = father.Phone };
                    var motherJson = new { mo_id = mother.ID, ch_name = mother.ChristianName, fname = mother.FirstName, name = mother.Name, role = mother.Role.RoleName, phone = mother.Phone, role_id = p.ID_role };
                    var glvId = gv != null ? gv.ID : 91;
                    var child = new { id = p.ID, ch_name = p.ChristianName, fname = p.FirstName, name = p.Name, pclass = classString, role = p.Role.RoleName, glv = glv_name, id_class = p.ID_Class, birth = birthString, address = p.Address, father = fatherJson, mother = motherJson, role_id = p.ID_role, glv_id = glvId, note = p.Note };
                    //
                    ob.Add(child);
                }
                json.Add(ob);
            }
            
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}