using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tnda_fix.Models;
using tnda_fix.Services;

namespace tnda_fix.Controllers
{
    public class PersonController : Controller
    {
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
        //[ValidateAntiForgeryToken]
        public ActionResult DelPerson(FormCollection form)
        {
            tndaEntities db = new tndaEntities();
            Person p = db.People.Find(int.Parse(form["child-id"]));
            if (p != null)
            {
                if (p.ID_role == 1 || p.ID_role == 2)
                {
                    if (p.ID_Class != null)
                    {
                        Class c = db.Classes.Find(p.ID_Class);
                        c.teacher_names = null;
                    }
                }
                if (p.ID_role == 4 || p.ID_role == 7)
                {
                    if (p.ID_Class != null)
                    {
                        Class c = db.Classes.Find(p.ID_Class);
                        c.students_count -= 1;
                    }
                    if (p.ID_Farmily != null)
                    {
                        Family fam = db.Families.Find(p.ID_Farmily);
                        Person father = db.People.Find(fam.ID_Dad);
                        Person mother = db.People.FirstOrDefault(per => per.ID_Farmily == fam.ID && per.ID != father.ID && per.ID != p.ID);
                        db.Families.Remove(fam);
                        db.People.Remove(father);
                        db.People.Remove(mother);
                    }
                }
                db.People.Remove(p);
                db.SaveChanges();
            }
            return Redirect(form["current_location"].ToString());
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

        public ViewResult successfullView()
        {
            return View();
        }

        public JsonResult GetPersonDetail()
        {
            return getPersonDetailWithArg(int.Parse(Request.QueryString["id"]));
        }

        public JsonResult getPersonDetailWithArg(int id)
        {
            using (tndaEntities db = new tndaEntities())
            {
                Person child = db.People.Find(id);
                //
                if (child.ID_role == 4 || child.ID_role == 7)
                {
                    Family family = db.Families.Find(child.ID_Farmily);
                    //
                    Person father = db.People.Find(family.ID_Dad);
                    //
                    Person mother = db.People.FirstOrDefault(p =>
                        p.ID_Farmily == family.ID && p.ID != father.ID && p.ID != child.ID);
                    //
                    Person glv = db.People.FirstOrDefault(p =>
                        p.ID_Class == child.ID_Class && (p.ID_role == 1 || p.ID_role == 2) && p.ID_Class != null);
                    string glv_name = glv != null ? (glv.ChristianName + " " + glv.FirstName + " " + glv.Name) : "";
                    string img = child.Image;
                    if (string.IsNullOrEmpty(img))
                    {
                        img = "https://cdn.pixabay.com/photo/2016/08/08/09/17/avatar-1577909_1280.png";
                    }

                    string birthString = child.Birth != null ? child.Birth.Value.ToString("yyyy-MM-dd") : "";
                    string classString = child.Class != null
                        ? (child.Class.Grade.GradeName + " " + child.Class.ClassName)
                        : "";
                    //
                    var fatherJson = new
                    {
                        fa_id = father.ID,
                        ch_name = father.ChristianName,
                        fname = father.FirstName,
                        name = father.Name,
                        role = father.Role.RoleName,
                        phone = father.Phone
                    };
                    var motherJson = new
                    {
                        mo_id = mother.ID,
                        ch_name = mother.ChristianName,
                        fname = mother.FirstName,
                        name = mother.Name,
                        role = mother.Role.RoleName,
                        phone = mother.Phone,
                        role_id = child.ID_role
                    };
                    int glvId = glv?.ID ?? 91;
                    var json = new
                    {
                        id = child.ID,
                        ch_name = child.ChristianName,
                        fname = child.FirstName,
                        name = child.Name,
                        pclass = classString,
                        role = child.Role.RoleName,
                        glv = glv_name,
                        id_class = child.ID_Class,
                        birth = birthString,
                        address = child.Address,
                        father = fatherJson,
                        mother = motherJson,
                        role_id = child.ID_role,
                        glv_id = glvId,
                        img = img,
                        note = child.Note
                    };
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

                    string birthString = child.Birth != null ? child.Birth.Value.ToString("yyyy-MM-dd") : "";
                    string classString = child.Class != null
                        ? (child.Class.Grade.GradeName + " " + child.Class.ClassName)
                        : "";
                    string gender = child.Gender.Value ? "Nam" : "Nữ";
                    var json = new
                    {
                        id = child.ID,
                        ch_name = child.ChristianName,
                        fname = child.FirstName,
                        name = child.Name,
                        pclass = classString,
                        id_class = child.ID_Class,
                        role = child.Role.RoleName,
                        birth = birthString,
                        address = child.Address,
                        phone = child.Phone,
                        role_id = child.ID_role,
                        img = image,
                        gender = gender,
                        genderValue = child.Gender.Value
                    };
                    return Json(json, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult getPersonByQuery()
        {
            string query = Request.QueryString["query"];
            query = Tools.convert(query).ToUpper();
            //
            using (tndaEntities db = new tndaEntities())
            {
                const string sql =
                    "SELECT * FROM Person p WHERE p.for_search LIKE CONCAT('%',@query,'%') AND p.ID_role = 4 ";
                List<Person> list = db.People.SqlQuery(sql, new SqlParameter("@query", query)).ToList();
                List<object> objects = new List<object>();
                foreach (Person p in list)
                {
                    string birthString = p.Birth != null ? p.Birth.Value.ToString("yyyy-MM-dd") : "";
                    string classString = p.Class != null ? (p.Class.Grade.GradeName + " " + p.Class.ClassName) : "";
                    var ob = new
                    {
                        id = p.ID,
                        ch_name = p.ChristianName,
                        fname = p.FirstName,
                        name = p.Name,
                        pclass = classString,
                        birth = birthString,
                        role = p.Role.RoleName
                    };
                    //
                    objects.Add(ob);
                }

                return Json(objects, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult getPersonByClass()
        {
            int id_class = int.Parse(Request.QueryString["id_class"]);
            tndaEntities db = new tndaEntities();
            List<object> json = new List<object>();
            List<Person> people = db.People.Where(p => p.ID_role == 4 && p.ID_Class == id_class).OrderBy(p =>p.Name).ToList();
            foreach (Person p in people)
            {
                var ob = new
                {
                    id = p.ID,
                    ch_name = p.ChristianName,
                    fname = p.FirstName,
                    name = p.Name,
                    pclass = p.Class.Grade.GradeName + " " + p.Class.ClassName,
                    birth = p.Birth != null ? p.Birth.Value.ToString("yyyy-MM-dd") : "",
                    role = p.Role.RoleName
                };
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

                string birthString = child.Birth != null ? child.Birth.Value.ToString("yyyy-MM-dd") : "";
                var ob = new
                {
                    id = child.ID,
                    ch_name = child.ChristianName,
                    fname = child.FirstName,
                    name = child.Name,
                    birth = birthString
                };
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
                string birthString = p.Birth != null ? p.Birth.Value.ToString("yyyy-MM-dd") : "";
                string classString = p.Class != null ? (p.Class.Grade.GradeName + " " + p.Class.ClassName) : "";
                var ob = new
                {
                    id = p.ID,
                    ch_name = p.ChristianName,
                    fname = p.FirstName,
                    name = p.Name,
                    pclass = classString,
                    birth = birthString
                };
                //
                json.Add(ob);
            }

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult setImg(FormCollection form)
        {
            try
            {
                int id = int.Parse(form["id"]);
                HttpPostedFileBase file = Request.Files[0];
                //
                string _FileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string _path = Path.Combine(Server.MapPath("~/img/upload"), _FileName);
                string result = Tools.uploadAndResizeImg(file, _path, _FileName);
                //
                tndaEntities db = new tndaEntities();
                db.People.Find(id).Image = result;
                db.SaveChanges();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message }, JsonRequestBehavior.AllowGet);
            }
            //
        }

        //
        [HttpPost]
        public JsonResult addPerson(FormCollection form)
        {
            using (PersonService personService = new PersonService())
            {
                Response response = personService.addPerson(form);
                return Json(new { success = response.success, message = response.message },
                    JsonRequestBehavior.AllowGet);
            }
        }

        //Edit Person
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPerson(FormCollection form)
        {
            using (PersonService personService = new PersonService())
            {
                Response response = personService.editPerson(form);
                return Json(new { success = response.success, message = response.message },
                    JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditClass(FormCollection form)
        {
            tndaEntities db = new tndaEntities();
            int id = int.Parse(form["child-id"]);
            Person p = db.People.Find(id);
            if (p != null)
            {
                Class newClass = db.Classes.Find(int.Parse(form["child-class"]));
                Class oldClass = db.Classes.Find(int.Parse(form["child-old-class"]));
                if (p.ID_role == 1 || p.ID_role == 2)
                {
                    if (newClass != null)
                    {
                        p.ID_Class = int.Parse(form["child-class"]);
                        newClass.teacher_names = p.ChristianName + " " + p.FirstName + " " + p.Name;
                        if (oldClass != null)
                        {
                            oldClass.teacher_names = "";
                        }
                    }
                }
                else if (p.ID_role == 4 || p.ID_role == 7)
                {
                    if (newClass != null)
                    {
                        p.ID_Class = int.Parse(form["child-class"]);
                        newClass.students_count += 1;
                        if (oldClass != null)
                        {
                            oldClass.students_count -= 1;
                        }
                    }
                }
                db.SaveChanges();
            }
            return Redirect(form["current_location"].ToString());
        }

    }    
}
