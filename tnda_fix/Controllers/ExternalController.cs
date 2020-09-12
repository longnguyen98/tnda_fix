

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
        public ActionResult detail()
        {
            return View();
        }


        public JsonResult getPersonDetail()
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
            query = convert(query).ToUpper();
            //
            tndaEntities db = new tndaEntities();
            List<Person> list = new List<Person>();
            list = db.People.ToList();
            List<object> objects = new List<object>();
            foreach (Person p in list)
            {
                string cname = convert(p.ChristianName).ToUpper();
                string fname = convert(p.FirstName).ToUpper();
                string name = convert(p.Name).ToUpper();
                //
                bool cfn = (cname + " " + fname + " " + name).Trim().Equals(query);
                bool cn = (cname + " " + name).Trim().Equals(query);
                bool fn = (fname + " " + name).Trim().Equals(query);
                bool role = p.ID_role == 3 || p.ID_role == 1 || p.ID_role == 2;
                if (role)
                {
                    if (convert(p.ChristianName.ToUpper()).Equals(query) || convert(p.FirstName.ToUpper()).Equals(query) || convert(p.Name.ToUpper()).Equals(query) || cfn || cn || fn)
                    {
                        var ob = new { id = p.ID, ch_name = p.ChristianName, fname = p.FirstName, name = p.Name, pclass = p.Class.Grade.GradeName + " " + p.Class.ClassName, birth = p.Birth.Value.ToShortDateString(), role = p.Role.RoleName };
                        //
                        objects.Add(ob);
                    }
                }

            }
            return Json(objects, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Query()
        {
            string query = Request.QueryString["query"];
            query = convert(query);
            return Redirect("~/External/Listing?query=" + query);
        }
        private static readonly string[] VietNamChar = new string[]
   {
        "aAeEoOuUiIdDyY",
        "áàạảãâấầậẩẫăắằặẳẵ",
        "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
        "éèẹẻẽêếềệểễ",
        "ÉÈẸẺẼÊẾỀỆỂỄ",
        "óòọỏõôốồộổỗơớờợởỡ",
        "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
        "úùụủũưứừựửữ",
        "ÚÙỤỦŨƯỨỪỰỬỮ",
        "íìịỉĩ",
        "ÍÌỊỈĨ",
        "đ",
        "Đ",
        "ýỳỵỷỹ",
        "ÝỲỴỶỸ"
   };
        public static string convert(string str)
        {
            //Thay thế và lọc dấu từng char      
            for (int i = 1; i < VietNamChar.Length; i++)
            {
                for (int j = 0; j < VietNamChar[i].Length; j++)
                {
                    str = str.Replace(VietNamChar[i][j], VietNamChar[0][i - 1]);
                }
            }
            return str;
        }
    }
}