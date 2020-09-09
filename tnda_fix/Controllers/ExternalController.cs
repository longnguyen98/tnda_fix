

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

                if (convert(p.ChristianName.ToUpper()).Equals(query) || convert(p.FirstName.ToUpper()).Equals(query) || convert(p.Name.ToUpper()).Equals(query) || cfn || cn || fn)
                {
                    var ob = new { id = p.ID, ch_name = p.ChristianName, fname = p.FirstName, name = p.Name };
                    objects.Add(ob);
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