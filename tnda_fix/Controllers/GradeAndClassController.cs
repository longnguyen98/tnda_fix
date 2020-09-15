using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tnda_fix.Models;

namespace tnda_fix.Controllers
{
    public class GradeAndClassController : Controller
    {
       public JsonResult getAllGrades()
        {
            tndaEntities db = new tndaEntities();
            List<object> grades = new List<object>();
            foreach (Grade g in db.Grades.ToList())
            {
                var ob = new { ID = g.ID, gradeName = g.GradeName };
                grades.Add(ob);
            }
            return Json(grades, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getClassByGrade()
        {
            int grade_id = int.Parse(Request.QueryString["id"]);
            tndaEntities db = new tndaEntities();
            List<object> classes = new List<object>();
            //
            foreach(Class c in db.Classes.Where(c=>c.ID_Grade == grade_id).ToList())
            {
                var ob = new { ID = c.ID, className = c.ClassName };
                classes.Add(ob);
            }
            return Json(classes, JsonRequestBehavior.AllowGet);
        }
       
    }
}