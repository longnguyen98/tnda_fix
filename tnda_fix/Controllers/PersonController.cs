using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tnda_fix.Models;

namespace tnda_fix.Controllers
{
    public class PersonController : Controller
    {
        public JsonResult GetPersonDetail()
        {
            return getPersonDetailWithArg(int.Parse(Request.QueryString["id"]));
        }
        public JsonResult getPersonDetailWithArg(int id)
        {
            tndaEntities db = new tndaEntities();
            Person child = db.People.Find(id);
            //
            if (child.ID_role == 4 || child.ID_role == 7)
            {
                Family family = db.Families.Find(child.ID_Farmily);
                //
                Person father = db.People.Find(family.ID_Dad);
                //            
                Person mother = db.People.Where(p => p.ID_Farmily == family.ID && p.ID != father.ID && p.ID != child.ID).FirstOrDefault();
                //
                Person glv = db.People.Where(p => p.ID_Class == child.ID_Class && (p.ID_role == 1 || p.ID_role == 2) && p.ID_Class != null).FirstOrDefault();
                string glv_name = glv != null ? (glv.ChristianName + " " + glv.FirstName + " " + glv.Name) : "";
                string img = child.Image;
                if (string.IsNullOrEmpty(img))
                {
                    img = "https://cdn.pixabay.com/photo/2016/08/08/09/17/avatar-1577909_1280.png";
                }
                string birthString = child.Birth != null ? child.Birth.Value.ToString("dd.MM.yyy") : "";
                string classString = child.Class != null ? (child.Class.Grade.GradeName + " " + child.Class.ClassName) : "";
                //
                var fatherJson = new { fa_id = father.ID, ch_name = father.ChristianName, fname = father.FirstName, name = father.Name, role = father.Role.RoleName, phone = father.Phone };
                var motherJson = new { mo_id = mother.ID, ch_name = mother.ChristianName, fname = mother.FirstName, name = mother.Name, role = mother.Role.RoleName, phone = mother.Phone, role_id = child.ID_role };
                var glvId = glv != null ? glv.ID : 91;
                var json = new { id = child.ID, ch_name = child.ChristianName, fname = child.FirstName, name = child.Name, pclass = classString, role = child.Role.RoleName, glv = glv_name, id_class = child.ID_Class, birth = birthString, address = child.Address, father = fatherJson, mother = motherJson, role_id = child.ID_role, glv_id = glvId, img = img, note = child.Note };
                //
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string image = child.Image;
                if (string.IsNullOrEmpty(image))
                {
                    image = "https://cdn.pixabay.com/photo/2016/08/08/09/17/avatar-1577909_1280.png";
                }
                string birthString = child.Birth != null ? child.Birth.Value.ToString("dd.MM.yyy") : "";
                string classString = child.Class != null ? (child.Class.Grade.GradeName + " " + child.Class.ClassName) : "";
                var json = new { id = child.ID, ch_name = child.ChristianName, fname = child.FirstName, name = child.Name, pclass = classString, id_class = child.ID_Class, role = child.Role.RoleName, birth = birthString, address = child.Address, phone = child.Phone, role_id = child.ID_role, img = image };
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
                bool role = p.ID_role == 4 || p.ID_role == 1 || p.ID_role == 2;
                if (role)
                {
                    if (Tools.convert(p.ChristianName.ToUpper()).Equals(query) || Tools.convert(p.FirstName.ToUpper()).Equals(query) || Tools.convert(p.Name.ToUpper()).Equals(query) || cfn || cn || fn)
                    {
                        string birthString = p.Birth != null ? p.Birth.Value.ToString("dd.MM.yyy") : "";
                        string classString = p.Class != null ? (p.Class.Grade.GradeName + " " + p.Class.ClassName) : "";
                        var ob = new { id = p.ID, ch_name = p.ChristianName, fname = p.FirstName, name = p.Name, pclass = classString, birth = birthString, role = p.Role.RoleName };
                        //
                        objects.Add(ob);
                    }
                }

            }
            return Json(objects, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getPersonByClass()
        {
            int id_class = int.Parse(Request.QueryString["id_class"]);
            tndaEntities db = new tndaEntities();
            List<object> json = new List<object>();
            List<Person> people = db.People.Where(p => p.ID_role == 4 && p.ID_Class == id_class).ToList();
            foreach (Person p in people)
            {
                var ob = new { id = p.ID, ch_name = p.ChristianName, fname = p.FirstName, name = p.Name, pclass = p.Class.Grade.GradeName + " " + p.Class.ClassName, birth = p.Birth.Value.ToShortDateString(), role = p.Role.RoleName };
                //
                json.Add(ob);
            }
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        //
        public JsonResult getNewPerson()
        {
            tndaEntities db = new tndaEntities();
            List<Person> people = db.People.Where(p => p.ID_role == 7).ToList();
            List<object> json = new List<object>();
            foreach (Person child in people)
            {
                string img = child.Image;
                if (string.IsNullOrEmpty(img))
                {
                    img = "https://cdn.pixabay.com/photo/2016/08/08/09/17/avatar-1577909_1280.png";
                }
                string birthString = child.Birth != null ? child.Birth.Value.ToString("dd.MM.yyy") : "";
                var ob = new { id = child.ID, ch_name = child.ChristianName, fname = child.FirstName, name = child.Name, birth = birthString };
                json.Add(ob);
            }
            return Json(json, JsonRequestBehavior.AllowGet);

        }
        public JsonResult getGLV()
        {
            tndaEntities db = new tndaEntities();
            List<object> json = new List<object>();
            List<Person> people = db.People.Where(p => p.ID_role == 1).ToList();
            foreach (Person p in people)
            {
                string birthString = p.Birth != null ? p.Birth.Value.ToString("dd.MM.yyy") : "";
                string classString = p.Class != null ? (p.Class.Grade.GradeName + " " + p.Class.ClassName) : "";
                var ob = new { id = p.ID, ch_name = p.ChristianName, fname = p.FirstName, name = p.Name, pclass = classString, birth = birthString };
                //
                json.Add(ob);
            }
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public ActionResult setImg(FormCollection form, HttpPostedFileBase file)
        {
            int id = int.Parse(form["id"]);
            //
            string _FileName = Tools.getUniqueNum() + Path.GetExtension(file.FileName);
            string _path = Path.Combine(Server.MapPath("~/img/upload"), _FileName);
            string result = Tools.uploadAndResizeImg(file, _path, _FileName);
            //
            tndaEntities db = new tndaEntities();
            db.People.Find(id).Image = result;
            db.SaveChanges();
            //



            return Redirect("~/internal/index?id=" + id);
        }

        //
        [HttpPost]
        public ActionResult AddPerson(FormCollection form)
        {
            tndaEntities db = new tndaEntities();
            int id_family_lead = -1;
            int id_family = -1;
            Person dad = null;
            Person mom = null;
            if (!(form["fa-ch-name"].ToString().Length == 0 && form["fa-fname"].ToString().Length == 0 && form["fa-name"].ToString().Length == 0))
            {
                dad = new Person
                {
                    ChristianName = form.Get("fa-ch-name"),
                    FirstName = form.Get("fa-fname"),
                    Name = form.Get("fa-name"),
                    Phone = form.Get("fa-phone"),
                    ID_role = 3
                };
                db.People.Add(dad);
                db.SaveChanges();
                int dad_id = dad.ID;

            }
            if (!(form["mo-ch-name"].ToString().Length == 0 && form["mo-fname"].ToString().Length == 0 && form["mo-name"].ToString().Length == 0))
            {
                mom = new Person
                {
                    ChristianName = form.Get("mo-ch-name"),
                    FirstName = form.Get("mo-fname"),
                    Name = form.Get("mo-name"),
                    Phone = form.Get("mo-phone"),
                    ID_role = 3
                };
                db.People.Add(mom);
                db.SaveChanges();
                int mom_id = mom.ID;
            }
            //
            if (dad == null)
            {
                id_family_lead = mom.ID;
            }
            else
            {
                id_family_lead = dad.ID;
            }
            //
            if (id_family_lead != -1)
            {
                Family fl = new Family
                {
                    ID_Dad = id_family_lead,
                };
                db.Families.Add(fl);
                db.SaveChanges();
                id_family = fl.ID;
                if (dad != null)
                {
                    dad.ID_Farmily = id_family;
                    db.SaveChanges();
                }
                if (mom != null)
                {
                    mom.ID_Farmily = id_family;
                    db.SaveChanges();
                }
            }
            //

            //
            Person p = new Person
            {
                ChristianName = form["child-ch-name"],
                FirstName = form["child-fname"],
                Name = form["child-name"],
                Birth = Convert.ToDateTime(form["child-birth"]), //
                Address = form["child-address"],
                ID_Class = int.Parse(form["child-class"]),
                //set later ID_Farmily = id_family,
                ID_role = int.Parse(form["child-role"]),
                //Image = "",
                //Phone = "",
                Status = false,
                Gender = bool.Parse(form["child-gender"]),
                CreateDate = DateTime.Now
            };
            if (id_family != -1)
            {
                p.ID_Farmily = id_family;
            }
            if (dad != null)
            {
                dad.Address = p.Address;
            }
            if (mom != null)
            {
                mom.Address = p.Address;
            }
            if (form["check-box"] != null && bool.Parse(form["check-box"]))
                p.Note = form["child-gp"] + " " + form["child-gx"] + " " + form["child-grade"] + " " + form["child-class"];
            db.People.Add(p);
            db.SaveChanges();
            //
            return Redirect(form["current_location"].ToString());
        }
        //Edit Person
        [HttpPost]
        public ActionResult EditPerson(FormCollection form)
        {
            tndaEntities db = new tndaEntities();
            List<Person> list = db.People.ToList();
            int id = int.Parse(form["child-id"]);
            int fa_id = int.Parse(form["fa-id"]);
            int mo_id = int.Parse(form["mo-id"]);
            foreach (Person p in list)
            {
                if (p.ID == id)
                {
                    p.Name = form.Get("child-name");
                    p.ChristianName = form.Get("child-ch-name");
                    p.Birth = DateTime.Parse(form["child-birth"]);
                    p.FirstName = form.Get("child-fname");
                    p.Address = form.Get("child-address");
                    p.Gender = bool.Parse(form["child-gender"]);
                    //p.ID_Class = int.Parse(form["child-class"]);
                    db.SaveChanges();
                }
                else if (p.ID == fa_id)
                {
                    p.ChristianName = form.Get("fa-ch-name");
                    p.FirstName = form.Get("fa-fname");
                    p.Name = form.Get("fa-name");
                    p.Phone = form.Get("fa-phone");
                    db.SaveChanges();
                }
                else if (p.ID == mo_id)
                {
                    p.ChristianName = form.Get("mo-ch-name");
                    p.FirstName = form.Get("mo-fname");
                    p.Name = form.Get("mo-name");
                    p.Phone = form.Get("mo-phone");
                    db.SaveChanges();
                }
            }
            return Redirect(form["current-location"].ToString());
        }
        
        [HttpPost]
        public ActionResult EditClass(FormCollection form)
        {
            tndaEntities db = new tndaEntities();
            List<Person> list = db.People.ToList();
            int id = int.Parse(form["child-id"]);
            foreach (Person p in list)
            {
                if (p.ID == id)
                {
                    p.ID_Class = int.Parse(form["child-class"]);
                    if (form["child-role"] != null)
                        p.ID_role = int.Parse(form["child-role"]);
                    db.SaveChanges();
                }
            }
            return Redirect(form["current_location"].ToString());
        }
        [HttpPost]
        public void EditFamily()
        {
            tndaEntities db = new tndaEntities();
            List<Person> list = db.People.ToList();
            string id = Request.QueryString["query"];
            int idF = int.Parse(Request.QueryString["query"]);
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
            DateTime date = DateTime.Parse(Request.QueryString["query"]);
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