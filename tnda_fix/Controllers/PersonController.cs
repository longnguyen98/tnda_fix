using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using tnda_fix.Models;

namespace tnda_fix.Controllers
{
    public class PersonController : Controller
    {
        public JsonResult GetPersonDetail()
        {
            string s_id = Request.QueryString["id"];
            int id = int.Parse(s_id);
            tndaEntities db = new tndaEntities();
            Person child = db.People.Find(id);
            //
            if (child.ID_role == 3)
            {
                Family family = db.Families.Find(child.ID_Farmily);
                //
                Person father = db.People.Find(family.ID_Dad);
                //            
                Person mother = db.People.Where(p => p.ID_Farmily == family.ID && p.ID != father.ID && p.ID != child.ID).FirstOrDefault();
                //
                Person glv = db.People.Where(p => p.ID_Class == child.ID_Class && (p.ID_role == 1 || p.ID_role == 2)).FirstOrDefault();
                string glv_name = glv.ChristianName + " " + glv.FirstName + " " + glv.Name;
                //
                var fatherJson = new { ch_name = father.ChristianName, fname = father.FirstName, name = father.Name, role = father.Role.RoleName, phone = father.Phone };
                var motherJson = new { ch_name = mother.ChristianName, fname = mother.FirstName, name = mother.Name, role = mother.Role.RoleName, phone = mother.Phone, role_id = child.ID_role };
                var json = new { id = child.ID, ch_name = child.ChristianName, fname = child.FirstName, name = child.Name, grade = child.Class.Grade.GradeName, pclass = child.Class.ClassName, role = child.Role.RoleName, glv = glv_name, birth = child.Birth.Value.ToShortDateString(), address = child.Address, father = fatherJson, mother = motherJson, role_id = child.ID_role, glv_id = glv.ID };
                //
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var json = new { id = child.ID, ch_name = child.ChristianName, fname = child.FirstName, name = child.Name, grade = child.Class.Grade.GradeName, pclass = child.Class.ClassName, role = child.Role.RoleName, birth = child.Birth.Value.ToShortDateString(), address = child.Address, phone = child.Phone, role_id = child.ID_role };


                return Json(json, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult getPersonDetailWithArg(int idd)
        {
            int id = idd;
            tndaEntities db = new tndaEntities();
            Person child = db.People.Find(id);
            //
            if (child.ID_role == 3)
            {
                Family family = db.Families.Find(child.ID_Farmily);
                //
                Person father = db.People.Find(family.ID_Dad);
                //            
                Person mother = db.People.Where(p => p.ID_Farmily == family.ID && p.ID != father.ID && p.ID != child.ID).FirstOrDefault();
                //
                Person glv = db.People.Where(p => p.ID_Class == child.ID_Class && (p.ID_role == 1 || p.ID_role == 2)).FirstOrDefault();
                string glv_name = glv.ChristianName + " " + glv.FirstName + " " + glv.Name;
                //
                var fatherJson = new { ch_name = father.ChristianName, fname = father.FirstName, name = father.Name, role = father.Role.RoleName, phone = father.Phone };
                var motherJson = new { ch_name = mother.ChristianName, fname = mother.FirstName, name = mother.Name, role = mother.Role.RoleName, phone = mother.Phone, role_id = child.ID_role };
                var json = new { id = child.ID, ch_name = child.ChristianName, fname = child.FirstName, name = child.Name, grade = child.Class.Grade.GradeName, pclass = child.Class.ClassName, role = child.Role.RoleName, glv = glv_name, birth = child.Birth.Value.ToShortDateString(), address = child.Address, father = fatherJson, mother = motherJson, role_id = child.ID_role, glv_id = glv.ID };
                //
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var json = new { id = child.ID, ch_name = child.ChristianName, fname = child.FirstName, name = child.Name, grade = child.Class.Grade.GradeName, pclass = child.Class.ClassName, role = child.Role.RoleName, birth = child.Birth.Value.ToShortDateString(), address = child.Address, phone = child.Phone, role_id = child.ID_role };


                return Json(json, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult getPersonByQuery()
        {
            string query = Request.QueryString["query"];
            query = Tools.convert(query).ToUpper();
            //
            tndaEntities db = new tndaEntities();
            List<Person> list = new List<Person>();
            list = db.People.ToList();
            List<object> objects = new List<object>();
            foreach (Person p in list)
            {
                string cname = Tools.convert(p.ChristianName).ToUpper();
                string fname = Tools.convert(p.FirstName).ToUpper();
                string name = Tools.convert(p.Name).ToUpper();
                //
                bool cfn = (cname + " " + fname + " " + name).Trim().Equals(query);
                bool cn = (cname + " " + name).Trim().Equals(query);
                bool fn = (fname + " " + name).Trim().Equals(query);
                bool role = p.ID_role == 3 || p.ID_role == 1 || p.ID_role == 2;
                if (role)
                {
                    if (Tools.convert(p.ChristianName.ToUpper()).Equals(query) || Tools.convert(p.FirstName.ToUpper()).Equals(query) || Tools.convert(p.Name.ToUpper()).Equals(query) || cfn || cn || fn)
                    {
                        var ob = new { id = p.ID, ch_name = p.ChristianName, fname = p.FirstName, name = p.Name, pclass = p.Class.Grade.GradeName + " " + p.Class.ClassName, birth = p.Birth.Value.ToShortDateString(), role = p.Role.RoleName };
                        //
                        objects.Add(ob);
                    }
                }

            }
            return Json(objects, JsonRequestBehavior.AllowGet);
        }
        //Edit Person
        [HttpPost]
        public void EditPerson(int id)
        {
            tndaEntities db = new tndaEntities();
            List<Person> list = db.People.ToList();
            foreach (Person p in list)
            {
                if (p.ID == id)
                {
                    int idTemp = p.ID;
                    db.People.Remove(p);
                    list[id].ID = idTemp;
                    db.SaveChanges();
                }
            }
        }
        [HttpPost]
        public void EditName()
        {
            tndaEntities db = new tndaEntities();
            List<Person> list = db.People.ToList();
            string id = Request.QueryString["query"];
            string name = Request.QueryString["query"];
            foreach (Person p in list)
            {
                if (p.ID == int.Parse(id))
                {
                    p.Name = name;
                    db.SaveChanges();
                }
            }
        }
        [HttpPost]
        public void EditFistName()
        {
            tndaEntities db = new tndaEntities();
            List<Person> list = db.People.ToList();
            string id = Request.QueryString["query"];
            string fname = Request.QueryString["query"];
            foreach (Person p in list)
            {
                if (p.ID == int.Parse(id))
                {
                    p.FirstName = fname;
                    db.SaveChanges();
                }
            }
        }
        [HttpPost]
        public void EditGender()
        {
            tndaEntities db = new tndaEntities();
            List<Person> list = db.People.ToList();
            string id = Request.QueryString["query"];
            bool gender = bool.Parse(Request.QueryString["query"]);
            foreach (Person p in list)
            {
                if (p.ID == int.Parse(id))
                {
                    p.Gender = gender;
                    db.SaveChanges();
                }
            }
        }
        [HttpPost]
        public void EditClass()
        {
            tndaEntities db = new tndaEntities();
            List<Person> list = db.People.ToList();
            string id = Request.QueryString["query"];
            int c = int.Parse( Request.QueryString["query"]);
            foreach (Person p in list)
            {
                if (p.ID == int.Parse(id))
                {
                    p.ID_Class = c;
                    db.SaveChanges();
                }
            }
        }
        [HttpPost]
        public void EditFamily()
        {
            tndaEntities db = new tndaEntities();
            List<Person> list = db.People.ToList();
            string id = Request.QueryString["query"];
            int idF = int.Parse( Request.QueryString["query"]);
            foreach (Person p in list)
            {
                if (p.ID == int.Parse(id))
                {
                    p.ID_Farmily = idF;
                    db.SaveChanges();
                }
            }
        }
        [HttpPost]
        public void EditImage()
        {
            tndaEntities db = new tndaEntities();
            List<Person> list = db.People.ToList();
            string id = Request.QueryString["query"];
            string img = Request.QueryString["query"];
            foreach (Person p in list)
            {
                if (p.ID == int.Parse(id))
                {
                    p.Image = img;
                    db.SaveChanges();
                }
            }
        }
        [HttpPost]
        public void EditRole()
        {
            tndaEntities db = new tndaEntities();
            List<Person> list = db.People.ToList();
            string id = Request.QueryString["query"];
            int role = int.Parse(Request.QueryString["query"]);
            foreach (Person p in list)
            {
                if (p.ID == int.Parse(id))
                {
                    p.ID_role = role;
                    db.SaveChanges();
                }
            }
        }
        [HttpPost]
        public void EditAddress()
        {
            tndaEntities db = new tndaEntities();
            List<Person> list = db.People.ToList();
            string id = Request.QueryString["query"];
            string adr = Request.QueryString["query"];
            foreach (Person p in list)
            {
                if (p.ID == int.Parse(id))
                {
                    p.Address = adr;
                    db.SaveChanges();
                }
            }
        }
        [HttpPost]
        public void EditBirth()
        {
            tndaEntities db = new tndaEntities();
            List<Person> list = db.People.ToList();
            string id = Request.QueryString["query"];
            DateTime date = DateTime.Parse( Request.QueryString["query"]);
            foreach (Person p in list)
            {
                if (p.ID == int.Parse(id))
                {
                    p.Birth = date;
                    db.SaveChanges();
                }
            }
        }
        [HttpPost]
        public void EditChristianName()
        {
            tndaEntities db = new tndaEntities();
            List<Person> list = db.People.ToList();
            string id = Request.QueryString["query"];
            string Cname = Request.QueryString["query"];
            foreach (Person p in list)
            {
                if (p.ID == int.Parse(id))
                {
                    p.ChristianName = Cname;
                    db.SaveChanges();
                }
            }
        }
        [HttpPost]
        public void EditNote()
        {
            tndaEntities db = new tndaEntities();
            List<Person> list = db.People.ToList();
            string id = Request.QueryString["query"];
            string note = Request.QueryString["query"];
            foreach (Person p in list)
            {
                if (p.ID == int.Parse(id))
                {
                    p.Note = note;
                    db.SaveChanges();
                }
            }
        }
        //Report
        [HttpPost]
        public bool ReportPerson(FormCollection form)
        {
            int idOldPerson = int.Parse(Request.QueryString["query"]);
            tndaEntities db = new tndaEntities();
            Person p = new Person
            {
                ChristianName = form["child-ch-name"],
                FirstName = form["fa-fname"],
                Name = form["fa-name"],
                Birth = Convert.ToDateTime(form["child-birth"]), //
                Address = form["child-address"],
                ID_Class = int.Parse(form["child-class"]),
                //ID_Farmily = form["child-address"],
                ID_role = 1,
                //Image = "",
                Note = "",
                Phone = "",
                Status = true,
                Gender = bool.Parse(form["child-gender"]),
                CreateDate = DateTime.Now
            };
            db.People.Add(p);
            //return Json(form["child-class"], JsonRequestBehavior.AllowGet);
            //db.People.Add(p);
            //db.SaveChanges();

            Report rp = new Report
            {
                ID_Person = idOldPerson,
                ID_NewPerson = p.ID,
                Date = DateTime.Now,
                Status = 0
            };
            db.Reports.Add(rp);
            db.SaveChanges();
            return true;
        }
        [HttpPost]
        public bool DelPerson(int id)
        {
            tndaEntities db = new tndaEntities();
            Person ps = new Person();
            List<Person> list = db.People.ToList();
            foreach (Person p in list)
            {
                if (p.ID == id)
                {
                    p.Status = false;
                    db.SaveChanges();
                }
            }
            return true;
        }
        //test data
        [HttpPost]
        public JsonResult testData(FormCollection form)
        {
            Dictionary<string, object> map = new Dictionary<string, object>();
            string[] keys = form.AllKeys;
            foreach (string key in keys)
            {
                map.Add(key, form[key]);
            }            
            return Json(map, JsonRequestBehavior.AllowGet);
        }
    }

}