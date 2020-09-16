﻿using System.Collections.Generic;
using System.Linq;
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
            foreach (Class c in db.Classes.Where(c => c.ID_Grade == grade_id).ToList())
            {
                var ob = new { ID = c.ID, className = c.ClassName };
                classes.Add(ob);
            }
            return Json(classes, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getClassById()
        {
            int id = int.Parse(Request.QueryString["id_class"]);
            tndaEntities db = new tndaEntities();
            Class c = db.Classes.Find(id);
            Grade g = db.Grades.Find(c.ID_Grade);
            Person glv = db.People.Where(p => p.ID_role == 1 && p.ID_Class == c.ID).FirstOrDefault();
            string glv_name = glv.ChristianName + " " + glv.FirstName + " " + glv.Name;
            //
            var json = new { className = g.GradeName + " " + c.ClassName, glv_name = glv_name };
            return Json(json, JsonRequestBehavior.AllowGet);
        }

    }
}