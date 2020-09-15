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
        [HttpPost]
        public void AddPerson()
        {
            tndaEntities db = new tndaEntities();
            Person p = new Person
            {
                ChristianName = "",
                FirstName = "",
                Name = "",
                Birth = DateTime.Now, //
                Address = "",
                ID_Class = 1,
                ID_Farmily = 1,
                ID_role = 1,
                Image = "",
                Note = "",
                Phone = "",
                Status = true,
                Gender = true,
                CreateDate = DateTime.Now
            };
            db.People.Add(p);
            db.SaveChanges();
        }
        //Edit Person
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
        public void EditName(Person p, string name)
        {
            tndaEntities db = new tndaEntities();
            //List<Person> list = db.People.ToList();
            p.Name = name;
            db.SaveChanges();
        }
        public void EditFistName(Person p, string name)
        {
            tndaEntities db = new tndaEntities();
            p.FirstName = name;
            db.SaveChanges();
        }
        public void EditGender(Person p, bool gender)
        {
            tndaEntities db = new tndaEntities();
            p.Gender = gender;
            db.SaveChanges();
        }
        public void EditClass(Person p, int c)
        {
            tndaEntities db = new tndaEntities();
            p.ID_Class = c;
            db.SaveChanges();
        }
        public void EditFamily(Person p, int c)
        {
            tndaEntities db = new tndaEntities();
            p.ID_Farmily = c;
            db.SaveChanges();
        }
        public void EditImage(Person p, string img)
        {
            tndaEntities db = new tndaEntities();
            p.Image = img;
            db.SaveChanges();
        }
        public void EditRole(Person p, int c)
        {
            tndaEntities db = new tndaEntities();
            p.ID_role = c;
            db.SaveChanges();
        }
        public void EditAddress(Person p, string adr)
        {
            tndaEntities db = new tndaEntities();
            p.Address = adr;
            db.SaveChanges();
        }
        public void EditBirth(Person p, DateTime dt)
        {
            tndaEntities db = new tndaEntities();
            p.Birth = dt;
            db.SaveChanges();
        }
        public void EditChristianName(Person p, string cn)
        {
            tndaEntities db = new tndaEntities();
            p.Image = cn;
            db.SaveChanges();
        }
        public void EditNote(Person p, string note)
        {
            tndaEntities db = new tndaEntities();
            p.Note = note;
            db.SaveChanges();
        }
        public void EditName(int id, string name)
        {
            tndaEntities db = new tndaEntities();
            Person p = new Person();
            List<Person> list = db.People.ToList();
            foreach (Person ps in list)
            {
                if (ps.ID == id)
                {
                    ps.Name = name;
                    db.SaveChanges();
                }
            }
        }
        //Report
        public bool ReportPerson(int idOldPerson)
        {
            tndaEntities db = new tndaEntities();
            Person ps = new Person();
            db.People.Add(ps);
            Report rp = new Report
            {
                ID_Person = idOldPerson,
                ID_NewPerson = ps.ID,
                Date = DateTime.Now,
                Status = 0
            };
            db.Reports.Add(rp);
            db.SaveChanges();
            return true;
        }
        public bool DelPerson(int id)
        {
            tndaEntities db = new tndaEntities();
            Person ps = new Person();
            List<Person> list = db.People.ToList();
            foreach (Person p in list)
            {
                if (p.ID == id)
                {
                    db.People.Remove(p);
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