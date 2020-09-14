

using System;
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
        public void addSomeData()
        {
            tndaEntities db = new tndaEntities();
            Role glv, tk, ph, tn;
            glv = new Role()
            {
                RoleName = "Giáo Lý Viên"
            };
            tk = new Role()
            {
                RoleName = "Giáo Lý Viên (Trưởng Khối)"
            };
            ph = new Role()
            {
                RoleName = "Phụ huynh"
            };
            tn = new Role()
            {
                RoleName = "Thiếu nhi"
            };
            
            db.Roles.Add(glv);
            db.Roles.Add(tk);
            db.Roles.Add(ph);
            db.Roles.Add(tn);
            db.SaveChanges();
        }

       
        public void AddPerson()
        {
            tndaEntities db = new tndaEntities();
            Person p = new Person();
            p.ChristianName = "";
            p.FirstName = "";
            p.Name = "";
            p.Birth = DateTime.Now ; //
            p.Address = "";
            p.ID_Class = 1;
            p.ID_Farmily = 1;
            p.ID_role = 1;
            p.Image = "";
            p.Note = "";
            p.Phone = "";
            p.Status = true;
            p.Gender = true;
            p.CreateDate = DateTime.Now;
            db.People.Add(p);
            db.SaveChanges();
        }

//Edit Person
        public void EditPerson(int id)
        {
            tndaEntities db = new tndaEntities();
            List<Person> list = db.People.ToList();
            foreach(Person p in list)
            {
                if(p.ID == id)
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
            foreach(Person ps in list)
            {
                if(ps.ID == id)
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
            Report rp = new Report();
            rp.ID_Person = idOldPerson;
            rp.ID_NewPerson = ps.ID;
            rp.Date = DateTime.Now;
            rp.Status = 0;
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
        

        
       
    }
}