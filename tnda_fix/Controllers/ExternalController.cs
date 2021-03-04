using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using IronXL;
using tnda_fix.Models;

namespace tnda_fix.Controllers
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
        [HttpPost]
        
        public string upClass()
        {
            string result = "";
            int current_class_id = int.Parse(Request.QueryString["id"]);
            string current_class_name, current_grade_name, new_class_name, new_grade_name;
            int current_grade_id, new_class_id, new_grade_id;
            //
            //entity
            tndaEntities db = new tndaEntities();
            //
            Class cur_class = db.Classes.Find(current_class_id);
            Grade cur_grade = db.Grades.Find(cur_class.ID_Grade);
            //
            current_grade_id = cur_grade.ID;
            //

            current_class_name = cur_class.ClassName;
            current_grade_name = cur_grade.GradeName;
            try
            {
                char[] charArray = current_class_name.ToCharArray();
                int temp = int.Parse(charArray[0].ToString());
                if (temp != 3)
                {
                    new_class_name = temp + 1 + charArray[1].ToString();
                    new_grade_name = current_grade_name;
                    //
                    new_grade_id = current_grade_id;
                    new_class_id = db.Classes.Where(c => c.ID_Grade == new_grade_id && c.ClassName == new_class_name).FirstOrDefault().ID;
                }
                else
                {
                    if (current_grade_id == 4)
                    {
                        return "Không thể lên lớp, đã kết thúc chương trình học";
                    }
                    //
                    new_grade_id = current_grade_id + 1;
                    new_grade_name = db.Grades.Find(new_grade_id).GradeName;
                    //
                    new_class_name = 1 + charArray[1].ToString();
                    new_class_id = db.Classes.Where(c => c.ID_Grade == new_grade_id && c.ClassName == new_class_name).FirstOrDefault().ID;
                }

            }
            catch (Exception e)
            {
                return e.Message;
            }
            result += "Khối hiện tại: " + current_grade_name;
            result += "| ";
            result += "ID Khối hiện tại: " + current_grade_id;
            result += "| ";
            result += "Lớp hiện tại: " + current_class_name;
            result += "| ";
            result += "ID lớp hiện tại: " + current_class_id;
            result += "| ";
            result += "| ";
            result += "Khối mới: " + new_grade_name;
            result += "| ";
            result += "ID khối mới: " + new_grade_id;
            result += "| ";
            result += "Lớp mới " + new_class_name;
            result += "| ";
            result += "ID lớp mới " + new_class_id;
            return result;
        }
       
        
    }
}
