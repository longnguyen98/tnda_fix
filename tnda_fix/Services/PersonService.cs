using System;
using System.Web.Mvc;
using tnda_fix.Models;

namespace tnda_fix.Services
{
    public class PersonService : IDisposable
    {
        private tndaEntities db = new tndaEntities();

        public void Dispose()
        {
            db.Dispose();
        }

        public Response addPerson(FormCollection form)
        {
            using (System.Data.Entity.DbContextTransaction trans = db.Database.BeginTransaction())
            {
                try
                {
                    int id_family_lead = -1;
                    int id_family = -1;
                    Person dad = null;
                    Person mom = null;
                    dad = new Person
                    {
                        ChristianName = form.Get("fa-ch-name"),
                        FirstName = form.Get("fa-fname"),
                        Name = form.Get("fa-name"),
                        Phone = form.Get("fa-phone"),
                        ID_role = 3
                    };
                    db.People.Add(dad);
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
                    int dad_id = dad.ID;
                    //
                    if (dad.Phone == null || dad.Phone.Length == 0)
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
                        //
                        id_family = fl.ID;
                        dad.ID_Farmily = id_family;
                        mom.ID_Farmily = id_family;
                        db.SaveChanges();
                    }

                    Person p = new Person
                    {
                        ChristianName = form["child-ch-name"],
                        FirstName = form["child-fname"],
                        Name = form["child-name"],
                        Address = form["child-address"],
                        ID_Class = int.Parse(form["child-class"]),
                        ID_role = int.Parse(form["child-role"]),
                        Status = false,
                        Gender = bool.Parse(form["child-gender"].ToUpper()),
                        CreateDate = DateTime.Now
                    };
                    try
                    {
                        p.Birth = Convert.ToDateTime(form["child-birth"]);
                    }
                    catch (Exception)
                    {
                        p.Birth = null;
                    }
                    p.ID_Farmily = id_family;
                    dad.Address = p.Address;
                    mom.Address = p.Address;
                    //
                    Class clazz = db.Classes.Find(p.ID_Class);
                    clazz.students_count++;
                    //
                    if (form["check-box"] != null && bool.Parse(form["check-box"]))
                    {
                        p.Note = form["child-gp"] + " " + form["child-gx"];
                    }

                    p.for_search = Tools.convert(p.ChristianName.Trim() + p.FirstName.Trim() + p.Name.Trim()).ToUpper();
                    db.People.Add(p);
                    db.SaveChanges();
                    trans.Commit();
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    return new Response()
                    {
                        success = false,
                        message = e.Message
                    };
                }
            }
            return new Response()
            {
                success = true,
            };
        }
    }
}